import { createClient } from "https://esm.sh/@supabase/supabase-js@2";

const corsHeaders = {
  "Access-Control-Allow-Origin": "*",
  "Access-Control-Allow-Methods": "GET, POST, PUT, DELETE, OPTIONS",
  "Access-Control-Allow-Headers": "Content-Type, Authorization, X-Client-Info, Apikey",
};

Deno.serve(async (req: Request) => {
  if (req.method === "OPTIONS") {
    return new Response(null, { status: 200, headers: corsHeaders });
  }

  try {
    const authHeader = req.headers.get("Authorization");
    if (!authHeader) throw new Error("Missing authorization");

    const supabaseAdmin = createClient(
      Deno.env.get("SUPABASE_URL")!,
      Deno.env.get("SUPABASE_SERVICE_ROLE_KEY")!,
      { auth: { autoRefreshToken: false, persistSession: false } }
    );

    const supabaseUser = createClient(
      Deno.env.get("SUPABASE_URL")!,
      Deno.env.get("SUPABASE_ANON_KEY")!,
      { global: { headers: { Authorization: authHeader } } }
    );

    const token = authHeader.replace("Bearer ", "");
    const payload = JSON.parse(atob(token.split(".")[1]));
    const callerId = payload.sub;

    const { data: caller, error: callerError } = await supabaseUser
      .from("profiles")
      .select("role,organization_id")
      .eq("id", callerId)
      .maybeSingle();

    if (callerError || !caller) {
      throw new Error("Unauthorized");
    }

    if (!["super_admin", "manager", "receptionist"].includes(caller.role)) {
      throw new Error("Unauthorized");
    }

    const body = await req.json();
    const { guest_id, email, staff_user_id } = body;

    if (!guest_id || !email || !staff_user_id) {
      throw new Error("Missing required fields: guest_id, email, staff_user_id");
    }

    // Get guest record
    const { data: guest, error: guestError } = await supabaseAdmin
      .from("guests")
      .select("*")
      .eq("id", guest_id)
      .maybeSingle();

    if (guestError || !guest) {
      throw new Error("Guest not found");
    }

    // Create the member record via RPC
    const { data: member, error: memberError } = await supabaseUser.rpc(
      "m13_create_member",
      { p_guest_id: guest_id, p_email: email, p_staff_user_id: staff_user_id }
    );

    if (memberError) throw memberError;
    if (!member) throw new Error("Failed to create member");

    // If auth_user_id is already set, member already existed
    if (member.auth_user_id) {
      return new Response(
        JSON.stringify({ success: true, member, already_existed: true }),
        { headers: { ...corsHeaders, "Content-Type": "application/json" } }
      );
    }

    const phone = guest.phone || "";
    const password = phone || member.member_number;

    // Create Supabase Auth user
    const { data: created, error: createError } = await supabaseAdmin.auth.admin.createUser({
      email,
      password,
      email_confirm: true,
      user_metadata: {
        m13_member_id: member.id,
        m13_member_number: member.member_number,
        full_name: guest.full_name,
      },
    });

    if (createError) {
      // If user already exists, try to find them
      if (createError.message.includes("already") || createError.message.includes("registered")) {
        const { data: existingUsers } = await supabaseAdmin.auth.admin.listUsers();
        const existing = existingUsers?.users?.find((u: { email: string }) => u.email === email);
        if (existing) {
          await supabaseAdmin
            .from("m13_members")
            .update({ auth_user_id: existing.id, updated_at: new Date().toISOString() })
            .eq("id", member.id);
          return new Response(
            JSON.stringify({ success: true, member: { ...member, auth_user_id: existing.id }, already_existed: true }),
            { headers: { ...corsHeaders, "Content-Type": "application/json" } }
          );
        }
      }
      throw createError;
    }

    const authUserId = created.user.id;

    // Link auth_user_id to member record
    const { error: linkError } = await supabaseAdmin
      .from("m13_members")
      .update({ auth_user_id: authUserId, updated_at: new Date().toISOString() })
      .eq("id", member.id);

    if (linkError) throw linkError;

    // Send welcome email via Supabase Auth invite
    try {
      await supabaseAdmin.auth.admin.inviteUserByEmail(email);
    } catch {
      // Non-blocking — member is created, email can be resent later
    }

    return new Response(
      JSON.stringify({
        success: true,
        member: { ...member, auth_user_id: authUserId },
        already_existed: false,
      }),
      { headers: { ...corsHeaders, "Content-Type": "application/json" } }
    );
  } catch (err) {
    return new Response(
      JSON.stringify({ error: err.message }),
      {
        status: 400,
        headers: { ...corsHeaders, "Content-Type": "application/json" },
      }
    );
  }
});
// M13 create member edge function
// M13 create member edge function
