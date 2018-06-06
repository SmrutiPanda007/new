using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;

namespace GrpTalk
{
    /// <summary>
    /// Summary description for SendAppDownloadLink
    /// </summary>
    public class SendAppDownloadLink : IHttpHandler
    {
        public string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["GrpTalk"].ToString();
        public void ProcessRequest(HttpContext context)
        {
            JObject RespJobj = new JObject();
            string respString = "";
            BusinessHelper helperObj = new BusinessHelper();
            respString = helperObj.SendAppDownloadLink(ConStr, context);
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