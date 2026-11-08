/*
# Club member email notifications (points earned + monthly reminders)

1. Plain-English summary
- Every time a club member receives loyalty points, a queued email is created for them.
- Every month, each active club member receives two reminder emails on two different random days (days 1 to 28). The reminders show their current points and the rewards they can claim.
- A background job sends the queued emails through the email provider and retries failed ones (up to 5 attempts).

2. New tables
- `m13_member_emails`: the email queue. One row per email, with the member, the kind ('points_earned' or 'monthly_reminder'), a send key that prevents duplicates, the status, the attempt count and the last error.
- `m13_monthly_reminder_schedule`: the two random reminder days chosen for each month (day one and day two are always different).

3. New functions
- `m13_queue_points_email()`: trigger function. Queues a 'points_earned' email when a positive EARN or ADJUSTMENT_CREDIT entry is added to the points ledger.
- `m13_plan_monthly_reminders()`: picks the two random days for the current month (once per month) and, on those days, queues a 'monthly_reminder' email for every active member with a linked login.

4. Schedules (pg_cron)
- `m13-plan-monthly-reminders`: every day at 03:00 Jakarta time.
- `m13-send-member-emails`: every 5 minutes. Calls the `m13-send-member-emails` edge function only when there are pending emails. The function URL points to this project; update it if the project is duplicated.

5. Security
- New tables have row level security enabled with no policies, so app users cannot read or change the queue or schedule.
- The queue is readable and updatable only by the server role (used by the sending function). Member, reward and guest data are readable by the server role only for building the email.
- Trigger and planner functions are not callable by app users (EXECUTE revoked from PUBLIC, anon and authenticated).

6. Important notes
- Re-running this migration is safe: tables and functions use IF NOT EXISTS or CREATE OR REPLACE, the trigger and jobs are recreated, and queued emails are de-duplicated by a unique key.
- Points are queued at the moment they are credited; the email is sent by the next background run and shows the balance at send time.
*/

CREATE EXTENSION IF NOT EXISTS pg_net WITH SCHEMA extensions;

CREATE TABLE IF NOT EXISTS m13_member_emails (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  member_id uuid NOT NULL REFERENCES m13_members(id) ON DELETE CASCADE,
  kind text NOT NULL CHECK (kind IN ('points_earned', 'monthly_reminder')),
  send_key text NOT NULL,
  points_added integer,
  status text NOT NULL DEFAULT 'pending' CHECK (status IN ('pending', 'sent', 'failed', 'skipped')),
  attempts integer NOT NULL DEFAULT 0,
  last_error text,
  created_at timestamptz NOT NULL DEFAULT now(),
  sent_at timestamptz,
  UNIQUE (member_id, kind, send_key)
);

CREATE INDEX IF NOT EXISTS m13_member_emails_pending_idx ON m13_member_emails (created_at) WHERE status = 'pending';

ALTER TABLE m13_member_emails ENABLE ROW LEVEL SECURITY;
GRANT SELECT, UPDATE ON m13_member_emails TO service_role;

CREATE TABLE IF NOT EXISTS m13_monthly_reminder_schedule (
  month_start date PRIMARY KEY,
  reminder_day_one smallint NOT NULL CHECK (reminder_day_one BETWEEN 1 AND 28),
  reminder_day_two smallint NOT NULL CHECK (reminder_day_two BETWEEN 1 AND 28),
  CHECK (reminder_day_one <> reminder_day_two)
);

ALTER TABLE m13_monthly_reminder_schedule ENABLE ROW LEVEL SECURITY;

GRANT SELECT ON m13_members, m13_rewards, guests TO service_role;

CREATE OR REPLACE FUNCTION m13_queue_points_email()
RETURNS trigger
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
BEGIN
  IF NEW.type IN ('EARN', 'ADJUSTMENT_CREDIT') AND NEW.points > 0 THEN
    INSERT INTO m13_member_emails (member_id, kind, send_key, points_added)
    VALUES (NEW.member_id, 'points_earned', NEW.id::text, NEW.points)
    ON CONFLICT (member_id, kind, send_key) DO NOTHING;
  END IF;
  RETURN NEW;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_queue_points_email() FROM PUBLIC, anon, authenticated;

DROP TRIGGER IF EXISTS m13_point_ledger_queue_email ON m13_point_ledger;
CREATE TRIGGER m13_point_ledger_queue_email
AFTER INSERT ON m13_point_ledger
FOR EACH ROW EXECUTE FUNCTION m13_queue_points_email();

CREATE OR REPLACE FUNCTION m13_plan_monthly_reminders()
RETURNS integer
LANGUAGE plpgsql
SET search_path = public
AS $$
DECLARE
  v_today date := (now() AT TIME ZONE 'Asia/Jakarta')::date;
  v_month date := date_trunc('month', (now() AT TIME ZONE 'Asia/Jakarta'))::date;
  v_d1 smallint;
  v_d2 smallint;
  v_queued integer := 0;
BEGIN
  INSERT INTO m13_monthly_reminder_schedule (month_start, reminder_day_one, reminder_day_two)
  SELECT v_month, s.d1, CASE WHEN s.d2 >= s.d1 THEN s.d2 + 1 ELSE s.d2 END
  FROM (
    SELECT (1 + floor(random() * 28))::smallint AS d1,
           (1 + floor(random() * 27))::smallint AS d2
  ) s
  ON CONFLICT (month_start) DO NOTHING;

  SELECT reminder_day_one, reminder_day_two INTO v_d1, v_d2
  FROM m13_monthly_reminder_schedule
  WHERE month_start = v_month;

  IF extract(day FROM v_today)::int IN (v_d1, v_d2) THEN
    INSERT INTO m13_member_emails (member_id, kind, send_key)
    SELECT m.id, 'monthly_reminder', v_today::text
    FROM m13_members m
    WHERE m.status = 'active' AND m.auth_user_id IS NOT NULL
    ON CONFLICT (member_id, kind, send_key) DO NOTHING;
    GET DIAGNOSTICS v_queued = ROW_COUNT;
  END IF;

  RETURN v_queued;
END;
$$;

REVOKE EXECUTE ON FUNCTION m13_plan_monthly_reminders() FROM PUBLIC, anon, authenticated;

DO $$
BEGIN
  IF EXISTS (SELECT 1 FROM cron.job WHERE jobname = 'm13-plan-monthly-reminders') THEN
    PERFORM cron.unschedule('m13-plan-monthly-reminders');
  END IF;
  IF EXISTS (SELECT 1 FROM cron.job WHERE jobname = 'm13-send-member-emails') THEN
    PERFORM cron.unschedule('m13-send-member-emails');
  END IF;
  PERFORM cron.schedule('m13-plan-monthly-reminders', '0 20 * * *', 'SELECT public.m13_plan_monthly_reminders()');
  PERFORM cron.schedule(
    'm13-send-member-emails',
    '*/5 * * * *',
    $cmd$
    SELECT net.http_post(
      url := 'https://iiyuccqiqpoxdgjdgnmj.supabase.co/functions/v1/m13-send-member-emails',
      headers := '{"Content-Type": "application/json"}'::jsonb,
      body := '{}'::jsonb
    )
    WHERE EXISTS (SELECT 1 FROM public.m13_member_emails WHERE status = 'pending' AND attempts < 5)
    $cmd$
  );
END $$;
