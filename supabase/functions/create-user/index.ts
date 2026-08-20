import { createClient } from "https://esm.sh/@supabase/supabase-js@2";

const corsHeaders={
  "Access-Control-Allow-Origin":"*",
  "Access-Control-Allow-Headers":"authorization, x-client-info, apikey, content-type",
  "Access-Control-Allow-Methods":"POST, OPTIONS"
};

Deno.serve(async(req)=>{
  if(req.method==="OPTIONS"){
    return new Response("ok",{headers:corsHeaders});
  }

  try{
    const authHeader=req.headers.get("Authorization");
    if(!authHeader) throw new Error("Unauthorized");

    const supabaseAdmin=createClient(
      Deno.env.get("SUPABASE_URL")!,
      Deno.env.get("SUPABASE_SERVICE_ROLE_KEY")!
    );

    const token=authHeader.replace("Bearer ","");

    const {data:{user:caller},error:authError}=await supabaseAdmin.auth.getUser(token);

    if(authError||!caller) throw new Error("Invalid session");

    const body=await req.json();

    const {
      email,
      password,
      full_name,
      role,
      organization_id,
      branchIds=[]
    }=body;

    if(!email||!password||!full_name||!organization_id){
      throw new Error("Missing required fields");
    }

    const {data:newUser,error:createError}=await supabaseAdmin.auth.admin.createUser({
      email,
      password,
      email_confirm:true,
      user_metadata:{
        full_name
      }
    });

    if(createError)throw createError;

    const userId=newUser.user.id;

    const {error:profileError}=await supabaseAdmin
      .from("profiles")
      .insert({
        id:userId,
        organization_id,
        email,
        full_name,
        role,
        is_active:true
      });

    if(profileError)throw profileError;


    if(role!=="super_admin"&&branchIds.length){
      const rows=branchIds.map((branch_id:string)=>({
        user_id:userId,
        branch_id
      }));

      const {error:accessError}=await supabaseAdmin
        .from("user_branch_access")
        .insert(rows);

      if(accessError)throw accessError;
    }


    return new Response(
      JSON.stringify({
        success:true,
        user_id:userId
      }),
      {
        status:200,
        headers:{
          ...corsHeaders,
          "Content-Type":"application/json"
        }
      }
    );

  }catch(error){

    return new Response(
      JSON.stringify({
        error:error.message
      }),
      {
        status:400,
        headers:{
          ...corsHeaders,
          "Content-Type":"application/json"
        }
      }
    );
  }
});