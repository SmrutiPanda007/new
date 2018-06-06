using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;



namespace GrpTalk.CommonClasses
{
    public static class UserSession
    {
      
        public static string mobile
        {
            get { return (string)HttpContext.Current.Session["mobile"]; }
            set { HttpContext.Current.Session["mobile"] = value; }
        }
        public static string SessionId
        {
            get
            {
                return (string)HttpContext.Current.Session["SessionId"];
            }
            set { HttpContext.Current.Session["SessionId"] = value; }
        }
        public static string UserName
        {
            get { return (string)HttpContext.Current.Session["username"]; }
            set { HttpContext.Current.Session["username"] = value; }
        }
        public static string LoginCaptcha
        {
            get { return (string)HttpContext.Current.Session["captcha"]; }
            set { HttpContext.Current.Session["captcha"] = value; }
        }

        public static string LoginTime
        {
            get {
                if (string.IsNullOrEmpty(HttpContext.Current.Session["LoginTime"] as string))
                {
                    return string.Empty;
                }
                else
                {
                    return (HttpContext.Current.Session["LoginTime"].ToString());
                }

               }
            set { HttpContext.Current.Session["LoginTime"] = value; }
        }

        public static string LoginAtmptWithWrongPwd
        {
            get
            {
                if (string.IsNullOrEmpty(HttpContext.Current.Session["logincnt"] as string))
                {
                    return string.Empty;
                }
                else
                {
                    return (HttpContext.Current.Session["logincnt"].ToString());
                }
            }
                //return (Int16)HttpContext.Current.Session["logincnt"]; }
            set { HttpContext.Current.Session["logincnt"] = value; }
        }
      
        public static string LoginRole
        {
            get { return (string)HttpContext.Current.Session["Role"]; }
            set { HttpContext.Current.Session["Role"] = value; }
        }
        public static string UserId
        {
            get {
                if (string.IsNullOrEmpty(HttpContext.Current.Session["userid"] as string))
                {
                    return string.Empty;
                }
                else {
                    return HttpContext.Current.Session["userid"].ToString();
                }
                 }
            set { HttpContext.Current.Session["userid"] = value; }
        }

    }
}