using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Web;
using System.Net;
using System.Configuration;
using System.Xml;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using GrpTalk.CommonClasses;
using System.Web.Routing;
using GT.Utilities;
using GT.Utilities.Properties;
using GT.BusinessLogicLayer.V_1_4;
using System.Drawing;

namespace GrpTalk.Handlers.V_1_4
{
    /// <summary>
    /// Summary description for AppService_V1__4__0
    /// </summary>
    public class AppService_V1__4__0 : IHttpHandler
    {
        #region "GLOBALVARIABLES"

        Dictionary<string, object> userObj = new Dictionary<string, object>();
        RouteValueDictionary pageRouteValues = null;

        ApiHelper helperObj = new ApiHelper();
        string requestID = System.Guid.NewGuid().ToString();
        string actionName = "";
        string appVersion = "";
        int userId = 0;
        int countryID = 0;

        #endregion
        public void ProcessRequest(HttpContext context)
        {
            HttpContext.Current.Items.Add("ProcessStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            System.Threading.Thread.CurrentThread.Name = requestID;
            System.Web.Routing.RouteData routeData = context.Request.RequestContext.RouteData;
            actionName = routeData.Values["ResourceName"].ToString();
            HttpContext.Current.Items.Add("ActionName", actionName);
            if (actionName == null)
            {
                helperObj.FlushErrorResponse(400, statusDescription: "Bad Request ( No Action Specified )");
            }
            else
            {
                if (!actionName.Contains("."))
                {
                    helperObj.Format = ApiHelper.JsonFormat;
                }
                else
                {
                    if (actionName.Split('.').Last().ToUpper() != ApiHelper.XmlFormat && actionName.Split('.').Last().ToUpper() != ApiHelper.JsonFormat)
                    {
                        helperObj.FlushErrorResponse(404);
                    }
                }

                if (actionName.Split('.').Last().ToUpper() == ApiHelper.XmlFormat)
                {
                    helperObj.Format = ApiHelper.XmlFormat;
                }
                else
                {
                    helperObj.Format = ApiHelper.JsonFormat;
                }
                actionName = actionName.Split('.').First();
                if (helperObj.IsXmlFormat)
                {
                    helperObj.InitializeXmlDoc();
                }
                else
                {
                    helperObj.InitializeJSonObjects();
                }

            }
            string hostName = Dns.GetHostName();
            Logger.TraceLog("IP :  " + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            if (HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() == "182.74.61.105" || HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() == "182.74.61.106" || HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString() == "183.82.2.22")
            {
                helperObj.SaveRequest(actionName);
            }
            userObj = (Dictionary<string, object>)HttpContext.Current.Items["UserObject"];
            if (userObj != null && userObj.ContainsKey("UserID"))
            {
                userId = Convert.ToInt32(userObj["UserID"]);
            }


            if (userObj != null && userObj.ContainsKey("CountryID"))
            {
                countryID = Convert.ToInt32(userObj["CountryID"]);
            }

            if (userId == 10054 || userId == 1 || userId == 2 || userId == 62497 || userId == 43922 || userId == 10058)
            {

            }
            else
            {
                short siteIsUnderMaintanance = 0;
                siteIsUnderMaintanance = Convert.ToInt16(System.Configuration.ConfigurationManager.AppSettings["siteIsUnderMaintanance"].ToString());
                if (siteIsUnderMaintanance == 1)
                {
                    JObject responseObj = new JObject();
                    responseObj = new JObject(new JProperty("Success", false),
                           new JProperty("Message", "Please Wait App is Under Maintenance"));
                    context.Response.Write(responseObj);
                    context.Response.End();
                }

            }



            switch (actionName)
            {
                case ApiHelper.REGISTER:
                    Registration(context);
                    break;
                case ApiHelper.VALIDATE_OTP:
                    ValidateOTP(context);
                    break;
                case ApiHelper.CREATE_GROUPCALL:
                    CreateGroupCall(context);
                    break;
                case ApiHelper.EDIT_GROUPCALL:
                    EditGroupCall(context);
                    break;
                case ApiHelper.ADD_PARTICIPANT_IN_CONFERENCE:
                    AddParticipantInGroupCall(context);
                    break;
                case ApiHelper.GET_ALL_GROUP_CALLS:
                    GetAllGroupCalls(context);
                    break;
                case ApiHelper.GET_REPORTS_BY_BATCH_ID:
                    GetReportsByBatchIdNew(context);
                    break;
                case ApiHelper.UPDATE_GROUPCALL_NAME:
                    UpdateGroupCallName(context);
                    break;
                case ApiHelper.PROFILE_IMAGE:
                    ProfileImage(context);
                    break;
                case ApiHelper.DELETE_GROUPCALL:
                    DeleteGroupCall(context);
                    break;
                case ApiHelper.MUTEUNMUTE:
                    MuteUnMute(context);
                    break;
                case ApiHelper.HANGUP:
                    Hangup(context);
                    break;
                case ApiHelper.ANDROID_INAPP_PURCHASE:
                    AndroidBuyCredits(context);
                    break;
                case ApiHelper.GET_COUNTRIES:
                    GetCountries(context);
                    break;
                case ApiHelper.DIAL:
                    Dial(context);
                    break;
                case ApiHelper.GET_GROUPCALL_ROOM:
                    GetGroupCallRoomBackUp(context);
                    break;
                case ApiHelper.GRPCALL_CANCEL:
                    GrpCallCancel(context);
                    break;
                case ApiHelper.CHECK_USER_CONFIRMATION:
                    CheckUserConfirmation(context);
                    break;
                case ApiHelper.USER_BALANCE:
                    UserBalance(context);
                    break;
                case ApiHelper.UPDATE_PROFILE:
                    UpdateProfile(context);
                    break;
                case ApiHelper.INAPP_PURCAHSE:
                    IOSBuyCredits(context);
                    break;
                case ApiHelper.LEAVE_GROUP_CALL:
                    LeaveGroupCall(context);
                    break;
                case ApiHelper.OTP_CALL:
                    OtpCall(context);
                    break;
                case ApiHelper.INAPP_PURCHASE_HISTORY:
                    InAppPurchaseHistory(context);
                    break;
                case ApiHelper.PhoneContactsSync:
                    PhoneContactsSync(context);
                    break;
                case ApiHelper.GroupCallHistory:
                    GroupCallHistory(context);
                    break;
                case ApiHelper.Get_All_ContactsLists:
                    GetAllContactsLists(context);
                    break;
                case ApiHelper.Get_GroupCall_Room:
                    GetGroupCallRoom(context);
                    break;
                case ApiHelper.Phone_Call_HistorySync:
                    PhoneCallHistroySync(context);
                    break;
                case ApiHelper.Get_User_Details:
                    GetUserDetails(context);
                    break;
                case ApiHelper.QR_Code_Login:
                    QRCodeLoginCheck(context);
                    break;
                case ApiHelper.QR_Code_Logout:
                    QrCodeLogout(context);
                    break;
                case ApiHelper.Check_Web_Login:
                    WebLoginCheck(context);
                    break;
                case ApiHelper.Add_Contact_To_Group:
                    AddContactToGroup(context);
                    break;
                case ApiHelper.ChangeTimeZone:
                    ChnageTimeZone(context);
                    break;
                case "IOSPUSH":
                    IOSPUSH();
                    break;
                case "OtpCall2":
                    OtpCall();
                    break;

            }



        }
        public void ChnageTimeZone(HttpContext context)
        {
            JObject responseJobj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(context.Request.InputStream);
            string _Payload = _IS.ReadToEnd();
            JObject _ParamObj = JObject.Parse(_Payload);
            Profile_V140 profileObj = new Profile_V140();
            responseJobj = profileObj.SetUserUtcOffSet(MyConf.MyConnectionString, userId, 5, _ParamObj.SelectToken("timezone").ToString());
            context.Response.Write(responseJobj);

        }
        public void PhoneCallHistroySync(HttpContext context)
        {
            JObject responseJobj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(context.Request.InputStream);
            string _Payload = _IS.ReadToEnd();
            JObject _ParamObj = JObject.Parse(_Payload);
            Contacts_V140 contactsObj = new Contacts_V140();
            responseJobj = contactsObj.PhoneCallHistorySync(MyConf.MyConnectionString, userId, _ParamObj.SelectToken("PhoneNumbers").ToString());
            context.Response.Write(responseJobj);
        }

        private void AddContactToGroup(HttpContext context)
        {
            JObject responseObj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader iStream = new StreamReader(context.Request.InputStream);
            string payLoad = iStream.ReadToEnd();
            JObject paramObj = JObject.Parse(payLoad);
            Contacts_V140 contactsObj = new Contacts_V140();
            try
            {
                if (paramObj.SelectToken("MobileNumber") == null || paramObj.SelectToken("ConferenceID") == null)
                {
                    responseObj = new JObject(new JProperty("Success", false),
                               new JProperty("Message", "Mandatory Parameter missing"), new JProperty("ErrorCode", "111"));
                    context.Response.Write(responseObj);
                    return;
                }

                responseObj = contactsObj.SaveContactToGroup(MyConf.MyConnectionString, userId, Convert.ToInt64(paramObj.SelectToken("ConferenceID")), paramObj.SelectToken("MobileNumber").ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In AddContactToGroup Api : " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                                            new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            context.Response.Write(responseObj);
        }

        public void OtpCall()
        {
            Login_V140 loginObj = new Login_V140();
            //bool res=loginObj.ApiOtpCall("9966359584", "123456");
            //JObject requestJobj = new JObject();
            //HttpWebRequest objWebRequest = null;
            //HttpWebResponse objWebResponse = null;
            //StreamWriter objStreamWriter = null;
            //StreamReader objStreamReader = null;
            //requestJobj =new JObject(new JProperty("Number", "919966359584"),
            //    new JProperty("CallerId", "919533598485"),
            //    new JProperty("RingUrl", "www.grptalk.com"),
            //    new JProperty("AnswerUrl", "www.grptalk.com"),
            //    new JProperty("HangupUrl", "www.grptalk.com"),
            //    new JProperty("HttpMethod", "POST"),
            //    new JProperty("Xml","<Response><Speak>Your O T P is 6 7 3 7 8 9</Speak><Hangup/></Response>"));
            //try
            //{
            //    string stringResult = null;
            //    objWebRequest = (HttpWebRequest)WebRequest.Create("https://restapi.smscountry.com/v0.1/Accounts/sIkXPrYqDPe6xxjZgT1z/Calls/");
            //    objWebRequest.Method = "POST";
            //    objWebRequest.ContentType = "application/json";
            //    objWebRequest.Headers.Add("authorization", "Basic c0lrWFByWXFEUGU2eHhqWmdUMXo6aGQ1VzJkMmJjQXpzOHRtUk1hd3hXSlR3MXFXcFZlNW9nMHNNMkNNVg==");
            //    objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
            //    objStreamWriter.Write(requestJobj);
            //    objStreamWriter.Flush();
            //    objStreamWriter.Close();
            //    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
            //    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
            //    stringResult = objStreamReader.ReadToEnd();
            //    objStreamReader.Close();
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
            //    if ((objStreamWriter != null))
            //    {
            //        objStreamWriter.Close();
            //    }
            //    if ((objStreamReader != null))
            //    {
            //        objStreamReader.Close();
            //    }
            //    objWebRequest = null;
            //    objWebResponse = null;
            //}

        }

        public void GetUserDetails(HttpContext con)
        {
            JObject jObj = new JObject();
            Profile_V140 profileObj = new Profile_V140();
            jObj = profileObj.GetProfileDetailsDisplay(MyConf.MyConnectionString, userId, 1);
            con.Response.Write(jObj);

        }
        public void GetAllContactsLists(HttpContext context)
        {
            JObject listsResponse = new JObject();
            Contacts_V140 contactsObj = new Contacts_V140();
            listsResponse = contactsObj.GetAllContactsLists(MyConf.MyConnectionString, userId);
            context.Response.Write(listsResponse);
        }
        public void IOSPUSH()
        {
            NotificationsPush obj = new NotificationsPush();
            obj.IOSpush("f609014b808566c2891582bf993a379aec8f28b7c6f4a3c1721ef69016e8d7dd", "hi");
            //obj.AndroidPush("PA91bHGT_FyVI7gZX1_oQ4MCytdc7QUiGxk1QIdjnxPB2J1R7UtT0er9RNmpCt51oqURLCGbovpeYFNAhGu-hR4yZH44Utxot20eBmTrH1u7yOIV4euVZ-wjXeKc1p7EgygpeZV2pDq", "Hi తెలుగు ");
        }
        public void GroupCallHistory(HttpContext context)
        {
            JObject historyJobj = new JObject();
            JObject historyResponse = new JObject();

            try
            {
                Groups_V140 groupsObj = new Groups_V140();
                historyResponse = groupsObj.GroupCallHistory(MyConf.MyConnectionString, userId, Convert.ToInt32(context.Request["GroupCallID"]), Convert.ToInt32(context.Request["PageNumber"]), Convert.ToInt32(context.Request["PageSize"]));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In GroupCallHistory : " + ex.ToString());
                historyResponse = new JObject(new JProperty("Success", false),
                                            new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
            }
            context.Response.Write(historyResponse);
        }
        public void PhoneContactsSync(HttpContext context)
        {
            JObject responseJobj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(context.Request.InputStream);
            string _Payload = _IS.ReadToEnd();
            JObject _ParamObj = JObject.Parse(_Payload);
            Profile_V140 profObj = new Profile_V140();
            responseJobj = profObj.PhoneContactsSync(MyConf.MyConnectionString, _ParamObj, userId, context.Request.Headers["DeviceUniqueID"].ToString(), context.Request.Headers["DeviceName"].ToString());
            context.Response.Write(responseJobj);
        }
        public void InAppPurchaseHistory(HttpContext context)
        {
            JObject responseJobj = new JObject();
            Groups_V140 groupsObj = new Groups_V140();
            responseJobj = groupsObj.InAppPurchaseHistory(MyConf.MyConnectionString, userId);
            context.Response.Write(responseJobj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseJobj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void OtpCall(HttpContext context)
        {
            JObject responseObj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(context.Request.InputStream);
            string _Payload = _IS.ReadToEnd();
            JObject _ParamObj = JObject.Parse(_Payload);

            if (_ParamObj.SelectToken("MobileNumber") == null)
            {
                responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Unable to read MobileNumber"), new JProperty("ErrorCode", "123"));
                context.Response.Write(responseObj);
                return;
            }
            try
            {
                Login_V140 loginObj = new Login_V140();
                responseObj = loginObj.OtpCall(MyConf.MyConnectionString, _ParamObj.SelectToken("MobileNumber").ToString());

            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                       new JProperty("Message", ex.ToString()), new JProperty("ErrorCode", "101"));
                context.Response.Write(responseObj);
            }
            context.Response.Write(responseObj);
            //Logger.TraceLog("OtpCall response  " + responseObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void LeaveGroupCall(HttpContext context)
        {
            Logger.TraceLog("context");
            string accessToken = "";
            JObject outputLeaveGrpObj = new JObject();
            accessToken = context.Request.Headers["AccessToken"].ToString();
            short callManager = 0;
            try
            {
                if (string.IsNullOrEmpty(context.Request["isSecondaryModerator"]) == false)
                {
                    callManager = Convert.ToInt16(context.Request["isSecondaryModerator"]);
                }
                Groups_V140 groupsObj = new Groups_V140();
                outputLeaveGrpObj = groupsObj.MemberLeaveFromGrpCall(MyConf.MyConnectionString, Convert.ToInt32(context.Request["ConferenceID"]), userId, callManager);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("LeaveGroupCall : " + ex.ToString());
                outputLeaveGrpObj = new JObject(new JProperty("Success", false),
                       new JProperty("Message", "Something went wrong"), new JProperty("ErrorCode", "101"));
            }
            context.Response.Write(outputLeaveGrpObj);
            //Logger.TraceLog("LeaveGroupCall response  " + outputLeaveGrpObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(outputLeaveGrpObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void AndroidBuyCredits(HttpContext context)
        {
            Logger.TraceLog("AndroidBuyCredits Started ");
            JObject paramObj = new JObject();
            JObject responseObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            string _RequestLogPath = System.Configuration.ConfigurationManager.AppSettings["InAppPurchaseLogsPath"].ToString();
            string _AccessToken = context.Request.Headers["AccessToken"].ToString();
            context.Request.SaveAs(_RequestLogPath + _AccessToken + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", true);
            Logger.TraceLog("_RequestLogPath : " + _RequestLogPath + "_AccessToken : " + _AccessToken);
            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                    Logger.TraceLog("paramObj : " + paramObj);
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog("Android Buy Credits : " + ex.ToString());
                    responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Inavalid Request"));
                    context.Response.Write(responseObj);
                    return;
                }
                Groups_V140 groupsObj = new Groups_V140();
                responseObj = groupsObj.AndroidBuyCredits(MyConf.MyConnectionString, paramObj, countryID);
                Logger.TraceLog("res : " + responseObj.ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(responseObj);
            //Logger.TraceLog("Purchase Response :" + responseObj.ToString());
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void IOSBuyCredits(HttpContext context)
        {
            helperObj.SaveRequest(actionName);
            JObject paramObj = new JObject();
            JObject responseObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            string _RequestLogPath = System.Configuration.ConfigurationManager.AppSettings["InAppPurchaseLogsPath"].ToString();
            string _AccessToken = context.Request.Headers["AccessToken"].ToString();
            context.Request.SaveAs(_RequestLogPath + _AccessToken + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", true);

            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                }
                catch (Exception ex)
                {

                    responseObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Inavalid Request"), new JProperty("ErrorCode", "116"));
                    context.Response.Write(responseObj);
                    return;
                }

                Groups_V140 groupsObj = new Groups_V140();
                responseObj = groupsObj.IOSBuyCredits(MyConf.MyConnectionString, paramObj, countryID);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(responseObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseObj.ToString()));
            //RMQClient.enQueueApiStat();
        }

        private void Dial(HttpContext context)
        {
            JObject dialObj = new JObject();
            JObject dialResponse = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader dialStream = new StreamReader(context.Request.InputStream);
            string dialLoad = dialStream.ReadToEnd();
            string grpCallCallBackUrl = System.Configuration.ConfigurationManager.AppSettings["grpCallCallBackUrl"].ToString();
            try
            {
                dialObj = JObject.Parse(dialLoad);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at parsing Input Stream In Dial Method : " + ex.ToString());
                dialResponse = new JObject(new JProperty("Success", false),
                                            new JProperty("Message", "Invalid Data"), new JProperty("ErrorCode", "134"));
                context.Response.Write(dialResponse);
                return;
            }
            GroupCall_V140 groupCallObj = new GroupCall_V140();
            dialResponse = groupCallObj.ValidateGrpCallDial(MyConf.MyConnectionString, dialObj, userId, grpCallCallBackUrl, 0);
            context.Response.Write(dialResponse);
            Logger.TraceLog("Dial response  " + dialObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(dialResponse.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void MuteUnMute(HttpContext context)
        {
            JObject muteUnmuteObj = new JObject();
            JObject muteUnmuteRespObj = new JObject();
            context.Request.InputStream.Position = 0;

            try
            {
                StreamReader muteUnMuteStream = new StreamReader(context.Request.InputStream);
                muteUnmuteObj = JObject.Parse(muteUnMuteStream.ReadToEnd().ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at parsing Input Stream In Mute Method : " + ex.ToString());
                muteUnmuteRespObj = new JObject(new JProperty("Success", false),
                                           new JProperty("Message", "Invalid Data"), new JProperty("ErrorCode", "134"));
                context.Response.Write(muteUnmuteRespObj);
                return;
            }
            GroupCall_V140 groupCallObj = new GroupCall_V140();
            muteUnmuteRespObj = groupCallObj.ValidateMuteUnmute(MyConf.MyConnectionString, muteUnmuteObj, userId);
            context.Response.Write(muteUnmuteRespObj);
            //Logger.TraceLog("MuteUnMute response  " + muteUnmuteRespObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(muteUnmuteRespObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void Hangup(HttpContext context)
        {
            JObject hangupObj = new JObject();
            JObject hangupRespObj = new JObject();
            context.Request.InputStream.Position = 0;
            try
            {
                StreamReader hangupStream = new StreamReader(context.Request.InputStream);
                hangupObj = JObject.Parse(hangupStream.ReadToEnd().ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at parsing Input Stream In Hangup Method : " + ex.ToString());
                hangupRespObj = new JObject(new JProperty("Success", false),
                                           new JProperty("Message", "Invalid Data"), new JProperty("ErrorCode", "134"));
            }
            GroupCall_V140 groupCallObj = new GroupCall_V140();
            hangupRespObj = groupCallObj.ValidateHangUpAction(MyConf.MyConnectionString, hangupObj, userId);
            context.Response.Write(hangupRespObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(hangupRespObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void CreateGroupCall(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject createObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                }
                catch (Exception ex)
                {

                    createObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Inavalid Request"), new JProperty("ErrorCode", "116"));
                    //new JProperty("Message", ex.ToString()));
                    context.Response.Write(createObj);
                    return;
                }
                Groups_V140 groupsObj = new Groups_V140();
                //paramObj.Add(new JProperty("IsOnlyDialIn", 0));
                createObj = groupsObj.CreateGroupCall(MyConf.MyConnectionString, paramObj, userId);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(createObj);
            //Logger.TraceLog("CreateGroupCall response  " + createObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(createObj.ToString()));
            //RMQClient.enQueueApiStat();

        }
        public void EditGroupCall(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject editObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            Int16 fromApp = 1;
            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                }
                catch (Exception ex)
                {
                    editObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Inavalid Request"), new JProperty("ErrorCode", "116"));
                    context.Response.Write(editObj);
                    return;
                }
                if (paramObj.SelectToken("GroupID") == null)
                {
                    editObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Invalid Group Id"));

                }
                else
                {
                    Groups_V140 groupsObj = new Groups_V140();
                    editObj = groupsObj.EditGroupCall(MyConf.MyConnectionString, paramObj, userId, fromApp);
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(editObj);
            //Logger.TraceLog("EditGroupCall response  " + editObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(editObj.ToString()));
            //RMQClient.enQueueApiStat();

        }
        public void AddParticipantInGroupCall(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject addParticipantObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                Groups_V140 groupsObj = new Groups_V140();
                addParticipantObj = groupsObj.AddParticipantInGroupCall(MyConf.MyConnectionString, paramObj, userId);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(addParticipantObj);
            //Logger.TraceLog("AddParticipantInGroupCall response  " + addParticipantObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(addParticipantObj.ToString()));
            //RMQClient.enQueueApiStat();

        }
        public void GetAllGroupCalls(HttpContext context)
        {


            JObject groupCallsObj = new JObject();
            int userId = 0;
            string deviceToken = null;
            int appSource = 0;
            string appVersion = "";
            string timeStamp = context.Request["timestamp"];


            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    groupCallsObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(groupCallsObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                if (string.IsNullOrEmpty(context.Request["DeviceToken"]) == true)
                {
                    deviceToken = "";
                }
                else
                {
                    deviceToken = context.Request["DeviceToken"].ToString();
                }
                if (string.IsNullOrEmpty(context.Request["AppSource"]) == true)
                {
                    appSource = 0;
                }
                else
                {
                    appSource = int.Parse(context.Request["AppSource"].ToString());
                }
                if (string.IsNullOrEmpty(context.Request["timestamp"]) == true)
                {
                    timeStamp = "";
                }
                else
                {
                    timeStamp = context.Request["timestamp"].ToString();
                }

                if (string.IsNullOrEmpty(context.Request["AppVersion"]) == true)
                {
                    appVersion = "";
                }

                else
                {
                    appVersion = context.Request["AppVersion"].ToString();
                }

                Groups_V140 groupsObj = new Groups_V140();
                groupCallsObj = groupsObj.GetAllGroupCalls(MyConf.MyConnectionString, userId, appSource, deviceToken, appVersion, timeStamp, countryID);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }

            context.Response.Write(groupCallsObj);
            //Logger.TraceLog("GetAllGroupCalls response  " + groupCallsObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(groupCallsObj.ToString()));
            //RMQClient.enQueueApiStat();

        }
        public void DeleteGroupCall(HttpContext context)
        {
            JObject deleteCallObj = new JObject();
            int userId = 0;
            int conferenceid = 0;
            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    deleteCallObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));
                    context.Response.Write(deleteCallObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                if (Convert.ToInt32(context.Request["ConferenceID"]) == 0)
                {
                    deleteCallObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Invalid Conference"),
                    new JProperty("ErrorCode", "124"));
                    context.Response.Write(deleteCallObj);
                    context.Response.End();
                }
                Groups_V140 groupsObj = new Groups_V140();
                deleteCallObj = groupsObj.DeleteGroupCall(MyConf.MyConnectionString, Convert.ToInt32(context.Request["ConferenceID"]));

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(deleteCallObj);
            //Logger.TraceLog("DeleteGroupCall response  " + deleteCallObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(deleteCallObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void UpdateGroupCallName(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject updateCallNameObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();


            int userid = 0;
            int confid = 0;
            string confname = "";

            try
            {


                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    updateCallNameObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(updateCallNameObj);
                    context.Response.End();
                }
                else
                {
                    userid = Convert.ToInt32(userObj["UserID"]);
                }
                if (context.Request["ConferenceID"] == null)
                {
                    try
                    {
                        paramObj = JObject.Parse(payload);
                    }
                    catch (Exception ex)
                    {
                        Logger.ExceptionLog(ex.StackTrace);
                        context.Response.Write("Invalid Request");
                        context.Response.End();
                    }

                    confid = Convert.ToInt32(paramObj.SelectToken("ConferenceID"));
                    confname = paramObj.SelectToken("ConferenceName").ToString();
                }
                else
                {
                    confid = Convert.ToInt32(context.Request["ConferenceID"]);
                    confname = context.Request["ConferenceName"].ToString();
                }


                Groups_V140 groupsObj = new Groups_V140();
                updateCallNameObj = groupsObj.UpdateGroupCallName(MyConf.MyConnectionString,
                    userid, confid, confname);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(updateCallNameObj);
            //Logger.TraceLog("UpdateGroupCallName response  " + updateCallNameObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(updateCallNameObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void GetGroupCallRoomBackUp(HttpContext context)
        {
            JObject roomObj = new JObject();
            int userId = 0;
            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    roomObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(roomObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                Groups_V140 groupsObj = new Groups_V140();
                roomObj = groupsObj.GetGroupCallRoomBackUp(MyConf.MyConnectionString, Convert.ToInt32(context.Request["ConferenceID"]), userId);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(roomObj);
            //Logger.TraceLog("GetGroupCallRoom response  " + roomObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(roomObj.ToString()));
            //RMQClient.enQueueApiStat();

        }

        public void GetGroupCallRoom(HttpContext context)
        {
            JObject roomObj = new JObject();
            int userId = 0;
            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    roomObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(roomObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                Groups_V140 groupsObj = new Groups_V140();
                roomObj = groupsObj.GetGroupCallRoom(MyConf.MyConnectionString, Convert.ToInt32(context.Request["GroupCallID"]), Convert.ToInt32(context.Request["Type"]), Convert.ToInt32(context.Request["PageSize"]), Convert.ToInt32(context.Request["PageNumber"]), userId, context.Request["SearchText"].ToString());

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(roomObj);
            //Logger.TraceLog("GetGroupCallRoom response  " + roomObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(roomObj.ToString()));
            //RMQClient.enQueueApiStat();

        }
        public void UserBalance(HttpContext context)
        {
            JObject balanceObj = new JObject();
            int userId = 0;
            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    balanceObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(balanceObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                Profile_V140 profileObj = new Profile_V140();
                balanceObj = profileObj.UserBalance(MyConf.MyConnectionString, userId);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(balanceObj);
            //Logger.TraceLog("UserBalance response  " + balanceObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(balanceObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void GetReportsByBatchIdNew(HttpContext context)
        {
            JObject reportsObj = new JObject();
            int userId = 0;
            try
            {
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    reportsObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(reportsObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }
                GroupCallReports_V140 groupcallReportsObj = new GroupCallReports_V140();
                reportsObj = groupcallReportsObj.GetReportsByBatchIdNew(MyConf.MyConnectionString, userId, Convert.ToInt32(context.Request["ConferenceID"]), context.Request["BatchID"].ToString(), Convert.ToInt32(context.Request["PageNumber"]), Convert.ToInt32(context.Request["PageSize"]), context.Request["SearchText"].ToString());

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in AppServices_v2 is " + ex.ToString());
            }
            context.Response.Write(reportsObj);
            //Logger.TraceLog("GetReportsByBatchIdNew response  " + reportsObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(reportsObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        public void Registration(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject registrationReq = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            Boolean isResend = false;
            try
            {
                try
                {
                    paramObj = JObject.Parse(payload);
                }
                catch (Exception ex)
                {
                    Logger.ExceptionLog(ex.StackTrace);
                    context.Response.Write("Invalid Request");
                    context.Response.End();
                }
                string txnID = context.Request.Headers["TxnID"];
                string clientIpAddress = helperObj.ClientIpAddress;
                if (context.Request["IsResend"] != null)
                {
                    isResend = true;

                }
                if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+1"))
                {
                    countryID = 241;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+971"))
                {
                    countryID = 239;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+973"))
                {
                    countryID = 19;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+965"))
                {
                    countryID = 124;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+974"))
                {
                    countryID = 188;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+966"))
                {
                    countryID = 199;
                }
                else if (paramObj.SelectToken("MobileNumber").ToString().Replace(" ", "").StartsWith("+968"))
                {
                    countryID = 173;
                }
                else
                {
                    countryID = 108;
                }
                Login_V140 loginObj = new Login_V140();
                registrationReq = loginObj.Registration(MyConf.MyConnectionString, paramObj, clientIpAddress, txnID, isResend, countryID);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Appservice_v2 is " + ex.ToString());
                //Logger.TraceLog(ex.ToString());
            }
            context.Response.Write(registrationReq);
            //Logger.TraceLog("Registration response  " + registrationReq);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(registrationReq.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void ValidateOTP(HttpContext context)
        {
            JObject validateObj = new JObject();
            string txnID = context.Request.Headers["TxnID"];


            if (context.Request["Otp"] == null || context.Request["MobileNumber"] == null)
            {
                validateObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Unable to read OTP/MobileNumber"), new JProperty("ErrorCode", "123"));
                context.Response.Write(validateObj);
                return;
            }
            try
            {
                string mobile = context.Request["MobileNumber"].ToString();
                Login_V140 loginObj = new Login_V140();
                validateObj = loginObj.ValidateOTP(MyConf.MyConnectionString, mobile, context.Request["Otp"].ToString(), txnID, 0);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Appservice_v2 is " + ex.ToString());
            }
            context.Response.Write(validateObj);
            //Logger.TraceLog("ValidateOTP response  " + validateObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(validateObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void ProfileImage(HttpContext context)
        {
            JObject profileImageObj = new JObject();
            string tempStoragePath = "", tempFileName = "", image = "";
            int userId = 0;

            try
            {
                if (context.Request["image"] == null)
                {
                    profileImageObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Unable to read image"), new JProperty("ErrorCode", "121"));
                    context.Response.Write(profileImageObj);
                    return;
                }
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    profileImageObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(profileImageObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }


                tempStoragePath = HttpContext.Current.Server.MapPath("/TempImages/");
                tempFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";
                image = context.Request["image"].ToString();
                Profile_V140 profileObj = new Profile_V140();
                profileImageObj = profileObj.ProfileImage(image, tempStoragePath, tempFileName, MyConf.MyConnectionString, userId);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Appservice_v2 is " + ex.ToString());
            }
            context.Response.Write(profileImageObj);
            //Logger.TraceLog("ProfileImage response  " + profileImageObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(profileImageObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void UpdateProfile(HttpContext context)
        {
            JObject paramObj = new JObject();
            JObject updateProfileObj = new JObject();
            HttpContext.Current.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(HttpContext.Current.Request.InputStream);
            string payload = _IS.ReadToEnd();
            int userId = 0;
            string nickName = "", email = "", webSitrUrl = "", workNumber = "", company = "", offSetValue = "";
            try
            {
                //Logger.TraceLog("UserId :" + userObj["UserID"].ToString());
                if (string.IsNullOrEmpty(userObj["UserID"].ToString()) == true)
                {
                    updateProfileObj = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Mandatory parameter missing"),
                        new JProperty("ErrorCode", "111"));

                    context.Response.Write(updateProfileObj);
                    context.Response.End();
                }
                else
                {
                    userId = Convert.ToInt32(userObj["UserID"]);
                }

                if (context.Request["nickname"] == null)
                {
                    try
                    {
                        paramObj = JObject.Parse(payload);
                    }
                    catch (Exception ex)
                    {
                        Logger.ExceptionLog(ex.StackTrace);
                        context.Response.Write("Invalid Request");
                        context.Response.End();
                    }
                    nickName = paramObj.SelectToken("nickname").ToString();
                    email = paramObj.SelectToken("email").ToString();
                    webSitrUrl = paramObj.SelectToken("websiteurl").ToString();
                    workNumber = paramObj.SelectToken("worknumber").ToString();
                    company = paramObj.SelectToken("company").ToString();
                    if (paramObj.SelectToken("timezone") == null)
                    {
                        offSetValue = string.Empty;
                    }
                    else
                    {
                        offSetValue = paramObj.SelectToken("timezone").ToString();
                    }
                }
                else
                {
                    if (context.Request["timezone"] == null)
                    { offSetValue = string.Empty; }
                    else
                    {
                        offSetValue = context.Request["timezone"].ToString();
                    }
                    nickName = context.Request["nickname"].ToString();
                    email = context.Request["email"].ToString();
                    webSitrUrl = context.Request["websiteurl"].ToString();
                    workNumber = context.Request["worknumber"].ToString();
                    company = context.Request["company"].ToString();

                }


                Profile_V140 profileObj = new Profile_V140();
                updateProfileObj = profileObj.UpdateProfile(MyConf.MyConnectionString,
                    userId, nickName, email, offSetValue, workNumber, webSitrUrl, company);
                //Logger.TraceLog("Response :" + updateProfileObj.ToString());

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Appservice_v2 is " + ex.ToString());
            }
            context.Response.Write(updateProfileObj);
            //Logger.TraceLog("UpdateProfile response  " + updateProfileObj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(updateProfileObj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void GrpCallCancel(HttpContext context)
        {
            JObject resJobj = new JObject();
            try
            {
                Groups_V140 groupsObj = new Groups_V140();
                resJobj = groupsObj.GrpCallCancel(MyConf.MyConnectionString, Convert.ToInt32(context.Request["ConferenceID"]));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GrpCallCancel : " + ex.ToString());

            }


            context.Response.Write(resJobj);
            //Logger.TraceLog("GrpCallCancel response  " + resJobj);
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(resJobj.ToString()));
            //RMQClient.enQueueApiStat();
        }
        private void GetCountries(HttpContext context)
        {
            JObject responseJobj = new JObject();
            try
            {
                Profile_V140 profileObj = new Profile_V140();
                responseJobj = profileObj.GetCountries(MyConf.MyConnectionString);
                context.Response.Write(responseJobj);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In GetCountries : " + ex.ToString());
            }
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseJobj.ToString()));
            //RMQClient.enQueueApiStat();
            //Logger.TraceLog("GetCountries response  " + responseJobj);

        }
        private void CheckUserConfirmation(HttpContext context)
        {
            JObject responseJObj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader _IS = new StreamReader(context.Request.InputStream);
            string _Payload = _IS.ReadToEnd();
            JObject _ParamObj = JObject.Parse(_Payload);
            string fromNumber = "";
            try
            {


                Groups_V140 groupsObj = new Groups_V140();
                responseJObj = groupsObj.CheckUserConfirmation(MyConf.MyConnectionString, _ParamObj);
                context.Response.Write(responseJObj);
                //Logger.TraceLog("CheckUserConfirmation response  " + responseJObj);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In CheckUserConfirmation : " + ex.ToString());
            }
            HttpContext.Current.Items.Add("OutBytes", System.Text.Encoding.UTF8.GetByteCount(responseJObj.ToString()));
            //RMQClient.enQueueApiStat();


        }

        private void QRCodeLoginCheck(HttpContext context)
        {
            JObject responseObj = new JObject();
            JObject jObj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader sReader = new StreamReader(context.Request.InputStream);
            string payLoad = sReader.ReadToEnd();
            jObj = JObject.Parse(payLoad);

            try
            {

                Login_V140 loginObj = new Login_V140();
                responseObj = loginObj.QR_Code_Check(MyConf.MyConnectionString, jObj, userId);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In QRCodeLoginCheck AppService_V1.3.0 : " + ex.ToString());
            }

            context.Response.Write(responseObj.ToString());

        }

        private void QrCodeLogout(HttpContext context)
        {
            JObject jresponseObj = new JObject();
            JObject jObj = new JObject();
            context.Request.InputStream.Position = 0;
            StreamReader sr = new StreamReader(context.Request.InputStream);
            string payload = sr.ReadToEnd();
            try
            {
                jObj = JObject.Parse(payload);
                Login_V140 loginObj = new Login_V140();
                jresponseObj = loginObj.QrCodeLogout(jObj, MyConf.MyConnectionString, userId);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In QrCodeLogout AppService_V1.3.0 : " + ex.ToString());
            }
            Logger.TraceLog("QrCode response " + jresponseObj.ToString());
            context.Response.Write(jresponseObj.ToString());

        }

        private void WebLoginCheck(HttpContext context)
        {
            JObject responseObj = new JObject();
            try
            {
                GT.BusinessLogicLayer.V_1_4.Login_V140 loginObj = new Login_V140();
                responseObj = loginObj.WebLoginCheck(MyConf.MyConnectionString, userId);
            }
            catch (Exception ex)
            {

                Logger.ExceptionLog("Exception In WebLoginCheck AppService_V1.3.0 : " + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                      new JProperty("Message", "Something Went Wrong"));
            }
            context.Response.Write(responseObj);


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