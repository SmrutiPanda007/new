using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GT.Utilities.Properties;
using PusherServer;
using PushSharp;
using GrpTalk.CommonClasses;
using GT.Utilities;

namespace GrpTalk
{
    /// <summary>
    /// Summary description for api_response
    /// </summary>

    public class api_response : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            string callBackReqStr = "";
            string methodType = "";
            string pusherAppId = "";
            string pusherAppKey = "";
            string pusherAppsecret = "";
            string HangupCausesForRetry = "";
            string callBackUrl = "";
            string callBackResponse = "";
            JObject callBackReqObj = default(JObject);
            StreamReader inputStream = null;
            context.Request.InputStream.Position = 0;
            inputStream = new StreamReader(context.Request.InputStream);
            callBackReqStr = inputStream.ReadToEnd();
            callBackReqObj = new JObject();
            callBackReqObj = JObject.Parse(callBackReqStr);
            methodType = context.Request.HttpMethod.ToString().ToUpper();
            UpdateCallBacksBussiness groupcallReportsObj = new UpdateCallBacksBussiness();
            pusherAppId = System.Configuration.ConfigurationManager.AppSettings["pusherAppId"].ToString();
            pusherAppKey = System.Configuration.ConfigurationManager.AppSettings["pusherAppKey"].ToString();
            pusherAppsecret = System.Configuration.ConfigurationManager.AppSettings["pusherAppsecret"].ToString();
            HangupCausesForRetry = System.Configuration.ConfigurationManager.AppSettings["HangupCausesForCallRetry"].ToString();
            callBackUrl = System.Configuration.ConfigurationManager.AppSettings["grpCallCallBackUrl"].ToString();
            callBackResponse = groupcallReportsObj.UpdateCallBacks(MyConf.MyConnectionString, methodType, context.Request.UserHostAddress, callBackReqObj, pusherAppId, pusherAppKey, pusherAppsecret, HangupCausesForRetry, context, callBackUrl);
            context.Response.Write(callBackResponse);
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