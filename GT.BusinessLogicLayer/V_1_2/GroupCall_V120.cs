using System;
using System.Collections;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT.DataAccessLayer;
using System.Data;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using PusherServer;
using GT.Utilities;
using GT.Utilities.Properties;
using Microsoft.VisualBasic.Devices;
using PushSharp;
using PushSharp.Core;
using PushSharp.Apple;



namespace GT.BusinessLogicLayer.V_1_2
{
    public class GroupCall_V120
    {
        JObject grpCallValidateObject = new JObject();
        string connString = "";

        private Dictionary<string, string> deviceToken = new Dictionary<string, string>();
        private Dictionary<string, string> IosdeviceToken = new Dictionary<string, string>();
        /// <summary>
        /// This Function is used to Validate the input data for  groupcall dial
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="inputStrmObj"></param>
        /// <param name="userId"></param>
        /// <param name="grpCallCallBackUrl"></param>
        /// <returns></returns>
        /// 

        public JObject ValidateGrpCallDial(string sConnString, JObject inputStrmObj, int userId, string grpCallCallBackUrl)
        {
            connString = sConnString;
            JObject dialResponse = new JObject();
            grpcall grpCallPropertiesObj = new grpcall();
            BusinessHelper helperObj = new BusinessHelper();
            int grpCallId = 0;
            string mobileNumber = "";
            bool isAll = false;
            bool isMute = false;
            bool isAutoDial = false;
            try
            {
                if (inputStrmObj.SelectToken("IsAutoDial") != null && string.IsNullOrEmpty(inputStrmObj.SelectToken("IsAutoDial").ToString()) == false)
                {
                    string validateResp = null;
                    validateResp = ValidateReq(sConnString, userId, inputStrmObj.SelectToken("ConferenceID").ToString(), inputStrmObj.SelectToken("Token").ToString());

                    if (validateResp == "SUCCESS")
                    {
                        isAutoDial = true;
                        grpCallPropertiesObj.AutoDialTocken = inputStrmObj.SelectToken("Token").ToString();
                    }
                    else
                    {
                        isAutoDial = false;
                    }
                }
                if (string.IsNullOrEmpty(userId.ToString()) == true)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", "Mandatory parameter missing");
                    helperObj.NewProperty("ErrorCode", "E0001");
                    return helperObj.GetResponse();
                }
                else
                {
                    grpCallPropertiesObj.UserId = userId;
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("ValidateGrpCallDial : " + ex.ToString());
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "Something Wrong with the server ");
                helperObj.NewProperty("ErrorCode", "E0002");
                return helperObj.GetResponse();
            }
            try
            {
                grpCallId = Convert.ToInt32(inputStrmObj.SelectToken("ConferenceID").ToString());
                grpCallPropertiesObj.ConferenceId = grpCallId;
                if (Convert.ToBoolean(inputStrmObj.SelectToken("IsAll").ToString()) == true)
                {
                    isAll = true;
                }
                else
                {
                    if (inputStrmObj.SelectToken("MobileNumber") == null)
                    {
                        helperObj.NewProperty("Success", false);
                        helperObj.NewProperty("Message", "MobileNumber Is Missing");
                        helperObj.NewProperty("ErrorCode", "E0001");
                        return helperObj.GetResponse();
                    }
                    mobileNumber = inputStrmObj.SelectToken("MobileNumber").ToString();
                }
                if (inputStrmObj.SelectToken("IsMute") != null)
                {
                    isMute = Convert.ToBoolean(inputStrmObj.SelectToken("IsMute").ToString());
                }
                if (isAll)
                {
                    grpCallPropertiesObj.IsAll = true;
                    if (isMute)
                    {
                        grpCallPropertiesObj.ConferenceAction = "MUTE_DIAL_ALL";
                        grpCallPropertiesObj.IsMute = true;
                    }
                    else
                    {
                        grpCallPropertiesObj.ConferenceAction = "UNMUTE_DIAL_ALL";
                        grpCallPropertiesObj.IsMute = false;
                    }
                }
                else
                {
                    grpCallPropertiesObj.IsAll = false;
                    grpCallPropertiesObj.MobileNumber = mobileNumber;
                    grpCallPropertiesObj.IsAutodial = Convert.ToInt16(isAutoDial);
                    if (isMute)
                    {
                        grpCallPropertiesObj.ConferenceAction = "MUTE_DIAL";
                        grpCallPropertiesObj.IsMute = true;
                    }
                    else
                    {
                        grpCallPropertiesObj.ConferenceAction = "UNMUTE_DIAL";
                        grpCallPropertiesObj.IsMute = false;
                    }
                }
                if (string.IsNullOrEmpty(inputStrmObj.SelectToken("IsCallFromBonus").ToString()) == false && Convert.ToInt16(inputStrmObj.SelectToken("IsCallFromBonus").ToString()) == 1)
                {
                    grpCallPropertiesObj.IsCallFromBonus = true;
                }
                else
                {
                    grpCallPropertiesObj.IsCallFromBonus = false;
                }
                ValidateGrpCallActions(grpCallPropertiesObj, sConnString);
                if (Convert.ToBoolean(grpCallValidateObject.SelectToken("Success").ToString()) == false)
                {
                    helperObj.NewProperty("Success", false);
                    helperObj.NewProperty("Message", grpCallValidateObject.SelectToken("Message").ToString());
                    return helperObj.GetResponse();
                }
                SetGrpCallVariables(grpCallValidateObject, grpCallPropertiesObj);
                JObject DialResponse = new JObject();
                DataTable NodeDependedMobileNumberReportIdTable = new DataTable();
                DataTable NodeDependendGateWaysTable = new DataTable();
                DialResponse = Dial(sConnString, grpCallPropertiesObj, grpCallCallBackUrl, NodeDependedMobileNumberReportIdTable, NodeDependendGateWaysTable);


                return DialResponse;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Dial Method In GrpcallBusiness : " + ex.ToString());
                helperObj.NewProperty("Success", false);
                helperObj.NewProperty("Message", "Something Wrong with the server ");
                helperObj.NewProperty("ErrorCode", "E0002");
                return helperObj.GetResponse();

            }
        }
        /// <summary>
        /// THis Function Is Used To Dial THe grpcall
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="grpCallProp"></param>
        /// <param name="grpCallCallBackUrl"></param>
        /// <param name="NodeDependedMobileNumberReportIdTable"></param>
        /// <param name="NodeDependendGateWaysTable"></param>
        /// <returns></returns>

