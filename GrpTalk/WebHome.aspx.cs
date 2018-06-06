using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class WebHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().Contains("android"))
            {
                Response.Redirect("intent://scan/#Intent;scheme=grptalk;package=com.mobile.android.grptalk;S.browser_fallback_url=;end");
            }
            if (Request.Cookies["SessionId"] != null)
            {
                HttpContext.Current.Session["UserID"] = Request.Cookies["SessionId"]["UserID"];
                HttpContext.Current.Session["CountryID"] = Request.Cookies["SessionId"]["CountryID"];
                HttpContext.Current.Session["HostMobile"] = Request.Cookies["UserData"]["HostMobile"];
                HttpContext.Current.Session["UserType"] = Request.Cookies["UserData"]["UserType"];
                HttpContext.Current.Session["QrCode"] = Request.Cookies["UserData"]["QrCode"];
            }

            if (Session["UserID"] != null)
            {
                Response.Redirect("/MyGrptalks.aspx");
            }
            //if (Session.IsNewSession)
            //{
            //    Logger.TraceLog("Session_start Event Fired");
            //    if (Request.Cookies["UserID"] != null)
            //    {
            //        Logger.TraceLog("If Condition in Session_start Event Fired");
            //        HttpContext.Current.Session["UserID"] = Request.Cookies["UserID"].Value;
            //        Logger.TraceLog("User id in Session_start Event Fired" + HttpContext.Current.Session["UserID"]);
            //        Response.Redirect("/Logout.aspx");
            //    }
            //}
        }
    }
}