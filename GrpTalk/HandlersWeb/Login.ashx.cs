using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;
using System.Web.SessionState;
using GT.Utilities;


namespace GrpTalk.WebHandlers
{
    /// <summary>
    /// Summary description for Login
    /// </summary>
    public class Login : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int type = Convert.ToInt32(HttpContext.Current.Request["Type"]);
            JObject Jobj = new JObject();
            switch (type)
            {
                case 1:
                    Jobj = LoginRequest(context);
                    context.Response.Write(Jobj);
                    return;
                case 2:
                    Jobj = ValidateOTP(context);
                    context.Response.Write(Jobj);
                    return;
                case 3:
                    Jobj = ValidateAccessToken(context);
                    context.Response.Write(Jobj);
                    return;
                case 4:
                    Jobj = ValidateQrCode(context);
                    context.Response.Write(Jobj);
                    return;

                case 5:
                    Jobj = QrCodeGenerator(context);
                    context.Response.Write(Jobj);
                    return;
            }
        }

        private JObject LoginRequest(HttpContext context)
        {
            string txnID = "";
            if (Convert.ToInt32(context.Request["isResend"]) == 1)
            {
                txnID = context.Request["txnID"].ToString();
            }
            else
            {
                txnID = Guid.NewGuid().ToString();
            }
            JObject paramObj = new JObject();
            JObject responseJobj = new JObject();
            paramObj = new JObject(new JProperty("MobileNumber", context.Request["mobileNumber"].ToString()),
                new JProperty("DeviceToken", ""),
                new JProperty("DeviceUniqueID", ""),
                new JProperty("OsID", 3));
            bool resend = Convert.ToBoolean(Convert.ToInt32(context.Request["isResend"]));
            GT.BusinessLogicLayer.V_1_5.Login loginObj = new GT.BusinessLogicLayer.V_1_5.Login();
            responseJobj = loginObj.Registration(MyConf.MyConnectionString, paramObj, "", txnID, resend, 108);
            responseJobj.Add(new JProperty("txnID", txnID));

            return responseJobj;
        }

        private JObject ValidateOTP(HttpContext context)
        {
            JObject responseJobj = new JObject();
            GT.BusinessLogicLayer.V_1_5.Login loginObj = new GT.BusinessLogicLayer.V_1_5.Login();
            responseJobj = loginObj.ValidateOTP(MyConf.MyConnectionString, context.Request["mobileNumber"].ToString(), context.Request["Otp"].ToString(), "", 1,108);
            responseJobj.Add(new JProperty("RedirectURL", System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString() + "MyGrpTalks.aspx"));
            string res = responseJobj.SelectToken("Success").ToString();
            int userid = Convert.ToInt32(responseJobj.SelectToken("UserId"));
            if (responseJobj.SelectToken("Success").ToString() == "True")
            {
                string SessionId = Guid.NewGuid().ToString();
                HttpCookie SessCookie = new HttpCookie("SessionId", SessionId);
                SessCookie.HttpOnly = true;
                SessCookie.Values.Add("UserID", "5");
                SessCookie.Values.Add("CountryID", "108");
                HttpContext.Current.Response.Cookies.Add(SessCookie);
                HttpContext.Current.Session["UserID"] = userid;
                HttpContext.Current.Session["QrCode"] = "CHANNEL";
                HttpContext.Current.Session["CountryID"] = Convert.ToInt32(responseJobj.SelectToken("CountryID"));
                HttpContext.Current.Session["HostMobile"] = context.Request["mobileNumber"].ToString();
                HttpContext.Current.Session["IpAddress"] = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
                HttpContext.Current.Session["Offset"] = responseJobj.SelectToken("Offset").ToString();

                if (Convert.ToInt32(context.Request["keepMeSignIn"]) == 1)
                {
                   // HttpContext.Current.Session.Timeout = 1440;
                    SessCookie.Expires = DateTime.Now.AddDays(1);

                }
                Logger.TraceLog("UserId" + userid);
            }

            return responseJobj;
        }


        private JObject ValidateAccessToken(HttpContext context)
        {
            JObject responseJobj = new JObject();
            GT.BusinessLogicLayer.V_1_5.Login loginObj = new GT.BusinessLogicLayer.V_1_5.Login();
            if (context.Request["accessToken"] != null)
            {
                responseJobj = loginObj.ValidateAccessToken(MyConf.MyConnectionString, context.Request["accessToken"].ToString());

                if (responseJobj.SelectToken("Success").ToString() == "True")
                {
                    string SessionId = Guid.NewGuid().ToString();
                    HttpCookie SessCookie = new HttpCookie("SessionId", SessionId);
                    SessCookie.HttpOnly = true;
                    HttpContext.Current.Response.Cookies.Add(SessCookie);
                    HttpContext.Current.Session["UserID"] = Convert.ToInt32(responseJobj.SelectToken("UserID")); ;
                    HttpContext.Current.Session["CountryID"] = Convert.ToInt32(responseJobj.SelectToken("CountryID"));
                    HttpContext.Current.Session["HostMobile"] = Convert.ToString(responseJobj.SelectToken("HostMobile"));
                    HttpContext.Current.Session["UserType"] = Convert.ToInt16(responseJobj.SelectToken("UserType"));
                    if (Convert.ToInt16(responseJobj.SelectToken("UserType")) == 2)
                    {
                        responseJobj.Add(new JProperty("RedirectURL", System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString() + "MyGrpTalks.aspx"));
                    }
                    else
                    {
                        responseJobj.Add(new JProperty("RedirectURL", System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString() + "MyGroup.aspx"));
                    }
                }
            }
            return responseJobj;

        }

        private JObject ValidateQrCode(HttpContext context)
        {
            Logger.TraceLog("QrCode retval BAl " + context.Request["qrCodeAccessToken"]);
            JObject responseJobj = new JObject();
            GT.BusinessLogicLayer.V_1_5.Login loginObj = new GT.BusinessLogicLayer.V_1_5.Login();
            if (context.Request["qrCodeAccessToken"] != null)
            {
                try
                {
                    string ipAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
                    responseJobj = loginObj.ValidateQrCode(MyConf.MyConnectionString, context.Request["qrCodeAccessToken"], context.Request["QrCode"].ToString(), context.Request["DeviceUniqueId"].ToString(), context.Request["DeviceToken"].ToString(), ipAddress, context.Request["browserName"].ToString(), Convert.ToInt16(context.Request["OsId"]), context.Request["osName"]);
                    if (Convert.ToBoolean(responseJobj.SelectToken("Success")) == true)
                    {


                        if (Convert.ToInt32(context.Request["keepMeLoggedIn"]) == 1)
                        {
                            string SessionId = Guid.NewGuid().ToString();
                            HttpCookie SessCookie = new HttpCookie("SessionId", SessionId);
                            SessCookie.HttpOnly = true;
                            HttpContext.Current.Response.Cookies.Add(SessCookie);
                            SessCookie.Values.Add("UserID", responseJobj.SelectToken("UserID").ToString());
                            SessCookie.Values.Add("CountryID", responseJobj.SelectToken("CountryID").ToString());
                            SessCookie.Values.Add("Offset", responseJobj.SelectToken("Offset").ToString());
                            HttpCookie userCookie = new HttpCookie("UserData");
                            userCookie.HttpOnly = true;
                            userCookie.Values.Add("HostMobile", responseJobj.SelectToken("HostMobile").ToString());
                            userCookie.Values.Add("UserType", responseJobj.SelectToken("UserType").ToString());
                            userCookie.Values.Add("QrCode", context.Request["QrCode"].ToString());
                            HttpContext.Current.Response.Cookies.Add(userCookie);
                            if (responseJobj.SelectToken("UserID").ToString() != "31814" || responseJobj.SelectToken("UserID").ToString() != "63223")
                            {
                                SessCookie.Expires = DateTime.Now.AddDays(1);
                                userCookie.Expires = DateTime.Now.AddDays(1);
                            }
                            else
                            {
                                SessCookie.Expires = DateTime.Now.AddDays(30);
                                userCookie.Expires = DateTime.Now.AddDays(30);
                            }
                            
                            HttpContext.Current.Session.Timeout = 1440;

                        }
                        else
                        {

                            HttpContext.Current.Session["UserID"] = Convert.ToInt32(responseJobj.SelectToken("UserID"));
                            HttpContext.Current.Session["CountryID"] = Convert.ToInt32(responseJobj.SelectToken("CountryID"));
                            HttpContext.Current.Session["HostMobile"] = Convert.ToString(responseJobj.SelectToken("HostMobile"));
                            HttpContext.Current.Session["UserType"] = Convert.ToInt16(responseJobj.SelectToken("UserType"));
                            HttpContext.Current.Session["QrCode"] = context.Request["QrCode"].ToString();
                            HttpContext.Current.Session["Offset"] = responseJobj.SelectToken("Offset").ToString();
                            Logger.TraceLog("sessions" + HttpContext.Current.Session["UserID"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Exception in ValidateQrCode Login.ashx" + ex.ToString());

                }
            }
            return responseJobj;


        }
        public JObject QrCodeGenerator(HttpContext context)
        {
            JObject jobj = new JObject();
            GT.BusinessLogicLayer.V_1_5.Login loginObj = new GT.BusinessLogicLayer.V_1_5.Login();
            try
            {
                jobj = loginObj.QrCodeGenerator();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Index.aspx QrCodeGenerator() : " + ex.ToString());

            }
            return jobj;

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