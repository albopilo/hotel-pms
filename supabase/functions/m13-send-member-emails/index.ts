import { createClient } from "npm:@supabase/supabase-js@2";

const corsHeaders = {
  "Access-Control-Allow-Origin": "*",
  "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS",
  "Access-Control-Allow-Headers": "Content-Type, Authorization, X-Client-Info, Apikey",
};

const MEMBER_URL = "https://millennium-pms.netlify.app/#/m13";
const SENDER = { email: "millenniumgarden17@gmail.com", name: "Millennium Garden M13 Club" };
const REPLY_TO = { email: "millenniumgarden17@gmail.com", name: "Millennium Garden M13 Club" };
const BATCH_SIZE = 50;
const MAX_ATTEMPTS = 5;

type Lang = "en" | "id";

interface RewardRow {
  name: string;
  points_required: number;
  redemption_deadline: string | null;
  total_redeemed: number;
  total_redemption_limit: number;
}

interface QueuedEmail {
  id: string;
  kind: "points_earned" | "monthly_reminder";
  points_added: number | null;
  attempts: number;
  member: {
    id: string;
    organization_id: string;
    points_balance: number;
    auth_user_id: string | null;
    pms_guest: { full_name: string; email: string | null } | null;
  } | null;
}

const escapeHtml = (value: string) =>
  value
    .replace(/&/g, "&amp;")
    .replace(/</g, "&lt;")
    .replace(/>/g, "&gt;")
    .replace(/"/g, "&quot;")
    .replace(/'/g, "&#39;");

const TEXT: Record<string, Record<Lang, string>> = {
  hotel: { en: "Millennium Garden", id: "Millennium Garden" },
  club: { en: "M13 Club Member", id: "Anggota M13 Club" },
  subjectEarned: { en: "You earned {n} new points!", id: "Anda mendapatkan {n} poin baru!" },
  subjectMonthly: { en: "Your M13 Club points reminder", id: "Pengingat poin M13 Club Anda" },
  greeting: { en: "Dear {name},", id: "Yth. {name}," },
  earnedBody: {
    en: "{n} points have been added to your account.",
    id: "{n} poin telah ditambahkan ke akun Anda.",
  },
  monthlyBody: {
    en: "Here is a quick reminder of your club points and the rewards you can claim.",
    id: "Berikut pengingat singkat mengenai poin club Anda dan hadiah yang dapat Anda klaim.",
  },
  balance: { en: "Your current balance", id: "Saldo poin Anda saat ini" },
  points: { en: "points", id: "poin" },
  rewardsTitle: { en: "Rewards", id: "Hadiah" },
  claimable: { en: "Ready to claim", id: "Siap diklaim" },
  needMore: { en: "{n} more points needed", id: "masih butuh {n} poin lagi" },
  validUntil: { en: "Valid until", id: "Berlaku hingga" },
  noRewards: {
    en: "No rewards are available right now. Keep collecting points!",
    id: "Belum ada hadiah yang tersedia saat ini. Terus kumpulkan poin!",
  },
  cta: { en: "Open my member page", id: "Buka halaman member saya" },
  loginTitle: { en: "How to log in", id: "Cara masuk" },
  loginEmail: {
    en: "Username: the email address registered with us.",
    id: "Nama pengguna: alamat email yang terdaftar pada kami.",
  },
  loginPassword: {
    en: "Default password: the phone number you registered with at the hotel.",
    id: "Kata sandi awal: nomor telepon yang Anda daftarkan di hotel.",
  },
  loginChange: {
    en: "We recommend changing your password after your first login.",
    id: "Kami menyarankan untuk mengubah kata sandi setelah masuk pertama kali.",
  },
  footer: {
    en: "This is an automated message. To ask a question, reply to this email.",
    id: "Ini adalah pesan otomatis. Untuk bertanya, silakan balas email ini.",
  },
};

const t = (key: keyof typeof TEXT, lang: Lang, vars: Record<string, string | number> = {}) =>
  Object.entries(vars).reduce(
    (text, [name, value]) => text.split(`{${name}}`).join(String(value)),
    TEXT[key][lang],
  );

const formatNumber = (value: number, lang: Lang) =>
  value.toLocaleString(lang === "en" ? "en-US" : "id-ID");

const formatDate = (iso: string, lang: Lang) =>
  new Date(`${iso}T00:00:00Z`).toLocaleDateString(lang === "en" ? "en-GB" : "id-ID", {
    day: "numeric",
    month: "long",
    year: "numeric",
    timeZone: "UTC",
  });

function renderRewards(rewards: RewardRow[], balance: number, lang: Lang): string {
  if (rewards.length === 0) {
    return `<p style="margin:0;color:#475569;font-size:14px;line-height:1.5;">${t("noRewards", lang)}</p>`;
  }
  return rewards
    .map((reward) => {
      const claimable = balance >= reward.points_required;
      const status = claimable
        ? `<span style="display:inline-block;padding:4px 10px;border-radius:999px;background:#dcfce7;color:#166534;font-size:12px;font-weight:600;">${t("claimable", lang)}</span>`
        : `<span style="font-size:12px;color:#92400e;">${t("needMore", lang, { n: formatNumber(reward.points_required - balance, lang) })}</span>`;
      const deadline = reward.redemption_deadline
        ? `<div style="margin-top:4px;font-size:12px;color:#64748b;">${t("validUntil", lang)}: ${formatDate(reward.redemption_deadline, lang)}</div>`
        : "";
      return `
        <tr>
          <td style="padding:14px 0;border-bottom:1px solid #e2e8f0;">
            <table role="presentation" width="100%" cellspacing="0" cellpadding="0">
              <tr>
                <td style="font-size:15px;font-weight:600;color:#0f172a;">${escapeHtml(reward.name)}</td>
                <td align="right" style="font-size:14px;font-weight:600;color:#0f172a;white-space:nowrap;">${formatNumber(reward.points_required, lang)} ${t("points", lang)}</td>
              </tr>
              <tr>
                <td colspan="2" style="padding-top:6px;">${status}${deadline}</td>
              </tr>
            </table>
          </td>
        </tr>`;
    })
    .join("");
}

function renderSection(kind: QueuedEmail["kind"], ctx: EmailContext, lang: Lang): string {
  const intro =
    kind === "points_earned"
      ? t("earnedBody", lang, { n: formatNumber(ctx.pointsAdded ?? 0, lang) })
      : t("monthlyBody", lang);
  const langLabel = lang === "en" ? "English" : "Bahasa Indonesia";
  return `
    <tr>
      <td style="padding:32px 32px 8px 32px;">
        <p style="margin:0 0 4px 0;font-size:11px;letter-spacing:1px;text-transform:uppercase;color:#b45309;font-weight:600;">${langLabel}</p>
        <p style="margin:0 0 16px 0;font-size:16px;color:#0f172a;">${t("greeting", lang, { name: escapeHtml(ctx.name) })}</p>
        <p style="margin:0 0 20px 0;font-size:15px;line-height:1.5;color:#334155;">${intro}</p>
        <table role="presentation" width="100%" cellspacing="0" cellpadding="0" style="background:#f8fafc;border-radius:12px;">
          <tr>
            <td style="padding:20px 24px;">
              <p style="margin:0;font-size:13px;color:#64748b;">${t("balance", lang)}</p>
              <p style="margin:4px 0 0 0;font-size:32px;font-weight:700;line-height:1.2;color:#0f172a;">${formatNumber(ctx.balance, lang)} <span style="font-size:14px;font-weight:500;color:#64748b;">${t("points", lang)}</span></p>
            </td>
          </tr>
        </table>
        <p style="margin:28px 0 4px 0;font-size:16px;font-weight:700;color:#0f172a;">${t("rewardsTitle", lang)}</p>
        <table role="presentation" width="100%" cellspacing="0" cellpadding="0">
          ${renderRewards(ctx.rewards, ctx.balance, lang)}
        </table>
        <table role="presentation" cellspacing="0" cellpadding="0" style="margin:24px 0 8px 0;">
          <tr>
            <td style="border-radius:8px;background:#0f172a;">
              <a href="${MEMBER_URL}" style="display:inline-block;padding:12px 24px;font-size:14px;font-weight:600;color:#ffffff;text-decoration:none;">${t("cta", lang)}</a>
            </td>
          </tr>
        </table>
        <table role="presentation" width="100%" cellspacing="0" cellpadding="0" style="margin-top:20px;border:1px solid #e2e8f0;border-radius:12px;">
          <tr>
            <td style="padding:18px 20px;">
              <p style="margin:0 0 10px 0;font-size:14px;font-weight:700;color:#0f172a;">${t("loginTitle", lang)}</p>
              <p style="margin:0 0 6px 0;font-size:14px;line-height:1.5;color:#334155;">&bull; ${t("loginEmail", lang)}</p>
              <p style="margin:0 0 6px 0;font-size:14px;line-height:1.5;color:#334155;">&bull; ${t("loginPassword", lang)}</p>
              <p style="margin:0;font-size:13px;line-height:1.5;color:#64748b;">${t("loginChange", lang)}</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>`;
}

interface EmailContext {
  name: string;
  balance: number;
  pointsAdded: number | null;
  rewards: RewardRow[];
}

function buildEmail(kind: QueuedEmail["kind"], ctx: EmailContext): { subject: string; html: string } {
  const subject =
    kind === "points_earned"
      ? `${t("subjectEarned", "en", { n: formatNumber(ctx.pointsAdded ?? 0, "en") })} / ${t("subjectEarned", "id", { n: formatNumber(ctx.pointsAdded ?? 0, "id") })}`
      : `${t("subjectMonthly", "en")} / ${t("subjectMonthly", "id")}`;

  const html = `<!DOCTYPE html>
<html lang="en">
<head>
  <meta charset="UTF-8">
  <meta name="viewport" content="width=device-width, initial-scale=1.0">
  <title>${escapeHtml(subject)}</title>
</head>
<body style="margin:0;padding:0;background:#f1f5f9;font-family:Helvetica,Arial,sans-serif;">
  <table role="presentation" width="100%" cellspacing="0" cellpadding="0" style="background:#f1f5f9;padding:24px 12px;">
    <tr>
      <td align="center">
        <table role="presentation" width="100%" cellspacing="0" cellpadding="0" style="max-width:600px;background:#ffffff;border-radius:16px;overflow:hidden;">
          <tr>
            <td style="background:#0f172a;padding:28px 32px;">
              <p style="margin:0;font-size:13px;letter-spacing:1px;text-transform:uppercase;color:#fbbf24;font-weight:600;">${t("hotel", "en")}</p>
              <p style="margin:6px 0 0 0;font-size:22px;font-weight:700;color:#ffffff;">M13 Club</p>
              <p style="margin:4px 0 0 0;font-size:13px;color:#cbd5e1;">${t("club", "en")} / ${t("club", "id")}</p>
            </td>
          </tr>
          ${renderSection(kind, ctx, "en")}
          <tr>
            <td style="padding:8px 32px;">
              <table role="presentation" width="100%" cellspacing="0" cellpadding="0">
                <tr><td style="border-top:2px dashed #e2e8f0;font-size:0;line-height:0;">&nbsp;</td></tr>
              </table>
            </td>
          </tr>
          ${renderSection(kind, ctx, "id")}
          <tr>
            <td style="padding:24px 32px 32px 32px;">
              <p style="margin:0;font-size:12px;line-height:1.5;color:#94a3b8;text-align:center;">${t("footer", "en")}<br>${t("footer", "id")}</p>
            </td>
          </tr>
        </table>
      </td>
    </tr>
  </table>
</body>
</html>`;

  return { subject, html };
}

async function sendViaBrevo(apiKey: string, to: { email: string; name: string }, subject: string, html: string) {
  const res = await fetch("https://api.brevo.com/v3/smtp/email", {
    method: "POST",
    headers: {
      "api-key": apiKey,
      "Content-Type": "application/json",
      Accept: "application/json",
    },
    body: JSON.stringify({ sender: SENDER, to: [to], replyTo: REPLY_TO, subject, htmlContent: html }),
  });
  if (!res.ok) {
    const detail = await res.text();
    throw new Error(`Brevo ${res.status}: ${detail.slice(0, 300)}`);
  }
}

Deno.serve(async (req: Request) => {
  if (req.method === "OPTIONS") {
    return new Response(null, { status: 200, headers: corsHeaders });
  }

  try {
    const apiKey = Deno.env.get("BREVO_API_KEY");
    if (!apiKey) {
      throw new Error("BREVO_API_KEY is not configured");
    }

    const admin = createClient(
      Deno.env.get("SUPABASE_URL")!,
      Deno.env.get("SUPABASE_SERVICE_ROLE_KEY")!,
    );

    const { data, error } = await admin
      .from("m13_member_emails")
      .select(
        "id, kind, points_added, attempts, member:m13_members(id, organization_id, points_balance, auth_user_id, pms_guest:guests(full_name, email))",
      )
      .eq("status", "pending")
      .lt("attempts", MAX_ATTEMPTS)
      .order("created_at", { ascending: true })
      .limit(BATCH_SIZE);
    if (error) throw error;

    const queue = (data ?? []) as unknown as QueuedEmail[];
    const today = new Date().toISOString().slice(0, 10);
    const rewardCache = new Map<string, RewardRow[]>();
    const counts = { processed: 0, sent: 0, failed: 0, skipped: 0 };

    for (const item of queue) {
      counts.processed += 1;
      const member = item.member;
      const recipient = member?.pms_guest?.email?.trim();

      if (!member || !member.auth_user_id || !recipient) {
        await admin
          .from("m13_member_emails")
          .update({ status: "skipped", last_error: "Member has no login or email address" })
          .eq("id", item.id);
        counts.skipped += 1;
        continue;
      }

      try {
        if (!rewardCache.has(member.organization_id)) {
          const { data: rewardData, error: rewardError } = await admin
            .from("m13_rewards")
            .select("name, points_required, redemption_deadline, total_redeemed, total_redemption_limit")
            .eq("organization_id", member.organization_id)
            .eq("is_active", true)
            .order("points_required", { ascending: true });
          if (rewardError) throw rewardError;
          const available = ((rewardData ?? []) as RewardRow[]).filter(
            (r) => (!r.redemption_deadline || r.redemption_deadline >= today) && r.total_redeemed < r.total_redemption_limit,
          );
          rewardCache.set(member.organization_id, available);
        }

        const ctx: EmailContext = {
          name: member.pms_guest?.full_name ?? "",
          balance: member.points_balance,
          pointsAdded: item.points_added,
          rewards: rewardCache.get(member.organization_id) ?? [],
        };
        const { subject, html } = buildEmail(item.kind, ctx);
        await sendViaBrevo(apiKey, { email: recipient, name: ctx.name }, subject, html);

        await admin
          .from("m13_member_emails")
          .update({ status: "sent", sent_at: new Date().toISOString(), attempts: item.attempts + 1, last_error: null })
          .eq("id", item.id);
        counts.sent += 1;
      } catch (sendErr) {
        const message = sendErr instanceof Error ? sendErr.message : String(sendErr);
        const attempts = item.attempts + 1;
        await admin
          .from("m13_member_emails")
          .update({
            status: attempts >= MAX_ATTEMPTS ? "failed" : "pending",
            attempts,
            last_error: message.slice(0, 500),
          })
          .eq("id", item.id);
        counts.failed += 1;
      }
    }

    return new Response(JSON.stringify(counts), {
      headers: { ...corsHeaders, "Content-Type": "application/json" },
    });
  } catch (err) {
    const message = err instanceof Error ? err.message : "Unknown error";
    return new Response(JSON.stringify({ error: message }), {
      status: 500,
      headers: { ...corsHeaders, "Content-Type": "application/json" },
    });
  }
});
