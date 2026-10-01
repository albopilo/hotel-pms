-- Updated safe guest deletion: allows deletion when all reservations are voided
-- Nulls out guest_id on all related tables so voided records remain intact

CREATE OR REPLACE FUNCTION safe_delete_guest(p_guest_id uuid)
RETURNS void
LANGUAGE plpgsql
SECURITY DEFINER
SET search_path = public
AS $$
DECLARE
  v_count integer;
BEGIN
  -- Guest must exist
  PERFORM 1 FROM guests WHERE id = p_guest_id;
  IF NOT FOUND THEN
    RAISE EXCEPTION 'Guest not found';
  END IF;

  -- Block deletion if any NON-VOIDED reservations exist
  SELECT count(*) INTO v_count
  FROM reservations
  WHERE primary_guest_id = p_guest_id AND status <> 'void';
  IF v_count > 0 THEN
    RAISE EXCEPTION 'Cannot delete guest: % reservation(s) are not voided. Void all reservations first.', v_count;
  END IF;

  -- Null out guest_id on all related tables so voided records stay intact
  UPDATE reservations SET primary_guest_id = NULL WHERE primary_guest_id = p_guest_id;
  UPDATE folios SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE folio_items SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE invoices SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE payments SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE deposits SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE additional_charges SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE card_issuances SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE transactions SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE refunds SET guest_id = NULL WHERE guest_id = p_guest_id;
  UPDATE reservation_guests SET guest_id = NULL WHERE guest_id = p_guest_id;

  -- guest_documents and m13_members have ON DELETE CASCADE, they will be removed
  DELETE FROM guests WHERE id = p_guest_id;
END;
$$;

REVOKE EXECUTE ON FUNCTION safe_delete_guest(uuid) FROM anon, authenticated;
GRANT EXECUTE ON FUNCTION safe_delete_guest(uuid) TO authenticated;