using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Security.Principal;
using System.Web.SessionState;
using log4net;
using System.Web.Routing;
using GT.Utilities;


namespace GrpTalk
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Logger.Initialize();
            RegisterRoutes(RouteTable.Routes);

        }

        protected void Session_Start(object sender, EventArgs e)
        {
           
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //Logger.TraceLog("Session ENd" + this.Session["UserID"]);
            //logout logout = new logout();
            //logout.UpdateQrCodeLogout(Convert.ToInt32(this.Session["UserID"]), 1);
            //Session.Abandon();
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
        public void RegisterRoutes(RouteCollection routes)
        {

            RouteValueDictionary rvd = new RouteValueDictionary();
            routes.Add(new Route("{Version}/{ResourceName}/", null, rvd, new Handlers.RoutesHandler()));
            routes.MapPageRoute("downloads", "downloads", "~/downloads.aspx");
            routes.MapPageRoute("Home", "Home", "~/HomePage.aspx");
            routes.MapPageRoute("AboutUs", "AboutUs", "~/About_Us.aspx");
            routes.MapPageRoute("Features", "Features", "~/Features.aspx");
            routes.MapPageRoute("Pricing", "Pricing", "~/Features.aspx");
            routes.MapPageRoute("Login", "Login", "~/QrCodeLogin.aspx");


        }




    }
}