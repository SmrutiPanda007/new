using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GrpTalk.CommonClasses;
using GT.Utilities;

namespace GrpTalk.Handlers
{
    /// <summary>
    /// Summary description for Index
    /// </summary>
    public class Index : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            
            int type = Convert.ToInt32(context.Request["type"]);
            JObject respJobj = new JObject();
            switch (type)
            {

                case 1:
                    respJobj = SendAppDownloadLink(context);
                    context.Response.Write(respJobj);
                    return;
            }
        }
        public JObject SendAppDownloadLink(HttpContext context)
        {
            JObject respJobj = new JObject();
            try
            {
                GT.BusinessLogicLayer.Index obj = new GT.BusinessLogicLayer.Index();
                respJobj = obj.SendAppDownloadLink(MyConf.MyConnectionString, Convert.ToInt32(context.Request["CountryId"]), Convert.ToString(context.Request["MobileNumber"]), Convert.ToString(context.Request["Email"]), Convert.ToInt32(context.Request["LeadType"]));


            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception In Index.ashx :" + ex.ToString());
            }
            return respJobj;
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