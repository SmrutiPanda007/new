using System;
using System.Collections;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GT.DataAccessLayer;
using GT.Utilities.Properties;
using System.Data;
using System.Data.SqlClient;
using PusherServer;
using PushSharp;
using System.Globalization;
using GT.Utilities;
using Microsoft.VisualBasic;
using System.Configuration;
using System.Web.UI.WebControls;
namespace GT.BusinessLogicLayer.V_1_5
{
    public class GroupCallReports
    {

        string grpCallName = "";
        CallBackVariable callBackPropObj = new CallBackVariable();
        DataSet globalDs = new DataSet();
        StringBuilder callBackResponse = new StringBuilder();
        JObject jObj = new JObject();
        DataSet ds = new DataSet();
        int isInProgress = 0;
        /// <summary>
        /// THis Method is used to update the call status from remote server
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="methodType"></param>
        /// <param name="callBackIpAddr"></param>
        /// <param name="callBackReqObj"></param>
        /// <param name="pusherAppId"></param>
        /// <param name="pusherAppKey"></param>
        /// <param name="pusherAppsecret"></param>
        /// <param name="HangupCauses"></param>
        /// <param name="respContext"></param>
        /// <param name="callBackUrl"></param>
        /// <returns></returns>
        public string UpdateCallBacks(string sConnString, string methodType, string callBackIpAddr, JObject callBackReqObj, string pusherAppId, string pusherAppKey, string pusherAppsecret, string HangupCauses, HttpContext respContext, string callBackUrl)
        {
            //Parsing The Data
            ParseResponse(methodType, callBackReqObj, respContext);

            string pusherCallStatus = callBackPropObj.CallStatus;
            string eventName = "call_status";
            string[] hangupCasuesForRetry = null;
            hangupCasuesForRetry = HangupCauses.Split(",".ToCharArray());
            try
            {
                //Updating GroupCall Status Method calling
                globalDs = UpdateCallBacksCallDb(sConnString, callBackPropObj, callBackIpAddr);
                Pusher pusherObj = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);

                JObject retryResponse = new JObject();
                if (hangupCasuesForRetry.Contains(callBackPropObj.EndReason))
                {
                    BusinessLogicLayer.V_1_5.GroupCall groupcallObj = new BusinessLogicLayer.V_1_5.GroupCall();
                    retryResponse = groupcallObj.GrpCallRetryBLL(sConnString, callBackPropObj.CallUUID, callBackPropObj.RequestUUID, callBackUrl, 0);

                    callBackResponse.Append("Retry Response : " + retryResponse.ToString());
                    if (Convert.ToBoolean(retryResponse.SelectToken("Success").ToString()) == true)
                    {
                        pusherCallStatus = "redial";
                    }
                }


                if (globalDs.Tables.Count > 0 && globalDs.Tables[0].Rows.Count > 0)
                {
                    var _with1 = globalDs.Tables[0];
                    if (Convert.ToInt32(globalDs.Tables[0].Rows[0]["IsInterConnect"]) == 0)
                    {
                        ITriggerResult PusherResponse = null;
                        PusherResponse = pusherObj.Trigger(globalDs.Tables[0].Rows[0]["ConferenceName"].ToString(), eventName, new
                        {
                            direction = "outbound",
                            plivo_event = callBackPropObj.Event,
                            plivo_conferene_action = callBackPropObj.GrpCallAction,
                            to_num = globalDs.Tables[0].Rows[0]["MobileNumber"],
                            conf_room = grpCallName,
                            call_status = pusherCallStatus,
                            mute = globalDs.Tables[0].Rows[0]["Mute"],
                            deaf = globalDs.Tables[0].Rows[0]["Deaf"],
                            member = globalDs.Tables[0].Rows[0]["Member"],
                            conf_type = globalDs.Tables[0].Rows[0]["ConferenceType"],
                            conf_digits = callBackPropObj.Digits,
                            conf_id = globalDs.Tables[0].Rows[0]["ConferenceId"],
                            record_status = globalDs.Tables[0].Rows[0]["RecordStatus"],
                            record_count = globalDs.Tables[0].Rows[0]["RecordCount"],
                            dig_prs_count = globalDs.Tables[0].Rows[0]["DigitPressCount"],
                            inpro_count = globalDs.Tables[0].Rows[0]["InprogressCount"],
                            unmute_count = globalDs.Tables[0].Rows[0]["UnmuteCount"],
                            mute_count = globalDs.Tables[0].Rows[0]["MuteCount"],
                            end_reason = globalDs.Tables[0].Rows[0]["end_reason"],
                            isinprogress = isInProgress
                        });
                    }
                    callBackResponse.Append("CurrentMemberDetails ==> ");
                    foreach (DataRow _Row in globalDs.Tables[0].Rows)
                    {
                        foreach (DataColumn _Column in _Row.Table.Columns)
                        {
                            if (_Row[_Column.ColumnName] == DBNull.Value)
                            {
                                callBackResponse.Append(_Column.ColumnName + " : NULL");
                            }
                            else
                            {
                                callBackResponse.Append(_Column.ColumnName + " : " + _Row[_Column.ColumnName]);
                            }
                            callBackResponse.Append(" | ");
                        }
                        callBackResponse.Append(" @@@ ");
                    }
                    //If isRecording = 0 AndAlso globalglobalDs.Tables(0).Rows.Item(0)("InprogressCount") > 1 Then
                    //    logclass.LogRequest("Enters Into Conference Recording block --" & globalDs.Tables(0).Rows.Item(0)("ConferenceId").Tostring())
                    //    ConferenceRecord(globalDs.Tables(0).Rows.Item(0)("ConferenceId"), 1)
                    //End If
                }
                else
                {
                    callBackResponse.Append("No Member Data Returned From Database");
                }
                if (globalDs.Tables.Count > 1 && globalDs.Tables[1].Rows.Count > 0)
                {
                    callBackResponse.Append("AloneMemberDetails ==> ");
                    foreach (DataRow _Row in globalDs.Tables[1].Rows)
                    {
                        foreach (DataColumn _Column in _Row.Table.Columns)
                        {
                            if (_Row[_Column.ColumnName] == DBNull.Value)
                            {
                                callBackResponse.Append(_Column.ColumnName + " : NULL");
                            }
                            else
                            {
                                callBackResponse.Append(_Column.ColumnName + " : " + _Row[_Column.ColumnName]);
                            }
                            callBackResponse.Append(" | ");
                        }
                        callBackResponse.Append(" @@@ ");
                    }
                    JObject aloneHangupResponse = new JObject();
                    JObject alonePlayResponse = new JObject();
                    var _with2 = globalDs.Tables[1];
                    V_1_5.GroupCall GrpcallObj = new V_1_5.GroupCall();
                    alonePlayResponse = GrpcallObj.PlayToAConferenceCall(globalDs.Tables[1].Rows[0]["ConferenceRoom"].ToString(), Convert.ToInt32(globalDs.Tables[1].Rows[0]["MemberId"]), globalDs.Tables[1].Rows[0]["HttpUrl"].ToString(), "http://new.grpTalk.com/DefaultClips/thank_you.mp3");
                    System.Threading.Thread.Sleep(6000);
                    //ConferenceObject.ConferenceRecording(globalDs.Tables(0).Rows.Item(0)("ConferenceId"), 2)
                    grpcall callObj = new grpcall();
                    callObj.ConferenceId = Convert.ToInt32(globalDs.Tables[1].Rows[0]["ConferenceId"]);
                    callObj.ConferenceRoom = globalDs.Tables[1].Rows[0]["ConferenceRoom"].ToString();
                    callObj.CallUUID = globalDs.Tables[1].Rows[0]["CallUUID"].ToString();
                    callObj.ConferenceAction = "HANGUP_ALL";
                    callObj.IsAll = true;
                    callObj.HttpConferenceApiUrl = globalDs.Tables[1].Rows[0]["HttpUrl"].ToString();

                    aloneHangupResponse = GrpcallObj.GrpCallHanUp(sConnString, callObj);

                    //Send Notification to App
                    PusherNotifier mobileNotifierObj = new PusherNotifier();
                    mobileNotifierObj.IsStarted = 0;
                    mobileNotifierObj.GrpCallID = Convert.ToInt32(globalDs.Tables[1].Rows[0]["ConferenceId"]);
                    GrpcallObj.MobileNotifier(sConnString, mobileNotifierObj);

                    callBackResponse.Append("Alone Play Response Is : " + alonePlayResponse.ToString());
                    callBackResponse.Append("Alone Hangup Response Is : " + aloneHangupResponse.ToString());
                }
                else
                {
                    callBackResponse.Append("No Alone Member");
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at callBackresponseBLL : " + ex.ToString());
                callBackResponse.Append("Exception : " + ex.ToString()).ToString();
            }
            return callBackResponse.ToString();
        }
        /// <summary>
        /// This fuction is used to Call db to update the call status
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="callBackProp"></param>
        /// <param name="callBackAddr"></param>
        /// <returns></returns>
        public DataSet UpdateCallBacksCallDb(string sConnString, CallBackVariable callBackProp, string callBackAddr)
        {
            int isRecording = 0;
            int notify = 0;
            int grpCallId = 0;

            DataSet callBackDs = new DataSet();
            DataAccessLayer.V_1_5.GroupcallReports groupCallReportsObj = new DataAccessLayer.V_1_5.GroupcallReports(sConnString);
            callBackDs = groupCallReportsObj.UpdateCallBacksOfGroupCall(callBackProp, callBackAddr, out isRecording, out notify, out grpCallId, out grpCallName, out isInProgress);
            return callBackDs;
        }
        /// <summary>
        /// This Method is used to Parse the input data
        /// </summary>
        /// <param name="reqType"></param>
        /// <param name="reqObj"></param>
        /// <param name="respContext"></param>
        public void ParseResponse(string reqType, JObject reqObj, HttpContext respContext)
        {
            if (reqType == "GET")
            {
                if (string.IsNullOrEmpty(respContext.Request["smscresponse[sequencenumber]"]) == false)
                {
                    callBackPropObj.SeqNumber = Convert.ToInt64(respContext.Request["smscresponse[sequencenumber]"]);
                }
                else
                {
                    callBackPropObj.SeqNumber = 0;
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[event]"]) == false)
                {
                    callBackPropObj.Event = respContext.Request["smscresponse[event]"];
                }
                else
                {
                    callBackPropObj.Event = "";
                }
                if (string.IsNullOrEmpty(respContext.Request["smscresponse[calluid]"]) == false)
                {
                    callBackPropObj.CallUUID = respContext.Request["smscresponse[calluid]"];
                }
                else
                {
                    callBackPropObj.CallUUID = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[direction]"]) == false)
                {
                    callBackPropObj.Direction = respContext.Request["smscresponse[direction]"];
                }
                else
                {
                    callBackPropObj.Direction = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[from]"]) == false)
                {
                    callBackPropObj.From = respContext.Request["smscresponse[from]"];
                }
                else
                {
                    callBackPropObj.From = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[to]"]) == false)
                {
                    callBackPropObj.To = respContext.Request["smscresponse[to]"];
                }
                else
                {
                    callBackPropObj.To = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[callstatus]"]) == false)
                {
                    callBackPropObj.CallStatus = respContext.Request["smscresponse[callstatus]"];
                }
                else
                {
                    callBackPropObj.CallStatus = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceMemberID]"]) == false)
                {
                    callBackPropObj.grpCallMemberID = respContext.Request["smscresponse[ConferenceMemberID]"];
                }
                else
                {
                    callBackPropObj.grpCallMemberID = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceName]"]) == false)
                {
                    callBackPropObj.GrpCallName = respContext.Request["smscresponse[ConferenceName]"];
                }
                else
                {
                    callBackPropObj.GrpCallName = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[RequestUUID]"]) == false)
                {
                    callBackPropObj.RequestUUID = respContext.Request["smscresponse[RequestUUID]"];


                }
                else
                {
                    callBackPropObj.RequestUUID = "";

                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceAction]"]) == false)
                {
                    callBackPropObj.GrpCallAction = respContext.Request["smscresponse[ConferenceAction]"];
                }
                else
                {
                    callBackPropObj.GrpCallAction = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceDigitsMatch]"]) == false)
                {
                    callBackPropObj.Digits = respContext.Request["smscresponse[ConferenceDigitsMatch]"];
                }
                else
                {
                    callBackPropObj.Digits = "";
                }
                if (string.IsNullOrEmpty(respContext.Request["smscresponse[endreason]"]) == false)
                {
                    callBackPropObj.EndReason = respContext.Request["smscresponse[endreason]"].ToString();
                }
                else
                {
                    callBackPropObj.EndReason = "";
                }

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[starttime]"]) == false)
                {
                    callBackPropObj.StartTime = Convert.ToInt32(respContext.Request["smscresponse[starttime]"]);
                }
                else
                {
                    callBackPropObj.StartTime = 0;
                }
                if (string.IsNullOrEmpty(respContext.Request["smscresponse[endtime]"]) == false)
                {
                    callBackPropObj.EndTime = Convert.ToInt32(respContext.Request["smscresponse[endtime]"]);
                }
                else
                {
                    callBackPropObj.EndTime = 0;
                }
            }
            else
            {
                if (reqObj.SelectToken("smscresponse").SelectToken("sequencenumber") != null)
                {
                    callBackPropObj.SeqNumber = Convert.ToInt64(reqObj.SelectToken("smscresponse").SelectToken("sequencenumber").ToString());
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("calluid") != null)
                {
                    callBackPropObj.CallUUID = reqObj.SelectToken("smscresponse").SelectToken("calluid").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("direction") != null)
                {
                    callBackPropObj.Direction = reqObj.SelectToken("smscresponse").SelectToken("direction").ToString();
                }
                callBackPropObj.From = reqObj.SelectToken("smscresponse").SelectToken("from").ToString();
                callBackPropObj.To = reqObj.SelectToken("smscresponse").SelectToken("to").ToString();
                if (reqObj.SelectToken("smscresponse").SelectToken("callstatus") != null)
                {
                    callBackPropObj.CallStatus = reqObj.SelectToken("smscresponse").SelectToken("callstatus").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceMemberID") != null)
                {
                    callBackPropObj.grpCallMemberID = reqObj.SelectToken("smscresponse").SelectToken("ConferenceMemberID").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceName") != null)
                {
                    callBackPropObj.GrpCallName = reqObj.SelectToken("smscresponse").SelectToken("ConferenceName").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceAction") != null)
                {
                    callBackPropObj.GrpCallAction = reqObj.SelectToken("smscresponse").SelectToken("ConferenceAction").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("event") != null)
                {
                    callBackPropObj.Event = reqObj.SelectToken("smscresponse").SelectToken("event").ToString();
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceDigitsMatch") != null)
                {
                    callBackPropObj.Digits = reqObj.SelectToken("smscresponse").SelectToken("ConferenceDigitsMatch").ToString();
                }
                else
                {
                    callBackPropObj.Digits = "";
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("endreason") != null)
                {
                    callBackPropObj.EndReason = reqObj.SelectToken("smscresponse").SelectToken("endreason").ToString();
                }
                else
                {
                    callBackPropObj.EndReason = "";
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("starttime") != null)
                {
                    callBackPropObj.StartTime = Convert.ToInt32(reqObj.SelectToken("smscresponse").SelectToken("starttime").ToString());
                }
                else
                {
                    callBackPropObj.StartTime = 0;
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("endtime") != null)
                {
                    callBackPropObj.EndTime = Convert.ToInt32(reqObj.SelectToken("smscresponse").SelectToken("endtime").ToString());
                }
                else
                {
                    callBackPropObj.EndTime = 0;
                }
                if (reqObj.SelectToken("smscresponse").SelectToken("RequestUUID") != null)
                {
                    callBackPropObj.RequestUUID = reqObj.SelectToken("smscresponse").SelectToken("RequestUUID").ToString();
                }
                if (reqObj.ToString().ToUpper() == "EXIT")
                {
                    callBackPropObj.CallStatus = "completed";
                }
            }
        }
        /// <summary>
        /// This function is used to get the grpcall reports from db
        /// </summary>
        /// <param name="sConnString"></param>
        /// <param name="ConfID"></param>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public JObject GetReportsByBatchIdNew(string sConnString, int userID, int confID, string batchID, int pageIndex, int pageSize, string searchText)
        {
            DataSet ds = new DataSet();
            JObject jObj = new JObject();
            JObject jObj1 = new JObject();
            int retVal = 0, isConfRecorded = 0, pageCount = 0, errorCode = 0;
            int isRecorded = 0;
            string retMessage = "";
            short isCallFromBonus = 0;
            string confRoom = "", recordedFile = "";
            string callstatus = "";
            string recordPathURL = ConfigurationManager.AppSettings["recordClipPath"].ToString();
            JArray jArrClips = new JArray();
            JArray jArr = new JArray();
            JArray jArr1 = new JArray();
            try
            {
                DataAccessLayer.V_1_5.GroupcallReports groupCallReportsObj = new DataAccessLayer.V_1_5.GroupcallReports(sConnString);
                ds = groupCallReportsObj.GetReportsByBatchIdNew(userID, confID, batchID, pageIndex, pageSize, searchText, out pageCount, out retVal, out retMessage, out isCallFromBonus, out errorCode);
                if (retVal == 1)
                {

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            var _with1 = ds.Tables[0];
                            if (Convert.ToString(_with1.Rows[0]["RecordClip"].ToString()) != string.Empty)
                            {
                                recordPathURL = recordPathURL + _with1.Rows[0]["RecordClip"].ToString();
                            }
                            else
                            {
                                recordPathURL = "";
                            }
                            jObj1 = (new JObject(new JProperty("Members", _with1.Rows[0]["members"]),
                                                    new JProperty("TotalMembers", _with1.Rows[0]["totmembers"].ToString()),
                                                    new JProperty("CouldNotJoin", _with1.Rows[0]["notjoined"].ToString()),
                                                    new JProperty("ConfName", _with1.Rows[0]["confname"].ToString()),
                                                    new JProperty("RecordClip", recordPathURL),
                                                    new JProperty("TotalCallDuration", _with1.Rows[0]["CallDuration"].ToString()),
                                                    new JProperty("DurationInHours", _with1.Rows[0]["HoursDuration"].ToString()),
                                                    new JProperty("Currency", _with1.Rows[0]["Currency"].ToString()),
                                                    new JProperty("TotalCallPrice", _with1.Rows[0]["TotalCallPrice"].ToString()),
                                                    new JProperty("TotalMinutes", _with1.Rows[0]["TotalMinutes"].ToString()),
                                                    new JProperty("Date", _with1.Rows[0]["Date"].ToString())));



                        }

                        if (ds.Tables[1].Rows.Count > 0)
                        {
                            var _with2 = ds.Tables[1];

                            for (int iterator = 0; iterator < _with2.Rows.Count; iterator++)
                            {
                                if (_with2.Rows[iterator]["status"].ToString() == "")
                                {
                                    callstatus = "No Call";
                                }
                                else
                                {
                                    callstatus = _with2.Rows[iterator]["status"].ToString().Replace("_", " ");
                                }

                                jArr.Add(new JObject(new JProperty("Member", _with2.Rows[iterator]["number"]),
                                new JProperty("Status", callstatus),
                                new JProperty("MobileNumber", _with2.Rows[iterator]["mobilenumber"].ToString()),
                                new JProperty("PricePerMinute", _with2.Rows[iterator]["PricePerMinute"].ToString()),
                                new JProperty("mintime", _with2.Rows[iterator]["mintime"].ToString()),
                                new JProperty("IsAdded", _with2.Rows[iterator]["IsAdded"].ToString()),
                                new JProperty("maxtime", _with2.Rows[iterator]["maxtime"].ToString()),
                                new JProperty("Duration", _with2.Rows[iterator]["Duration"].ToString()),
                                new JProperty("LeftOn", _with2.Rows[iterator]["LeftOn"].ToString()),
                                new JProperty("DurationInHours", _with2.Rows[iterator]["DurationInHours"].ToString())));


                            }
                        }


                        if (ds.Tables[2].Rows.Count > 0)
                        {

                            var _with3 = ds.Tables[2];
                            for (int iterator = 0; iterator < _with3.Rows.Count; iterator++)
                            {

                                jArr1.Add(new JObject(new JProperty("Member", _with3.Rows[iterator]["name"]),
                                new JProperty("Number", _with3.Rows[iterator]["MobileNo"].ToString()),
                                new JProperty("IsInbound", _with3.Rows[iterator]["isinbound"].ToString()),
                                new JProperty("CallPrice", _with3.Rows[iterator]["callprice"].ToString()),
                                new JProperty("Duration", _with3.Rows[iterator]["duration"].ToString())));


                            }

                        }

                        jObj = new JObject(new JProperty("Success", true),
                                            new JProperty("Message", retMessage),
                                            new JProperty("ErrorCode", errorCode),
                                            new JProperty("Data", jObj1),
                                            new JProperty("Data2", jArr1),
                                            new JProperty("Items", jArr),
                                            new JProperty("RcFiles", jArrClips),
                                            new JProperty("IsCallFromBonus", isCallFromBonus),
                                            new JProperty("TotalPageCount", pageCount));


                    }
                    else
                    {
                        jObj = new JObject(new JProperty("Success", false),
                                           new JProperty("Message", "There are no schedules to this conference"), new JProperty("ErrorCode", "126"));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupCallReportsBusiness.GetGrpCallReports is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                                    new JProperty("Message", "Something Went Wrong"),
                                    new JProperty("ErrorCode", "101"));
            }
            return jObj;
        }

        //public JObject GetReportsByBatchIdNew(string sConnString, int userID, int confID, string batchID, int pageIndex, int pageSize)
        //{

        //    int retVal = 0, pageCount = 0;
        //    string retMessage = "";
        //    short isCallFromBonus = 0;
        //    JObject totalCallReportJobj = new JObject();
        //    JObject detailedCallReportJobj = new JObject();
        //    JArray jArr = new JArray();
        //    JArray jArr2 = new JArray();

        //    try
        //    {
        //        DataAccessLayer.V_1_3.GroupcallReports_V130 groupCallReportsObj = new DataAccessLayer.V_1_3.GroupcallReports_V130(sConnString);
        //        ds = groupCallReportsObj.GetReportsByBatchIdNew(userID, confID, batchID, pageIndex, pageSize, out pageCount, out retVal, out retMessage, out isCallFromBonus);
        //        if (retVal == 1)
        //        {
        //            if (ds.Tables.Count > 0)
        //            {
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {
        //                    foreach (DataRow _row in ds.Tables[0].Rows)
        //                    {
        //                        totalCallReportJobj = new JObject();
        //                        foreach (DataColumn _column in ds.Tables[0].Columns)
        //                        {
        //                            totalCallReportJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
        //                        }
        //                        jArr.Add(totalCallReportJobj);
        //                    }
        //                }
        //                if (ds.Tables[1].Rows.Count > 0)
        //                {

        //                    foreach (DataRow _row in ds.Tables[1].Rows)
        //                    {
        //                        detailedCallReportJobj = new JObject();
        //                        foreach (DataColumn _column in ds.Tables[1].Columns)
        //                        {
        //                            detailedCallReportJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
        //                        }
        //                        jArr2.Add(detailedCallReportJobj);
        //                    }

        //                }
        //                jObj = new JObject(new JProperty("Success", true),
        //                                    new JProperty("Message", retMessage),
        //                                    new JProperty("Data", jArr),
        //                                    new JProperty("Data2", jArr2),
        //                                    new JProperty("PageCount", pageCount),
        //                                    new JProperty("IsCallFromBonus", isCallFromBonus));
        //            }

        //            else
        //            {
        //                jObj = new JObject(new JProperty("Success", false),
        //                                  new JProperty("Message", "There are no schedules to this conference"));
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ExceptionLog("Exception in GroupCallReportsBusiness.GetGrpCallReportsLatest is " + ex.ToString());
        //        jObj = new JObject(new JProperty("Success", false),
        //                            new JProperty("Message", "Something Went Wrong"),
        //                            new JProperty("ErrorCode", "E0002"));
        //    }
        //    return jObj;
        //}


        public void Download_ExcelFile(string sConnString, string batchId)
        {
            DataTable dt = new DataTable();
            int retVal = 0;
            string retMessage = "";
            string reportName = "";
            try
            {
                HttpResponse Response = HttpContext.Current.Response;
                DataAccessLayer.V_1_5.GroupcallReports grpReports = new DataAccessLayer.V_1_5.GroupcallReports(sConnString);
                dt = grpReports.Download_ExcelFile(batchId, out retVal, out retMessage, out reportName);
                if (retVal == 1)
                {
                    Response.ClearContent();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", reportName + ".xls"));
                    Response.ContentType = "application/ms-excel";
                    string str = string.Empty;
                    foreach (DataColumn dtcol in dt.Columns)
                    {
                        Response.Write(str + dtcol.ColumnName);
                        str = "\t";
                    }
                    Response.Write("\n");
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            str = "";
                            for (int j = 0; j < dt.Columns.Count; j++)
                            {
                                Response.Write(str + Convert.ToString(dr[j]));
                                str = "\t";
                            }
                            Response.Write("\n");
                        }
                    }
                    else
                        Response.Write(retMessage);
                    Response.End();
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupCallReportsBusiness.GetGrpCallReports is " + ex.ToString());
            }


        }
        public JObject SendEmailReportOfGroup(string sConnString, int userId, JObject paramJobj)
        {
            JObject responseJObj = new JObject();
            DataTable dt = new DataTable();
            
            short retVal = 0;
            string retMsg = string.Empty;
            try
            {

                if (string.IsNullOrEmpty(Convert.ToString(paramJobj.SelectToken("GroupID"))))
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                                              new JProperty("Message", "Mandatory Parameter missing"), new JProperty("ErrorCode", "111"));
                    return responseJObj;
                }
                GT.DataAccessLayer.V_1_5.GroupcallReports reportsObj = new DataAccessLayer.V_1_5.GroupcallReports(sConnString);
                dt = reportsObj.SendEmailReportOfGroup(userId, Convert.ToInt64(paramJobj.SelectToken("GroupID")), Convert.ToString(paramJobj.SelectToken("BatchID")), out retVal, out retMsg);
               // string fileName = "Report" + DateTime.Now.ToString("_MMddyyyy_HHmmss") + ".xls";
               // dt.WriteXml(System.Configuration.ConfigurationManager.AppSettings["ReportsPhysicalPath"]+fileName);
                
                if (retVal == 1)
                {
                    responseJObj = new JObject(new JProperty("Success", true),
                                       new JProperty("Message", retMsg),
                                       new JProperty("ErrorCode", "101"));
                }
                else
                {
                    responseJObj = new JObject(new JProperty("Success", false),
                                      new JProperty("Message", retMsg),
                                      new JProperty("ErrorCode", "101"));
                }
               // reportsObj.SendReportsMail(userId, Convert.ToInt64(paramJobj.SelectToken("GroupID")), fileName, out retVal, out retMsg);
            }
            catch (Exception ex)
            {

                Logger.ExceptionLog("Exception in SendEmailReportOfGroup BAL is " + ex.ToString());
                responseJObj = new JObject(new JProperty("Success", false),
                                    new JProperty("Message", "Something Went Wrong"),
                                    new JProperty("ErrorCode", "101"));
            }
            return responseJObj;
        }


    }
}
