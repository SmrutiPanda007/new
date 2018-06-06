using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;
using GT.BusinessLogicLayer.V_1_5;
using System.Web.SessionState;
using System.Net;
using GT.Utilities;
using System.Configuration;
namespace GrpTalk.WebHandlers
{
    /// <summary>
    /// Summary description for GrpTalkReports
    /// </summary>
    public class GrpTalkReports : IHttpHandler, IRequiresSessionState
    {
        JObject Jobj = new JObject();
        public void ProcessRequest(HttpContext context)
        {
            int type = Convert.ToInt32(HttpContext.Current.Request["Type"]);

            switch (type)
            {

                case 1:
                    Jobj = GetReportsByBatchId(context);
                    context.Response.Write(Jobj);
                    return;
                case 2:
                    Download_VoiceFile(context);
                    return;
                case 3:
                    Download_ExcelFile(context);
                    return;

                    
            }
           
        }

        private void Download_ExcelFile(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.GroupCallReports obj = new GT.BusinessLogicLayer.V_1_5.GroupCallReports();
            obj.Download_ExcelFile(MyConf.MyConnectionString, context.Request["batchID"].ToString());            
        }


        private void Download_VoiceFile(HttpContext con)
        {
            string fileName = con.Request["fileName"];
            string downloadFileName = con.Request["reportName"];
            
            try
            {
                
                WebClient req = new WebClient();
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = true;
                response.AddHeader("Content-Disposition", "attachment;filename=" + downloadFileName);
               byte[] data = req.DownloadData( fileName);
               response.BinaryWrite(data);
               HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Download_File" + ex.ToString());
            }

        }
        private JObject GetReportsByBatchId(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.GroupCallReports obj = new GT.BusinessLogicLayer.V_1_5.GroupCallReports();
            Jobj = obj.GetReportsByBatchIdNew(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["groupID"]), context.Request["batchID"].ToString(), Convert.ToInt32(context.Request["pageIndex"]),10,"");
            return Jobj;
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