        public JObject Dial(string sConnString, grpcall grpCallProp, string grpCallCallBackUrl, DataTable NodeDependedMobileNumberReportIdTable, DataTable NodeDependendGateWaysTable)
        {

            string _Xml = "<Response><Hangup/></Response>";
            JObject dialResp = new JObject();
            JObject dialApiResObj = new JObject();

            DataTable ReportIdMobileNumberTable = new DataTable();
            DataTable GateWayDetailsTable = new DataTable();
            DataTable reportIdRequestUUIDTable = new DataTable();
            JArray RequestUUIdsArr = new JArray();
            string ReportIds = "";
            reportIdRequestUUIDTable.Columns.Add("ReportId", typeof(int));
            reportIdRequestUUIDTable.Columns.Add("RequestUUID", typeof(string));
            JObject SuccessStatusGateways = new JObject();


            string mobileNumbers = "";
            short retVal = 0;
            string retMsg = "";
            string sendDigits = "";
            int durationLimit = 0;
            int TimeLimit = 0;
            int ispaidClient = 0;
            string sendDigitsString="";
            short isCallFromBonus = 0;
            DataSet dsDial = new DataSet();

            try
            {
                if (!(NodeDependedMobileNumberReportIdTable.Rows.Count > 0))
                {
                    DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                    dsDial = groupcallObj.GrpCallDialEnt(grpCallProp, out retVal, out retMsg, out durationLimit, out isCallFromBonus, out ispaidClient,out sendDigitsString);
                    grpCallProp.IsCallFromBonus = Convert.ToBoolean(isCallFromBonus);
                }
                if (retVal == 1 | NodeDependedMobileNumberReportIdTable.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(grpCallProp.IsAll) == true)
                    {
                        PusherNotifier notifierObj = new PusherNotifier();
                        notifierObj.IsStarted = 1;
                        notifierObj.GrpCallID = grpCallProp.ConferenceId;
                        MobileNotifier(sConnString, notifierObj);
                    }
                    grpCallProp.TimeLimit = durationLimit;
                    grpCallProp.IsPaidClient = ispaidClient;
                    if (dsDial.Tables.Count > 0) {
                        ReportIdMobileNumberTable = dsDial.Tables[0];
                        GateWayDetailsTable = dsDial.Tables[1];
                    }
                    else if (NodeDependedMobileNumberReportIdTable.Rows.Count > 0)
                    {
                        ReportIdMobileNumberTable = NodeDependedMobileNumberReportIdTable;
                        GateWayDetailsTable = NodeDependendGateWaysTable;
                    }
                    if (ReportIdMobileNumberTable.Rows.Count > 0)
                    {
                        
                        for (int i = 0; i <= ReportIdMobileNumberTable.Rows.Count - 1; i++)
                        {
                            mobileNumbers = ReportIdMobileNumberTable.Rows[i]["MobileNumbers"].ToString();
                            mobileNumbers = "@" + mobileNumbers;
                            if (Convert.ToBoolean(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["isCountryPrefixAllowed"]) == false)
                            {
                                mobileNumbers = mobileNumbers.Replace("@" + GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["CountryPrefix"], "@" + GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["GatewayId"])[0]["DialPrefix"]);
                            }
                            
                            if (Convert.ToBoolean(ReportIdMobileNumberTable.Rows[i]["IsModerator"]) == true)
                            {
                                _Xml = GetConferenceXml(true, false, grpCallProp, grpCallCallBackUrl);
                            }
                            else if (Convert.ToBoolean(ReportIdMobileNumberTable.Rows[i]["IsInterConnect"]) == true)
                            {
                                _Xml = GetConferenceXml(false, grpCallProp.IsMute, grpCallProp, grpCallCallBackUrl, true,sendDigitsString);
                            }
                            else
                            {
                                _Xml = GetConferenceXml(false, grpCallProp.IsMute, grpCallProp, grpCallCallBackUrl);
                            }

                            mobileNumbers = mobileNumbers.Right(mobileNumbers.Length - 1);
                            ReportIds = ReportIdMobileNumberTable.Rows[i]["ReportId"].ToString();
                            grpCallProp.GatewayID = Convert.ToInt32(GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"].ToString())[0]["GatewayId"]);
                            grpCallProp.HttpConferenceApiUrl = GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["HttpUrl"].ToString();
                            grpCallProp.OriginationUrl = GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["OriginationUrl"].ToString();
                            grpCallProp.CallerIdNumber = GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["OriginationCallerID"].ToString();
                            grpCallProp.ExtraDialString = GateWayDetailsTable.Select("GatewayId=" + ReportIdMobileNumberTable.Rows[i]["gatewayid"])[0]["ExtraDialString"].ToString();
                            grpCallProp.BulkDialDelimiter = "@";

                            if (grpCallProp.GatewayID != 4)
                            {
                                dialApiResObj = DialAPI(mobileNumbers, _Xml, grpCallProp, sConnString, grpCallCallBackUrl, ReportIds);
                            }
                            else
                            {
                                PlivoClientBusiness plivoClientObj = new PlivoClientBusiness();
                                if (mobileNumbers.Split(grpCallProp.BulkDialDelimiter.ToCharArray()).Length > 1)
                                {
                                    dialApiResObj = plivoClientObj.MakeBulkCall(grpCallProp.CallerIdNumber, mobileNumbers.ToString(), grpCallProp.BulkDialDelimiter);
                                }
                                else
                                {
                                    dialApiResObj = plivoClientObj.MakeCall(grpCallProp.CallerIdNumber, mobileNumbers.ToString());
                                }
                            }

                            if (Convert.ToBoolean(dialApiResObj.SelectToken("Success").ToString()) == false)
                            {
                                Logger.ExceptionLog("Dial BLL DialApi Response = " + dialApiResObj.SelectToken("Message").ToString());
                                dialResp.Add(new JProperty("Success", false));
                                dialResp.Add(new JProperty("Message", "server error"));
                                return dialResp;
                            }
                            else
                            {
                                RequestUUIdsArr = JArray.Parse(dialApiResObj.SelectToken("RequestUUIDs").ToString());

                                string[] FReportIds = ReportIdMobileNumberTable.Rows[i]["ReportId"].ToString().Split("@".ToCharArray());
                                for (int r = 0; r <= FReportIds.Length - 1; r++)
                                {
                                    reportIdRequestUUIDTable.Rows.Add(FReportIds[r], RequestUUIdsArr[r]);
                                }
                                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                                groupcallObj.UpdateDialResponse(grpCallProp, "123", reportIdRequestUUIDTable);
                                reportIdRequestUUIDTable.Rows.Clear();

                            }
                        }
                        dialResp.Add(new JProperty("Success", true));
                        dialResp.Add(new JProperty("Message", "OK"));
                    }
                    else
                    {
                        dialResp.Add(new JProperty("Success", false));
                        dialResp.Add(new JProperty("Message", "Member Not Found"));
                        return dialResp;
                    }

                }
                else
                {
                    dialResp.Add(new JProperty("Success", false));
                    dialResp.Add(new JProperty("Message", "" + retMsg));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at Dial BLL : " + ex.ToString());
                dialResp.Add(new JProperty("Success", false));
                dialResp.Add(new JProperty("Message", "Something Went Wrong"));
            }
            return dialResp;
        }

