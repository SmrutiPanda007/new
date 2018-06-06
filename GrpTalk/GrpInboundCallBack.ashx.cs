using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using System.IO;
using GT.BusinessLogicLayer;
using GT.Utilities;
using GrpTalk.CommonClasses;
using Microsoft.VisualBasic;

namespace GrpTalk
{
    /// <summary>
    /// Summary description for GrpInboundCallBack
    /// </summary>
    public class GrpInboundCallBack : IHttpHandler
    {
        public string _CallUUID = null;
        public string _Event = null;
        public string _Direction = null;
        public string _FromNumber = null;
        public string _ToNumber = null;
        public string _CallStatus = null;
        public string _RecordingUrl = null;
        public string _RecordingDuration = null;
        public string _Digits = null;
        public string _FileName = null;
        public string _ConferenceUUID = null;
        public int _ConferenceMemberId = 0;
        public string _ConferenceName = null;
        public string _ConferenceDigits = null;
        public string _ConferenceAction = null;
        public string _ConferenceRequestUUID = null;
        public string _RequestUUID = null;
        public int _ConferenceId = 0;
        public int _ActiveCalls = 0;
        public Int64 _StartTime = 0;
        public Int64 _EndTime = 0;
        public string _EndReason = "";
        Int32 _conferenceSize = 0;


        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            ParseParameters(context);

            //if (_FromNumber.StartsWith("05") || _FromNumber.StartsWith("04"))
            //{
            //    _FromNumber = _FromNumber.SubString(1, _FromNumber.Length - 1);
            //}
            //else if (_FromNumber.StartsWith("971"))
            //{
            //    _FromNumber = _FromNumber.Replace("971", "");
            //}
            //else if (_FromNumber.StartsWith("973"))
            //{
            //    _FromNumber = Strings.Right(_FromNumber, _FromNumber.Length - 3);
            //}
            //if (_FromNumber.Length > 10)
            //{
            //    _FromNumber = _FromNumber.Substring(_FromNumber.Length - 10);
            //}
            //if (_ToNumber.Length > 10 && !_ToNumber.StartsWith("971"))
            //{
            //    _ToNumber = _ToNumber.Substring(_ToNumber.Length - 10);
            //}
            UpdateReport();
            //PublishMessageToWebSocket();

