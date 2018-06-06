using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GT.Utilities;

namespace GrpTalk.CommonClasses
{
    public class AuthModule : IHttpModule
    {
        public void Init(HttpApplication app)
        {
            app.AuthenticateRequest += new EventHandler(SmscAuthenticateRequest);
        }
        private void SmscAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.Items.Add("StartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            HttpContext.Current.Items.Add("RequestID", Guid.NewGuid());
            HttpApplication app = (HttpApplication)sender;
            bool isProceed = false;
            string testVar = app.Request.RawUrl.ToString().ToUpper();
            if ("V1,V2,V2.1,V1.1,V1.2,V1.3,V2.0".Split(',').Contains(app.Request.RawUrl.ToString().ToUpper().Split('/')[1]))
            {
                isProceed = true;
            }
            if (!isProceed)
            {
                return;
            }
            if (app.Request.ServerVariables["REMOTE_ADDR"] == "192.168.1.37")
            {
                try
                {
                    app.Request.SaveAs("D:\\Websites\\GrpTalkLatest\\Logs\\AuthModule_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", true);
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Exception in AuthModule is " + ex.ToString());
                }
            }
            if (!app.Request.Headers.AllKeys.Contains("Authorization") || !app.Request.Headers["Authorization"].StartsWith("Basic"))
            {
                FlushResponse(app, 401, 1, "Please Provide Authorization Headers with your request");
            }
            else
            {
                GrpTalk.CommonClasses.AuthClient authClientObj = new GrpTalk.CommonClasses.AuthClient();
                string[] credentials = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(app.Request.Headers["Authorization"].Substring(6))).Split(new char[] { ':' });
                if (credentials.Length != 2 || string.IsNullOrEmpty(credentials[0]) || string.IsNullOrEmpty(credentials[1]))
                {
                    FlushResponse(app, 401, 1, "Please Provide Authorization Headers with your request");
                }
                else
                {
                    authClientObj.ApiKey = credentials[0];
                    authClientObj.ApiSecret = credentials[1];
                    if (authClientObj.Authenticate() == true)
                    {
                        app.Context.Items.Add("UserObject", authClientObj.authValidateResponse);
                    }
                    else
                    {
                        FlushResponse(app, Convert.ToInt32(authClientObj.authValidateResponse["StatusCode"]), Convert.ToInt32(authClientObj.authValidateResponse["SubStatusCode"]), authClientObj.authValidateResponse["StatusDescription"].ToString());
                    }
                }
            }
        }

        public void Dispose() { }

        private void FlushResponse(HttpApplication httpApp, int statusCode, int subStatusCode, string statusDescription)
        {
            httpApp.Response.StatusCode = statusCode;
            if (subStatusCode >= 0)
            {
                httpApp.Response.SubStatusCode = subStatusCode;
            }
            if (statusDescription.Length > 0)
            {
                httpApp.Response.StatusDescription = statusDescription;
            }
            httpApp.Response.AppendHeader("WWW-Authenticate", "Basic");
            httpApp.CompleteRequest();
        }
    }
}