import { serve } from "https://deno.land/std@0.168.0/http/server.ts";
import { createClient } from "https://esm.sh/@supabase/supabase-js@2";

const cors = {
  "Access-Control-Allow-Origin": "*",
  "Access-Control-Allow-Headers": "authorization, x-client-info, apikey, content-type",
  "Access-Control-Allow-Methods": "POST, OPTIONS",
};

serve(async (req) => {
  if (req.method === "OPTIONS") return new Response("ok", { headers: cors });

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

    const { data: caller, error: callerError } = await supabaseUser
      .from("profiles")
      .select("role,organization_id")
      .single();

    if (callerError || caller?.role !== "super_admin") {
      throw new Error("Unauthorized");
    }

    const body = await req.json();

    const {
      email,
      password,
      full_name,
      role,
      branchIds = [],
      is_active = true,
    } = body;

    if (!email || !password || !full_name) {
      throw new Error("Missing required fields");
    }

    const { data: created, error:createError } =
      await supabaseAdmin.auth.admin.createUser({
        email,
        password,
        email_confirm: true,
      });

    if (createError) throw createError;

    const userId = created.user.id;

    const { error:profileError } =
      await supabaseAdmin.from("profiles").insert({
        id:userId,
        organization_id:caller.organization_id,
        full_name,
        email,
        role,
        is_active,
      });

    if (profileError) throw profileError;


    if (role !== "super_admin" && branchIds.length) {
      const rows = branchIds.map((branch_id:string)=>({
        user_id:userId,
        branch_id,
      }));

      const { error:accessError } =
        await supabaseAdmin
          .from("user_branch_access")
          .insert(rows);

      if(accessError) throw accessError;
    }


    return new Response(
      JSON.stringify({
        success:true,
        user_id:userId,
      }),
      {
        headers:{
          ...cors,
          "Content-Type":"application/json",
        },
      }
    );

  } catch(err) {

    return new Response(
      JSON.stringify({
        error:err.message,
      }),
      {
        status:400,
        headers:{
          ...cors,
          "Content-Type":"application/json",
        },
      }
    );
  }
});