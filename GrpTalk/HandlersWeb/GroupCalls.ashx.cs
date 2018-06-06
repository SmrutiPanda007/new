using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer.V_1_5;
using GrpTalk.CommonClasses;
using System.Web.SessionState;
using GT.Utilities;

namespace GrpTalk.HandlersWeb
{
    /// <summary>
    /// Summary description for GroupCalls
    /// </summary>
    public class GroupCalls : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int type = Convert.ToInt32(HttpContext.Current.Request["Type"]);
            JObject returnObj = new JObject();
            switch (type)
            {


                case 1:
                    returnObj = Dial(context);
                    context.Response.Write(returnObj);
                    return;
                case 2:
                    returnObj = GetGroupCallRoom(context);
                    context.Response.Write(returnObj);
                    return;
                case 3:
                   returnObj = HangUp(context);
                    context.Response.Write(returnObj.ToString());
                    return;
                case 4:
                    returnObj = MuteUnMute(context);
                    context.Response.Write(returnObj);
                    return;
                case 5:
                    returnObj = TransferCall(context);
                    context.Response.Write(returnObj);
                    return;
            }
        }
        private JObject MuteUnMute(HttpContext context)
        {

            JObject responseJObj = new JObject();
            GT.BusinessLogicLayer.V_1_5.GroupCall groupCallObj = new GT.BusinessLogicLayer.V_1_5.GroupCall();
            responseJObj = groupCallObj.ValidateMuteUnmute(MyConf.MyConnectionString, JObject.Parse(context.Request["muteUnMuteObj"].ToString()), Convert.ToInt32(context.Request["UserID"]));
            return responseJObj;
        }
        private JObject HangUp(HttpContext context)
        {
            JObject responseJObj = new JObject();
            GT.BusinessLogicLayer.V_1_5.GroupCall groupCallObj = new GT.BusinessLogicLayer.V_1_5.GroupCall();
            responseJObj = groupCallObj.ValidateHangUpAction(MyConf.MyConnectionString, JObject.Parse(context.Request["hangUpObj"].ToString()), Convert.ToInt32(context.Request["UserID"]));
            return responseJObj;
        }
        private JObject GetGroupCallRoom(HttpContext context)
        {
            JObject responseJObj = new JObject();
            GT.BusinessLogicLayer.V_1_5.Groups groupsObj = new GT.BusinessLogicLayer.V_1_5.Groups();

            responseJObj = groupsObj.GetGroupCallRoom(MyConf.MyConnectionString, Convert.ToInt32(context.Request["grpCallId"]), Convert.ToInt32(context.Request["mode"]), Convert.ToInt32(context.Request["PageSize"]), Convert.ToInt32(context.Request["PageNumber"]), Convert.ToInt32(context.Session["UserID"]), context.Request["SearchText"].ToString());
            return responseJObj;
        }
        private JObject Dial(System.Web.HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.GroupCall groupCallObj = new GT.BusinessLogicLayer.V_1_5.GroupCall();
            JObject dialResponseJobj = new JObject();
            JObject dialObj = new JObject();
            dialObj = JObject.Parse(context.Request["dialObj"].ToString());

            string grpCallCallBackUrl = System.Configuration.ConfigurationManager.AppSettings["grpCallCallBackUrl"].ToString();
            dialResponseJobj = groupCallObj.ValidateGrpCallDial(MyConf.MyConnectionString, dialObj, Convert.ToInt32(context.Session["UserId"]
                ), grpCallCallBackUrl,1);
            Logger.TraceLog("DIal In handlers web" + dialObj.ToString());
            return dialResponseJobj;
        }

        private JObject TransferCall(HttpContext context)
        {
            JObject responseJObj = new JObject();
            GT.BusinessLogicLayer.V_1_5.GroupCall groupsObj = new GT.BusinessLogicLayer.V_1_5.GroupCall();
            responseJObj = groupsObj.TransferCall(MyConf.MyConnectionString, JObject.Parse(context.Request["privateUnPrivateObj"].ToString()), Convert.ToInt32(context.Session["UserID"]));
            return responseJObj;
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