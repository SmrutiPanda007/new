using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Web;
using System.Net;
using GT.DataAccessLayer;
using GT.Utilities.Properties;
using GT.Utilities;
using System.Configuration;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Drawing;


namespace GT.BusinessLogicLayer.V_1_5
{
    public class Groups
    {

        public JObject CallRecordSubscription(string sConnString, int userId, int subscribeStatus)
        {
            DataSet CallSubscriptionDs = new DataSet();
            JObject CallSubscriptionObj = new JObject();
            int retVal = 0;
            string retMsg = "";
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                CallSubscriptionDs = groupsObj.CallRecordSubscription(userId, subscribeStatus, out retVal, out retMsg);

                if (retVal == 1)
                {
                    CallSubscriptionObj = new JObject(new JProperty("Success", true),
                                             new JProperty("Message", retMsg),
                                             new JProperty("Status", retVal));
                }
                else
                {
                    CallSubscriptionObj = new JObject(new JProperty("Success", false),
                                             new JProperty("Message", retMsg));

                }

            }
            catch (Exception ex)
            {
                CallSubscriptionObj = new JObject(new JProperty("Success", false),
                                             new JProperty("Message", retMsg),
                                             new JProperty("Status", retVal));
            }
            return CallSubscriptionObj;
        }
        public JObject GroupCallHistory(string sConnString, int userId, int grpCallID, int pageIndex, int pageSize)
        {
            JObject responseJobj = new JObject();
            DataSet historyDS = new DataSet();
            JArray historyJarr = new JArray();
            JArray upcomingJarr = new JArray();
            JArray monthJarr = new JArray();
            JObject monthJobj = new JObject();
            JObject historyJobj = new JObject();
            JObject upcomingJobj = new JObject();
            int isCallRecordSubscribed = 0;
            int pageCount = 0;
            Int16 retVal = 0;
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                historyDS = groupsObj.GroupCallHistory(userId, grpCallID, pageIndex, pageSize, out pageCount, out retVal);

                historyJarr = new JArray();

                if (retVal == 1)
                {
                    if (historyDS.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j <= historyDS.Tables[0].Rows.Count - 1; j++)
                        {
                            historyJobj = new JObject(new JProperty("BatchID", historyDS.Tables[0].Rows[j]["BatchID"]),
                                new JProperty("StartTime", (historyDS.Tables[0].Rows[j]["StartTime"]).ToString().Replace("  ", " ")),
                                new JProperty("Duration", historyDS.Tables[0].Rows[j]["Duration"]),
                                new JProperty("Invites", historyDS.Tables[0].Rows[j]["Invites"]),
                                new JProperty("GrpCallID", grpCallID),
                                new JProperty("Connected", historyDS.Tables[0].Rows[j]["Connected"]),
                                 new JProperty("YearMonth", historyDS.Tables[0].Rows[j]["YearMonth"]));
                            historyJarr.Add(historyJobj);
                        }
                    }

                    upcomingJarr = new JArray();

                    responseJobj = new JObject(new JProperty("Success", true), new JProperty("ErrorCode", "117"),
                        new JProperty("PageCount", pageCount),
                        new JProperty("History", historyJarr),
                        new JProperty("UpComing", upcomingJarr));
                    if (historyDS.Tables[historyDS.Tables.Count - 1].Rows.Count > 0)
                    {

                        foreach (DataColumn dc in historyDS.Tables[historyDS.Tables.Count - 1].Columns)
                        {

                            responseJobj.Add(new JProperty(dc.ColumnName, historyDS.Tables[historyDS.Tables.Count - 1].Rows[0][dc.ColumnName]));
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In GetCallHistory :" + ex.ToString());
                responseJobj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }

            return responseJobj;
        }
        /// <summary>
        /// This Function is used to Leave a memeber from grpCall 
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="paramObj"></param>
        /// <returns></returns>
        public JObject MemberLeaveFromGrpCall(string sConnString, int ConferenceID, int userId, int isSecondaryModerator)
        {
            JObject respLeaveGroupCall = new JObject();
            try
            {

                int grpTalkID = ConferenceID;
                short retVal = 0;
                short leaveStatus = 0;
                short osID = 0;
                string hostMobile = "";
                string grpTalkName = "";
                string schTime = "";
                string deviceToken = "";
                string retMessage = "";
                string notificationMsg = "";
                string leaveMemName = "";
                int errorCode = 0;
                Int16 dialIn = 0, allowNonMems = 0;
                DataSet ds = new DataSet();
                string appVersion = "";
                DataAccessLayer.V_1_5.Groups leaveGroupObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                ds = leaveGroupObj.MemberLeaveFromGrpCall(userId, grpTalkID, out leaveStatus, out grpTalkName, out deviceToken, out osID, out hostMobile, out schTime, out retVal, out retMessage, out leaveMemName, out errorCode, out appVersion);
                if (retVal == 0)
                {
                    respLeaveGroupCall = new JObject(new JProperty("Success", false),
                                                   new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                }
                else
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            dialIn = Convert.ToInt16(ds.Tables[0].Rows[0]["DialIn"]);
                            allowNonMems = Convert.ToInt16(ds.Tables[0].Rows[0]["AllowNonMembers"]);

                        }
                    }
                    GT.BusinessLogicLayer.NotificationPush pushObj = new GT.BusinessLogicLayer.NotificationPush();

                    if (isSecondaryModerator == 1)
                    {
                        notificationMsg = leaveMemName + " assigned call manager of the group " + grpTalkName + ", has left the group and “assign call manager” setting has been disabled! ";
                        if (appVersion.Contains("2.1"))
                        {
                            pushObj.FCMPushNotifications(notificationMsg, deviceToken, osID);
                        }
                        else
                        {
                            if (osID == 1)
                            {
                                pushObj.IOSpush(deviceToken, notificationMsg);
                            }
                            else
                            {
                                pushObj.sendAndroidPush(notificationMsg, deviceToken);
                            }
                        }
                    }

                    if (dialIn != 0 && allowNonMems != 0)
                    {
                        if (leaveStatus == 1) //scheduled grpcall with less than 3 mebers and schedule canceled case
                        {
                            notificationMsg = leaveMemName + " has left the grpTalk " + grpTalkName + ", your upcoming grpTalk @ " + schTime + " has been cancelled because members ";
                            notificationMsg = notificationMsg + " are less than 3 members. Add more members and re-schedule your grpTalk";
                        }
                        else if (leaveStatus == 2) //Normal grpcall with less than 3 mebers and grpcall updated to inactive
                        {
                            notificationMsg = leaveMemName + " has left the grpTalk " + grpTalkName + ", You do not have enough members in your group add more members to continue grpTalk";
                        }
                        else if (leaveStatus == 3) //more than 2 members,just intimation to host
                        {
                            notificationMsg = leaveMemName + " has left the groupTalk " + grpTalkName;
                        }
                        if (appVersion.Contains("2.1"))
                        {
                            pushObj.FCMPushNotifications(notificationMsg, deviceToken, osID);
                        }
                        else
                        {
                            if (osID == 1)
                            {
                                pushObj.IOSpush(notificationMsg, deviceToken);
                            }
                            else
                            {
                                pushObj.sendAndroidPush(notificationMsg, deviceToken);
                            }
                        }
                    }
                    respLeaveGroupCall = new JObject(new JProperty("Success", true),
                                                  new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode),
                                                  new JProperty("ConferenceID", grpTalkID));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at MemberLeaveFromGrpCall : " + ex.ToString());
                respLeaveGroupCall = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return respLeaveGroupCall;
        }

