-- Safe guest deletion: only allows deletion when no financial/reservation history exists
-- SECURITY DEFINER so it can bypass RLS for the pre-check, but runs explicit checks

CREATE OR REPLACE FUNCTION safe_delete_guest(p_guest_id uuid)
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_guest guests%ROWTYPE;
  v_count integer;
BEGIN
  SELECT * INTO v_guest FROM guests WHERE id = p_guest_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Guest not found';
  END IF;

  -- Block deletion if any reservations exist (active or past)
  SELECT count(*) INTO v_count FROM reservations WHERE primary_guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with % reservation(s). Use merge instead.', v_count;
  END IF;

  -- Block if any folios exist
  SELECT count(*) INTO v_count FROM folios WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with % folio(s).', v_count;
  END IF;

  -- Block if any folio_items exist
  SELECT count(*) INTO v_count FROM folio_items WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with folio item history.';
  END IF;

  -- Block if any invoices exist
  SELECT count(*) INTO v_count FROM invoices WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with % invoice(s).', v_count;
  END IF;

  -- Block if any payments exist
  SELECT count(*) INTO v_count FROM payments WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with payment history.';
  END IF;

  -- Block if any deposits exist
  SELECT count(*) INTO v_count FROM deposits WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with deposit history.';
  END IF;

  -- Block if any additional charges exist
  SELECT count(*) INTO v_count FROM additional_charges WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with additional charge history.';
  END IF;

  -- Block if any card issuances exist
  SELECT count(*) INTO v_count FROM card_issuances WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with % card issuance record(s).', v_count;
  END IF;

  -- Block if any transactions exist
  SELECT count(*) INTO v_count FROM transactions WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with transaction history.';
  END IF;

  -- Block if any refunds exist
  SELECT count(*) INTO v_count FROM refunds WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest with refund history.';
  END IF;

  -- Block if any reservation_guests (additional guests on group bookings) exist
  SELECT count(*) INTO v_count FROM reservation_guests WHERE guest_id = p_guest_id;
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest who appears on % group reservation(s).', v_count;
  END IF;

  -- Safe to delete: cascade will handle guest_documents and m13_members
  DELETE FROM guests WHERE id = p_guest_id;
END;
$$;

REVOKE EXECUTE ON FUNCTION safe_delete_guest(uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION safe_delete_guest(uuid) TO authenticated;