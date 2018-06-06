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


namespace GT.BusinessLogicLayer
{
    public class UpdateCallBacksBussiness
    {
        string grpCallName = "";
        CallBackVariable callBackPropObj = new CallBackVariable();
        DataSet globalDs = new DataSet();
        StringBuilder callBackResponse = new StringBuilder();
        JObject jObj = new JObject();
        DataSet ds = new DataSet();
        int isInProgress = 0;
        public string UpdateCallBacks(string sConnString, string methodType, string callBackIpAddr, JObject callBackReqObj, string pusherAppId, string pusherAppKey, string pusherAppsecret, string HangupCauses, HttpContext respContext, string callBackUrl)
        {
            //Parsing The Data
            ParseResponse(methodType, callBackReqObj, respContext);

            string pusherCallStatus = callBackPropObj.CallStatus;
            string eventName = "call_status";
            string[] hangupCasuesForRetry = null;
            int hangUpCount = 0;
            int callDrop = 0;
            hangupCasuesForRetry = HangupCauses.Split(",".ToCharArray());
            try
            {
                //Updating GroupCall Status Method calling
                globalDs = UpdateCallBacksCallDb(sConnString, callBackPropObj, callBackIpAddr);
                Pusher pusherObj = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);

                JObject retryResponse = new JObject();
                if (hangupCasuesForRetry.Contains(callBackPropObj.EndReason))
                {
                    if (callBackPropObj.StartTime > 0 && (callBackPropObj.EndTime - callBackPropObj.StartTime) > 0)
                    { callDrop = 1; }
                    BusinessLogicLayer.V_1_5.GroupCall groupcallObj = new BusinessLogicLayer.V_1_5.GroupCall();
                    retryResponse = groupcallObj.GrpCallRetryBLL(sConnString, callBackPropObj.CallUUID, callBackPropObj.RequestUUID, callBackUrl, callDrop);
                    callBackResponse.Append("Retry Response : " + retryResponse.ToString());
                    if (string.IsNullOrEmpty(retryResponse.SelectToken("Success").ToString()) == false)
                    {
                        if (Convert.ToBoolean(retryResponse.SelectToken("Success").ToString()) == true)
                        {

                            pusherCallStatus = "redial";

                        }
                    }

                }
                if (globalDs.Tables.Count > 0 && globalDs.Tables[0].Rows.Count > 0)
                {
                    var _with1 = globalDs.Tables[0];
                    
                    Logger.TraceLog("Pusher REsponse for outbound Main");
                    if (Convert.ToInt32(globalDs.Tables[0].Rows[0]["IsInterConnect"]) == 0)
                    {
                        if (pusherCallStatus == "redial")
                        {
                            hangUpCount = Convert.ToInt32(globalDs.Tables[0].Rows[0]["HangUpCount"]) - 1;
                        }
                        else
                        {
                            hangUpCount = Convert.ToInt32(globalDs.Tables[0].Rows[0]["HangUpCount"]);
                        }


                        Logger.TraceLog("Pusher REsponse for outbound");
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
                            isprivate = globalDs.Tables[0].Rows[0]["IsPrivate"],
                            member = globalDs.Tables[0].Rows[0]["Member"],
                            MemberJoinTime = Convert.ToString(globalDs.Tables[0].Rows[0]["MemberJoinTime"]),
                            conf_type = globalDs.Tables[0].Rows[0]["ConferenceType"],
                            conf_digits = callBackPropObj.Digits,
                            IsMember = globalDs.Tables[0].Rows[0]["IsMember"],
                            conf_id = globalDs.Tables[0].Rows[0]["ConferenceId"],
                            record_status = globalDs.Tables[0].Rows[0]["RecordStatus"],
                            record_count = globalDs.Tables[0].Rows[0]["RecordCount"],
                            dig_prs_count = globalDs.Tables[0].Rows[0]["DigitPressCount"],
                            inpro_count = globalDs.Tables[0].Rows[0]["InprogressCount"],
                            unmute_count = globalDs.Tables[0].Rows[0]["UnmuteCount"],
                            mute_count = globalDs.Tables[0].Rows[0]["MuteCount"],
                            end_reason = globalDs.Tables[0].Rows[0]["end_reason"],
                            isinprogress = isInProgress,
                            AllMembersCount = globalDs.Tables[0].Rows[0]["AllMembersCount"],
                            OnCallCount = globalDs.Tables[0].Rows[0]["OnCallCount"],
                            HangUpCount = hangUpCount,
                            MuteCount = globalDs.Tables[0].Rows[0]["MuteMemCount"],
                            HandRaiseCount = globalDs.Tables[0].Rows[0]["HandRaiseCount"],
                            MembersCount = globalDs.Tables[0].Rows[0]["AllMembersCount"],
                            PrivateCount = globalDs.Tables[0].Rows[0]["PrivateCount"]
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

                }

                else
                {
                    callBackResponse.Append("No Member Data Returned From Database");
                }
                if (globalDs.Tables.Count > 1 && globalDs.Tables[1].Rows.Count > 0)
                {
                    if (Convert.ToInt32(globalDs.Tables[0].Rows[0]["PrivateCount"]) == 0)
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
                        BusinessLogicLayer.V_1_4.GroupCall_V140 GrpcallObj = new BusinessLogicLayer.V_1_4.GroupCall_V140();
                        alonePlayResponse = GrpcallObj.PlayToAConferenceCall(globalDs.Tables[1].Rows[0]["ConferenceRoom"].ToString(), Convert.ToString(globalDs.Tables[1].Rows[0]["MemberId"]), globalDs.Tables[1].Rows[0]["HttpUrl"].ToString(), "https://new.grpTalk.com/DefaultClips/thank_you.mp3");
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
                }
                else
                {
                    callBackResponse.Append("No Alone Member");
                }
                if (globalDs.Tables.Count > 2)
                {

                    JObject privateResponse = new JObject();
                    if (globalDs.Tables[2].Rows.Count > 0)
                    {
                        Logger.TraceLog("Private Membs" + globalDs.Tables[2].Rows.Count);
                        V_1_4.GroupCall_V140 grpCallObj = new V_1_4.GroupCall_V140();
                        for (int row = 0; row < globalDs.Tables[2].Rows.Count; row++)
                        {

                            privateResponse = grpCallObj.PrivatePublicApi(Convert.ToString(globalDs.Tables[2].Rows[row]["Conferenceroom"]), Convert.ToString(globalDs.Tables[2].Rows[row]["member_id"]), false, Convert.ToString(globalDs.Tables[2].Rows[row]["HttpUrl"]), Convert.ToBoolean(globalDs.Tables[2].Rows[row]["Mute"]));
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at callBackresponseBLL : " + ex.ToString());
                callBackResponse.Append("Exception : " + ex.ToString());
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
            try
            {
                UpdateCallBacksEntity groupCallReportsObj = new UpdateCallBacksEntity(sConnString);
                callBackDs = groupCallReportsObj.UpdateCallBacksOfGroupCall(callBackProp, callBackAddr, out isRecording, out notify, out grpCallId, out grpCallName, out isInProgress);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at UpdateCallBacksCallDb :" + ex.ToString());
                throw ex;
            }
            return callBackDs;

        }


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

                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceUUID]"]) == false)
                {
                    callBackPropObj.ConferenceUUID = respContext.Request["smscresponse[ConferenceUUID]"];
                }
                else
                {
                    callBackPropObj.ConferenceUUID = "";
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
                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConfInstanceMemberID]"]) == false)
                {
                    callBackPropObj.grpCallMemberID = respContext.Request["smscresponse[ConfInstanceMemberID]"];
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


                if (string.IsNullOrEmpty(respContext.Request["smscresponse[ConferenceSize]"]) == false)
                {
                    Logger.TraceLog("Con Size" + respContext.Request["smscresponse[ConferenceSize]"] + "callBackPropObj.GrpCallAction" + callBackPropObj.GrpCallAction);
                    callBackPropObj.ConferenceSize = Convert.ToInt32(respContext.Request["smscresponse[ConferenceSize]"]);
                }
                else
                {
                    callBackPropObj.ConferenceSize = 0;
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
                if (reqObj.SelectToken("smscresponse").SelectToken("ConfInstanceMemberID") != null)
                { // for tata gateway
                    callBackPropObj.grpCallMemberID = reqObj.SelectToken("smscresponse").SelectToken("ConfInstanceMemberID").ToString();
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
                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceUUID") != null)
                {
                    callBackPropObj.ConferenceUUID = reqObj.SelectToken("smscresponse").SelectToken("ConferenceUUID").ToString();
                }

                if (reqObj.SelectToken("smscresponse").SelectToken("RequestUUID") != null)
                {
                    callBackPropObj.RequestUUID = reqObj.SelectToken("smscresponse").SelectToken("RequestUUID").ToString();
                }
                if (reqObj.ToString().ToUpper() == "EXIT")
                {
                    callBackPropObj.CallStatus = "completed";
                }


                if (reqObj.SelectToken("smscresponse").SelectToken("ConferenceSize") != null)
                {
                    Logger.TraceLog("Con Size" + reqObj.SelectToken("smscresponse").SelectToken("ConferenceSize") + "callBackPropObj.GrpCallAction" + callBackPropObj.GrpCallAction);
                    callBackPropObj.ConferenceSize = Convert.ToInt32(reqObj.SelectToken("smscresponse").SelectToken("ConferenceSize").ToString());
                }
                else
                {
                    callBackPropObj.ConferenceSize = 0;
                }


            }
        }
    }
}
