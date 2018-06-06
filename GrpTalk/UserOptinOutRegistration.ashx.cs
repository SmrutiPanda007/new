using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;
using System.Web;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using PusherServer;
using GT.BusinessLogicLayer;

namespace GrpTalk
{
    /// <summary>
    /// Summary description for UserOptinOutRegistration
    /// </summary>
    public class UserOptinOutRegistration : IHttpHandler
    {
        public string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["GrpTalk"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            JObject RespJobj = new JObject();
			//hiiiiiiiiiiiiii
            string respString = "";
            string optinNumber = "";
            string optOutClip = "";
            optinNumber = System.Configuration.ConfigurationManager.AppSettings["OptinNumber"].ToString();
            optOutClip = System.Configuration.ConfigurationManager.AppSettings["OptOutClip"].ToString();
            BusinessHelper helperObj = new BusinessHelper();
            respString = helperObj.UserOptInOutRegistraton(ConStr, context, optinNumber, optOutClip);
            context.Response.Write(respString);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}