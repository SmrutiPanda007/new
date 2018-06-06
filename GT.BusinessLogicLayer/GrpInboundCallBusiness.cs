using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GT.DataAccessLayer;
using Newtonsoft.Json.Linq;
using System.Data;
using GT.Utilities;
using GT.Utilities.Properties;
using PusherServer;
using System.Configuration;
namespace GT.BusinessLogicLayer
{
    public class GrpInboundCallBusiness
    {
        public string ValidateInboundCall()
        {
            grpcall grpCallPropObj = new grpcall();
            String responseXml = "<Response><Hangup reason='start'/></Response>";
            string voiceClipUrl = ConfigurationManager.AppSettings["GrpTalkVoiceClipsUrl"].ToString();
            this.WelcomeClip = voiceClipUrl + "GrpTalkWelcomeClipNew.mp3";
            if ((this.Event == "newcall") || (this.Event == "getkeys"))
            {
                int nodeGatewayId = 0;

                nodeGatewayId = CheckIsInterConnectCall(this.FromNumber, this.ToNumber);
                this.NodeGatewayId = nodeGatewayId;
                if (nodeGatewayId > 0)
                {
                    string[] digitsObj = null;
                    if (this.Event == "newcall")
                    {
                        responseXml = "<Response><GetDigits timeout='20' numDigits='100' action='" + this.InboundAnswerUrl + "' validDigits='1234567890@*#abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ'><Play>silence_stream://1000</Play></GetDigits></Response>";
                        return responseXml;
                    }
                    else if (this.Event == "getkeys")
                    {

                        digitsObj = this.Digits.Split('A');
                        this.IsAll = false;
                        DataSet interConnectCallsData = GetInterConnectCallssData(Convert.ToInt32(Digits));
                        if (interConnectCallsData.Tables.Count > 0)
                        {

                            this.IsAll = Convert.ToBoolean(interConnectCallsData.Tables[0].Rows[0]["IsAll"]);
                            this.BatchId = Convert.ToString(interConnectCallsData.Tables[0].Rows[0]["BatchId"]);
                            this.ConferenceId = Convert.ToInt32(interConnectCallsData.Tables[0].Rows[0]["ConfId"]);
                            this.MobileNumber = Convert.ToString(interConnectCallsData.Tables[0].Rows[0]["MobileNumber"]);


                            grpCallPropObj.IsAll = Convert.ToBoolean(interConnectCallsData.Tables[0].Rows[0]["IsAll"]);
                            grpCallPropObj.IsMute = false;
                            grpCallPropObj.CallUUID = interConnectCallsData.Tables[0].Rows[0]["BatchId"].ToString();
                            grpCallPropObj.ConferenceId = Convert.ToInt32(interConnectCallsData.Tables[0].Rows[0]["ConfId"]);
                            grpCallPropObj.MobileNumber = interConnectCallsData.Tables[0].Rows[0]["MobileNumber"].ToString();

                            responseXml = this.IsAll.ToString() + "---" + this.BatchId + "---conf_id=" + this.ConferenceId.ToString();

                        }
                        else
                        {
                            responseXml = "<Response><Hangup reason='InterConnect Calls data not found'/></Response>";
                            return responseXml;
                        }
                        int TimeLimit = 0;
                        DataSet interConnectMembersData = new DataSet();
                        interConnectMembersData = GetInterConnectMembersData(out TimeLimit);
                        if (interConnectMembersData.Tables.Count > 0)
                        {
                            grpCallPropObj.TimeLimit = TimeLimit;
                            grpCallPropObj.WaitClip = this.WaitClip;
                            grpCallPropObj.WelcomeClip = this.WelcomeClip;
                            grpCallPropObj.ConferenceRoom = this.ConferenceRoom;
                            responseXml = this.IsAll.ToString() + "---" + this.BatchId + "---conf_id inter connect members > 0=" + this.ConferenceId.ToString();
                            this.NodeDependedMobileNumberReportIdTable = interConnectMembersData.Tables[0];
                            this.NodeDependendGateWaysTable = interConnectMembersData.Tables[1];
                            V_1_2.GroupCall_V120 grpCallObj = new V_1_2.GroupCall_V120();
                            grpCallObj.Dial(this.ConnString, grpCallPropObj, this.PreJoinCallBackUrl, this.NodeDependedMobileNumberReportIdTable, this.NodeDependendGateWaysTable);
                            responseXml = "<Response><Conference stayAlone='false'>" + this.ConferenceRoom + "</Conference></Response>";
                            return responseXml;
                        }
                    };
                }
                else
                {
                    responseXml = this.IsAll.ToString() + "---" + this.BatchId + "---conf_id all public calls here =" + this.ConferenceId.ToString() + this.Event;
                    DataSet newcallData = null;
                    DataSet InBoundCallData = new DataSet();
                    string dbMessage = "";
                    int mode = 0;
                    Boolean isModetator = false;

                    newcallData = NewInboundCall(out dbMessage);
                    try
                    {
                        if (dbMessage != "")
                        {
                            Logger.TraceLog("dbMessage " + dbMessage);

                            if (dbMessage.ToLower() == "joinconf")
                            {
                                Logger.TraceLog("table count" + newcallData.Tables[0].Rows.Count.ToString());
                                if (newcallData.Tables[0].Rows.Count > 0)
                                {
                                    mode = Convert.ToInt32(newcallData.Tables[0].Rows[0]["Mode"]);
                                    Logger.TraceLog("Mode : " + mode.ToString());
                                    if (Convert.ToInt32(newcallData.Tables[0].Rows[0]["IsValidatePin"]) == 1)
                                    {
                                        if (newcallData.Tables[0].Rows[0]["AccessKey"].ToString() == this.Digits)
                                        {
                                            responseXml = InBoundCallDial(Convert.ToInt64(newcallData.Tables[0].Rows[0]["GrpCallId"].ToString()), this.FromNumber, this.ToNumber, this.CallUuid, mode);
                                        }
                                        else
                                        {
                                            responseXml = "<Response><play>" + voiceClipUrl + "InvalidPIN.mp3" + "</play></Response>";
                                        }
                                    }
                                    else
                                    {
                                        Logger.TraceLog("from " + this.FromNumber);
                                        responseXml = InBoundCallDial(Convert.ToInt64(newcallData.Tables[0].Rows[0]["GrpCallId"].ToString()), this.FromNumber, this.ToNumber, this.CallUuid, mode);
                                    }

                                }
                                else
                                {
                                    responseXml = "<Response><Speak>No Group Call</Speak></Response>";
                                }
                                Logger.TraceLog("responseXml : " + responseXml);
                                return responseXml;

                            }
                            else if (dbMessage.ToLower() == "playivrforpin")
                            {
                                responseXml = "<Response><GetDigits action='" + this.PreJoinCallBackUrl + "' method='GET' numDigits='6' timeout='15'><Play>" + voiceClipUrl + "EnterPin.mp3" + "</Play></GetDigits></Response>";
                                Logger.TraceLog("Response Xml : " + responseXml);
                            }
                            else if (dbMessage.ToLower() == "pleasewait")
                            {
                                responseXml = "<Response><play>" + voiceClipUrl + "NonLiveGroupCall.mp3 " + "</play></Response>";
                                Logger.TraceLog("Response Xml : " + responseXml);
                            }
                            else if (dbMessage.ToLower() == "playivr")
                            {
                                Logger.TraceLog("playivr Started");
                                int i = 0;
                                string ivrMessage = "<play>" + voiceClipUrl + "PleasePress.mp3" + "</play>";
                                for (i = 0; i <= newcallData.Tables[0].Rows.Count - 1; i++)
                                {
                                    if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 1)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "One.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 2)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Two.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 3)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Three.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 4)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Four.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 5)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Five.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 6)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Six.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 7)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Seven.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 8)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Eight.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }
                                    else if (Convert.ToInt32(newcallData.Tables[0].Rows[i]["slno"]) == 9)
                                    {
                                        ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "Nine.mp3" + "</play><play>" + voiceClipUrl + "ToJoin.mp3" + "</play>";
                                    }

                                    ivrMessage = ivrMessage + "<speak>" + newcallData.Tables[0].Rows[i]["ConfName"].ToString() + "</speak>";

                                }
                                ivrMessage = ivrMessage + "<play>" + voiceClipUrl + "PleasePressStar.mp3" + "</play>";
                                ivrMessage = "<play>" + voiceClipUrl + "MorethanOneCalls.mp3" + "</play>" + ivrMessage;
                                responseXml = "<Response><GetDigits validDigits ='1234567890*' action='" + this.InboundAnswerUrl + "' numDigits='1' timeout='15' method='GET'>" + ivrMessage + "</GetDigits></Response>";
                                Logger.TraceLog("responseXml " + responseXml);
                            }
                            else if (dbMessage.ToLower() == "justcompleted")
                            {
                                responseXml = "<Response><Speak>Selected Group was just before completed</Speak><Hangup/></Response>";
                            }
                            else if (dbMessage.ToLower() == "noconf")
                            {
                                responseXml = "<Response><Speak>No group created With your number</Speak><Hangup/></Response>";
                            }
                            else if (dbMessage.ToLower() == "invalidaccesskey")
                            {
                                responseXml = "<Response><play>" + voiceClipUrl + "InvalidPIN.mp3" + "</play></Response>";
                            }
                            else if (dbMessage.ToLower() == "hangup")
                            {
                                responseXml = "<Response><Hangup 'hangup'/></Response>";
                            }
                            else if (dbMessage.ToLower() == "exception")
                            {
                                responseXml = "<Response><Hangup data= '" + dbMessage + "'/></Response>";
                            }
                            else
                            {
                                responseXml = "<Response><Hangup data= '" + dbMessage + "'/></Response>";
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ExceptionLog("exception at xml creation : " + ex.ToString());
                    }
                }
            };
            return responseXml;
        }
        private string InBoundCallDial(Int64 conferenceId, string fromNumber, string toNumber, string callUUID, int mode)
        {
            Logger.TraceLog("Insert data started");
            Logger.TraceLog("Insert data started fromNumber " + fromNumber);
            DataSet InBoundCallData = new DataSet();
            string responseXml = "";
            Int16 retVal = 0;
            long durationLimit = 0;
            string retMsg = "";
            Boolean isModetator = false;
            GT.DataAccessLayer.GrpInboundCallEntity inBoundObj = new GrpInboundCallEntity(this.ConnString);
            InBoundCallData = inBoundObj.ConnectInBoundToGroupCall(conferenceId, fromNumber, toNumber, callUUID, mode, out retVal, out retMsg, out durationLimit);
            Logger.TraceLog("retval " + retVal.ToString());

            if (retVal == 1)
            {
                this.ConferenceRoom = InBoundCallData.Tables[0].Rows[0]["ConfRoom"].ToString();
                isModetator = Convert.ToBoolean(InBoundCallData.Tables[0].Rows[0]["Ismoderator"]);
                if (isModetator == false)
                {

                    if (Convert.ToInt16(InBoundCallData.Tables[0].Rows[0]["OnCallInProgressCount"]) == 1 || Convert.ToInt16(InBoundCallData.Tables[0].Rows[0]["IsModeratorPresent"]) == 0)
                    {
                        this.WaitClip = ConfigurationManager.AppSettings["GrpTalkVoiceClipsUrl"].ToString() + "WaitClipForHost.mp3";
                    }
                    if (Convert.ToBoolean(InBoundCallData.Tables[0].Rows[0]["Ismute"]) == true)
                    {
                        responseXml = "<Response><Play>" + this.WelcomeClip + "</Play><Conference callbackUrl='" + this.PostJoinCallBackUrl + "'  muted='true' timeLimitForce='" + durationLimit + "' timeLimit='" + durationLimit + "'  digitsMatch='0'  waitSound ='" + this.WaitClip + "'  >" + this.ConferenceRoom + "</Conference></Response>";
                    }
                    else
                    {
                        responseXml = "<Response><Play>" + this.WelcomeClip + "</Play><Conference callbackUrl='" + this.PostJoinCallBackUrl + "'  muted='false' timeLimitForce='" + durationLimit + "' timeLimit='" + durationLimit + "' digitsMatch='0'  waitSound ='" + this.WaitClip + "'  >" + this.ConferenceRoom + "</Conference></Response>";
                    }
                }
                else if (isModetator == true)
                {
                    if (Convert.ToInt16(InBoundCallData.Tables[0].Rows[0]["OnCallInProgressCount"]) == 0)
                    {
                        this.WaitClip = ConfigurationManager.AppSettings["GrpTalkVoiceClipsUrl"].ToString() + "WaitClipForMembers.mp3";
                    }
                    //responseXml = "<Response><Play>" + this.WelcomeClip + "</Play><Conference callbackUrl='" + this.PostJoinCallBackUrl + "' endConferenceOnExit='true' digitsMatch='0'  waitSound ='" + this.WaitClip + "'  >" + this.ConferenceRoom + "</Conference></Response>";
                    responseXml = "<Response><Play>" + this.WelcomeClip + "</Play><Conference callbackUrl='" + this.PostJoinCallBackUrl + "' timeLimitForce='" + durationLimit + "' timeLimit='" + durationLimit + "'  digitsMatch='0'  waitSound ='" + this.WaitClip + "'  >" + this.ConferenceRoom + "</Conference></Response>";
                }
                else
                {
                    responseXml = "<Response><Play>" + this.WelcomeClip + "</Play><Conference callbackUrl='" + this.PostJoinCallBackUrl + "' digitsMatch='0'  waitSound ='" + this.WaitClip + "'  >" + this.ConferenceRoom + "</Conference></Response>";
                }

            }
            else
            {
                responseXml = "<Response><Speak>No Group Call Found</Speak></Response>";
            }
            return responseXml;

        }

        private int CheckIsInterConnectCall(string fromNumber, string toNumber)
        {
            int gatewayId = 0;
            try
            {
                GrpInboundCallEntity GrpInboundCallEntityObj = new GrpInboundCallEntity(this.ConnString);
                gatewayId = GrpInboundCallEntityObj.CheckIsInterConnectCallEntity(this.FromNumber, this.ToNumber);
                return gatewayId;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" grpInboundCallBusiness CheckIsInterConnectCall() :  " + ex.ToString());
                return 0;
            }
        }
        private DataSet GetInterConnectCallssData(int slNo)
        {
            DataSet interConnectCallsData = new DataSet();
            try
            {
                GrpInboundCallEntity GrpInboundCallEntityObj = new GrpInboundCallEntity(this.ConnString);
                interConnectCallsData = GrpInboundCallEntityObj.GetInterConnectCallssDataEntity(slNo);
                return interConnectCallsData;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" grpInboundCallBusiness GetInterConnectCallssData() :  " + ex.ToString());
                return interConnectCallsData;
            }

        }
        private DataSet GetInterConnectMembersData(out int DurationLimit)
        {
            int timeLimit = 0;
            DataSet interConnectMembersData = new DataSet();
            try
            {

                GrpInboundCallEntity GrpInboundCallEntityObj = new GrpInboundCallEntity(this.ConnString);
                interConnectMembersData = GrpInboundCallEntityObj.GetInterConnectMembersDataEntity(this.BatchId, this.ConferenceId, this.IsAll, this.MobileNumber, this.NodeSlno, this.NodeGatewayId, out timeLimit);
                if (interConnectMembersData.Tables.Count > 0)
                {

                    this.ConferenceRoom = interConnectMembersData.Tables[1].Rows[0]["conf_room"].ToString();
                    this.ConferenceId = Convert.ToInt32(interConnectMembersData.Tables[1].Rows[0]["id"]);
                    this.WelcomeClip = interConnectMembersData.Tables[1].Rows[0]["welcome_clip"].ToString();
                    this.WaitClip = interConnectMembersData.Tables[1].Rows[0]["wait_clip"].ToString();
                    //this.IsMute = Convert.ToBoolean(interConnectMembersData.Tables[2].Rows[0]["mute"]);
                    this.IsMute = false;
                    //if (interConnectMembersData.Tables[2].Rows[0]["end_conf_onexit"] != System.DBNull.Value)
                    //{
                    //    this.EndConferenceOnExit = Convert.ToBoolean(interConnectMembersData.Tables[2].Rows[0]["end_conf_onexit"]);
                    //}
                    //else
                    //{
                    //    this.EndConferenceOnExit = false;
                    //}


                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" grpInboundCallBusiness GetInterConnectMembersData() :  " + ex.ToString());
                throw ex;
            }
            DurationLimit = timeLimit;
            return interConnectMembersData;
        }
        private DataSet NewInboundCall(out string dbMessage)
        {
            Logger.TraceLog("NewInboundCall BLL");
            DataSet newInboundCall = null;
            string retMessege;
            try
            {
                GrpInboundCallEntity GrpInboundCallEntityObj = new GrpInboundCallEntity(this.ConnString);
                newInboundCall = GrpInboundCallEntityObj.NewInboundCallEntity(this.Event, this.ToNumber, this.FromNumber, this.CallUuid, this.CallStatus, this.Digits, out retMessege);
                dbMessage = retMessege;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" grpInboundCallBusiness NewInboundCall() :  " + ex.ToString());
                dbMessage = "exception";

            }
            return newInboundCall;
        }

        string PusherAppKey = "", PusherAppSecret = "", WebSocketEventName = "";
        string PusherAppId = "";

        public string InBoundCallBack(string sConnString, string conferenceUUID, int conferenceSize, string callUUID, string fromNumber, string toNumber, string eventName, string callStatus, string requestUUID, int grpCallMemberId, string action, string digits, Int64 startTime, Int64 endTime, string endReason)
        {
            Logger.TraceLog("end reason bll " + endReason);
            Logger.TraceLog(conferenceUUID + "-conferenceUUID " + callUUID + "fromNumber" + fromNumber + "toNumber" + toNumber + "event" + eventName + "callStatus" + callStatus + "digits" + digits + "requestUUID" + requestUUID + "grpCallMemberId" + grpCallMemberId);
            DataSet responseDataSet = new DataSet();
            GrpInboundCallEntity GrpInboundCallEntityObj = new GrpInboundCallEntity(sConnString);
            responseDataSet = GrpInboundCallEntityObj.InBoundCallBack(conferenceUUID, conferenceSize, callUUID, fromNumber, toNumber, eventName, callStatus, requestUUID, grpCallMemberId, action, digits, startTime, endTime, endReason);
            PublishMessageToWebSocket(responseDataSet, fromNumber, toNumber, callStatus, action);
            if (responseDataSet.Tables.Count > 1)
            {

                JObject privateResponse = new JObject();
                if (responseDataSet.Tables[1].Rows.Count > 0)
                {
                    Logger.TraceLog("Private Membs" + responseDataSet.Tables[1].Rows.Count);
                    V_1_4.GroupCall_V140 grpCallObj = new V_1_4.GroupCall_V140();
                    for (int row = 0; row < responseDataSet.Tables[1].Rows.Count; row++)
                    {
                        Logger.TraceLog("Inbound Private Mems Count" + responseDataSet.Tables[1].Rows.Count);
                        privateResponse = grpCallObj.PrivatePublicApi(Convert.ToString(responseDataSet.Tables[1].Rows[row]["Conferenceroom"]), Convert.ToString(responseDataSet.Tables[1].Rows[row]["member_id"]), false, Convert.ToString(responseDataSet.Tables[1].Rows[row]["HttpUrl"]), Convert.ToBoolean(responseDataSet.Tables[1].Rows[row]["Mute"]));
                    }

                }
            }
            return "";
        }
        public void PublishMessageToWebSocket(DataSet Ds, string fromNumber, string toNumber, string callStatus, string conferenceAction)
        {
            try
            {
                PusherAppId = System.Configuration.ConfigurationManager.AppSettings["PusherAppId"];
                PusherAppKey = System.Configuration.ConfigurationManager.AppSettings["PusherAppKey"].ToString();
                PusherAppSecret = System.Configuration.ConfigurationManager.AppSettings["PusherAppSecret"].ToString();

                if (Ds != null)
                {
                    if (Ds.Tables.Count > 0 && Ds.Tables[0].Rows.Count > 0)
                    {
                        Pusher pusher_obj = new Pusher(PusherAppId, PusherAppKey, PusherAppSecret);
                        string WebSocketEventName = "call_status";

                        ITriggerResult PusherResponse = null;
                        PusherResponse = pusher_obj.Trigger(Ds.Tables[0].Rows[0]["ConferenceRoom"].ToString(), WebSocketEventName, new
                        {
                            direction = "inbound",
                            plivo_event = "",
                            plivo_conferene_action = conferenceAction,
                            to_num = Ds.Tables[0].Rows[0]["ToNumber"],
                            from_num = Ds.Tables[0].Rows[0]["FromNumber"],
                            conf_room = Ds.Tables[0].Rows[0]["ConferenceRoom"],
                            call_status = callStatus,
                            mute = Ds.Tables[0].Rows[0]["Mute"],
                            deaf = false,
                            isprivate = Ds.Tables[0].Rows[0]["IsPrivate"],
                            IsMember = Ds.Tables[0].Rows[0]["IsMember"].ToString(),
                            member = Ds.Tables[0].Rows[0]["Member"],
                            MemberJoinTime =Convert.ToString(Ds.Tables[0].Rows[0]["MemberJoinTime"]),
                            conf_type = "unmute",
                            conf_digits = 0,
                            conf_id = Ds.Tables[0].Rows[0]["ConferenceId"],
                            record_status = "",
                            record_count = 0,
                            dig_prs_count = 0,
                            inpro_count = 0,
                            unmute_count = 0,
                            //isinprogress = "1",
                            isinprogress = Ds.Tables[0].Rows[0]["OnCallCount"],
                            mute_count = 0,
                            end_reason = "",
                            AllMembersCount = Ds.Tables[0].Rows[0]["AllMembersCount"],
                            OnCallCount = Ds.Tables[0].Rows[0]["OnCallCount"],
                            HangUpCount = Ds.Tables[0].Rows[0]["HangUpCount"],
                            MuteCount = Ds.Tables[0].Rows[0]["MuteMemCount"],
                            HandRaiseCount = Ds.Tables[0].Rows[0]["HandRaiseCount"],
                            MembersCount = Ds.Tables[0].Rows[0]["AllMembersCount"],
                            PrivateCount = Ds.Tables[0].Rows[0]["PrivateCount"]
                        });

                        Logger.TraceLog("Push Sharp Response Hand raise" + Ds.Tables[0].Rows[0]["HandRaiseCount"] + "MUte COunt" + Ds.Tables[0].Rows[0]["MuteMemCount"] + "All members COunt" + Ds.Tables[0].Rows[0]["AllMembersCount"]);
                    }
                    else
                    {
                        Logger.ExceptionLog("No Data Returned From Database");
                    }
                }
                else
                {
                    Logger.ExceptionLog("Dataset Is Nothing");
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("PublishMessageToWebSocket :" + ex.ToString());
            }
        }


        public string ToNumber { get; set; }
        public string FromNumber { get; set; }
        public string CallUuid { get; set; }
        public string Event { get; set; }
        public string Direction { get; set; }
        public string Digits { get; set; }
        public string CallStatus { get; set; }
        public string ConnString { get; set; }
        public int ConferenceId { get; set; }
        public Boolean IsAll { get; set; }
        public string BatchId { get; set; }
        public string ConferenceRoom { get; set; }
        public string MobileNumber { get; set; }
        public int NodeGatewayId { get; set; }
        public int NodeSlno { get; set; }
        public Boolean IsMute { get; set; }
        public string WelcomeClip { get; set; }
        public string WaitClip { get; set; }
        public Boolean EndConferenceOnExit { get; set; }
        public DataTable NodeDependedMobileNumberReportIdTable { get; set; }
        public DataTable NodeDependendGateWaysTable { get; set; }
        public string InboundAnswerUrl { get; set; }
        public string PreJoinCallBackUrl { get; set; }

        public string PostJoinCallBackUrl { get; set; }

    }
}
