using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;
using GT.BusinessLogicLayer.V_1_5;
using System.Web.SessionState;
using log4net;
using GT.Utilities;

namespace GrpTalk.WebHandlers
{
    /// <summary>
    /// Summary description for Groups
    /// </summary>
    public class Groups : IHttpHandler,IRequiresSessionState
    {
        JObject Jobj = new JObject();
        public void ProcessRequest(HttpContext context)
        {
            int type = Convert.ToInt32(HttpContext.Current.Request["Type"]);

            switch (type)
            {


                case 1:
                    Jobj = GetAllGroupCalls(context);
                    context.Response.Write(Jobj);
                    return;
                case 2:
                    Jobj = GrpTalksHistoryDetails(context);
                    context.Response.Write(Jobj);
                    return;
                case 3:
                    Jobj = CallRecordSubscription(context);
                    context.Response.Write(Jobj);
                    return;
                case 4:
                    Jobj = CreateGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 5:
                    Jobj = EditGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 6:
                    Jobj = DeleteGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 7:
                    Jobj = LeaveGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 8:
                    Jobj = AddParticipantInGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 9:
                    Jobj = CancelGroupCall(context);
                    context.Response.Write(Jobj);
                    return;
                case 10:
                    Jobj = LiveCallDetails(context);
                    context.Response.Write(Jobj);
                    return;
                case 11:
                    Jobj = GetGrpCallDetails(context);
                    context.Response.Write(Jobj);
                    return;
                

            }
        }
        private JObject LiveCallDetails(HttpContext context)
        {
            if (context.Session["UserID"] != null)
            {
                GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
                Jobj = obj.LiveCallDetails(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]));
                
            }
            else {
                Jobj = new JObject(new JProperty("Success", false), new JProperty("Message", "Session Expired"));
            }
            return Jobj;
        }
        private JObject CancelGroupCall(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.GrpCallCancel(MyConf.MyConnectionString, Convert.ToInt32(context.Request["grpCallId"]));
            return Jobj;
        }
        private JObject AddParticipantInGroupCall(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.AddParticipantInGroupCall(MyConf.MyConnectionString, JObject.Parse(context.Request["paramObj"].ToString()), Convert.ToInt32(context.Session["UserID"]));

            return Jobj;
        }
        private JObject CreateGroupCall(System.Web.HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.CreateGroupCall(MyConf.MyConnectionString, JObject.Parse(context.Request["paramObj"].ToString().Replace(@"//",@"/").Replace(@"\\\\",@"\\")), Convert.ToInt32(context.Session["UserID"]));
            return Jobj;
        }
        private JObject CallRecordSubscription(System.Web.HttpContext context)
        {
            
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.CallRecordSubscription(MyConf.MyConnectionString,Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["subscribeStatus"]));
            return Jobj;
        }

        private JObject GrpTalksHistoryDetails(System.Web.HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.GroupCallHistory(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), Convert.ToInt32(context.Request["grpCallID"]), Convert.ToInt32(context.Request["pageIndex"]), Convert.ToInt32(context.Request["pageSize"]));
            return Jobj;
        }
        private JObject GetAllGroupCalls(HttpContext context)
        {
            
            GT.BusinessLogicLayer.V_1_4.Groups_V140 obj = new GT.BusinessLogicLayer.V_1_4.Groups_V140();
            Jobj = obj.GetAllGroupCalls(MyConf.MyConnectionString, Convert.ToInt32(context.Session["UserID"]), 3, "", "", "", Convert.ToInt32(context.Session["CountryID"]));
            return Jobj;
        }
        private JObject EditGroupCall(HttpContext context)
        {


            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.EditGroupCall(MyConf.MyConnectionString, JObject.Parse(context.Request["jsonObject"].ToString().Replace(@"//",@"/").Replace(@"\\\\",@"\\")), Convert.ToInt32(context.Session["UserID"]),0);
            return Jobj;
        }
        private JObject DeleteGroupCall(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.DeleteGroupCall(MyConf.MyConnectionString, Convert.ToInt32(context.Request["grpCallId"]));
            return Jobj;
        }
        private JObject LeaveGroupCall(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.MemberLeaveFromGrpCall(MyConf.MyConnectionString, Convert.ToInt32(context.Request["grpCallId"]), Convert.ToInt32(context.Session["UserID"]), Convert.ToInt16(context.Request["isSecondaryModerator"]));
            return Jobj;
        }
        private JObject GetGrpCallDetails(HttpContext context)
        {
            GT.BusinessLogicLayer.V_1_5.Groups obj = new GT.BusinessLogicLayer.V_1_5.Groups();
            Jobj = obj.GetGrpCallDetails(MyConf.MyConnectionString, Convert.ToInt32(context.Request["grpCallId"]), Convert.ToInt32(context.Session["UserID"]));
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