        /// <summary>
        /// This Function is used For Send Webrequest for GroupCall
        /// </summary>
        /// <param name="mobileNumbers"></param>
        /// <param name="_Xml"></param>
        /// <param name="dApiGrpCall"></param>
        /// <param name="sConnString"></param>
        /// <param name="grpCallCallBackUrl"></param>
        /// <param name="reportIds"></param>
        /// <returns></returns>
        public JObject DialAPI(string mobileNumbers, string _Xml, grpcall dApiGrpCall, string sConnString, string grpCallCallBackUrl, string reportIds = null)
        {
            WebRequest webReqObj = null;
            HttpWebResponse _Resp = null;
            StreamReader sReader = null;
            StreamWriter sWriter = null;
            string postData = "";
            string httpAPIResponseString = "";
            JObject resultObj = new JObject();
            JObject responseObj = new JObject();
            JArray requestUUIDs = new JArray();
            int totalCount = 0;
            StringBuilder originationUrlNew = new StringBuilder();
            short retryAttempt = 0;

        ConnectRetry:
            try
            {

                if (mobileNumbers.Split(dApiGrpCall.BulkDialDelimiter.ToCharArray()).Length > 1)
                {
                    webReqObj = HttpWebRequest.Create(dApiGrpCall.HttpConferenceApiUrl.ToString() + "BulkCall/");
                    totalCount = mobileNumbers.Split(dApiGrpCall.BulkDialDelimiter.ToCharArray()).Count();
                }
                else
                {
                    webReqObj = HttpWebRequest.Create(dApiGrpCall.HttpConferenceApiUrl + "Call/");
                    totalCount = 1;
                }
                if (totalCount > 1)
                {
                    originationUrlNew.Clear();

                    for (int i = 1; i <= totalCount; i++)
                    {
                        originationUrlNew.Append(dApiGrpCall.OriginationUrl + dApiGrpCall.BulkDialDelimiter);
                    }
                    originationUrlNew = new StringBuilder(originationUrlNew.ToString().Left(originationUrlNew.Length - 1));
                }
                else
                {
                    originationUrlNew = new StringBuilder(dApiGrpCall.OriginationUrl);
                }
                webReqObj.Method = "POST";
                webReqObj.ContentType = "application/x-www-form-urlencoded";
                postData = "";
                if (totalCount > 1)
                {
                    postData = "Delimiter=" + dApiGrpCall.BulkDialDelimiter + "&";
                }

                if (dApiGrpCall.CallerIdNumber.StartsWith("971440571"))
                {
                    dApiGrpCall.CallerIdNumber = dApiGrpCall.CallerIdNumber.Right(4);
                }
                else
                {
                    dApiGrpCall.CallerIdNumber = dApiGrpCall.CallerIdNumber;
                }


                postData = postData + "Priority=H&From=" + HttpUtility.UrlEncode(dApiGrpCall.CallerIdNumber) + "&To=" + HttpUtility.UrlEncode(mobileNumbers.ToString()) + "&AnswerUrl=" + grpCallCallBackUrl + "&SequenceNumber=" + reportIds.ToString() + "&Gateways=" + HttpUtility.UrlEncode(originationUrlNew.ToString()) + "&HangupUrl=" + grpCallCallBackUrl + "&ExtraDialString='" + dApiGrpCall.ExtraDialString + ",drb_calluuid=" + dApiGrpCall.CallUUID + "'&AnswerXml=" + _Xml;
                sWriter = new StreamWriter(webReqObj.GetRequestStream());
                sWriter.Write(postData);
                sWriter.Flush();
                sWriter.Close();
                sReader = new StreamReader(webReqObj.GetResponse().GetResponseStream());
                httpAPIResponseString = sReader.ReadToEnd();
                resultObj = JObject.Parse(httpAPIResponseString);
                bool IsApiCallSuccess = false;
                IsApiCallSuccess = Convert.ToBoolean(resultObj.SelectToken("Success").ToString());
                responseObj = new JObject();
                if (IsApiCallSuccess == true)
                {
                    responseObj.Add(new JProperty("Success", true));
                    foreach (string UUID in resultObj.SelectToken("RequestUUID").ToString().Replace("[", "").Replace("]", "").Replace("\"", "").Split(",".ToCharArray()))
                    {
                        requestUUIDs.Add(UUID);
                    }
                    responseObj.Add("RequestUUIDs", requestUUIDs);
                }
                else
                {
                    //logclass.SimpleLog(ResultObj)
                    responseObj.Add(new JProperty("Success", false));
                    responseObj.Add(new JProperty("Message", mobileNumbers));
                }
            }
            catch (System.Net.WebException ax)

            {
                Logger.ExceptionLog("DailApi Exception " + ax.ToString());
                retryAttempt += 1;
                if (retryAttempt < 3)
                {
                    System.Threading.Thread.Sleep(1000);
                    goto ConnectRetry;
                }
                else
                {
                    
                    DataTable tableReportIds = new DataTable();
                    tableReportIds.Columns.Add("Id", typeof(int));

                    foreach (string _id in reportIds.ToString().Split(dApiGrpCall.BulkDialDelimiter.ToCharArray()))
                    {
                        tableReportIds.Rows.Add(_id);
                    }
                    DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                    groupcallObj.UpdateSystemDownReports(tableReportIds, "NODE_DOWN");

                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "System Down"));
                }
                //DialAPI(MobileNumbers, _Xml)
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("DailApi grpCallBusiness BLL : " + ex.ToString());
                Logger.ExceptionLog("uPDATING sYSTEM dOWN,ConferenceId " + dApiGrpCall.ConferenceId + ",ReportIds : " + reportIds.ToString());
                DataTable tableReportIds = new DataTable();
                tableReportIds.Columns.Add("Id", typeof(int));
                foreach (string _id in tableReportIds.ToString().Split(dApiGrpCall.BulkDialDelimiter.ToCharArray()))
                {
                    tableReportIds.Rows.Add(_id);
                }
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                groupcallObj.UpdateSystemDownReports(tableReportIds, "NODE_DOWN");

                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Error"));
            }
            return responseObj;
        }

        /// <summary>
        /// This Function Is Used To Validate Input Data For Mute and Unmute Of GrpCall
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="grpCallPropObj"></param>
        /// <returns></returns>
        public JObject ValidateMuteUnmute(string sConnString, JObject muteUnmuteObject, int userId)
        {
            int conferenceId = 0;
            bool isAll = false;
            bool isMute = false;
            string action = "";
            string mobileNumber = "";
            grpcall grpCallProp = new grpcall();
            BusinessHelper helper = new BusinessHelper();
            JObject MuteUnMuteResponse = new JObject();
            try
            {
                conferenceId = Convert.ToInt32(muteUnmuteObject.SelectToken("ConferenceID").ToString());
                isAll = Convert.ToBoolean(muteUnmuteObject.SelectToken("IsAll").ToString().ToUpper());
                if (isAll == true)
                {
                    grpCallProp.IsAll = true;
                }
                else
                {
                    if (muteUnmuteObject.SelectToken("MobileNumber") == null)
                    {
                        helper.NewProperty("Success", false);
                        helper.NewProperty("Message", "MobileNumber Is Missing");
                        helper.NewProperty("ErrorCode", "E0001");
                        return helper.GetResponse();
                    }
                    mobileNumber = muteUnmuteObject.SelectToken("MobileNumber").ToString();
                }
                if (muteUnmuteObject.SelectToken("IsMute") != null)
                {
                    isMute = Convert.ToBoolean(muteUnmuteObject.SelectToken("IsMute").ToString());
                }
                grpCallProp.ConferenceId = conferenceId;
                if (isMute)
                {
                    grpCallProp.IsMute = true;
                    if (isAll)
                    {
                        grpCallProp.IsAll = true;
                        action = "MUTE_ALL";
                    }
                    else
                    {
                        grpCallProp.IsAll = false;
                        grpCallProp.MobileNumber = mobileNumber;
                        action = "MUTE_MEMBER";
                    }
                }
                else
                {
                    grpCallProp.IsMute = false;
                    if (isAll)
                    {
                        grpCallProp.IsAll = true;
                        action = "UNMUTE_ALL";
                    }
                    else
                    {
                        grpCallProp.IsAll = false;
                        grpCallProp.MobileNumber = mobileNumber;
                        action = "UNMUTE_MEMBER";
                    }
                }
                ValidateGrpCallActions(grpCallProp, sConnString);
                if (Convert.ToBoolean(grpCallValidateObject.SelectToken("Success").ToString()) == false)
                {
                    helper.NewProperty("Success", false);
                    helper.NewProperty("Message", grpCallValidateObject.SelectToken("Message").ToString());
                    return helper.GetResponse();
                }
                grpCallProp.ConferenceAction = action;
                grpCallProp.UserId = userId;
                SetGrpCallVariables(grpCallValidateObject, grpCallProp);

                MuteUnMuteResponse = GrpCallMuteUnmte(sConnString, grpCallProp);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("GrpCallMuteUnmute : " + ex.ToString());
                helper.NewProperty("Success", false);
                helper.NewProperty("Message", "Server Internal Error");
                return helper.GetResponse();
            }
            return MuteUnMuteResponse;
        }
        /// <summary>
        /// This Function Is Used To Get GrpCall related Info To Mute and Unmute the Call
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="grpCallPropObj"></param>
        /// <returns></returns>
        private JObject GrpCallMuteUnmte(string sConnString, grpcall grpCallPropObj)
        {
            JObject respObj = new JObject();
            DataSet sqlDsMuteUnmutem = default(DataSet);
            string memberIds = "";
            int retval;
            string retmsg = "";
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                sqlDsMuteUnmutem = groupcallObj.MuteUnmute(grpCallPropObj, out retval, out retmsg);
                DataTable muteUnmuteMembersTable = new DataTable();
                DataTable muteUnmuteGatewaysTable = new DataTable();
                if (retval != 1)
                {
                    respObj.Add(new JProperty("Success", false));
                    respObj.Add(new JProperty("Message", retmsg));
                    return respObj;
                }
                muteUnmuteMembersTable = sqlDsMuteUnmutem.Tables[0];
                muteUnmuteGatewaysTable = sqlDsMuteUnmutem.Tables[1];
                if (muteUnmuteMembersTable.Rows.Count > 0)
                {
                    for (int i = 0; i <= muteUnmuteMembersTable.Rows.Count - 1; i++)
                    {
                        memberIds = muteUnmuteMembersTable.Rows[i]["MemeberId"].ToString();
                        grpCallPropObj.HttpConferenceApiUrl = muteUnmuteGatewaysTable.Rows[i]["HttpURL"].ToString();
                        grpCallPropObj.GatewayID = Convert.ToInt16(muteUnmuteGatewaysTable.Rows[i]["GatewayID"]);
                        if (string.IsNullOrEmpty(memberIds.Trim()))
                        {
                            respObj.Add(new JProperty("Success", false));
                            respObj.Add(new JProperty("Message", "Unable to perform action now"));
                        }
                        else
                        {
                            if (grpCallPropObj.GatewayID != 4)
                            {
                                respObj = MuteUnmuteApi(memberIds, grpCallPropObj);
                            }
                            else
                            {
                                PlivoClientBusiness _PlivoClient = new PlivoClientBusiness();
                                if (grpCallPropObj.IsMute == true)
                                {
                                    respObj = _PlivoClient.ConferenceMute(grpCallPropObj.ConferenceRoom, memberIds);
                                }
                                else
                                {
                                    respObj = _PlivoClient.ConferenceUnMute(grpCallPropObj.ConferenceRoom, memberIds);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                respObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Server Error shashi"));
            }
            return respObj;
        }

        /// <summary>
        /// This Function Is Used To Make a Webrequest To Mute And Unmute The Call
        /// </summary>
        /// <param name="memberIds"></param>
        /// <param name="grpCallPropObject"></param>
        /// <returns></returns>
        public JObject MuteUnmuteApi(string memberIds, grpcall grpCallPropObject)
        {
            JObject respObj = new JObject();
            WebRequest webReqObj = null;
            HttpWebResponse webRespObj = null;
            StreamReader webSReader = null;
            StreamWriter webSWriter = null;
            string httpApiResponseString = "";
            string postingData = "";
            try
            {
                if (grpCallPropObject.IsMute == true)
                {
                    webReqObj = HttpWebRequest.Create(grpCallPropObject.HttpConferenceApiUrl.ToString() + "ConferenceMute/");
                }
                else
                {
                    webReqObj = HttpWebRequest.Create(grpCallPropObject.HttpConferenceApiUrl.ToString() + "ConferenceUnmute/");
                }
                postingData = "ConferenceName=" + grpCallPropObject.ConferenceRoom + "&MemberID=" + memberIds;
                webReqObj.Method = "POST";
                //_Req.KeepAlive = false;
                webReqObj.ContentType = "application/x-www-form-urlencoded";
                webSWriter = new StreamWriter(webReqObj.GetRequestStream());
                webSWriter.Write(postingData);
                webSWriter.Flush();
                webSWriter.Close();
                webSReader = new StreamReader(webReqObj.GetResponse().GetResponseStream());
                httpApiResponseString = webSReader.ReadToEnd();
                Pusher_Conference_MuteUnMuteAndDeafUnDeaf(grpCallPropObject.MemberName, httpApiResponseString, grpCallPropObject.ConferenceRoom, grpCallPropObject.IsMute, "Mute", grpCallPropObject.IsAll);
                respObj = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                respObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Cannot mute at this moment : "));
            }
            return respObj;
        }

        /// <summary>
        /// This Function Is Used To Validate Input Data For Hangup The Call
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="hangUpObj"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public JObject ValidateHangUpAction(string sConnString, JObject hangUpObj, int userId)
        {

            int grpCallId = 0;
            string mobileNumber = "";
            bool isAll = false;
            string action = "";

            JObject hanUpRespObj = new JObject();
            JObject HangupResponse = new JObject();
            BusinessHelper helper = new BusinessHelper();
            grpcall grpCallPropObj = new grpcall();

            grpCallId = Convert.ToInt32(hangUpObj.SelectToken("ConferenceID").ToString());
            if (Convert.ToBoolean(hangUpObj.SelectToken("IsAll").ToString().ToUpper()) == true)
            {
                isAll = true;
            }
            else
            {
                if (hangUpObj.SelectToken("MobileNumber") == null)
                {
                    helper.NewProperty("Success", false);
                    helper.NewProperty("Message", "MobileNumber Is Missing");
                    helper.NewProperty("ErrorCode", "E0001");
                    return helper.GetResponse();
                }
                mobileNumber = hangUpObj.SelectToken("MobileNumber").ToString();
            }
            grpCallPropObj.ConferenceId = grpCallId;
            if (isAll)
            {
                grpCallPropObj.IsAll = true;
                action = "HANGUP_ALL";
            }
            else
            {
                grpCallPropObj.IsAll = false;
                grpCallPropObj.MobileNumber = mobileNumber;
                action = "HANGUP_MEMBER";
            }
            ValidateGrpCallActions(grpCallPropObj, sConnString);
            if (Convert.ToBoolean(grpCallValidateObject.SelectToken("Success").ToString()) == false)
            {
                helper.NewProperty("Success", false);
                helper.NewProperty("Message", grpCallValidateObject.SelectToken("Message").ToString());
                return helper.GetResponse();

            }
            grpCallPropObj.ConferenceAction = action;
            grpCallPropObj.UserId = userId;
            SetGrpCallVariables(grpCallValidateObject, grpCallPropObj);
            hanUpRespObj = GrpCallHanUp(sConnString, grpCallPropObj);
            if (isAll == true)
            {
                PusherNotifier notifierObj = new PusherNotifier();
                notifierObj.GrpCallID = grpCallId;
                notifierObj.IsStarted = 0;
                MobileNotifier(sConnString, notifierObj);
            }

            return hanUpRespObj;
        }
        /// <summary>
        /// This Funtion Is Used To Get The GrpCall Related Info For Hangup The Call
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="GrpCallPropObj"></param>
        /// <returns></returns>
        public JObject GrpCallHanUp(string sConnString, grpcall GrpCallPropObj)
        {
            JObject hangupResponse = null;
            DataSet sqlDsHangup = null;
            short retval;
            string retmsg = "";
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                sqlDsHangup = groupcallObj.Hangup(GrpCallPropObj, out retval, out retmsg);
                if (retval != 1)
                {
                    hangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", retmsg));
                    return hangupResponse;
                }
                if (sqlDsHangup.Tables.Count > 0)
                {
                    if (sqlDsHangup.Tables[0].Rows.Count == 0)
                    {
                        hangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Members Found"));
                    }
                    else
                    {
                        for (int i = 0; i <= sqlDsHangup.Tables[0].Rows.Count - 1; i++)
                        {
                            GrpCallPropObj.GatewayID = Convert.ToInt16(sqlDsHangup.Tables[1].Rows[i]["GatewayId"]);
                            GrpCallPropObj.HttpConferenceApiUrl = Convert.ToString(sqlDsHangup.Tables[1].Rows[i]["HttpURL"]);
                            if (GrpCallPropObj.GatewayID != 4)
                            {
                                hangupResponse = HangupApi(sqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString(), sqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString(), GrpCallPropObj, sConnString);
                            }
                            else
                            {
                                string _PInput = "";
                                PlivoClientBusiness _PlivoClient = new PlivoClientBusiness();
                                if (GrpCallPropObj.IsAll == true)
                                {
                                    hangupResponse = _PlivoClient.KillConference(GrpCallPropObj.ConferenceRoom);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(sqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString()))
                                    {
                                        _PInput = GetPlivoCallUUIDs(sqlDsHangup.Tables[0].Rows[i]["MemeberId"].ToString(), 1, GrpCallPropObj, sConnString);
                                        hangupResponse = _PlivoClient.HangupCalls(_PInput);
                                    }
                                    if (!string.IsNullOrEmpty(sqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString()))
                                    {
                                        _PInput = GetPlivoCallUUIDs(sqlDsHangup.Tables[0].Rows[i]["RequestUUID"].ToString(), 0, GrpCallPropObj, sConnString);
                                        hangupResponse = _PlivoClient.HangupCalls(_PInput);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    hangupResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Table Returned"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                hangupResponse = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Server Error"));
            }
            finally
            {

            }
            return hangupResponse;
        }
        /// <summary>
        /// This Function Is Used To Make a Webrequest To Hangup The Call
        /// </summary>
        /// <param name="requestUUIds"></param>
        /// <param name="memberIds"></param>
        /// <param name="grpCallObj"></param>
        /// <param name="sConnString"></param>
        /// <returns></returns>
        private JObject HangupApi(string requestUUIds, string memberIds, grpcall grpCallObj, string sConnString)
        {
            JObject hanupresObj = null;
            WebRequest webReq = null;
            StreamReader sReader = null;
            StreamWriter sWriter = null;
            string postingData = "";
            string httpAPIResponseString = "";
            JObject responseObj = new JObject();
            try
            {
                if (!string.IsNullOrEmpty(memberIds.Trim()))
                {
                    webReq = HttpWebRequest.Create(grpCallObj.HttpConferenceApiUrl + "ConferenceHangup/");
                    webReq.Method = "POST";
                    webReq.ContentType = "application/x-www-form-urlencoded";
                    postingData = "ConferenceName=" + grpCallObj.ConferenceRoom + "&MemberID=" + memberIds;
                    sWriter = new StreamWriter(webReq.GetRequestStream());
                    sWriter.Write(postingData);
                    sWriter.Flush();
                    sWriter.Close();
                    sReader = new StreamReader(webReq.GetResponse().GetResponseStream());
                    httpAPIResponseString = sReader.ReadToEnd();
                    sReader.Close();
                    responseObj = JObject.Parse(httpAPIResponseString);
                    if (grpCallObj.IsAll == true)
                    {
                        if (Convert.ToBoolean(responseObj.SelectToken("Success").ToString()) == true)
                        {
                            //StopConferenceRecord(sConnString, grpCallObj.ConferenceId);
                        }
                    }
                }
                if (!string.IsNullOrEmpty(requestUUIds.Trim()))
                {
                    foreach (string Uid in requestUUIds.Split(','))
                    {
                        try
                        {
                            webReq = WebRequest.Create(grpCallObj.HttpConferenceApiUrl + "HangupCall/");
                            webReq.Method = "POST";
                            webReq.Timeout = 10000;
                            //_Req.KeepAlive = false;
                            webReq.ContentType = "application/x-www-form-urlencoded";
                            postingData = "RequestUUID=" + Uid;
                            sWriter = new StreamWriter(webReq.GetRequestStream());
                            sWriter.Write(postingData);
                            sWriter.Flush();
                            sWriter.Close();
                            sReader = new StreamReader(webReq.GetResponse().GetResponseStream());
                            httpAPIResponseString = sReader.ReadToEnd();
                            sReader.Close();
                        }
                        catch (Exception ex)
                        {
                            Logger.ExceptionLog("Hangup Api : " + ex.ToString());
                            continue;
                        }
                    }
                }
                hanupresObj = new JObject(new JProperty("Success", true), new JProperty("Message", "Hangup Success"));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                hanupresObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Cannot hangup at this moment"));
            }
            return hanupresObj;
        }

        public JObject PlayToAConferenceCall(string ConferenceRoom, int MemberId, string HttpUrl, string FilePath)
        {
            JObject PlayResponse = null;
            WebRequest _Req = null;
            HttpWebResponse _Resp = null;
            StreamReader SReader = null;
            StreamWriter SWriter = null;
            string PostingData = "";
            string HttpAPIResponseString = "";
            try
            {
                _Req = HttpWebRequest.Create(HttpUrl + "ConferencePlay/");
                _Req.Method = "POST";
                _Req.ContentType = "application/x-www-form-urlencoded";
                PostingData = "ConferenceName=" + ConferenceRoom + "&MemberID=" + MemberId.ToString() + "&FilePath=" + FilePath;
                SWriter = new StreamWriter(_Req.GetRequestStream());
                SWriter.Write(PostingData);
                SWriter.Flush();
                SWriter.Close();
                SReader = new StreamReader(_Req.GetResponse().GetResponseStream());
                HttpAPIResponseString = SReader.ReadToEnd();
                SReader.Close();
                PlayResponse = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                PlayResponse = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Server Error"));
            }
            return PlayResponse;
        }
        /// <summary>
        /// This Function Is Used To Validate The AutoDial Input Data 
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="userId"></param>
        /// <param name="callId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// 
        public string ValidateReq(string sConnString, int userId, string callId, string token)
        {
            string resp = null;
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                resp = groupcallObj.ValidateRequest(Convert.ToInt32(userId), Convert.ToInt32(callId), token);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Validate Request in GrpCallBusiness : " + ex.ToString());
                resp = "Something wrong with the server";
            }
            return resp;
        }
        /// <summary>
        /// This Function Is Used To Validate The Input Dial Actions
        /// </summary>
        /// <param name="grpcObj"></param>
        /// <param name="connString"></param>
        public void ValidateGrpCallActions(grpcall grpcObj, string connString)
        {
            try
            {
                string[] validateActions = {
	            "MUTE_DIAL_ALL",
	            "UNMUTE_DIAL_ALL",
	            "MUTE_DIAL",
	            "DIAL"
	            };
                if (validateActions.Contains(grpcObj.ConferenceAction))
                {
                    if (grpcObj.ConferenceAction == "DIAL" || grpcObj.ConferenceAction == "MUTE_DIAL")
                    {
                        grpcObj.IsValidate = true;
                        grpcObj.Direction = "OUTBOUND";
                        grpcObj.ConferenceNumber = "";
                        grpcObj.ConferenceAccessKey = "";
                        grpcObj.TotalNumbers = 1;
                    }
                    else
                    {
                        grpcObj.IsValidate = true;
                        grpcObj.Direction = "OUTBOUND";
                        grpcObj.ConferenceNumber = "";
                        grpcObj.ConferenceAccessKey = "";
                        grpcObj.TotalNumbers = 0;
                    }
                }
                else
                {
                    grpcObj.IsValidate = false;
                    grpcObj.Direction = "OUTBOUND";
                    grpcObj.ConferenceNumber = "";
                    grpcObj.ConferenceAccessKey = "";
                    grpcObj.TotalNumbers = 0;
                }

                grpCallValidateObject = ValidateGrpCall(grpcObj, connString);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("ValidateGrpCallActions : " + ex.ToString());

            }

        }
        /// <summary>
        /// This Function Is Used TO Validate The GroupCall
        /// </summary>
        /// <param name="gCallObj"></param>
        /// <param name="connString"></param>
        /// <returns></returns>
        public JObject ValidateGrpCall(grpcall gCallObj, string connString)
        {
            JObject validateResponse = null;
            DataSet validateDs;
            short retval;
            string retmsg = "";
            string callUUID = "";
            long instanceId = 0;
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(connString);
                validateDs = groupcallObj.ValidateGrpCall(gCallObj, out retval, out retmsg, out callUUID,out instanceId);
                validateResponse = new JObject();
                if (retval == 1)
                {
                    validateResponse.Add(new JProperty("Success", true));

                    if (validateDs.Tables.Count > 0 && validateDs.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataColumn _Column in validateDs.Tables[0].Columns)
                        {
                            validateResponse.Add(new JProperty(_Column.ColumnName, validateDs.Tables[0].Rows[0][_Column.ColumnName]));
                        }
                    }
                    else
                    {
                        validateResponse = new JObject(new JProperty("Success", false), new JProperty("Message", "No Data Returned From Database"));
                    }
                }
                else
                {
                    validateResponse = new JObject(new JProperty("Success", false), new JProperty("Message", retmsg));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("ValidateGrpCall in grpcallBusiness : " + ex.ToString());
                validateResponse = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"));
            }
            finally
            {

            }
            return validateResponse;
        }
        /// <summary>
        /// This Function Is Used To Set GroupCall Variables
        /// </summary>
        /// <param name="validateObj"></param>
        /// <param name="grpPropObj"></param>
        /// <returns></returns>
        public grpcall SetGrpCallVariables(JObject validateObj, grpcall grpPropObj)
        {
            try
            {
                if (validateObj.SelectToken("WelcomeClip") != null && string.IsNullOrEmpty(validateObj.SelectToken("WelcomeClip").ToString()) == false)
                {
                    grpPropObj.WelcomeClip = validateObj.SelectToken("WelcomeClip").ToString();
                }
                if (validateObj.SelectToken("WaitClip") != null && string.IsNullOrEmpty(validateObj.SelectToken("WaitClip").ToString()) == false)
                {
                    grpPropObj.WaitClip = validateObj.SelectToken("WaitClip").ToString();
                }
                grpPropObj.ConferenceRoom = validateObj.SelectToken("ConferenceRoom").ToString();
                grpPropObj.MemberMute = validateObj.SelectToken("Mute").ToString();
                grpPropObj.StartConferenceOnEnter = validateObj.SelectToken("StartConferenceOnEnter").ToString();
                grpPropObj.EndConferenceOnExit = validateObj.SelectToken("EndConferenceOnExit").ToString();
                grpPropObj.Moderator = validateObj.SelectToken("Moderator").ToString();
                grpPropObj.ConferenceAccessKey = validateObj.SelectToken("ConferenceAccessKey").ToString();
                grpPropObj.UserId = Convert.ToInt32(validateObj.SelectToken("UserId").ToString());
                grpPropObj.ConferenceId = Convert.ToInt32(validateObj.SelectToken("ConferenceId").ToString());
                grpPropObj.InstanceId = Convert.ToInt64(validateObj.SelectToken("InstanceId").ToString());
                if (validateObj.SelectToken("Calluid") != null)
                {
                    grpPropObj.CallUUID = validateObj.SelectToken("Calluid").ToString();
                }
                else
                {
                    grpPropObj.CallUUID = validateObj.SelectToken("CallUUID").ToString();
                }
                grpPropObj.HttpConferenceApiUrl = validateObj.SelectToken("HttpUrl").ToString();
                grpPropObj.OriginationUrl = validateObj.SelectToken("OriginationUrl").ToString();
                grpPropObj.ExtraDialString = validateObj.SelectToken("ExtraDialString").ToString();
                grpPropObj.CallerIdNumber = validateObj.SelectToken("CallerIdNumber").ToString();
                if (!string.IsNullOrEmpty(grpPropObj.ExtraDialString.Trim()))
                {
                    if (!grpPropObj.ExtraDialString.EndsWith(","))
                    {
                        //  GObj.ExtraDialString = _ExtraDialString + ",";
                        grpPropObj.ExtraDialString = "@" + ",";
                    }
                }
                grpPropObj.GatewayID = Convert.ToInt16(validateObj.SelectToken("GatewayID").ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" SetGrpCallVariables  in grpCallbusiness : " + ex.ToString());
                grpPropObj.WelcomeClip = ""; grpPropObj.WaitClip = ""; grpPropObj.ConferenceRoom = ""; grpPropObj.MemberMute = ""; grpPropObj.StartConferenceOnEnter = "";
                grpPropObj.EndConferenceOnExit = ""; grpPropObj.Moderator = ""; grpPropObj.ConferenceAccessKey = ""; grpPropObj.UserId = 0;
                grpPropObj.ConferenceId = 0; grpPropObj.CallUUID = ""; grpPropObj.HttpConferenceApiUrl = ""; grpPropObj.OriginationUrl = "";
                grpPropObj.ExtraDialString = ""; grpPropObj.CallerIdNumber = ""; grpPropObj.ExtraDialString = ""; grpPropObj.GatewayID = 0;
            }
            return grpPropObj;
        }
        /// <summary>
        /// This Function Is Used To Construct The Xml For GroupCall Dialing
        /// </summary>
        /// <param name="IsModerator"></param>
        /// <param name="IsMute"></param>
        /// <param name="grpCallProp"></param>
        /// <param name="grpCallCallBackUrl"></param>
        /// <returns></returns>






        public string GetConferenceXml(bool IsModerator, bool IsMute, grpcall grpCallProp, string grpCallCallBackUrl,bool isInterConnect = false,string sendDigitsString ="")
        {
            string isGrpTalk = System.Configuration.ConfigurationManager.AppSettings["isGrpTalk"].ToString();
            string _Xml = "<Response><Hangup data='in getconf method'/></Response>";
            if (!string.IsNullOrEmpty(grpCallProp.WelcomeClip))
            {
                if (grpCallProp.WelcomeClip == "http://yconference.com/DefaultClips/welcome.mp3")
                {
                    grpCallProp.WelcomeClip = "/usr/local/freeswitch/recordings/yconf_welcome_clip.mp3";
                }
            }

            if (!string.IsNullOrEmpty(grpCallProp.WelcomeClip))
            {
                if (isInterConnect == true)
                {
                    _Xml = "<Response><Wait length=\'3\'/><SendDigits>"+ sendDigitsString +"</SendDigits><Conference stayAlone=\'false\'>"+ grpCallProp.ConferenceRoom +"</Conference></Response>";
                    //_Xml = "<Response><Conference callbackUrl=\'" + grpCallCallBackUrl + "\' stayAlone=\'false\' >" + grpCallProp.ConferenceRoom + "</Conference></Response>";
                }
                else
                {
                    if (grpCallProp.IsCallFromBonus == false)
                    {
                        _Xml = "<Response><Play>" + grpCallProp.WelcomeClip + "</Play><Conference";
                        //if (grpCallProp.IsPaidClient == 0)
                        //{
                        //    _Xml = _Xml + " isGrpTalk=\'" + isGrpTalk + "\'";

                        //}
                        if (grpCallProp.IsAll == false)
                        {
                            _Xml = _Xml + " method=\'GET\' maxMembers=\'3000\' timeLimit =\'" + grpCallProp.TimeLimit.ToString() + "\' silenceTimeout=\"180\" timeLimitForce =\'" + grpCallProp.TimeLimit.ToString() + "\'  callbackUrl=\'" + grpCallCallBackUrl + "\' endConferenceOnExit=\'true\' waitSound =\'" + grpCallProp.WaitClip + "\' digitsMatch=\'0\' >" + grpCallProp.ConferenceRoom + "</Conference></Response>";
                        }
                        else
                        {
                            _Xml = _Xml + " method=\'GET\' maxMembers=\'3000\'  timeLimit =\'" + grpCallProp.TimeLimit.ToString() + "\' silenceTimeout=\"180\"  callbackUrl=\'" + grpCallCallBackUrl + "\' endConferenceOnExit=\'true\' waitSound =\'" + grpCallProp.WaitClip + "\' digitsMatch=\'0\' >" + grpCallProp.ConferenceRoom + "</Conference></Response>";
                        }
                    }
                    else
                    {
                        _Xml = "<Response><Play>" + grpCallProp.WelcomeClip + "</Play><Conference";
                        if (grpCallProp.IsAll == false)
                        {
                            _Xml = _Xml + " method=\'GET\' maxMembers=\'3000\' callbackUrl=\'" + grpCallCallBackUrl + "\' endConferenceOnExit=\'true\' waitSound =\'" + grpCallProp.WaitClip + "\' digitsMatch=\'0\' >" + grpCallProp.ConferenceRoom + "</Conference></Response>";
                        }
                        else
                        {
                            _Xml = _Xml + " method=\'GET\' maxMembers=\'3000\'  timeLimit =\'" + grpCallProp.TimeLimit.ToString() + "\'  callbackUrl=\'" + grpCallCallBackUrl + "\' endConferenceOnExit=\'true\' waitSound =\'" + grpCallProp.WaitClip + "\' digitsMatch=\'0\' >" + grpCallProp.ConferenceRoom + "</Conference></Response>";
                        }

                    }
                }

                
            }
            return _Xml;
        }
        /// <summary>
        /// This Function Is Used To Get The PlivocallUUids For Hangup The Call
        /// </summary>
        /// <param name="requestUUIds"></param>
        /// <param name="type"></param>
        /// <param name="grpCallObj"></param>
        /// <param name="sConnString"></param>
        /// <returns></returns>
        /// 

        public string GetPlivoCallUUIDs(string requestUUIds, short type, grpcall grpCallObj, string sConnString)
        {
            string response = "";
            DataSet dsPlivo = null;
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                dsPlivo = groupcallObj.GetPlivoCallUUIDs(requestUUIds, type, grpCallObj);
                if (dsPlivo.Tables.Count > 0 && dsPlivo.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i <= dsPlivo.Tables[0].Rows.Count - 1; i++)
                    {
                        response = response + dsPlivo.Tables[0].Rows[i]["PlivoCallUUID"].ToString() + ",";
                    }
                    response = response.Left(response.Length - 1);
                }
                else
                {
                    response = "";
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Error Getting Plivo CallUUIDs, Reason : " + ex.ToString());
                response = "exc";
            }
            return response;
        }

        public void Pusher_Conference_MuteUnMuteAndDeafUnDeaf(string _Mem, string Respons, string CRoom, bool Isaction, string caction, bool is_All)
        {
            string PusherAppId = "0";
            string PusherAppKey = "";
            string PusherAppSecret = "";
            string _CAction = "";
            string _ConfAction = "";
            int _isActionAll = 0;
            _CAction = caction;
            if (is_All == true)
            {
                _isActionAll = 1;
            }
            if (_isActionAll == 1)
            {
                if (_CAction == "Mute")
                {
                    if (Isaction == false)
                    {
                        _ConfAction = "unmute_all";
                    }
                    else
                    {
                        _ConfAction = "mute_all";
                    }
                }
                else if (_CAction == "Deaf")
                {
                    if (Isaction == false)
                    {
                        _ConfAction = "undeaf_all";
                    }
                    else
                    {
                        _ConfAction = "deaf_all";
                    }
                }
            }
            else
            {
                if (_CAction == "Mute")
                {
                    if (Isaction == false)
                    {
                        _ConfAction = "unmute_member";
                    }
                    else
                    {
                        _ConfAction = "mute_member";
                    }
                }
                else if (_CAction == "Deaf")
                {
                    if (Isaction == false)
                    {
                        _ConfAction = "undeaf_member";
                    }
                    else
                    {
                        _ConfAction = "deaf_member";
                    }
                }
            }

            PusherAppId = System.Configuration.ConfigurationManager.AppSettings["PusherAppId"].ToString();
            PusherAppKey = System.Configuration.ConfigurationManager.AppSettings["PusherAppKey"].ToString();
            PusherAppSecret = System.Configuration.ConfigurationManager.AppSettings["PusherAppSecret"].ToString();
            //dynamic pusher_obj = new Pusher(PusherAppId, PusherAppKey, PusherAppSecret);
            //dynamic PusherResponse = pusher_obj.Trigger(CRoom, "CallActions", new {
            //	conf_action = _ConfAction,
            //	member_name = _Mem,
            //	is_all = _isActionAll,
            //	to_number = pushtonum,
            //	direction = "outbound"
            //});
        }

        public void MobileNotifier(string sConnString, PusherNotifier pushObj)
        {
            int retVal = 0;
            int osID = 0;
            string hostName = "";
            string grpCallName = "";
            string pusherRetVal = "";
            DataSet respNotifier = new DataSet();
            try
            {
                DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
                respNotifier = groupcallObj.MobileNotifier(pushObj, out retVal, out grpCallName, out hostName);
                if (retVal == 1)
                {


                    if (respNotifier.Tables[0].Rows.Count > 0)
                    {
                        
                        var _with1 = respNotifier.Tables[0];
                        string message = "";
                        string HostName = hostName;

                        for (int i = 0; i <= _with1.Rows.Count - 1; i++)
                        {
                            osID = Convert.ToInt32(_with1.Rows[i]["OsId"]);
                            if (Convert.ToInt32(_with1.Rows[i]["IsHost"]) == 1)
                            {
                                if (pushObj.IsStarted == 1)
                                {
                                    message = "Your grpTalk '" + grpCallName + "' started.";
                                }
                                else
                                {
                                    message = "Your grpTalk '" + grpCallName + "' completed.";
                                }

                            }
                            else
                            {
                                if (pushObj.IsStarted == 1)
                                {
                                    message = HostName + "'s grpTalk '" + grpCallName + "' started.";
                                }
                                else
                                {
                                    message = HostName + "'s grpTalk '" + grpCallName + "' completed.";
                                }
                            }
                            if (Convert.ToInt32(_with1.Rows[i]["OsId"]) == 1)
                            {
                                IosdeviceToken.Add(_with1.Rows[i]["devicetoken"].ToString(), message);
                            }
                            else
                            {
                                deviceToken.Add(_with1.Rows[i]["devicetoken"].ToString(), message);
                            }
                            


                        }
                        IOSpush(grpCallName, pushObj);
                        sendAndroidPush(grpCallName, pushObj);
                       
                    }

                    
                }
            }
            catch(Exception ex)
            {
                Logger.TraceLog("exception in mobile notifier :" + ex.ToString());
            }
            
        }
        public void sendAndroidPush(string cname, PusherNotifier pushObj)
        {
            try
            {
                
                foreach (KeyValuePair<string, string> akey in deviceToken)
                {
                    AndroidPush(akey.Key, akey.Value, pushObj);
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.StackTrace);
            }
        }

        public string AndroidPush(string devicetoken, string message, PusherNotifier pushObj)
        {
            try
            {

                string regId = devicetoken;
                string applicationID = "AIzaSyBfX67bZ_F0NWSqDYOelVfXoz8pYiXhIcY";
                string SENDER_ID = "678288961581";
                WebRequest tRequest = default(WebRequest);
                tRequest = WebRequest.Create("https://android.googleapis.com/gcm/send");
                tRequest.Method = "post";
                tRequest.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                tRequest.Headers.Add(string.Format("Authorization: key={0}", applicationID));
                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
                message = System.Web.HttpUtility.UrlEncode(message);
                string postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=" + message + "&data.time=" + pushObj.GrpCallID.ToString() + "," + pushObj.IsStarted.ToString() + "&registration_id=" + regId + "&conf_id=" + pushObj.IsStarted.ToString() + "&confname=" + message + "&isstarted=" + pushObj.IsStarted.ToString();
                //postData = "collapse_key=score_update&time_to_live=108&delay_while_idle=1&data.message=Gopi&data.time=0,0&registration_id=APA91bHVvQ0Of89q5XPq7ZtcQ3xx6h7vESxUW-wGxtz9JXKn6mBCQw6rGYSo0DRLLqjq85PebjeP1d34dsIdpGqnDuKyVblyYNKhXT-xW0rsvIF1-E3ExO0bgnS_UNEDeOPi1in65LsW&conf_id=0&confname=Gopi&isstarted=1";
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                string sResponseFromServer = tReader.ReadToEnd();
                //Get response from GCM server.
                tReader.Close();
                dataStream.Close();
                tResponse.Close();

                return "Success";
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.StackTrace);
                return "Exception";
            }

        }

        public JObject GrpCallRetryBLL(string sConnString, string callUUId, string requestUUID, string callBackUrl)
        {
            short retVal = 0;
            string retMsg = "";
            string memberName = "";
            bool isMute = false;
            int grpCallID = 0;
            string mobileNumber = "";
            bool isModerator = false;
            DataSet retryRespSet = new DataSet();
            JObject retryRespObj = new JObject();
            JObject retryCallRespObj = new JObject();
            JObject grpCallValidateObj = new JObject();
            grpcall grpCallRetryProp = new grpcall();
            DataTable nodeTable = new DataTable();
            DataTable gateWayTable = new DataTable();

            DataAccessLayer.V_1_2.GroupCall_V120 groupcallObj = new DataAccessLayer.V_1_2.GroupCall_V120(sConnString);
            retryRespSet = groupcallObj.GrpCallRetry(callUUId, requestUUID, out retVal, out retMsg, out grpCallID, out isModerator, out isMute, out mobileNumber);
            try
            {
                if (retVal != 1)
                {
                    retryCallRespObj = new JObject(new JProperty("Success", false),
                                                new JProperty("Message", retMsg));
                }
                else
                {
                    if (retryRespSet.Tables.Count > 0 && retryRespSet.Tables[0].Rows.Count > 0)
                    {
                        var _with2 = retryRespSet.Tables[0];
                        foreach (DataColumn _Column in _with2.Columns)
                        {

                            grpCallValidateObj.Add(new JProperty(_Column.ColumnName,
                                _with2.Rows[0][_Column.ColumnName]));
                        }
                        SetGrpCallVariables(grpCallValidateObj, grpCallRetryProp);
                        grpCallRetryProp.CallUUID = callUUId;
                        grpCallRetryProp.MemberName = memberName;
                        grpCallRetryProp.IsMute = isMute;
                        grpCallRetryProp.IsRetry = true;
                        grpCallRetryProp.MobileNumber = mobileNumber;
                        grpCallRetryProp.IsModerator = isModerator;
                        if (grpCallRetryProp.Moderator == mobileNumber)
                        {
                            grpCallRetryProp.ConferenceAction = "MODERATOR_DIAL";
                            grpCallRetryProp.IsModerator = true;
                            grpCallRetryProp.IsMute = false;
                            grpCallRetryProp.IsAll = false;
                        }
                        else
                        {
                            grpCallRetryProp.IsModerator = false;
                            grpCallRetryProp.IsAll = false;
                            if (grpCallRetryProp.IsMute == true)
                            {
                                grpCallRetryProp.ConferenceAction = "MUTE_DIAL";
                            }
                            else
                            {
                                grpCallRetryProp.ConferenceAction = "DIAL";
                            }
                        }

                        retryCallRespObj = Dial(sConnString, grpCallRetryProp, callBackUrl, nodeTable, gateWayTable);
                    }
                    else
                    {
                        retryCallRespObj = new JObject(new JProperty("Success", false),
                                                        new JProperty("Message", "No Data Retirned From DataBase"));
                    }
                }
            }
            catch (Exception ex)
            {
                retryCallRespObj = new JObject(new JProperty("Success", false),
                                                new JProperty("Message", "Something Went Wrong"));
            }
            return retryCallRespObj;
        }
        
        
        public void IOSpush(string cname, PusherNotifier pushObj)
        {
            byte[] appleCert = File.ReadAllBytes(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, HttpContext.Current.Server.MapPath("~/Certificate/Certificates.p12")));
            ApnsConfiguration config = new ApnsConfiguration(ApnsConfiguration.ApnsServerEnvironment.Production, appleCert, "", true);
            var broker = new ApnsServiceBroker (config);
            broker.OnNotificationFailed += (notification, aggregateEx) => 
            {
                aggregateEx.Handle (ex =>
                {
                    if (ex is ApnsNotificationException) 
                    {
                        var notificationException = ex as ApnsNotificationException;
                        var apnsNotification = notificationException.Notification;
                        var statusCode = notificationException.ErrorStatusCode;
                        Logger.TraceLog("Notification Failed: ID={apnsNotification.Identifier}, Code={statusCode}");
                    } 
                    else 
                    {
                        Logger.TraceLog("Notification Failed for some (Unknown Reason) : {ex.InnerException}");
                    }
                    return true;
                });
            };

            broker.OnNotificationSucceeded += (notification) =>
            {
                Logger.TraceLog("Notification Sent!");
            };
            broker.Start ();
            foreach (KeyValuePair<string, string> akey in IosdeviceToken)
            {
                 broker.QueueNotification (new ApnsNotification
                 {
                     
                     DeviceToken = akey.Key,
                     Payload = JObject.Parse("{\"aps\":{\"badge\":1, \"alert\":\"" + akey.Value + "\", \"isstarted\":\"" + pushObj.IsStarted.ToString() + "\", \"confname\":\"" + cname + "\", \"conf_id\":\"" + pushObj.GrpCallID.ToString() + "\"}}")
                 });
            }

            broker.Stop ();


        }

        
    }

        
    
    static class Extensions
    {
        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }
        public static string Left(this string value, int length)
        {
            return value.Substring(0, Math.Min(length, value.Length));

        }


    }
}
