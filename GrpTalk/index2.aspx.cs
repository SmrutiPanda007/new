﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;
using System.Security.Cryptography;
using System.IO;
using PushSharp;
using PusherServer;
using System.Drawing.Drawing2D;
using System.Configuration;
using System.Net;


namespace GrpTalk
{
    public partial class index2 : System.Web.UI.Page
    {
        public string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
        public string baseString = "";
        public string conf_code = "";
        IPAddress[] ipServer = Dns.GetHostAddresses("web.grptalk.com");

       

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.TraceLog(Convert.ToString(ipServer[0]));
         
            if (Session["UserID"] != null)
            {
                Response.Redirect("/MyGrptalks.aspx");
            }
            else
            {
                var location = Request.Url.Host.ToLower();
                if (location.Contains("web.grptalk.com"))
                {
                    Response.Redirect("https://web.grptalk.com/Home.aspx");
                }

            }


        }


    }


}