        public JObject GetGrpCallDetails(string sConnString, int ConferenceID, int userId)
        {
            JObject responseObj = new JObject();
            JArray jarMembers = new JArray();
            JArray jarGroup = new JArray();
            JObject jGroup = new JObject();
            try
            {
                int retVal = 0;
                string retMsg = "";
                DataSet ds = new DataSet();
                DataAccessLayer.V_1_5.Groups grpObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                ds = grpObj.GetGrpCallDetails(ConferenceID, userId, out retVal, out retMsg);
                if (retVal == 0)
                {
                    responseObj = new JObject(new JProperty("Success", false),
                                                   new JProperty("Message", retMsg));
                }
                else
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataColumn dc in ds.Tables[0].Columns)
                            {
                                jGroup.Add(new JProperty(dc.ColumnName, ds.Tables[0].Rows[0][dc.ColumnName]));
                            }
                        }
                    }
                    if (ds.Tables.Count > 1)
                    {
                        if (ds.Tables[1].Rows.Count > 0)
                        {


                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {

                                jarMembers.Add(new JObject(new JProperty("Name", ds.Tables[1].Rows[i]["Name"]),
                                    new JProperty("MobileNumber", ds.Tables[1].Rows[i]["MobileNumber"]),
                                new JProperty("ListId", ds.Tables[1].Rows[i]["ListId"]),
                                new JProperty("ContactId", ds.Tables[1].Rows[i]["ContactId"])));

                            }
                        }
                        jGroup.Add(new JProperty("GrpParticipants", jarMembers));
                        jarGroup.Add(jGroup);
                    }


                    responseObj = new JObject(new JProperty("Success", true),
                                                  new JProperty("Message", retMsg),
                                                  new JProperty("Groups", jarGroup));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at GetGrpCallDetails BLL : " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseObj;
        }
        /// <summary>
        /// This Function is used to Create a GroupCall
        /// </summary>
        public JObject CreateGroupCall(string sConnString, JObject paramObj, int userId)
        {
            BusinessHelper helperObj = new BusinessHelper();
            helperObj.connString = sConnString;
            DataTable grpCallMembers = null;
            JObject responseObj = new JObject();
            JObject grpCallInfObj = new JObject();
            JArray jarrAllPrt = new JArray();
            JArray jarryDnd = new JArray();
            JObject upComingObj = new JObject();
            JArray jarrUpComing = new JArray();
            JArray historyJarr = new JArray();
            JArray webListJarr = new JArray();
            JObject weblistJobj = new JObject();
            DataTable MembersForThisGroup = new DataTable();
            DataSet grpCallInfo = new DataSet();
            Int16 retVal = 0;
            Int16 contactsRetVal = 0;
            string contactsRetMessage = "";
            DataSet contactsDS = new DataSet();
            int HostIsDnd;
            string retMessage = "";
            bool DndFlag = false;
            string MemberName = "";
            string MemberMobile = "";
            int ListId = 0;
            int errorCode = 0;
            int contactsErrorCode = 0;

            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);

            MembersForThisGroup.Columns.Add("Name", typeof(string));
            MembersForThisGroup.Columns.Add("Mobile", typeof(string));
            MembersForThisGroup.Columns.Add("ListID", typeof(int));
            MembersForThisGroup.Columns.Add("IsDndCheck", typeof(int));

            if (paramObj.SelectToken("WebListIds").ToString() != "")
            {
                try
                {
                    if (Convert.ToInt16(Convert.ToString(paramObj.SelectToken("WebListIds"))) != 0)
                    {
                        contactsDS = groupsObj.GetWebListContacts(userId, paramObj.SelectToken("WebListIds").ToString(), out contactsRetVal, out contactsRetMessage, out contactsErrorCode);
                        if (contactsRetVal == 1)
                        {
                            foreach (DataRow _row in contactsDS.Tables[0].Rows)
                            {
                                MembersForThisGroup.Rows.Add(_row["Name"].ToString(), _row["MobileNumber"].ToString(), Convert.ToInt32(_row["ListId"]), false);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Exception In Getting Web Contacts List : " + ex.ToString());
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "Error parsing participants");
                    helperObj.NewProperty("ErrorCode", "118");
                    HttpContext.Current.Response.Write(helperObj.GetResponse());
                    return helperObj.GetResponse();
                }
            }

            foreach (JObject _Member in (JArray)paramObj.SelectToken("Participants"))
            {
                foreach (JProperty _Token in _Member.Properties())
                {
                    if (_Token.Name == "IsDndFlag")
                    {
                        DndFlag = Convert.ToBoolean(_Token.Value.ToString());
                    }
                    else if (_Token.Name == "ListId")
                    {
                        ListId = Convert.ToInt32(_Token.Value);
                    }
                    else
                    {
                        MemberName = _Token.Name;
                        MemberMobile = _Token.Value.ToString();

                    }
                }

                MembersForThisGroup.Rows.Add(MemberName, MemberMobile, ListId, DndFlag);

            }


            grpCallMembers = helperObj.GetConferenceMebersNew(MembersForThisGroup, userId, "v1.1", out HostIsDnd);

            if (grpCallMembers == null)
            {
                helperObj.NewProperty("Success", false);

                helperObj.NewProperty("Message", "Error parsing participants");
                helperObj.NewProperty("ErrorCode", "118");
                HttpContext.Current.Response.Write(helperObj.GetResponse());
                return helperObj.GetResponse();
            }

            grpcreate createObj = new grpcreate();
            createObj.grpCallName = paramObj.SelectToken("GroupCallName").ToString();
            createObj.grpCallDate = paramObj.SelectToken("SchduledDate").ToString();
            createObj.grpCallTime = paramObj.SelectToken("SchduledTime").ToString();
            createObj.SchType = Convert.ToInt16(paramObj.SelectToken("SchType").ToString());
            createObj.DaysInWeek = paramObj.SelectToken("WeekDays").ToString();
            createObj.ReminderMins = Convert.ToInt32(paramObj.SelectToken("Reminder").ToString());
            createObj.IsMuteDial = Convert.ToInt16(paramObj.SelectToken("IsMuteDial").ToString());
            createObj.WebListIds = paramObj.SelectToken("WebListIds").ToString();
            createObj.IsOnlyDialIn = Convert.ToInt32(paramObj.SelectToken("IsOnlyDialIn"));
            createObj.IsAllowNonMembers = Convert.ToInt32(paramObj.SelectToken("IsAllowNonMembers"));
            createObj.OpenLineBefore = Convert.ToInt32(paramObj.SelectToken("OpenLineBefore"));
            createObj.managerMobile = Convert.ToString(paramObj.SelectToken("managerMobile"));
            createObj.EndDate = Convert.ToString(paramObj.SelectToken("EndDate"));
            createObj.EndTime = Convert.ToString(paramObj.SelectToken("EndTime"));


            try
            {
                grpCallInfo = groupsObj.CreateGrpCall(grpCallMembers, userId, createObj, HostIsDnd, out retVal, out retMessage, out errorCode);

                if (retVal == 1)
                {
                    var _with1 = grpCallInfo.Tables[1];
                    if (_with1.Rows.Count > 0)
                    {
                        for (int i = 0; i <= _with1.Rows.Count - 1; i++)
                        {
                            jarrAllPrt.Add(new JObject(new JProperty("Name", _with1.Rows[i]["member_name"]),
                                new JProperty("Mobile", _with1.Rows[i]["mobile_number"]),
                                new JProperty("IsSecondaryModerator", _with1.Rows[i]["IsSecondaryModerator"])));
                        }
                    }

                    var _with2 = grpCallInfo.Tables[2];

                    if (_with2.Rows.Count > 0)
                    {
                        for (int i = 0; i <= _with2.Rows.Count - 1; i++)
                        {
                            jarryDnd.Add(new JObject(new JProperty("Name", _with2.Rows[i]["member_name"]),
                                new JProperty("Mobile", _with2.Rows[i]["mobile_number"]),
                                new JProperty("IsDnd", _with2.Rows[i]["is_dnd"]),
                                new JProperty("IsOptedIn", _with2.Rows[i]["IsOptedIn"]),
                                new JProperty("IsOptinSent", _with2.Rows[i]["OptedInstructionsSent"])));
                        }
                    }

                    if (paramObj.SelectToken("WebListIds").ToString() != "")
                    {
                        foreach (DataRow _row in grpCallInfo.Tables[4].Rows)
                        {
                            weblistJobj = new JObject();
                            foreach (DataColumn _column in grpCallInfo.Tables[4].Columns)
                            {
                                weblistJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                            }
                            webListJarr.Add(weblistJobj);
                        }
                    }




                    if (grpCallInfo.Tables[3].Rows.Count > 0)
                    {
                        var _with3 = grpCallInfo.Tables[3];
                        upComingObj = new JObject(new JProperty("BatchID", ""),
                            new JProperty("StartTime", _with3.Rows[0]["StartTime"]),
                            new JProperty("Duration", _with3.Rows[0]["Duration"]),
                            new JProperty("CallPrice", _with3.Rows[0]["CallPrice"]),
                            new JProperty("Invites", _with3.Rows[0]["Invites"]),
                            new JProperty("TimeToGo", ""),
                            new JProperty("Connected", ""));

                        jarrUpComing.Add(upComingObj);
                    }

                    var _with4 = grpCallInfo.Tables[0];
                    if (_with4.Rows.Count > 0)
                    {
                        grpCallInfObj = new JObject(new JProperty("GroupID", _with4.Rows[0]["ConferenceId"]),
                                                     new JProperty("GroupName", createObj.grpCallName),
                                                     new JProperty("TotalMembers", _with4.Rows[0]["totalmembers"]),
                                                     new JProperty("SchduledDate", _with4.Rows[0]["StartDate"]),
                                                     new JProperty("SchduledTime", createObj.grpCallTime),
                                                     new JProperty("EndDate", Convert.ToString(_with4.Rows[0]["EndDate"])),
                                                     new JProperty("EndTime", Convert.ToString(_with4.Rows[0]["EndTime"])),
                                                     new JProperty("CreatedTime", DateTime.Now.ToString("MMM dd yyyy")),
                                                     new JProperty("IsStarted", "0"),
                                                     new JProperty("LastDate", ""),
                                                     new JProperty("LastGroupCall", "Jan  1 1900 12:00AM"),
                                                     new JProperty("StartDateTime", _with4.Rows[0]["ConferenceStartTime"]),
                                                     new JProperty("SchType", createObj.SchType),
                                                     new JProperty("IsMuteDial", createObj.IsMuteDial),
                                                     new JProperty("IsOnlyDialIn", createObj.IsOnlyDialIn),
                                                     new JProperty("IsAllowNonMembers", createObj.IsAllowNonMembers),
                                                     new JProperty("OpenLineBefore", createObj.OpenLineBefore),
                                                     new JProperty("IsSecondaryModerator", Convert.ToInt16(_with4.Rows[0]["IsSecondaryModerator"]).ToString()),
                                                     new JProperty("SecondaryModerNumber", ""),
                                                     new JProperty("SecondaryModeratorName", ""),
                                                     new JProperty("GroupCallPin", _with4.Rows[0]["conferencepin"]),
                                                     new JProperty("GrpCallRoom", _with4.Rows[0]["conferenceroom"]),
                                                     new JProperty("ParticipantNames", _with4.Rows[0]["participants"]),
                                                     new JProperty("WeekDays", createObj.DaysInWeek),
                                                     new JProperty("IsCreated", "1"),
                                                     new JProperty("CreatedBy", _with4.Rows[0]["CreatedBy"]),
                                                     new JProperty("CreatedByMobile", _with4.Rows[0]["CreatedByMobile"]),
                                                     new JProperty("History", historyJarr),
                                                     new JProperty("Upcoming", jarrUpComing),
                                                     new JProperty("Participants", jarrAllPrt),
                                                     new JProperty("WebLists", webListJarr));
                    }

                    responseObj = new JObject(new JProperty("Success", true),
                                             new JProperty("Message", "Ok"), new JProperty("ErrorCode", errorCode),
                                             new JProperty("GroupCallDetails", grpCallInfObj),
                                             new JProperty("DndContacts", jarryDnd));

                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                }

            }

            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in CreateGroupCallBussiness " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseObj;
        }


        /// <summary>
        /// This Function is used to Edit a GroupCall
        /// </summary>
        public JObject EditGroupCall(string sConnString, JObject paramObj, int userId, Int16 appOrWeb)
        {

            BusinessHelper helperObj = new BusinessHelper();
            helperObj.connString = sConnString;
            JObject Jobj = new JObject();
            JArray upComingJarr = new JArray();
            JObject upComingObj = new JObject();
            JArray historyJarr = new JArray();
            JObject historyJObj = new JObject();
            JArray jarrAllPrt = new JArray();
            JArray prtJarr = new JArray();
            JArray dndJarr = new JArray();
            JObject grpCallObj = new JObject();
            JObject grpCallInfObj = new JObject();
            DataTable membersForThisGroupCall = new DataTable();
            JObject responseObj = new JObject();
            DataTable grpCallMembers = null;
            DataSet grpCallInfo = new DataSet();
            Int16 retVal = 0;
            int HostIsDND;
            string RetMessage = "";
            bool DndFlag = false;
            string MemberName = "";
            string MemberMobile = "";
            JObject leaveParticipantsObj = new JObject();
            JArray leaveParticipantsJarr = new JArray();
            Int16 contactsRetVal = 0;
            string contactsRetMessage = "";
            DataSet contactsDS = new DataSet();
            JObject weblistJobj = new JObject();
            JArray webListJarr = new JArray();
            int contactsErrorCode = 0, errorCode = 0;
            int ListId = 0;
            string dupMsg = "";
            membersForThisGroupCall.Columns.Add("Name", typeof(string));
            membersForThisGroupCall.Columns.Add("Mobile", typeof(string));
            membersForThisGroupCall.Columns.Add("ListID", typeof(int));
            membersForThisGroupCall.Columns.Add("IsDndCheck", typeof(int));

            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);

            if (paramObj.SelectToken("WebListIds").ToString() != "" && paramObj.SelectToken("WebListIds").ToString() != "0")
            {

                try
                {
                    if (appOrWeb != 1 && Convert.ToInt16(Convert.ToString(paramObj.SelectToken("WebListIds"))) != 0)
                    {
                        contactsDS = groupsObj.GetWebListContacts(userId, paramObj.SelectToken("WebListIds").ToString(), out contactsRetVal, out contactsRetMessage, out contactsErrorCode);
                        if (contactsRetVal == 1)
                        {
                            foreach (DataRow _row in contactsDS.Tables[0].Rows)
                            {
                                membersForThisGroupCall.Rows.Add(_row["Name"].ToString(), _row["MobileNumber"].ToString(), Convert.ToInt32(_row["ListId"]), false);
                            }
                        }
                    }



                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Exception In Getting Web COntacts List : " + ex.ToString());
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "Error parsing participants");
                    helperObj.NewProperty("ErrorCode", "118");
                    HttpContext.Current.Response.Write(helperObj.GetResponse());
                    return helperObj.GetResponse();
                }
            }

            foreach (JObject _Member in (JArray)paramObj.SelectToken("Participants"))
            {
                foreach (JProperty _Token in _Member.Properties())
                {
                    if (_Token.Name == "IsDndFlag")
                    {
                        DndFlag = Convert.ToBoolean(_Token.Value.ToString());
                    }
                    else if (_Token.Name == "ListId")
                    {
                        ListId = Convert.ToInt32(_Token.Value);
                    }
                    else
                    {
                        MemberName = _Token.Name;
                        MemberMobile = _Token.Value.ToString();
                    }
                }

                membersForThisGroupCall.Rows.Add(MemberName, MemberMobile, ListId, DndFlag);
            }


            //foreach (JObject _member in (JArray)paramObj.SelectToken("Participants"))
            //{
            //    foreach (JProperty _Token in _member.Properties())
            //    {
            //        if (_Token.Name == "IsDndFlag")
            //        {
            //            DndFlag = Convert.ToBoolean(_Token.Value.ToString());
            //        }
            //        else
            //        {
            //            MemberName = _Token.Name;
            //            MemberMobile = _Token.Value.ToString();
            //        }
            //    }

            //    membersForThisGroupCall.Rows.Add(MemberName, MemberMobile, DndFlag);
            //}
            grpCallMembers = helperObj.GetConferenceMebersNew(membersForThisGroupCall, userId, "v1.0.1", out HostIsDND);

            if (grpCallMembers == null)
            {
                helperObj.NewProperty("Success", false);

                helperObj.NewProperty("Message", "Error parsing participants");
                helperObj.NewProperty("ErrorCode", "118");
                HttpContext.Current.Response.Write(helperObj.GetResponse());
                return helperObj.GetResponse();
            }
            grpEdit editObj = new grpEdit();
            editObj.grpcallID = Convert.ToInt32(paramObj.SelectToken("GroupID").ToString());
            editObj.grpCallName = paramObj.SelectToken("GroupCallName").ToString();
            editObj.grpCallDate = paramObj.SelectToken("SchduledDate").ToString();
            editObj.grpCallTime = paramObj.SelectToken("SchduledTime").ToString();
            editObj.SchType = Convert.ToInt16(paramObj.SelectToken("SchType").ToString());
            editObj.DaysInWeek = paramObj.SelectToken("WeekDays").ToString();
            editObj.ReminderMins = Convert.ToInt32(paramObj.SelectToken("Reminder").ToString());
            editObj.IsMuteDial = Convert.ToInt16(paramObj.SelectToken("IsMuteDial").ToString());
            if (appOrWeb != 1)
                editObj.WebListIds = paramObj.SelectToken("WebListIds").ToString();
            else
                editObj.WebListIds = "";
            editObj.IsOnlyDialIn = Convert.ToInt32(paramObj.SelectToken("IsOnlyDialIn"));
            editObj.IsAllowNonMembers = Convert.ToInt32(paramObj.SelectToken("IsAllowNonMembers"));
            editObj.OpenLineBefore = Convert.ToInt32(paramObj.SelectToken("OpenLineBefore"));
            editObj.managerMobile = Convert.ToString(paramObj.SelectToken("managerMobile"));
            editObj.EndDate = Convert.ToString(paramObj.SelectToken("EndDate"));
            editObj.EndTime = Convert.ToString(paramObj.SelectToken("EndTime"));

            try
            {
                grpCallInfo = groupsObj.EditGroupCall(grpCallMembers, userId, editObj, appOrWeb, out retVal, out RetMessage, out errorCode);

                if (retVal == 1)
                {
                    if (paramObj.SelectToken("WebListIds").ToString() != "" || appOrWeb == 1)
                    {
                        if (grpCallInfo.Tables.Count == 7)
                        {
                            if (grpCallInfo.Tables[6].Rows.Count > 0)
                            {
                                foreach (DataRow _row in grpCallInfo.Tables[6].Rows)
                                {
                                    weblistJobj = new JObject();
                                    foreach (DataColumn _column in grpCallInfo.Tables[6].Columns)
                                    {
                                        weblistJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                                    }
                                    webListJarr.Add(weblistJobj);
                                }
                            }
                        }
                    }


                    var _with1 = grpCallInfo.Tables[1];

                    if (_with1.Rows.Count > 0)
                    {
                        for (int i = 0; i <= _with1.Rows.Count - 1; i++)
                        {
                            prtJarr.Add(new JObject(new JProperty("Name", _with1.Rows[i]["member_name"]),
                                new JProperty("Mobile", _with1.Rows[i]["mobile_number"]),
                                new JProperty("IsSecondaryModerator", _with1.Rows[i]["IsSecondaryModerator"])));
                        }
                    }

                    var _with2 = grpCallInfo.Tables[2];

                    if (_with2.Rows.Count > 0)
                    {

                        for (int i = 0; i <= _with2.Rows.Count - 1; i++)
                        {
                            dndJarr.Add(new JObject(new JProperty("Name", _with2.Rows[i]["member_name"]),
                                new JProperty("Mobile", _with2.Rows[i]["mobile_number"]),
                                new JProperty("IsDnd", _with2.Rows[i]["is_dnd"]),
                                new JProperty("IsOptedIn", _with2.Rows[i]["IsOptedIn"]),
                                new JProperty("IsOptinSent", _with2.Rows[i]["OptedInstructionsSent"])));
                        }

                    }


                    var _with3 = grpCallInfo.Tables[3];
                    if (grpCallInfo.Tables[3].Rows.Count > 0)
                    {
                        upComingObj = new JObject(new JProperty("BatchID", ""),
                            new JProperty("StartTime", _with3.Rows[0]["StartTime"]),
                            new JProperty("Duration", _with3.Rows[0]["Duration"]),
                            new JProperty("CallPrice", _with3.Rows[0]["CallPrice"]),
                            new JProperty("Invites", _with3.Rows[0]["Invites"]),
                            new JProperty("TimeToGo", _with3.Rows[0]["TimeToGo"]),
                            new JProperty("Connected", ""));

                        upComingJarr.Add(upComingObj);
                    }

                    var _with4 = grpCallInfo.Tables[4];
                    if (grpCallInfo.Tables[4].Rows.Count > 0)
                    {
                        for (int j = 0; j <= _with4.Rows.Count - 1; j++)
                        {
                            historyJObj = new JObject(new JProperty("BatchID", _with4.Rows[j]["BatchID"]),
                                new JProperty("StartTime", _with4.Rows[j]["StartTime"]),
                                new JProperty("Duration", _with4.Rows[j]["Duration"]),
                                new JProperty("CallPrice", _with4.Rows[j]["CallPrice"]),
                                new JProperty("Invites", _with4.Rows[j]["Invites"]),
                                new JProperty("TimeToGo", _with4.Rows[j]["TimeToGo"]),
                                new JProperty("Connected", _with4.Rows[j]["Connected"]));

                            historyJarr.Add(historyJObj);
                        }


                    }
                    if (grpCallInfo.Tables[5].Rows.Count > 0)
                    {
                        for (int j = 0; j <= grpCallInfo.Tables[5].Rows.Count - 1; j++)
                        {
                            leaveParticipantsObj = new JObject(new JProperty("Name", grpCallInfo.Tables[5].Rows[j]["member_name"]),
                                       new JProperty("Mobile", grpCallInfo.Tables[5].Rows[j]["mobile_number"]), new JProperty("IsSecondaryModerator", 0), new JProperty("LeftOn", grpCallInfo.Tables[5].Rows[j]["LeftOn"]));
                            leaveParticipantsJarr.Add(leaveParticipantsObj);
                        }

                    }
                    var _with5 = grpCallInfo.Tables[0];

                    if (_with5.Rows.Count > 0)
                    {
                        dupMsg = Convert.ToString(_with5.Rows[0]["DuplicateMsg"]);
                        string _Cdate1 = editObj.grpCallDate.Replace("-", "/");
                        grpCallObj = new JObject(new JProperty("GroupID", _with5.Rows[0]["ConferenceId"]),
                                                new JProperty("GroupName", editObj.grpCallName),
                                                new JProperty("TotalMembers", _with5.Rows[0]["totalmembers"]),
                                                new JProperty("SchduledDate", _with5.Rows[0]["StartDate"].ToString()),
                                                new JProperty("SchduledTime", editObj.grpCallTime),
                                                new JProperty("EndDate", _with5.Rows[0]["EndDate"].ToString()),
                                                new JProperty("EndTime", _with5.Rows[0]["EndTime"].ToString()),
                                                new JProperty("CreatedTime", DateTime.Now.ToString("MMM dd yyyy")),
                                                new JProperty("IsStarted", "0"),
                                                new JProperty("LastDate", ""),
                                                new JProperty("LastGroupCall", "Jan  1 1900 12:00AM"),
                                                new JProperty("StartDateTime", _with5.Rows[0]["ConferenceStartTime"]),
                                                new JProperty("SchType", editObj.SchType),
                                                new JProperty("IsMuteDial", editObj.IsMuteDial),
                                                new JProperty("IsOnlyDialIn", editObj.IsOnlyDialIn),
                                                new JProperty("IsAllowNonMembers", editObj.IsAllowNonMembers),
                                                new JProperty("IsSecondaryModerator", Convert.ToInt16(_with5.Rows[0]["IsSecondaryModerator"]).ToString()),
                                                new JProperty("SecondaryModerNumber", _with5.Rows[0]["SecondaryModerNumber"]),
                                                new JProperty("SecondaryModeratorName", _with5.Rows[0]["SecondaryModeratorName"]),
                                                new JProperty("OpenLineBefore", editObj.OpenLineBefore),
                                                new JProperty("GroupCallPin", _with5.Rows[0]["conferencepin"]),
                                                new JProperty("GrpCallRoom", _with5.Rows[0]["conferenceroom"]),
                                                new JProperty("ParticipantNames", _with5.Rows[0]["participants"]),
                                                new JProperty("WeekDays", editObj.DaysInWeek),
                                                new JProperty("IsCreated", "1"),
                                                new JProperty("CreatedBy", _with5.Rows[0]["CreatedBy"]),
                                                new JProperty("CreatedByMobile", _with5.Rows[0]["CreatedByMobile"]),
                                                new JProperty("History", historyJarr),
                                                new JProperty("Upcoming", upComingJarr),
                                                new JProperty("Participants", prtJarr),
                                                new JProperty("WebLists", webListJarr),
                                                new JProperty("LeaveParticipants", leaveParticipantsJarr));
                    }

                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "Ok"), new JProperty("ErrorCode", errorCode),
                        new JProperty("DuplicateMessage", dupMsg),
                        new JProperty("GroupCallDetails", grpCallObj),
                        new JProperty("DndContacts", dndJarr));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", RetMessage), new JProperty("ErrorCode", errorCode));
                }

            }

            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in EditGroupCallBussiness " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseObj;
        }

        /// <summary>
        /// This Function is Used to Get AllGrpCalls
        /// </summary>
        public JObject GetAllGroupMembers(string sConnString, int GroupID)
        {

            JObject responseObj = new JObject();
            DataSet resultDS = new DataSet();
            Int16 retVal;
            string retMessage;
            GT.DataAccessLayer.V_1_5.Groups groupsObj = new GT.DataAccessLayer.V_1_5.Groups(sConnString);
            try
            {
                resultDS = groupsObj.GetAllGroupMembers(GroupID, out retVal, out retMessage);
                if (retVal == 1)
                {
                    responseObj = ReturnGrpCallMembers(resultDS);
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", retMessage));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in EditGroupCallBussiness " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }

            return responseObj;
        }


        public JObject GetAllGroupCalls(string sConnString, int userId, int AppSource, string DeviceToken, string AppVersion, string TimeStamp, int countryID)
        {
            Logger.TraceLog("BLL Started");
            DataSet allGrpCalls = new DataSet();
            JObject allGrpCallsObj = new JObject();
            JObject leaveParticipantsObj = new JObject();
            JArray allGrpCallsJarr = new JArray();
            JArray membersJarr = new JArray();
            JObject MemObj = new JObject();
            JArray historyJarr = new JArray();
            JArray deleteJarr = new JArray();
            JArray upcomingJarr = new JArray();
            JArray leaveParticipantsJarr = new JArray();
            JObject listsJobj = new JObject();
            JArray listsJarr = new JArray();
            int retVal = 0, errorCode = 0;
            int count = 0;
            string retMessage = "";
            int isMuteDial = 0;
            double userBal;
            string profileImagePath = "";
            string outTimeStamp = "";
            string userCurrentDate = "";
            string userRegTimeStamp = "";
            int bonusDuration = 0;
            string bonusexpiryTime = "";
            int maxConcurrency = 0;
            bool isBonusAvailable;
            string CurrentAppVersion = "";
            string minimumBalance = "";
            int membersConcurrency = 0;


            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);

                allGrpCalls = groupsObj.GetAllGroupCalls(userId, AppSource, DeviceToken, AppVersion, TimeStamp, out outTimeStamp, out count, out userBal, out profileImagePath, out userCurrentDate, out userRegTimeStamp, out retVal, out retMessage, out errorCode);

                if (retVal == 1)
                {
                    if (countryID == 19)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["BahrainMinimumBalance"].ToString();
                    }
                    else if (countryID == 239)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["UAEMinimumBalance"].ToString();
                    }
                    else if (countryID == 241)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["UsaMinimumbalance"].ToString();
                    }
                    else if (countryID == 199)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["SaudiMinimumbalance"].ToString();
                    }
                    else if (countryID == 173)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["OmanMinimumbalance"].ToString();
                    }
                    else if (countryID == 188)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["QatarMinimumbalance"].ToString();
                    }
                    else if (countryID == 124)
                    {
                        minimumBalance = ConfigurationManager.AppSettings["KuwaitMinimumbalance"].ToString();
                    }
                    else
                    {
                        minimumBalance = ConfigurationManager.AppSettings["IndiaMinimumBalance"].ToString();
                    }
                    if (allGrpCalls.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i <= allGrpCalls.Tables[0].Rows.Count - 1; i++)
                        {
                            //membersJarr = new JArray();
                            //MemObj = new JObject();
                            //DataRow[] result = allGrpCalls.Tables[1].Select("ConfID =" + allGrpCalls.Tables[0].Rows[i]["ConferenceId"] + " ");
                            //foreach (DataRow row in result)
                            //{
                            //    MemObj = new JObject(new JProperty("Name", row["Name"]),
                            //        new JProperty("GroupId", row["ConfID"]),
                            //            new JProperty("Mobile", row["Mobile"]));
                            //    membersJarr.Add(MemObj);
                            //}
                            //leaveParticipantsJarr = new JArray();
                            //leaveParticipantsObj = new JObject();
                            //DataRow[] leave = allGrpCalls.Tables[4].Select("conf_id=" + allGrpCalls.Tables[0].Rows[i]["ConferenceId"] + " ");
                            //foreach (DataRow leaverow in leave)
                            //{
                            //    leaveParticipantsObj = new JObject(new JProperty("Name", leaverow["member_name"]),
                            //            new JProperty("Mobile", leaverow["mobile_number"]));
                            //    leaveParticipantsJarr.Add(leaveParticipantsObj);
                            //}
                            //listsJobj = new JObject();
                            //listsJarr = new JArray();
                            //if (AppSource != 3)
                            //{
                            //    DataRow[] lists = allGrpCalls.Tables[5].Select("ConfID =" + allGrpCalls.Tables[0].Rows[i]["ConferenceId"] + " ");
                            //    foreach (DataRow list in lists)
                            //    {
                            //        listsJobj = new JObject(new JProperty("ListId", list["ListId"]),
                            //                new JProperty("ListName", list["ListName"]),
                            //                new JProperty("MembersCount", list["MembersCount"]));
                            //        listsJarr.Add(listsJobj);
                            //    }
                            //}

                            var _with3 = allGrpCalls.Tables[0];

                            if (Convert.ToBoolean(_with3.Rows[i]["IsmuteDial"]) == true)
                            {
                                isMuteDial = 1;
                            }
                            else if (Convert.ToBoolean(_with3.Rows[i]["IsmuteDial"]) == false)
                            {
                                isMuteDial = 0;
                            }
                            else
                            {
                                isMuteDial = 0;
                            }

                            allGrpCallsJarr.Add(new JObject(new JProperty("GroupID", _with3.Rows[i]["GroupId"].ToString()),
                                               new JProperty("GroupName", _with3.Rows[i]["GroupName"].ToString()),
                                               new JProperty("TotalMembers", _with3.Rows[i]["TotalMembers"].ToString()),
                                               new JProperty("CreatedTime", _with3.Rows[i]["CreatedTime"].ToString()),
                                               new JProperty("LastGroupCall", _with3.Rows[i]["LastGroupCall"].ToString()),
                                               new JProperty("StartDateTime", _with3.Rows[i]["StartDateTime"].ToString()),
                                               new JProperty("EndDate", _with3.Rows[i]["EndDate"].ToString()),
                                               new JProperty("EndTime", _with3.Rows[i]["EndTime"].ToString()),
                                               new JProperty("IsStarted", _with3.Rows[i]["IsStarted"].ToString()),
                                               new JProperty("SchType", _with3.Rows[i]["SchType"].ToString()),
                                               new JProperty("IsMuteDial", isMuteDial),
                                               new JProperty("IsOnlyDialIn", Convert.ToInt16(_with3.Rows[i]["IsOnlyDialIn"])),
                                               new JProperty("OpenLineBefore", Convert.ToInt16(_with3.Rows[i]["OpenLineBefore"].ToString())),
                                               new JProperty("IsAllowNonMembers", Convert.ToInt16(_with3.Rows[i]["IsAllowNonMembers"].ToString())),
                                               new JProperty("GroupCallPin", _with3.Rows[i]["GroupCallPin"].ToString()),
                                               new JProperty("GrpCallRoom", _with3.Rows[i]["GrpCallRoom"].ToString()),
                                               new JProperty("WeekDays", _with3.Rows[i]["WeekDays"].ToString()),
                                               new JProperty("IsCreated", _with3.Rows[i]["IsCreated"].ToString()),
                                               new JProperty("IsSecondaryModerator", _with3.Rows[i]["IsSecondaryModerator"].ToString()),
                                               new JProperty("CreatedBy", _with3.Rows[i]["CreatedBy"].ToString()),
                                               new JProperty("CreatedByMobile", _with3.Rows[i]["CreatedByMobile"].ToString())));
                        }

                    }
                    deleteJarr = new JArray();

                    if (allGrpCalls.Tables[2].Rows.Count > 0)
                    {

                        foreach (DataRow _row in allGrpCalls.Tables[2].Rows)
                        {
                            foreach (DataColumn _column in allGrpCalls.Tables[2].Columns)
                            {
                                deleteJarr.Add(_row["conf_id"]);
                            }

                        }
                    }
                    if (AppSource == 1)
                    {
                        CurrentAppVersion = System.Configuration.ConfigurationManager.AppSettings["IOSCurrentAppVersion"].ToString();
                        if (AppVersion == "1.3")
                        {
                            membersConcurrency = 3;
                        }
                        else
                        {
                            membersConcurrency = Convert.ToInt32(ConfigurationManager.AppSettings["IOSMembersConcurrency"]);
                        }

                    }
                    else
                    {
                        CurrentAppVersion = System.Configuration.ConfigurationManager.AppSettings["AndroidCurrentAppVersion"].ToString();
                        membersConcurrency = Convert.ToInt32(ConfigurationManager.AppSettings["AndroidMembersConcurrency"]);
                    }
                    bonusDuration = Convert.ToInt32(allGrpCalls.Tables[1].Rows[0]["BonusDuration"]);
                    bonusexpiryTime = allGrpCalls.Tables[1].Rows[0]["BonusExpirationTime"].ToString();
                    maxConcurrency = Convert.ToInt32(allGrpCalls.Tables[1].Rows[0]["MaxConcurrency"]);
                    if (Convert.ToInt32(allGrpCalls.Tables[1].Rows[0]["CountryID"]) == 108)
                    {
                        if (Convert.ToInt32(allGrpCalls.Tables[1].Rows[0]["IsPremiumUser"]) == 1)
                            isBonusAvailable = false;
                        else
                            isBonusAvailable = true;

                    }
                    else
                    {
                        isBonusAvailable = false;
                    }
                    allGrpCallsObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "Success"), new JProperty("ErrorCode", errorCode),
                           new JProperty("TimeStamp", outTimeStamp),
                                        new JProperty("Count", count),
                                        new JProperty("DNDNumber", System.Configuration.ConfigurationManager.AppSettings["DNDNumber"].ToString()),
                                        new JProperty("UserBal", userBal),
                                        new JProperty("ProfileImagePath", System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString() + profileImagePath),
                                        new JProperty("UserCurrentDate", userCurrentDate),
                                        new JProperty("UserRegisteredDate", userRegTimeStamp),
                                        new JProperty("ServiceProviderNumber", System.Configuration.ConfigurationManager.AppSettings["ServiceProviderNumber"].ToString()),
                                        new JProperty("BonusDuration", bonusDuration),
                                        new JProperty("BonusExpiryTime", bonusexpiryTime),
                                        new JProperty("MaxConcurrency", maxConcurrency),
                                        new JProperty("IsBonusAvailable", Convert.ToBoolean(allGrpCalls.Tables[1].Rows[0]["IsBonusAvailable"])),
                                        new JProperty("TimeZone", Convert.ToString(allGrpCalls.Tables[1].Rows[0]["TimeZone"])),
                                        new JProperty("MinimumBalance", minimumBalance),
                                        new JProperty("CurrentAppVersion", "2.0"),
                                        new JProperty("IsForcibleUpdate", "0"),
                                        new JProperty("IsTZApply", System.Configuration.ConfigurationManager.AppSettings["IsTZApply"].ToString()),
                                        new JProperty("IosAppVersion", System.Configuration.ConfigurationManager.AppSettings["IOSCurrentAppVersion"].ToString()),
                                        new JProperty("AndroidAppVersion", System.Configuration.ConfigurationManager.AppSettings["AndroidCurrentAppVersion"].ToString()),
                                        new JProperty("OpenLineBeforeMinutes", "30"),
                                        new JProperty("Currency", allGrpCalls.Tables[1].Rows[0]["CurrencyName"].ToString()),
                                        new JProperty("RequestedAmount", allGrpCalls.Tables[1].Rows[0]["PaidAmount"].ToString()),
                                        new JProperty("RequestedMinutes", allGrpCalls.Tables[1].Rows[0]["AddedAmount"].ToString()),
                                        new JProperty("WebAccess", allGrpCalls.Tables[1].Rows[0]["WebAccess"].ToString()),
                                        new JProperty("UserName", allGrpCalls.Tables[1].Rows[0]["UserName"].ToString()),
                                        new JProperty("Email", allGrpCalls.Tables[1].Rows[0]["Email"].ToString()),
                                        new JProperty("IsPremiumUser", allGrpCalls.Tables[1].Rows[0]["IsPremiumUser"].ToString()),
                                        new JProperty("BonusExpiryIndays", allGrpCalls.Tables[1].Rows[0]["BonusExpiryIndays"].ToString()),
                                        new JProperty("InAppPurchase", true),
                                        new JProperty("SupportEmailID", "hello@grptalk.com"),
                                        new JProperty("MembersConcurrency", membersConcurrency),
                                        new JProperty("BonusMinutes", bonusDuration),
                                        new JProperty("MinimumScheduleMinutes", System.Configuration.ConfigurationManager.AppSettings["ScheduleMins"].ToString()),
                                        new JProperty("Groups", allGrpCallsJarr),
                                        new JProperty("DeletedArray", deleteJarr),
                                        new JProperty("AdvertisingBanner", ""), new JProperty("SupportNumber", System.Configuration.ConfigurationManager.AppSettings["SupportNumber"].ToString()));

                    //new JProperty("AdvertisingBanner", System.Configuration.ConfigurationManager.AppSettings["WebUrl"].ToString() + "images/didyouknow.png"));

                }
                else
                {
                    allGrpCallsObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));

                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetAllGroupCallsBussiness" + ex.ToString());
                allGrpCallsObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return allGrpCallsObj;
        }

        /// <summary>
        /// This Function is Used to Delete a GroupCall
        /// </summary>
        public JObject DeleteGroupCall(string sConnString, int groupCallID)
        {
            JObject responseObj = new JObject();
            int retVal = 0, errorCode = 0;
            string retMsg = "";
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                retMsg = groupsObj.DeleteGroupCall(groupCallID, out retVal, out errorCode);
                if (retVal == 1)
                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", errorCode),
                        new JProperty("ConferenceID", groupCallID));
                else
                    responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", errorCode));

            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Server Internal Error"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("DeleteGroupCall---" + ex.ToString());
            }
            return responseObj;

        }

        /// <summary>
        /// This Function is Used to Update a GroupCall
        /// </summary>
        public JObject UpdateGroupCallName(string sConnString, int userId, int groupCallID, string groupCallName)
        {
            JObject responseObj = new JObject();
            int RetVal = 0;
            string RetMessage = "";
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                RetMessage = groupsObj.UpdateGroupCallName(userId, groupCallID, groupCallName, out RetVal);
                if (RetVal == 1)
                    responseObj = new JObject(new JProperty("Status", true),
                        new JProperty("Message", RetMessage), new JProperty("ErrorCode", "117"));
                else
                    responseObj = new JObject(new JProperty("Status", false),
                        new JProperty("Message", RetMessage), new JProperty("ErrorCode", "122"));

            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Status", false),
                    new JProperty("Message", "Server Internal Error"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("GroupMasterBussiness_---" + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This Function is Used to Get GroupCall Details By GroupID
        /// </summary>
        public JObject GetGroupCallDetailsByGoupID(string sConnString, int userId, int groupCallID)
        {


            JArray jArr = new JArray();
            JArray jArr1 = new JArray();
            JArray participantsJarr = new JArray();
            JArray jArr3 = new JArray();
            JObject reportsObj = new JObject();
            int isMuteDial = 0;
            string startTime = "";
            string duration = "";
            string timeToGo = "";

            JObject responseObj = new JObject();
            DataSet reportsDs = new DataSet();
            int retVal = 0;
            string retMsg = "";
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                reportsDs = groupsObj.GetGroupCallDetailsByGroupCallID(userId, groupCallID, out retVal, out retMsg);

                if (retVal == 1)
                {
                    if (reportsDs.Tables[0].Rows.Count > 0)
                    {
                        var _with1 = reportsDs.Tables[0];
                        for (int i = 0; i <= _with1.Rows.Count - 1; i++)
                        {
                            participantsJarr = new JArray();
                            if ((_with1.Rows[i]["participants"] != DBNull.Value))
                            {

                                foreach (string item in _with1.Rows[i]["participants"].ToString().Split(','))
                                {
                                    participantsJarr.Add(item);
                                }


                            }
                            if (Convert.ToBoolean(_with1.Rows[i]["ismutedial"]) == true)
                            {
                                isMuteDial = 1;
                            }
                            else if (Convert.ToBoolean(_with1.Rows[i]["ismutedial"]) == false)
                            {
                                isMuteDial = 0;
                            }
                            else
                            {
                                isMuteDial = 0;
                            }
                            if (Convert.ToBoolean(string.IsNullOrEmpty(_with1.Rows[i]["duration"].ToString())) == true)
                            {
                                duration = "0";
                            }
                            else
                            {
                                duration = _with1.Rows[i]["duration"].ToString();
                            }
                            if (_with1.Rows[i]["starttime"] == null)
                            {
                                timeToGo = "0";
                                startTime = "0";
                            }
                            else
                            {
                                timeToGo = getTimeString(_with1.Rows[i]["starttime"].ToString());
                                startTime = _with1.Rows[i]["confdonetime"].ToString().Substring(0, 11);

                            }
                            reportsObj = (new JObject(new JProperty("conferenceid", _with1.Rows[i]["conferenceid"].ToString()),
                                                new JProperty("conferencename", _with1.Rows[i]["conferencename"].ToString()),
                                                new JProperty("pin", _with1.Rows[i]["pin"].ToString()),
                                                new JProperty("totalmembers", _with1.Rows[i]["totalmembers"].ToString()),
                                                new JProperty("moderatorname", _with1.Rows[i]["moderator"].ToString()),
                                                new JProperty("moderatormobile", _with1.Rows[i]["moderatormobile"].ToString()),
                                                new JProperty("createdtime", _with1.Rows[i]["created"].ToString()),
                                                new JProperty("batchid", _with1.Rows[i]["batchid"].ToString()),
                                                new JProperty("lastconference", _with1.Rows[i]["confdonetime"].ToString()),
                                                new JProperty("startdatetime", _with1.Rows[i]["startdatetime"].ToString()),
                                                new JProperty("schduleddate", _with1.Rows[i]["startdate"].ToString()),
                                                new JProperty("schduledtime", _with1.Rows[i]["start_time"].ToString()),
                                                new JProperty("IsStarted", _with1.Rows[i]["InProgress"].ToString()),
                                                new JProperty("timetogo", timeToGo), new JProperty("lastdate", startTime),
                                                new JProperty("schtype", _with1.Rows[i]["schtype"].ToString()),
                                                new JProperty("callerid", _with1.Rows[i]["callerid"].ToString()),
                                                new JProperty("ismutedial", isMuteDial),
                                                new JProperty("conferenceroom", _with1.Rows[i]["conf_room"].ToString()),
                                                new JProperty("confstatus", "update"),
                                                new JProperty("duration", duration),
                                                new JProperty("participantnames", _with1.Rows[i]["names"].ToString()),
                                                new JProperty("recursdays", _with1.Rows[i]["recursdays"].ToString()),
                                                new JProperty("weekdays", _with1.Rows[i]["weekdays"].ToString()),
                                                new JProperty("monthweek", _with1.Rows[i]["monthweek"].ToString()),
                                                new JProperty("monthday", _with1.Rows[i]["monthday"].ToString()),
                                                new JProperty("timezone", _with1.Rows[i]["timezone"].ToString()),
                                                new JProperty("reminder", _with1.Rows[i]["reminder"].ToString()),
                                                new JProperty("participants", participantsJarr)));

                        }
                    }


                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "success"),
                        new JProperty("Conference", reportsObj));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "failed"),
                        new JProperty("ErrorCode", "E0002"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetAllGroupCallDetailsByGroupIDBussiness---" + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "ServerError"));
            }
            return responseObj;
        }

        /// <summary>
        /// This Function is Used to Get GroupCall Room
        /// </summary>
        public JObject GetGroupCallRoomBackUp(string sConnString, int confID, int userID)
        {

            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
            DataSet callRoomDs = new DataSet();
            JObject responseJObj = new JObject();
            JObject jObj1 = new JObject();
            JObject jObj2 = new JObject();
            JArray grpCallRoomJarr = new JArray();
            JObject grpCallRoomJObj = new JObject();
            Int16 retVal = 0, iterator = 0;
            int errorCode = 0;
            string retMsg = "", startTime = "", serverTime = "";
            byte conferenceState = 0;
            try
            {



                callRoomDs = groupsObj.GetConferenceRoomBackUp(confID, userID, out retVal, out retMsg, out serverTime, out startTime, out errorCode, out conferenceState);


                if (retVal == 1)
                {
                    if (callRoomDs.Tables.Count > 0 && callRoomDs.Tables[0].Rows.Count > 0)
                    {
                        //for (iterator = 0; iterator <= callRoomDs.Tables[0].Rows.Count - 1; iterator++)
                        //{
                        //    grpCallRoomJarr.Add(new JObject(new JProperty("member", callRoomDs.Tables[0].Rows[iterator]["name"].ToString()),
                        //                         new JProperty("to_num", callRoomDs.Tables[0].Rows[iterator]["mobile"].ToString()),
                        //                         new JProperty("direction", callRoomDs.Tables[0].Rows[iterator]["direction"].ToString()),
                        //                         new JProperty("type", callRoomDs.Tables[0].Rows[iterator]["type"].ToString()),
                        //                         new JProperty("mute", callRoomDs.Tables[0].Rows[iterator]["mute"].ToString()),
                        //                         new JProperty("deaf", callRoomDs.Tables[0].Rows[iterator]["deaf"].ToString()),
                        //                         new JProperty("handraise", callRoomDs.Tables[0].Rows[iterator]["handraise"].ToString()),
                        //                         new JProperty("isprivate", callRoomDs.Tables[0].Rows[iterator]["isprivate"].ToString()),
                        //                         new JProperty("IsMember", callRoomDs.Tables[0].Rows[iterator]["IsMember"].ToString()),
                        //                         new JProperty("call_status", callRoomDs.Tables[0].Rows[iterator]["callstatus"].ToString()),
                        //                         new JProperty("MemberJoinTime", callRoomDs.Tables[0].Rows[iterator]["MemberJoinTime"].ToString()),
                        //                           new JProperty("conf_digits", callRoomDs.Tables[0].Rows[iterator]["Conf_Digits"].ToString())
                        //                         , new JProperty("IsSecondaryModerator", callRoomDs.Tables[0].Rows[iterator]["IsSecondaryModerator"])));
                        //}

                        foreach (DataRow dr in callRoomDs.Tables[0].Rows)
                        {
                            grpCallRoomJObj = new JObject();

                            foreach (DataColumn dc in callRoomDs.Tables[0].Columns)
                            {

                                if (dc.ColumnName != "MemberJoinTime")
                                {
                                    grpCallRoomJObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                                }
                                else
                                {
                                    grpCallRoomJObj.Add(new JProperty(dc.ColumnName,Convert.ToString(dr[dc.ColumnName])));
                                }

                            }

                            grpCallRoomJarr.Add(grpCallRoomJObj);
                        }
                        jObj1 = new JObject(new JProperty("status", "true"), new JProperty("msg", retMsg), new JProperty("ErrorCode", errorCode), new JProperty("ConferenceState", conferenceState));

                        jObj2 = new JObject(new JProperty("data", grpCallRoomJarr), new JProperty("servertime", serverTime), new JProperty("starttime", startTime));

                        responseJObj = new JObject(new JProperty("response", jObj1), new JProperty("result", jObj2));

                    }
                    else
                    {
                        responseJObj = new JObject(new JProperty("status", "false"), new JProperty("msg", "no data"), new JProperty("ErrorCode", "128"));
                    }

                }
                else
                {
                    responseJObj = new JObject(new JProperty("status", "false"), new JProperty("msg", retMsg), new JProperty("ErrorCode", errorCode));
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupMasterBusiness.GetGroupCallRoom is ==>" + ex.ToString());
                responseJObj = new JObject(new JProperty("status", "false"),
                    new JProperty("msg", "Something Went Wrong"), new JProperty("ErrorCode", "101"));


            }

            return responseJObj;

        }


        public JObject GetGroupCallRoom(string sConnString, int confID, int type, int pageSize, int pageNumber, int userID, string searchText)
        {

            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
            DataSet callRoomDs = new DataSet();
            JObject responseJObj = new JObject();
            JObject jObj1 = new JObject();
            JObject jObj2 = new JObject();
            JArray grpCallRoomJarr = new JArray();
            Int16 retVal = 0, iterator = 0;
            string retMsg = "", startTime = "", serverTime = "";
            int MembersCount = 0, errorCode = 0;
            try
            {
                callRoomDs = groupsObj.GetConferenceRoom(confID, userID, type, pageSize, pageNumber, searchText, out retVal, out retMsg, out serverTime, out startTime, out MembersCount, out errorCode);
                if (retVal == 1)
                {
                    if (callRoomDs.Tables.Count > 0 && callRoomDs.Tables[0].Rows.Count > 0)
                    {
                        for (iterator = 0; iterator <= callRoomDs.Tables[0].Rows.Count - 1; iterator++)
                        {
                            grpCallRoomJarr.Add(new JObject(new JProperty("member", callRoomDs.Tables[0].Rows[iterator]["name"].ToString()),
                                                 new JProperty("to_num", callRoomDs.Tables[0].Rows[iterator]["mobile"].ToString()),
                                                 new JProperty("direction", callRoomDs.Tables[0].Rows[iterator]["direction"].ToString()),
                                                 new JProperty("type", callRoomDs.Tables[0].Rows[iterator]["type"].ToString()),
                                                 new JProperty("mute", callRoomDs.Tables[0].Rows[iterator]["mute"].ToString()),
                                                 new JProperty("deaf", callRoomDs.Tables[0].Rows[iterator]["deaf"].ToString()),
                                                 new JProperty("handraise", callRoomDs.Tables[0].Rows[iterator]["handraise"].ToString()),
                                                 new JProperty("isprivate", callRoomDs.Tables[0].Rows[iterator]["IsInPrivate"].ToString()),
                                                 new JProperty("IsMember", callRoomDs.Tables[0].Rows[iterator]["IsMember"].ToString()),
                                                 new JProperty("call_status", callRoomDs.Tables[0].Rows[iterator]["callstatus"].ToString())

                                                 ));
                        }
                        jObj1 = new JObject(new JProperty("status", "true"), new JProperty("msg", retMsg), new JProperty("ErrorCode", errorCode));

                        jObj2 = new JObject(new JProperty("data", grpCallRoomJarr),
                            new JProperty("servertime", serverTime),
                            new JProperty("starttime", startTime),
                            new JProperty("AllMembersCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["AllCount"])),
                            new JProperty("OnCallCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["OnCallCount"])),
                            new JProperty("HangUpCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["HangUpCount"])),
                            new JProperty("MuteCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["MuteCount"])),
                            new JProperty("HandRaiseCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["WantsToTalkCount"])),
                            new JProperty("OnCallInProgressCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["OnCallInProgressCount"])),
                            new JProperty("MembersCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["MembersCount"])),
                            new JProperty("TotalMembersCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["PageCount"])),
                            new JProperty("PrivateCount", Convert.ToInt32(callRoomDs.Tables[1].Rows[0]["PrivateCount"])));

                        responseJObj = new JObject(new JProperty("response", jObj1), new JProperty("result", jObj2));

                    }
                    else
                    {
                        jObj1 = new JObject(new JProperty("status", "false"), new JProperty("msg", "No Members Found"), new JProperty("ErrorCode", "128"));
                        responseJObj = new JObject(new JProperty("response", jObj1));
                    }

                }
                else
                {
                    jObj1 = new JObject(new JProperty("status", "false"), new JProperty("msg", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                    responseJObj = new JObject(new JProperty("response", jObj1));

                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupMasterBusiness.GetGroupCallRoom is ==>" + ex.ToString());
                jObj1 = new JObject(new JProperty("status", "false"), new JProperty("msg", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                responseJObj = new JObject(new JProperty("response", jObj1));
            }

            return responseJObj;

        }

        /// <summary>
        /// This Function is Used to Add Participant in GroupCall
        /// </summary>

        public JObject AddParticipantInGroupCall(string sConnString, JObject paramObj, int userID)
        {
            BusinessHelper helperObj = new BusinessHelper();
            helperObj.connString = sConnString;
            JObject responseJObj = new JObject();
            JArray jArrayParticipants = new JArray();
            DataTable membersForThisConference = default(DataTable);
            Int32 confID = 0, retVal = 0, errorCode = 0;
            string retMsg = "";
            DataTable conferenceMebers = null;
            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
            BusinessHelper businessHelperObj = new BusinessHelper();
            JArray jArryDnd = new JArray();
            DataSet resultDataSet = new DataSet();
            DataSet contactsDS = new DataSet();
            int HostIsDND;
            bool DndFlag = false;
            string MemberName = "";
            string MemberMobile = "";
            Int16 contactsRetVal = 0;
            string contactsRetMessage = "";
            int contactsErrorCode = 0, ListId = 0;
            short mode;
            JArray participantsArray = new JArray();
            JArray listArray = new JArray();
            JArray leftArray = new JArray();
            int membersCount = 0;
            try
            {
                businessHelperObj.connString = sConnString;
                membersForThisConference = new DataTable();
                membersForThisConference.Columns.Add("Name", typeof(string));
                membersForThisConference.Columns.Add("Mobile", typeof(string));
                membersForThisConference.Columns.Add("ListID", typeof(int));
                membersForThisConference.Columns.Add("IsDndCheck", typeof(int));

                try
                {
                    if (paramObj.SelectToken("ConferenceID") == null)
                    {
                        responseJObj = new JObject(new JProperty("Success", false),
                                         new JProperty("Message", "Invalid ConferenceID"), new JProperty("ErrorCode", "124"));
                        return responseJObj;
                    }
                    else
                    {
                        confID = Int32.Parse(paramObj.SelectToken("ConferenceID").ToString());
                    }
                    if (paramObj.SelectToken("Mode") == null)
                    {
                        mode = 1;
                    }
                    else
                    {
                        mode = Convert.ToInt16(paramObj.SelectToken("Mode"));
                    }

                    if (JArray.Parse(paramObj.SelectToken("Participants").ToString()).Count == 0 && paramObj.SelectToken("WebListIds").ToString() == string.Empty)
                    {
                        responseJObj = new JObject(new JProperty("Success", false),
                                             new JProperty("Message", "Please select participants"), new JProperty("ErrorCode", "127"));
                        return responseJObj;
                    }
                    else
                    {
                        if (paramObj.SelectToken("WebListIds").ToString() != "")
                        {
                            try
                            {
                                contactsDS = groupsObj.GetWebListContacts(userID, paramObj.SelectToken("WebListIds").ToString(), out contactsRetVal, out contactsRetMessage, out contactsErrorCode);
                                if (contactsRetVal == 1)
                                {
                                    foreach (DataRow _row in contactsDS.Tables[0].Rows)
                                    {
                                        membersForThisConference.Rows.Add(_row["Name"].ToString(), _row["MobileNumber"].ToString(), Convert.ToInt32(_row["ListId"]), false);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ExceptionLog("Exception In Getting Web Contacts List : " + ex.ToString());
                                helperObj.NewProperty("Success", false);
                                helperObj.NewProperty("Message", "Error parsing participants");
                                helperObj.NewProperty("ErrorCode", "118");
                                HttpContext.Current.Response.Write(helperObj.GetResponse());
                                return helperObj.GetResponse();
                            }
                        }
                        foreach (JObject _Members in (JArray)paramObj.SelectToken("Participants"))
                        {
                            foreach (JProperty _Token in _Members.Properties())
                            {
                                if (_Token.Name == "IsDndFlag")
                                {
                                    DndFlag = Convert.ToBoolean(_Token.Value.ToString());
                                }
                                else if (_Token.Name == "ListId")
                                {
                                    ListId = Convert.ToInt32(_Token.Value);
                                }
                                else
                                {
                                    MemberName = _Token.Name;
                                    MemberMobile = _Token.Value.ToString();
                                }
                            }

                            membersForThisConference.Rows.Add(MemberName, MemberMobile, ListId, DndFlag);

                        }
                        Logger.TraceLog("Add Particpant in grpcall" + MemberName + " Member Mobile " + MemberMobile);

                        conferenceMebers = businessHelperObj.GetConferenceMebersNew(membersForThisConference, userID, "v1.0.1", out HostIsDND);
                        if (conferenceMebers == null)
                        {
                            responseJObj = new JObject(new JProperty("Success", false),
                                                 new JProperty("Message", "Unable to parse participants"), new JProperty("ErrorCode", "118"));
                            return responseJObj;
                        }

                    }


                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Exception in GroupMasterBusiness.AddParticipantInGroupCall is ==>" + ex.ToString());
                    responseJObj = new JObject(new JProperty("Success", false),
                                       new JProperty("Message", "Server Error"), new JProperty("ErrorCode", "101"));
                    return responseJObj;

                }


                resultDataSet = groupsObj.SaveContactsToConference(conferenceMebers, userID, confID, paramObj.SelectToken("WebListIds").ToString(), mode, out retVal, out retMsg, out errorCode);

                if (retVal == 1 && mode == 1)
                {

                    if (resultDataSet.Tables.Count > 0)
                    {
                        if (resultDataSet.Tables[1].Rows.Count > 0)
                        {
                            var _with1 = resultDataSet.Tables[1];

                            for (int i = 0; i <= _with1.Rows.Count - 1; i++)
                            {
                                jArryDnd.Add(new JObject(new JProperty("Name", _with1.Rows[i]["member_name"]),
                                    new JProperty("Mobile", _with1.Rows[i]["mobile_number"]),
                                    new JProperty("IsDnd", _with1.Rows[i]["is_dnd"]),
                                    new JProperty("IsOptedIn", _with1.Rows[i]["IsOptedIn"]),
                                    new JProperty("IsOptinSent", _with1.Rows[i]["OptedInstructionsSent"])));

                            }
                        }

                        if (resultDataSet.Tables[2].Rows.Count > 0)
                        {
                            var _with2 = resultDataSet.Tables[2];
                            for (int i = 0; i <= resultDataSet.Tables[2].Rows.Count - 1; i++)
                            {
                                jArrayParticipants.Add(new JObject(new JProperty("Name", _with2.Rows[i]["member_name"]),
                                    new JProperty("Mobile", _with2.Rows[i]["mobile_number"]),
                                    new JProperty("IsDnd", _with2.Rows[i]["is_dnd"]),
                                    new JProperty("IsOptedIn", _with2.Rows[i]["IsOptedIn"]),
                                    new JProperty("IsOptinSent", _with2.Rows[i]["OptedInstructionsSent"])));
                            }
                        }
                    }



                    responseJObj = new JObject(new JProperty("Success", true),
                                       new JProperty("Message", retMsg),
                                       new JProperty("ErrorCode", errorCode),
                                        new JProperty("AddedParticipants", jArrayParticipants),
                                        new JProperty("DndContacts", jArryDnd));
                }
                else if (retVal == 1 && mode == 2)
                {

                    if (resultDataSet.Tables.Count > 0)
                    {
                        if (resultDataSet.Tables[0].Rows.Count > 0)
                        {
                            participantsArray = ResultJsonArray(resultDataSet.Tables[0]);
                        }
                        if (resultDataSet.Tables.Count > 1 && resultDataSet.Tables[1].Rows.Count > 0)
                        {
                            leftArray = ResultJsonArray(resultDataSet.Tables[1]);
                        }
                        if (resultDataSet.Tables.Count > 2 && resultDataSet.Tables[2].Rows.Count > 0)
                        {
                            listArray = ResultJsonArray(resultDataSet.Tables[2]);
                        }
                        if (resultDataSet.Tables.Count > 3 && resultDataSet.Tables[3].Rows.Count > 0)
                        {
                            membersCount = Convert.ToInt32(resultDataSet.Tables[3].Rows[0]["MembersCount"]);
                        }
                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                              new JProperty("Message", "Ok"),
                              new JProperty("MembersCount", membersCount),
                              new JProperty("PartcipantArray", participantsArray),
                              new JProperty("LeftParticipants", leftArray),
                              new JProperty("WebListArray", listArray));
                }

                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                                           new JProperty("Message", retMsg), new JProperty("ErrorCode", errorCode));
                }



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupMasterBusiness.AddParticipantInGroupCall is ==>" + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));

            }

            return responseJObj;
        }

        /// <summary>
        /// This Function is Used to Get Time in a string 
        /// </summary>

        public string getTimeString(string strdate)
        {
            TimeSpan ts = default(TimeSpan);
            DateTime dt = DateTime.Now;
            DateTime sdt = Convert.ToDateTime(strdate);
            ts = Convert.ToDateTime(dt.AddMinutes(330)) - Convert.ToDateTime(sdt.AddMinutes(330));
            int id = 0;
            int ih = 0;
            int im = 0;
            int j = 0;
            id = ts.Days;
            ih = ts.Hours;
            im = ts.Minutes;
            string retVal = "";
            if (id > 0 && id < 30)
            {
                if ((id == 1))
                {
                    retVal = "a day";
                }
                else
                {
                    retVal = id + " days";
                }

            }
            else if (id > 0 && id >= 30)
            {
                double x = id / 30;
                if ((Math.Round(x) == 1))
                {
                    retVal = "a month";
                }
                else
                {
                    retVal = Math.Round(x) + " months";
                }
            }
            else if (ih > 0)
            {
                if ((ih == 1))
                {
                    retVal = "a hour";
                }
                else
                {
                    retVal = ih + " hours";
                }

            }
            else if (im > 0)
            {
                if ((im == 1))
                {
                    retVal = "a minute";
                }
                else
                {
                    retVal = im + " minutes";
                }
            }
            else if (im == 0)
            {
                retVal = "a few second(s)";
            }
            return retVal;
        }

        /// <summary>
        /// This Function is Used to Get Group Members WIth Country Prefix
        /// </summary>


        public JObject IOSBuyCredits(string sConnString, JObject paramObj, int countryID)
        {

            BusinessHelper helperObj = new BusinessHelper();
            helperObj.connString = sConnString;
            JObject responseObj = new JObject();

            int retVal;
            String retMsg = "";
            double availbaleAmount = 0;
            string _AccessToken = "";
            string _TransactionID = "";
            string _RequestLogPath = "";
            string _PaymentStatus = "";
            double _InAppPurchaseAmnt = 0;
            string _TxnID = "";
            byte[] _receiptdata = null;
            string _InAppPurchaseRKey = "";
            JObject _ReceiptObj = new JObject();
            JObject _RespPlainData = new JObject();
            string _BundleID = "";
            string _ProductID = "";
            string _ReqAppVersion = "";
            string _ReqBuildNumber = "";
            string _ReqBundleID = "";
            JObject _InAppjobj = new JObject();
            DataSet purchaseDs = new DataSet();
            JObject purchaseObj = new JObject();


            _BundleID = System.Configuration.ConfigurationManager.AppSettings["BundleID"].ToString();

            _InAppPurchaseRKey = System.Configuration.ConfigurationManager.AppSettings["InAppPurchaseRKey"];
            _InAppPurchaseAmnt = Convert.ToDouble(System.Configuration.ConfigurationManager.AppSettings["InPurchaseRechargeAmount"]);
            _RequestLogPath = System.Configuration.ConfigurationManager.AppSettings["InAppPurchaseLogsPath"].ToString();

            if (countryID == 108)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductID"].ToString();
            }
            if (countryID == 239)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForUAE"].ToString();
            }
            if (countryID == 241)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForUS"].ToString();
            }
            if (countryID == 19)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForBAHRAIN"].ToString();
            }
            if (countryID == 173)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForOMAN"].ToString();
            }
            if (countryID == 188)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForQATAR"].ToString();
            }
            if (countryID == 124)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForKUWAIT"].ToString();
            }
            if (countryID == 199)
            {
                _ProductID = System.Configuration.ConfigurationManager.AppSettings["ProductIDForSAUDI"].ToString();
            }
            if (paramObj.SelectToken("TransactionIdentifier") == null)
            {
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "TransactionIdentifier Is Empty");
                helperObj.NewProperty("ErrorCode", "129");
                return helperObj.GetResponse();

            }
            if (paramObj.SelectToken("ReceiptData") == null)
            {
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "ReceiptData Is Empty");
                helperObj.NewProperty("ErrorCode", "130");
                return helperObj.GetResponse();
            }
            if (paramObj.SelectToken("PaymentStatus") == null)
            {
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "PaymentStatus Is Empty");
                helperObj.NewProperty("ErrorCode", "131");
                return helperObj.GetResponse();
            }

            try
            {
                _AccessToken = HttpContext.Current.Request.Headers["AccessToken"].ToString();
                _TransactionID = paramObj.SelectToken("TransactionIdentifier").ToString();
                _TxnID = HttpContext.Current.Request.Headers["TxnID"].ToString();
                _PaymentStatus = paramObj.SelectToken("PaymentStatus").ToString();
                _receiptdata = Convert.FromBase64String(paramObj.SelectToken("ReceiptData").ToString());
            }
            catch (Exception ex)
            {
                helperObj.NewProperty("Success", false);
                //helperObj.NewProperty("Message", "Something Went Wrong While Parsing Input Stream");
                helperObj.NewProperty("Message", ex.ToString());
                helperObj.NewProperty("ErrorCode", "101");
                Logger.ExceptionLog("exception In iosBuycredits :" + ex.ToString());
                return helperObj.GetResponse();
            }
            //-------------------------Parsing Receipt Data-------------
            try
            {

                _RespPlainData = JObject.Parse(PurchaseCredits(_receiptdata));

                if (_RespPlainData.SelectToken("status") == null || _RespPlainData.SelectToken("status").ToString() != "0")
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "Invlaid receipt data");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                if (_RespPlainData.SelectToken("receipt").SelectToken("bundle_id") == null)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "BundleID is empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                if (_RespPlainData.SelectToken("receipt").SelectToken("application_version") == null)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "application_version is empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                if (_RespPlainData.SelectToken("receipt").SelectToken("original_application_version") == null)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "original application version is empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                string res = _RespPlainData.SelectToken("receipt").SelectToken("in_app").ToString().Replace("[", "").Replace("]", "").ToString();
                _InAppjobj = JObject.Parse(res.Substring(0, res.IndexOf("}") + 1));

                //if (_InAppjobj.SelectToken("transaction_id") == null || _InAppjobj.SelectToken("transaction_id").ToString() != _TransactionID)
                //{
                //    helperObj.NewProperty("Success", false);
                //    helperObj.NewProperty("Message", "TransactionID mismatched");
                //    helperObj.NewProperty("ErrorCode", "E0001");
                //    return helperObj.GetResponse();
                //}
                //_ReqBuildNumber = _RespPlainData.SelectToken("receipt").SelectToken("application_version").ToString();
                //_ReqAppVersion = _RespPlainData.SelectToken("receipt").SelectToken("original_application_version").ToString();
                //_ReqBundleID = _RespPlainData.SelectToken("receipt").SelectToken("bundle_id").ToString();
                //if (_ReqBundleID != _BundleID)
                //{
                //    helperObj.NewProperty("Success", false);
                //    helperObj.NewProperty("Message", "BundleID mismatched");
                //    helperObj.NewProperty("ErrorCode", "E0001");
                //    return helperObj.GetResponse();
                //}
            }
            catch (Exception ex)
            {
                //Logger.ExceptionLog("Exception at InAppPurchase : " + ex.ToString());
                helperObj.NewProperty("Success", false);
                //helperObj.NewProperty("Message", "Something Went Wrong While Parsing receipt data");
                helperObj.NewProperty("Message", ex.ToString());
                helperObj.NewProperty("ErrorCode", "E0002");
                return helperObj.GetResponse();
            }

            //try
            //{
            //    string AppVersion = "1.2";
            //    string BuildNumber = "1";
            //    GetAppVersions(sConnString, _AccessToken, ref  AppVersion, ref  BuildNumber);

            //    if (BuildNumber != _ReqBuildNumber)
            //    {
            //        helperObj.NewProperty("Success", false);
            //        helperObj.NewProperty("Message", "Build Number mismatched");
            //        helperObj.NewProperty("ErrorCode", "E0002");
            //        return helperObj.GetResponse();
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Logger.ExceptionLog("Exception at InAppPurchase : " + ex.ToString());
            //    helperObj.NewProperty("Success", false);
            //    helperObj.NewProperty("Message", "Something Went Wrong While Validation App version");
            //    helperObj.NewProperty("ErrorCode", "E0002");
            //    return helperObj.GetResponse();
            //}


            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
            purchaseDs = groupsObj.InAppBuyCredits(1, _AccessToken, _TransactionID, _TxnID, _PaymentStatus, out retVal, out availbaleAmount, out retMsg);

            if (retVal == 1)
            {
                foreach (DataRow _row in purchaseDs.Tables[0].Rows)
                {
                    purchaseObj = new JObject();
                    foreach (DataColumn _column in purchaseDs.Tables[0].Columns)
                    {
                        purchaseObj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                    }
                }
                helperObj.NewProperty("Success", true);
                helperObj.NewProperty("Message", retMsg);
                helperObj.NewProperty("AvailableBalance", availbaleAmount);
                helperObj.NewProperty("PurchaseData", purchaseObj);
                return helperObj.GetResponse();

            }

            else
            {
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", retMsg);
                return helperObj.GetResponse();
            }


        }

        public JObject AndroidBuyCredits(string sConnString, JObject paramObj, int countryID)
        {
            Logger.TraceLog("AndroidBuyCredits BLL Started ");
            BusinessHelper helperObj = new BusinessHelper();
            helperObj.connString = sConnString;
            JObject responseObj = new JObject();
            string retMsg = "";
            int retVal;
            double availbaleAmount = 0;
            JObject purchaseObj = new JObject();


            string _AccessToken = "";
            string _RequestLogPath = "";
            string _PaymentStatus = "";

            string _TxnID = "";


            string _PackageName = "";
            string _AndroidProductID = "";
            string reqPackagename = "";
            string productID = "";
            string OrderID = "";
            string reqOrderID = "";
            DataSet purchaseDs = new DataSet();

            _PackageName = ConfigurationManager.AppSettings["PackageName"].ToString();

            if (countryID == 108)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductID"].ToString();
            }
            if (countryID == 239)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForUAE"].ToString();
            }
            if (countryID == 241)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForUS"].ToString();
            }
            if (countryID == 19)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForBAHRAIN"].ToString();
            }
            if (countryID == 173)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForOMAN"].ToString();

            }
            if (countryID == 188)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForQATAR"].ToString();
            }
            if (countryID == 124)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForKUWAIT"].ToString();
            }
            if (countryID == 199)
            {
                _AndroidProductID = ConfigurationManager.AppSettings["AndroidProductIDForSUADI"].ToString();
            }
            Logger.TraceLog("_PackageName : " + _PackageName + "_AndroidProductID" + _AndroidProductID);

            _RequestLogPath = ConfigurationManager.AppSettings["InAppPurchaseLogsPath"].ToString();
            _AccessToken = HttpContext.Current.Request.Headers["AccessToken"];
            _TxnID = HttpContext.Current.Request.Headers["TxnID"];
            _PaymentStatus = HttpContext.Current.Request["PaymentStatus"];
            OrderID = HttpContext.Current.Request["OrderID"];
            Logger.TraceLog("_RequestLogPath : " + _RequestLogPath);

            try
            {
                if (paramObj.SelectToken("packageName") == null)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "TransactionIdentifier Is Empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                else
                {
                    reqPackagename = paramObj.SelectToken("packageName").ToString();
                }
                if (_PackageName != reqPackagename)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "PackageName MisMatched");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }

                if (paramObj.SelectToken("productId") == null)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "productId Is Empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                else
                {
                    productID = paramObj.SelectToken("productId").ToString();
                }
                if (_AndroidProductID != productID)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "ProductId MisMatched");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }

                if (paramObj.SelectToken("orderId") == null)
                {
                    //if (_AccessToken == "dEfHoajU7NMZnqkycpRi")
                    //{
                    //    reqOrderID = "empty";
                    //    OrderID = "empty";
                    //}
                    //else { 
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "orderId Is Empty");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                    //}
                }
                else
                {
                    reqOrderID = paramObj.SelectToken("orderId").ToString();
                }
                if (OrderID != reqOrderID)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "OrderID MisMatched");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }



            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception at InAppPurchase : " + ex.ToString());
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "Something Went Wrong While Parsing receipt data");
                helperObj.NewProperty("ErrorCode", "E0002");
                return helperObj.GetResponse();
            }

            JObject response = new JObject();
            string purchaseToken = "";
            purchaseToken = paramObj.SelectToken("purchaseToken").ToString();
            response = AndroidPurchaseCredits(purchaseToken, _AndroidProductID);
            Logger.TraceLog("AndroidPurchaseCredits respionse : " + response.ToString());
            var errorProperty = response.Property("error");

            if (response.SelectToken("Success").ToString() == "True")
            {
                if (errorProperty == null)
                {
                    Logger.TraceLog("purchaseTimeMillis : " + response.SelectToken("purchaseTimeMillis").ToString());
                    Logger.TraceLog("purchaseTime : " + paramObj.SelectToken("purchaseTime").ToString());
                    if (response.SelectToken("purchaseTimeMillis").ToString() == paramObj.SelectToken("purchaseTime").ToString())
                    {
                        try
                        {
                            Logger.TraceLog("Order ID TRACK STARTED");
                            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                            purchaseDs = groupsObj.InAppBuyCredits(2, _AccessToken, OrderID, _TxnID, _PaymentStatus, out retVal, out availbaleAmount, out retMsg);
                            if (retVal == 1)
                            {
                                foreach (DataRow _row in purchaseDs.Tables[0].Rows)
                                {
                                    purchaseObj = new JObject();
                                    foreach (DataColumn _column in purchaseDs.Tables[0].Columns)
                                    {
                                        purchaseObj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                                    }
                                }
                                helperObj.NewProperty("Success", true);
                                helperObj.NewProperty("Message", retMsg);
                                helperObj.NewProperty("AvailableBalance", availbaleAmount);
                                helperObj.NewProperty("PurchaseData", purchaseObj);
                                return helperObj.GetResponse();
                            }

                            else
                            {
                                helperObj.NewProperty("Success", false);
                                helperObj.NewProperty("ErrorCode", "E0002");
                                helperObj.NewProperty("Message", retMsg);
                                return helperObj.GetResponse();
                            }

                        }
                        catch (Exception ex)
                        {
                            Logger.ExceptionLog(ex.ToString());
                            helperObj.NewProperty("Success", false);
                            helperObj.NewProperty("ErrorCode", "E0002");
                            helperObj.NewProperty("Message", ex.ToString());
                            return helperObj.GetResponse();

                        }

                    }
                    else
                    {
                        helperObj.NewProperty("Success", false);
                        helperObj.NewProperty("Message", "Invalid Transaction");
                        helperObj.NewProperty("ErrorCode", "E0003");
                        return helperObj.GetResponse();
                    }


                }
                else
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "Invalid Transaction");
                    helperObj.NewProperty("ErrorCode", "E0003");
                    return helperObj.GetResponse();
                }
            }
            else
            {
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "Invalid Transaction");
                helperObj.NewProperty("ErrorCode", "E0003");
                return helperObj.GetResponse();
            }




            return helperObj.GetResponse();

        }
        private JObject AndroidPurchaseCredits(string purchaseToken, string productId)
        {
            Logger.TraceLog("AndroidPurchaseCredits BLL Started");
            JObject responseJobj = new JObject();
            string httpUrl = "https://accounts.google.com/o/oauth2/token";
            StreamReader Sreader = null;
            string _postdata = "";
            string ResponseString = "";
            JObject ResObj = new JObject();
            HttpWebRequest _Req = null;
            HttpWebResponse _Resp = null;
            StreamWriter Swriter = null;
            try
            {
                _Req = (HttpWebRequest)HttpWebRequest.Create(httpUrl.ToString());
                _Req.Method = "POST";
                _postdata = "grant_type=refresh_token&refresh_token=1/_LhK2ieJnaH7kvfGU0faUASHtoTQxOELEnlHVMcsJ3c&client_id=269370161838-imcjdstpn2vu610gjmu1pnbs1vhuk55a.apps.googleusercontent.com&client_secret=Zt6haRMbDxp_cJNGz_VC97w1";
                _Req.ContentType = "application/x-www-form-urlencoded";
                Swriter = new StreamWriter(_Req.GetRequestStream());
                Swriter.Write(_postdata.Trim());
                Swriter.Flush();
                Swriter.Close();
                _Resp = (HttpWebResponse)_Req.GetResponse();
                Sreader = new StreamReader(_Resp.GetResponseStream());
                ResponseString = Sreader.ReadToEnd();


                ResObj = JObject.Parse(ResponseString);
                Logger.TraceLog("ResObj" + ResObj.ToString());
                if (string.IsNullOrEmpty(ResObj.ToString()) == false)
                {
                    if (string.IsNullOrEmpty(ResObj.SelectToken("access_token").ToString()) == false)
                    {
                        responseJobj = ValidateAndroidInAppPurchaseData(ResObj.SelectToken("access_token").ToString(), purchaseToken, productId);
                    }
                }
                Logger.TraceLog("ResObj" + ResObj.ToString());


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at PurchaseCredits : " + ex.ToString());
                responseJobj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Transaction Failed"));

            }
            return responseJobj;

        }
        private JObject ValidateAndroidInAppPurchaseData(string accessToken, string purchaseToken, string productId)
        {
            Logger.TraceLog("ValidateAndroidInAppPurchaseData Started");
            string httpUrl = "https://www.googleapis.com/androidpublisher/v2/applications/com.mobile.android.grptalk/purchases/products/";
            httpUrl = httpUrl + productId + "/tokens/" + purchaseToken + "?accesstoken=" + accessToken;
            StreamReader Sreader = null;
            string ResponseString = "";
            JObject ResObj = new JObject();
            HttpWebRequest _Req = null;
            HttpWebResponse _Resp = null;
            try
            {
                _Req = (HttpWebRequest)HttpWebRequest.Create(httpUrl.ToString());
                _Req.Headers.Add("Authorization", "OAuth " + accessToken);
                _Resp = (HttpWebResponse)_Req.GetResponse();
                Sreader = new StreamReader(_Resp.GetResponseStream());
                ResponseString = Sreader.ReadToEnd();
                ResObj = JObject.Parse(ResponseString);
                ResObj.Add(new JProperty("Success", true));
                Logger.TraceLog("ValidateAndroidInAppPurchaseData Response : " + ResObj.ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ValidateInAppPurchaseData :" + ex.ToString());
                ResObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Failed"));
            }
            return ResObj;
        }

        private string PurchaseCredits(byte[] receiptData)
        {

            string retMessage = "";
            byte[] postBytes = null;
            JObject receiptObj = new JObject();
            HttpWebRequest webReq = null;
            string httpUrl = "";
            dynamic json = new JObject(new JProperty("receipt-data", receiptData)).ToString();
            try
            {
                postBytes = Encoding.UTF8.GetBytes(json);
                //if (HttpContext.Current.Request.Headers["AppVersion"].ToString() == "1.3.1")
                //{
                //    httpUrl = System.Configuration.ConfigurationManager.AppSettings["PurchaseCreditsSanboxURLStoreV1.3.1"].ToString();
                //}
                //else
                //{
                httpUrl = System.Configuration.ConfigurationManager.AppSettings["PurchaseCreditsSanboxURLStoreV1.3.1"].ToString();
                //}
                Logger.TraceLog("httpUrl " + httpUrl + " Version " + HttpContext.Current.Request.Headers["AppVersion"].ToString());
                webReq = (HttpWebRequest)HttpWebRequest.Create(httpUrl);
                webReq.Method = "POST";
                webReq.ContentType = "application/json";
                webReq.ContentLength = postBytes.Length;
                using (Stream stream = webReq.GetRequestStream())
                {
                    stream.Write(postBytes, 0, postBytes.Length);
                    stream.Flush();
                }

                dynamic sendresponse = webReq.GetResponse();
                string sendresponsetext = "";
                using (StreamReader streamreader = new StreamReader(sendresponse.GetResponseStream()))
                {
                    sendresponsetext = streamreader.ReadToEnd().Trim();
                }
                retMessage = sendresponsetext;

                return retMessage;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exp at PurchaseCredits : " + ex.ToString());
                return "{'status':'2000'}";
            }

        }

        public void GetAppVersions(string sConnString, string accessToken, ref string _version, ref string _bid)
        {

            DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
            groupsObj.GetAppversions(accessToken, out _version, out _bid);

        }

        public JObject GrpCallCancel(string sConnString, int ConferenceID)
        {
            JObject responseObj = new JObject();
            int retVal = 0, errorCode = 0;
            string lastCallDate = "";
            string retMsg = "";
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                retMsg = groupsObj.GrpCallCancel(ConferenceID, out retVal, out lastCallDate, out errorCode);
                if (retVal == 1)
                {
                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("LatCallDate", lastCallDate),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", errorCode));

                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", errorCode));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("GroupMasterBussiness_---" + ex.ToString());
            }
            return responseObj;
        }
        public JObject CheckUserConfirmation(string sConnString, JObject paramObj, int countryID)
        {
            JObject responseObj = new JObject();
            BusinessHelper businessHelperObj = new BusinessHelper();
            string mobile = "";
            int RetVal = 0;
            bool isConfirmed;
            DataSet RegDs = new DataSet();
            string RetMessage = ""; int errorCode = 0;
            string minimumBalance = "";
            if (paramObj.SelectToken("MobileNumber") == null)
            {
                responseObj = new JObject(new JProperty("Success", false),
              new JProperty("Message", "Unable to read MobileNumber"),
              new JProperty("ErrorCode", "107"));
                return responseObj;
            }
            mobile = paramObj.SelectToken("MobileNumber").ToString();
            businessHelperObj.GetOnlyNumeric(ref mobile);
            businessHelperObj.RemoveZeroPrefix(ref mobile);

            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                RegDs = groupsObj.CheckUserConfirmation(mobile, out RetVal, out RetMessage, out isConfirmed, out errorCode);

                if (RetVal != 1)
                {
                    responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", RetMessage),
                    new JProperty("MobileNumber", mobile), new JProperty("ErrorCode", errorCode));
                    if (RegDs.Tables.Count > 0 && RegDs.Tables[0].Rows.Count > 0)
                    {
                        var _with1 = RegDs.Tables[0].Rows[0];
                        foreach (DataColumn _Column in _with1.Table.Columns)
                        {
                            responseObj.Add(new JProperty(_Column.ColumnName, RegDs.Tables[0].Rows[0][_Column.ColumnName]));
                        }
                    }
                    return (responseObj);

                }
                else
                {
                    if (isConfirmed == true)
                    {
                        if (countryID == 19)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["BahrainMinimumBalance"].ToString();
                        }
                        else if (countryID == 239)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["UAEMinimumBalance"].ToString();
                        }
                        else if (countryID == 241)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["UsaMinimumbalance"].ToString();
                        }
                        else if (countryID == 199)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["SaudiMinimumbalance"].ToString();
                        }
                        else if (countryID == 173)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["OmanMinimumbalance"].ToString();
                        }
                        else if (countryID == 188)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["QatarMinimumbalance"].ToString();
                        }
                        else if (countryID == 124)
                        {
                            minimumBalance = ConfigurationManager.AppSettings["KuwaitMinimumbalance"].ToString();
                        }
                        else
                        {
                            minimumBalance = ConfigurationManager.AppSettings["IndiaMinimumBalance"].ToString();
                        }
                        responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "User Already Confirmed"),
                        new JProperty("MobileNumber", mobile), new JProperty("ErrorCode", errorCode));
                        if (RegDs.Tables.Count > 0 && RegDs.Tables[0].Rows.Count > 0)
                        {
                            var _with2 = RegDs.Tables[0].Rows[0];
                            foreach (DataColumn _Column in _with2.Table.Columns)
                            {
                                responseObj.Add(new JProperty(_Column.ColumnName, RegDs.Tables[0].Rows[0][_Column.ColumnName]));
                            }

                        }
                        responseObj.Add(new JProperty("MinimumBalance", minimumBalance));
                        responseObj.Add(new JProperty("MinMemberCount", 1));



                    }
                    else
                    {
                        responseObj = new JObject(new JProperty("Success", false),
                       new JProperty("MobileNumber", mobile), new JProperty("ErrorCode", errorCode));
                        return (responseObj);

                    }

                    return (responseObj);

                }
            }


            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                Logger.ExceptionLog("GroupMasterBussiness_---" + ex.ToString());
            }


            return responseObj;
        }

        public JObject InAppPurchaseHistory(string sConnString, int userID)
        {
            JObject responseJObj = new JObject();
            DataSet Ds = new DataSet();
            JObject tempJobj = new JObject();
            JArray Jarr = new JArray();
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                Ds = groupsObj.InAppPurchaseHistory(userID);
                if (Ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow _row in Ds.Tables[0].Rows)
                    {
                        tempJobj = new JObject();
                        foreach (DataColumn _column in Ds.Tables[0].Columns)
                        {
                            tempJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                        }
                        Jarr.Add(tempJobj);
                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "Success"), new JProperty("ErrorCode", "117"),
                        new JProperty("Items", Jarr));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "No Recharge History Found"), new JProperty("ErrorCode", "133"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;

        }
        public JObject LiveCallDetails(string sConnString, int userID)
        {
            JObject responseJObj = new JObject();
            DataSet Ds = new DataSet();
            JObject tempJobj = new JObject();
            JArray Jarr = new JArray();
            try
            {
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                Ds = groupsObj.LiveCallDetails(userID);
                if (Ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow _row in Ds.Tables[0].Rows)
                    {
                        tempJobj = new JObject();
                        foreach (DataColumn _column in Ds.Tables[0].Columns)
                        {
                            tempJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                        }
                        Jarr.Add(tempJobj);
                    }
                    responseJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", "Success"), new JProperty("ErrorCode", "117"),
                        new JProperty("Items", Jarr));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "No Live Calls Found"), new JProperty("ErrorCode", "133"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;


        }

        public JObject UpdateGrpMemberStatus(string sConnString, int userId, JObject paramJobj)
        {
            JObject responseObj = new JObject();
            GT.DataAccessLayer.V_1_5.Groups groupsObj = new GT.DataAccessLayer.V_1_5.Groups(sConnString);
            short retVal = 0;
            string retMsg = string.Empty;
            short mode;
            short isDNDFlag = 0;
            string newNumber = "";
            DataSet ds;
            int hostCountryId = 0, hostCountryCode = 0;
            Boolean isCountryPreficExists = false;
            string currenctCountryPrefix = "", callerIDFinal = "", userDefaultCountryCode = "91";
            int currentCountryID = 0;
            DataTable callerIDs = new DataTable(), finalConferenceMembers = new DataTable(), countryMaster = new DataTable();

            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("Mode"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("MobileNumber"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("ConferenceId"))))
                {
                    responseObj = new JObject(new JProperty("Success", false),
                                      new JProperty("Message", "Mandatory Parameter missing"), new JProperty("ErrorCode", "111"));
                    return responseObj;


                }

                mode = Convert.ToInt16(paramJobj.SelectToken("Mode"));
                if (mode == 2 && string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("NewNumber"))))
                {

                    responseObj = new JObject(new JProperty("Success", false),
                                      new JProperty("Message", "Mandatory Parameter missing for Edit Contact"), new JProperty("ErrorCode", "111"));

                    return responseObj;

                }
                if (mode == 2)
                {
                    DataAccessLayer.EntityHelper countryObj = new DataAccessLayer.EntityHelper(sConnString);
                    isDNDFlag = Convert.ToInt16(paramJobj.SelectToken("ISDNDFlag"));
                    newNumber = Convert.ToString(paramJobj.SelectToken("NewNumber"));
                    ds = countryObj.GetCountryDetails(userId);
                    countryMaster = ds.Tables[0];
                    callerIDs = ds.Tables[1];

                    hostCountryId = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
                    hostCountryCode = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryCode"]);
                    BusinessHelper bh = new BusinessHelper();
                    bh.GetOnlyNumeric(ref newNumber);
                    bh.RemoveZeroPrefix(ref newNumber);
                    foreach (DataRow _Country in countryMaster.Rows)
                    {
                        if (newNumber.StartsWith(_Country["CountryCode"].ToString()) && newNumber.Length == Int16.Parse(_Country["MaxLength"].ToString()))
                        {
                            isCountryPreficExists = true;
                            currenctCountryPrefix = _Country["CountryCode"].ToString();
                            currentCountryID = Int16.Parse(_Country["CountryID"].ToString());
                            break;
                        }
                    }
                    if (isCountryPreficExists == false)
                    {
                        newNumber = userDefaultCountryCode + newNumber;
                    }

                }

                groupsObj.UpdateGroupMemberStatus(userId, Convert.ToInt64(paramJobj.SelectToken("ConferenceId")), Convert.ToString(paramJobj.SelectToken("MobileNumber")), newNumber, isDNDFlag, mode, out retVal, out retMsg);

                if (retVal == 1)
                {
                    responseObj = new JObject(new JProperty("Success", true),
                                      new JProperty("Message", retMsg), new JProperty("ErrorCode", "102"));

                    return responseObj;
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false),
                                      new JProperty("Message", retMsg), new JProperty("ErrorCode", "111"));
                    return responseObj;

                }
            }
            catch (Exception ex)
            {

                Logger.ExceptionLog("exception in UpdateGrpMemberStatus Bll " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }

            return responseObj;
        }



        public JObject UpdateGrpSettings(string sConnString, int userID, JObject paramJobj)
        {
            JObject responseJObj = new JObject();
            grpEdit editObj = new grpEdit();
            DataSet resultDs;
            short retVal = 0;
            string retMsg = string.Empty;
            JObject settingObj = new JObject();
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("IsAllowNonMembers"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("IsMuteDial"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("IsOnlyDialIn"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("OpenLineBefore"))) || string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("GroupID"))))
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                                      new JProperty("Message", "Mandatory Parameter missing"), new JProperty("ErrorCode", "111"));
                    return responseJObj;


                }
                editObj.grpcallID = Convert.ToInt32(paramJobj.SelectToken("GroupID").ToString());
                editObj.IsMuteDial = Convert.ToInt16(paramJobj.SelectToken("IsMuteDial").ToString());
                editObj.IsOnlyDialIn = Convert.ToInt32(paramJobj.SelectToken("IsOnlyDialIn"));
                editObj.IsAllowNonMembers = Convert.ToInt32(paramJobj.SelectToken("IsAllowNonMembers"));
                editObj.OpenLineBefore = Convert.ToInt32(paramJobj.SelectToken("OpenLineBefore"));
                DataAccessLayer.V_1_5.Groups groupsObj = new DataAccessLayer.V_1_5.Groups(sConnString);
                resultDs = groupsObj.UpdateGrpSettings(userID, editObj, out retVal, out retMsg);
                if (retVal == 1)
                {
                    if (resultDs.Tables.Count > 0 && resultDs.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataColumn dc in resultDs.Tables[0].Columns)
                        {
                            settingObj.Add(new JProperty(dc.ColumnName, resultDs.Tables[0].Rows[0][dc.ColumnName]));
                        }

                    }

                    responseJObj = new JObject(new JProperty("Success", true),
                             new JProperty("Message", "Success"), new JProperty("ErrorCode", "117"),
                             new JProperty("Settings", settingObj));

                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "No Live Calls Found"), new JProperty("ErrorCode", "133"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception Bll " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            return responseJObj;


        }

        private JObject ReturnGrpCallMembers(DataSet resultDs)
        {


            JArray participantsArray = new JArray();
            JArray listArray = new JArray();
            JArray leftArray = new JArray();
            JObject responseObj = new JObject();
            try
            {
                if (resultDs.Tables.Count > 0)
                {
                    if (resultDs.Tables[0].Rows.Count > 0)
                    {
                        participantsArray = ResultJsonArray(resultDs.Tables[0]);
                    }
                    if (resultDs.Tables[1].Rows.Count > 0)
                    {
                        leftArray = ResultJsonArray(resultDs.Tables[1]);
                    }
                    if (resultDs.Tables[2].Rows.Count > 0)
                    {
                        listArray = ResultJsonArray(resultDs.Tables[2]);
                    }
                }
                responseObj = new JObject(new JProperty("Success", true),
                          new JProperty("Message", "Ok"),
                          new JProperty("PartcipantArray", participantsArray),
                          new JProperty("LeftParticipants", leftArray),
                          new JProperty("WebListArray", listArray));

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return responseObj;

        }

        private JArray ResultJsonArray(DataTable dt)
        {
            JObject resultObj = new JObject();
            JArray resultArray = new JArray();
            foreach (DataRow dr in dt.Rows)
            {
                resultObj = new JObject();
                foreach (DataColumn dc in dt.Columns)
                {
                    resultObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                }
                resultArray.Add(resultObj);
            }

            return resultArray;
        }

    }
}