            context.Response.Write("Hello World");
        }
        public void ParseParameters(HttpContext context)
        {
            try
            {
                Logger.TraceLog("Httpcontext " + context.ToString());
                if (context.Request.HttpMethod.ToString().ToUpper() == "GET")
                {
                    if (context.Request["smscresponse[event]"] != null)
                    {
                        _Event = context.Request["smscresponse[event]"];
                    }
                    else
                    {
                        _Event = "";
                    }

                    if (context.Request["smscresponse[calluid]"] != null)
                    {
                        _CallUUID = context.Request["smscresponse[calluid]"];
                    }
                    else
                    {
                        _CallUUID = "";
                    }
                    if (context.Request["smscresponse[ConferenceUUID]"] != null)
                    {
                        _ConferenceUUID = context.Request["smscresponse[ConferenceUUID]"];
                    }
                    else
                    {
                        _ConferenceUUID = "";
                    }

                    if (context.Request["smscresponse[direction]"] != null)
                    {
                        _Direction = context.Request["smscresponse[direction]"];
                    }
                    else
                    {
                        _Direction = "";
                    }
                    if (context.Request["smscresponse[to]"] != null)
                    {
                        _ToNumber = HttpUtility.UrlDecode(context.Request["smscresponse[to]"].ToString());
                    }
                    else
                    {
                        _ToNumber = "";
                    }
                    if (context.Request["smscresponse[from]"] != null)
                    {
                        _FromNumber = context.Request["smscresponse[from]"];
                        if (_ToNumber.Trim().StartsWith("+973") || _ToNumber.Trim().StartsWith("973"))
                        {
                            _FromNumber = "973" + Strings.Right(_FromNumber, 8);

                        }
                        else if (_ToNumber.Trim().StartsWith("+971") || _ToNumber.Trim().StartsWith("971"))
                        {
                            _FromNumber = "971" + Strings.Right(_FromNumber, 9);

                        }
                        else if (_ToNumber.Trim().Contains("22314188"))
                        {
                            _FromNumber = "968" + Strings.Right(_FromNumber, 8);

                        }
                        else if (_ToNumber.Trim().StartsWith("44") || _ToNumber.Trim().StartsWith("+44"))
                        {
                            _FromNumber = "44" + Strings.Right(_FromNumber, 10);

                        }
                        else if (_FromNumber.Length >= 10)
                        {

                            _FromNumber = "91" + Strings.Right(_FromNumber, 10);
                        }
                    }
                    else
                    {
                        _FromNumber = "";
                    }


                    if (context.Request["smscresponse[callstatus]"] != null)
                    {
                        _CallStatus = context.Request["smscresponse[callstatus]"];
                    }
                    else
                    {
                        _CallStatus = "";
                    }
                    if (string.IsNullOrEmpty(context.Request["smscresponse[ConferenceMemberID]"]) == false)
                    {

                        _ConferenceMemberId = Convert.ToInt32(context.Request["smscresponse[ConferenceMemberID]"]);

                    }
                    else
                    {
                        if (string.IsNullOrEmpty(context.Request["smscresponse[ConfInstanceMemberID]"]) == false)
                        {
                            _ConferenceMemberId = Convert.ToInt32(context.Request["smscresponse[ConfInstanceMemberID]"]);
                        }
                        else
                        {
                            _ConferenceMemberId = 0;
                        }
                    }


                    if (context.Request["smscresponse[ConferenceName]"] != null)
                    {
                        _ConferenceName = context.Request["smscresponse[ConferenceName]"];
                    }
                    else
                    {
                        _ConferenceName = "";
                    }
                    if (context.Request["smscresponse[RequestUUID]"] != null)
                    {
                        _RequestUUID = context.Request["smscresponse[RequestUUID]"];
                    }
                    else
                    {
                        _RequestUUID = "";
                    }
                    if (context.Request["smscresponse[ConferenceDigitsMatch]"] != null)
                    {
                        _ConferenceDigits = context.Request["smscresponse[ConferenceDigitsMatch]"];
                    }
                    else
                    {
                        _ConferenceDigits = "";
                    }
                    if (context.Request["smscresponse[ConferenceAction]"] != null)
                    {
                        _ConferenceAction = context.Request["smscresponse[ConferenceAction]"];
                    }
                    else
                    {
                        _ConferenceAction = "";
                    }
                    if (context.Request["smscresponse[starttime]"] != null)
                    {
                        _StartTime = Convert.ToInt64(context.Request["smscresponse[starttime]"]);
                    }
                    else
                    {
                        _StartTime = 0;
                    }
                    if (context.Request["smscresponse[endtime]"] != null)
                    {
                        _EndTime = Convert.ToInt64(context.Request["smscresponse[endtime]"]);
                    }
                    else
                    {
                        _EndTime = 0;
                    }
                    if (context.Request["smscresponse[endreason]"] != null)
                    {
                        _EndReason = context.Request["smscresponse[endreason]"].ToString();
                    }
                    else
                    {
                        _EndReason = "";
                    }
                    if (_ConferenceAction.ToLower() == "enter" || _ConferenceAction.ToLower() == "exit")
                    {
                        if (context.Request["smscresponse[ConferenceSize]"] != null)
                        {
                            _conferenceSize = Convert.ToInt32(context.Request["smscresponse[ConferenceSize]"]);
                        }
                        else
                        {
                            _conferenceSize = 0;
                        }

                    }
                    else
                    {
                        _conferenceSize = 0;
                    }
                    Logger.TraceLog("_EndReason" + _EndReason);
                }
                else if (context.Request.HttpMethod.ToString().ToUpper() == "POST")
                {
                    string jsonStr = "";
                    JObject jsonObj = default(JObject);
                    StreamReader inputStream = null;
                    context.Request.InputStream.Position = 0;
                    inputStream = new StreamReader(context.Request.InputStream);
                    jsonStr = inputStream.ReadToEnd();
                    // Request.SaveAs("/response.txt", True)
                    jsonObj = new JObject();
                    jsonObj = JObject.Parse(jsonStr);
                    jsonObj = JObject.Parse(jsonObj.SelectToken("smscresponse").ToString());
                    if (jsonObj.SelectToken("event") != null)
                    {
                        _Event = jsonObj.SelectToken("event").ToString();
                    }
                    else
                    {
                        _Event = "";
                    }
                    if (jsonObj.SelectToken("calluid") != null)
                    {
                        _CallUUID = jsonObj.SelectToken("calluid").ToString();
                    }
                    else
                    {
                        _CallUUID = "";
                    }
                    if (jsonObj.SelectToken("ConferenceUUID") != null)
                    {
                        _ConferenceUUID = jsonObj.SelectToken("ConferenceUUID").ToString();
                    }
                    else
                    {
                        _ConferenceUUID = "";
                    }
                    if (jsonObj.SelectToken("direction") != null)
                    {
                        _Direction = jsonObj.SelectToken("direction").ToString();
                    }
                    else
                    {
                        _Direction = "";
                    }

                    if (jsonObj.SelectToken("to") != null)
                    {
                        _ToNumber = HttpUtility.UrlDecode(jsonObj.SelectToken("to").ToString());
                    }
                    else
                    {
                        _ToNumber = "";
                    }
                    if (jsonObj.SelectToken("from") != null)
                    {
                        _FromNumber = jsonObj.SelectToken("from").ToString();
                        Logger.TraceLog("To Number Request" + _ToNumber);
                        if (_ToNumber.Trim().StartsWith("+973") || _ToNumber.Trim().StartsWith("973"))
                        {
                            _FromNumber = "973" + Strings.Right(_FromNumber, 8);

                        }
                        else if (_ToNumber.Trim().StartsWith("+971") || _ToNumber.Trim().StartsWith("971"))
                        {
                            _FromNumber = "971" + Strings.Right(_FromNumber, 9);

                        }
                        else if (_ToNumber.Trim().Contains("22314188"))
                        {
                            _FromNumber = "968" + Strings.Right(_FromNumber, 8);

                        }
                        else if (_ToNumber.Trim().StartsWith("44") || _ToNumber.Trim().StartsWith("+44"))
                        {
                            _FromNumber = "44" + Strings.Right(_FromNumber, 10);

                        }
                        else if (_FromNumber.Length >= 10)
                        {

                            _FromNumber = "91" + Strings.Right(_FromNumber, 10);
                        }
                        Logger.TraceLog("From Number Request" + _FromNumber);


                    }
                    else
                    {
                        _FromNumber = "";
                    }
                    if (jsonObj.SelectToken("callstatus") != null)
                    {
                        _CallStatus = jsonObj.SelectToken("callstatus").ToString();
                    }
                    else
                    {
                        _CallStatus = "";
                    }
                    if (jsonObj.SelectToken("ConfInstanceMemberID") != null)
                    {
                        _ConferenceMemberId = Convert.ToInt32(jsonObj.SelectToken("ConfInstanceMemberID").ToString());
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(Convert.ToString(jsonObj.SelectToken("ConferenceMemberID"))) == false)
                        {
                            _ConferenceMemberId = Convert.ToInt32(jsonObj.SelectToken("ConferenceMemberID"));
                        }
                        else
                        {


                            _ConferenceMemberId = 0;

                        }
                    }

                    if (jsonObj.SelectToken("ConferenceName") != null)
                    {
                        _ConferenceName = jsonObj.SelectToken("ConferenceName").ToString();
                    }
                    else
                    {
                        _ConferenceName = "";
                    }
                    if (jsonObj.SelectToken("ConferenceAction") != null)
                    {
                        _ConferenceAction = jsonObj.SelectToken("ConferenceAction").ToString();
                    }
                    else
                    {
                        _ConferenceAction = "";
                    }
                    if (jsonObj.SelectToken("ConferenceDigitsMatch") != null)
                    {
                        _ConferenceDigits = jsonObj.SelectToken("ConferenceDigitsMatch").ToString();
                    }
                    else
                    {
                        _ConferenceDigits = "";
                    }
                    if (jsonObj.SelectToken("starttime") != null)
                    {
                        _StartTime = Convert.ToInt64(jsonObj.SelectToken("starttime"));
                    }
                    else
                    {
                        _StartTime = 0;
                    }
                    if (jsonObj.SelectToken("endtime") != null)
                    {
                        _EndTime = Convert.ToInt64(jsonObj.SelectToken("endtime"));
                    }
                    else
                    {
                        _EndTime = 0;
                    }
                    if (jsonObj.SelectToken("endreason") != null)
                    {
                        _EndReason = jsonObj.SelectToken("endreason").ToString();
                    }
                    else
                    {
                        _EndReason = "";
                    }
                    if (_ConferenceAction.ToLower() == "enter" || _ConferenceAction.ToLower() == "exit")
                    {
                        if (jsonObj.SelectToken("ConferenceSize") != null)
                        {
                            _conferenceSize = Convert.ToInt32(jsonObj.SelectToken("ConferenceSize").ToString());
                        }
                        else
                        {
                            _conferenceSize = 0;
                        }
                    }
                    else
                    {
                        _conferenceSize = 0;
                    }
                    Logger.TraceLog("Inbound Post Request Log" + jsonObj.ToString());
                    //_conf_requuid = jsonObj.SelectToken("smscresponse").SelectToken("RequstUUID").ToString()
                }
                else
                {
                    context.Response.End();
                }
                if (_ConferenceAction.ToUpper() == "EXIT")
                {
                    _CallStatus = "completed";
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In Parsing Inbound Call Parameters" + ex.ToString());
            }


        }


        public void UpdateReport()
        {
            //public string _Direction = null;
            //public string _CallStatus = null;
            //public string _RecordingUrl = null;
            //public string _RecordingDuration = null;
            //public string _FileName = null;
            //public string _ConferenceUUID = null;
            //public string _ConferenceName = null;
            //public string _ConferenceDigits = null;
            //public string _ConferenceRequestUUID = null;
            string inboundresponse = "";
            GrpInboundCallBusiness grpInboundCallBusinessObj = new GrpInboundCallBusiness();
            inboundresponse = grpInboundCallBusinessObj.InBoundCallBack(MyConf.MyConnectionString, _ConferenceUUID, _conferenceSize, _CallUUID, _FromNumber, _ToNumber, _Event, _CallStatus, _RequestUUID, _ConferenceMemberId, _ConferenceAction, _ConferenceDigits, _StartTime, _EndTime, _EndReason);


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