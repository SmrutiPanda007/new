using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Util;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net;
using GT.Utilities;
using GT.Utilities.Properties;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;

namespace GrpTalk
{
    /// <summary>
    /// Summary description for GroupCall
    /// </summary>
    public class GroupCall : IHttpHandler
    {
        

	JObject ConferenceValidateObject = new JObject();
	string _Action = "";
	int _UserId = 0;
	int _ConferenceId = 0;
	string token = "";
	int AutoDial = 0;
    
  
  
    JObject Jobj = new JObject();
	

        public void ProcessRequest(HttpContext context)
        {
            Jobj = grpCall(context);
            context.Response.Write(Jobj);
            return;


        }

        private JObject grpCall(HttpContext context)
        {
            GroupCallBusiness GroupCallobj = new GroupCallBusiness();
            grpcall ConferenceObject = new grpcall();
            if (string.IsNullOrEmpty(context.Request["from"]) == false)
            {
                //if (context.RequestValidate(context.Request["userid"], context.Request["conf_id"], context.Request["token"]) == "SUCCESS")
                //{
                //    token = context.Request["token"];
                //    _UserId =Convert.ToInt16(context.Request["userid"]);
                //    _ConferenceId = Convert.ToInt16(context.Request["conf_id"]);
                //    ConferenceObject.AutoDialTocken = token;
                //    AutoDial = 1;
                //}
                //else
                //{
                //   Jobj.Add(new JObject(new JProperty("Status", 401), new JProperty("ErrorReason", "Unauthorized Access")));
                //    return Jobj;
                //}
            }
            else
            {
                if (context.Request.UrlReferrer != null && context.Request.UrlReferrer.ToString().Contains(System.Configuration.ConfigurationManager.AppSettings["Domain"].ToString()))
                {
                }
                else
                {
                    Jobj.Add(new JObject(new JProperty("Status", 401), new JProperty("ErrorReason", "Unauthorized Access")));
                    return Jobj;
                }
                if (string.IsNullOrEmpty(Convert.ToString(UserSession.UserId)))
                {
                    Jobj.Add(new JObject(new JProperty("Status", 401), new JProperty("ErrorReason", "SessionExpired")));
                    return Jobj;
                }
                if (!(context.Request.Cookies["SessionId"] != null && !string.IsNullOrEmpty(Convert.ToString(UserSession.UserId)) && context.Request.Cookies["SessionId"].Value == Convert.ToString(UserSession.UserId)))
                {
                    Jobj.Add(new JObject(new JProperty("Status", 401), new JProperty("ErrorReason", "SessionExpired")));
                    return Jobj;
                }
                _UserId = Convert.ToInt16(UserSession.UserId);
                _ConferenceId = Convert.ToInt16(context.Request["conf_id"]);
                AutoDial = 0;
            }


            try
            {
                string member = null;
                _Action = context.Request["action"].ToString();
                if (string.IsNullOrEmpty(context.Request["member"]))
                {
                    member = context.Request["moderator"];
                }
                else
                {
                    member = context.Request["member"];
                }
                int GroupId = Convert.ToInt32(context.Request["GroupId"]);

                ValidateConference(ConferenceObject);

                if (ConferenceValidateObject.SelectToken("Status").ToString() == "0")
                {
                    logclass.LogRequest(ConferenceValidateObject.SelectToken("Message").ToString());
                    Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", ConferenceValidateObject.SelectToken("Message").ToString()));
                    return Jobj;
                }
                ConferenceObject.ConferenceAction = _Action;
                ConferenceObject.UserId = _UserId;
                ConferenceObject = GroupCallobj.SetConferenceVariables(ConferenceValidateObject);

                if (_Action.ToUpper() == "MUTE_DIAL_ALL")
                {
                    try
                    {
                        JObject DialResponse = new JObject();
                        ConferenceObject.MemberName = "";
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsMute = true;
                        ConferenceObject.IsAll = true;
                        DialResponse = GroupCallobj.Dial(MyConf.MyConnectionString, ConferenceObject);
                        return DialResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong with the server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "UNMUTE_DIAL_ALL")
                {
                    try
                    {
                        JObject DialResponse = new JObject();
                        ConferenceObject.MemberName = "";
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsMute = false;
                        ConferenceObject.IsAll = true;
                        if (AutoDial == 1)
                        {
                            ConferenceObject.IsAutodial = 1;
                        }
                        else
                        {
                            ConferenceObject.IsAutodial = 0;
                        }
                        DialResponse = GroupCallobj.Dial(MyConf.MyConnectionString, ConferenceObject);
                        return DialResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "MODERATOR_DIAL")
                {
                    try
                    {
                        JObject DialResponse = new JObject();
                        ConferenceObject.MemberName = member;
                        ConferenceObject.IsModerator = true;
                        ConferenceObject.IsMute = false;
                        ConferenceObject.IsAll = false;
                        if (AutoDial == 1)
                        {
                            ConferenceObject.IsAutodial = 1;
                        }
                        else
                        {
                            ConferenceObject.IsAutodial = 0;
                        }
                        DialResponse = GroupCallobj.Dial(MyConf.MyConnectionString, ConferenceObject);
                        return DialResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "MUTE_DIAL")
                {
                    try
                    {
                        JObject DialResponse = new JObject();
                        ConferenceObject.MemberName = member;
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsMute = true;
                        ConferenceObject.IsAll = false;
                        DialResponse = GroupCallobj.Dial(MyConf.MyConnectionString, ConferenceObject);
                        return DialResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "DIAL")
                {
                    try
                    {
                        JObject DialResponse = new JObject();
                        ConferenceObject.MemberName = member;
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsMute = false;
                        ConferenceObject.IsAll = false;
                        DialResponse = GroupCallobj.Dial(MyConf.MyConnectionString, ConferenceObject);
                        return DialResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "MODERATOR_HANGUP")
                {
                    try
                    {
                        JObject HangupResponse = new JObject();
                        ConferenceObject.IsModerator = true;
                        ConferenceObject.IsAll = false;
                        HangupResponse = GroupCallobj.Hangup(MyConf.MyConnectionString, ConferenceObject);
                        return HangupResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "HANGUP_MEMBER")
                {
                    try
                    {
                        JObject HangupResponse = new JObject();
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsAll = false;
                        ConferenceObject.MemberName = member;
                        HangupResponse = GroupCallobj.Hangup(MyConf.MyConnectionString, ConferenceObject);
                        return HangupResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "HANGUP_ALL")
                {
                    try
                    {
                        JObject HangupResponse = new JObject();
                        ConferenceObject.IsModerator = false;
                        ConferenceObject.IsAll = true;
                        HangupResponse = GroupCallobj.Hangup(MyConf.MyConnectionString, ConferenceObject);
                        return HangupResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "MUTE_MEMBER")
                {
                    try
                    {
                        JObject MuteResponse = new JObject();
                        ConferenceObject.IsMute = true;
                        ConferenceObject.IsAll = false;
                        ConferenceObject.MemberName = member;
                        MuteResponse = GroupCallobj.MuteUnmute(ConferenceObject, MyConf.MyConnectionString);
                        return MuteResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "MUTE_ALL")
                {
                    try
                    {
                        JObject MuteResponse = new JObject();
                        ConferenceObject.IsMute = true;
                        ConferenceObject.IsAll = true;
                        ConferenceObject.MemberName = "";
                        MuteResponse = GroupCallobj.MuteUnmute(ConferenceObject, MyConf.MyConnectionString);
                        return MuteResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "UNMUTE_MEMBER")
                {
                    try
                    {
                        JObject UnMuteResponse = new JObject();
                        ConferenceObject.IsMute = false;
                        ConferenceObject.IsAll = false;
                        ConferenceObject.MemberName = member;
                        UnMuteResponse = GroupCallobj.MuteUnmute(ConferenceObject, MyConf.MyConnectionString);
                        return UnMuteResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "UNMUTE_ALL")
                {
                    try
                    {
                        JObject UnMuteResponse = new JObject();
                        ConferenceObject.IsMute = false;
                        ConferenceObject.IsAll = true;
                        ConferenceObject.MemberName = "";
                        UnMuteResponse = GroupCallobj.MuteUnmute(ConferenceObject, MyConf.MyConnectionString);
                        return UnMuteResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "DEAF_MEMBER")
                {
                    try
                    {
                        JObject DeafResponse = new JObject();
                        ConferenceObject.IsDeaf = true;
                        ConferenceObject.IsAll = false;
                        ConferenceObject.MemberName = member;
                        DeafResponse = GroupCallobj.DeafUndeaf(ConferenceObject, MyConf.MyConnectionString);
                        return DeafResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "DEAF_ALL")
                {
                    try
                    {
                        JObject DeafResponse = new JObject();
                        ConferenceObject.IsDeaf = true;
                        ConferenceObject.IsAll = true;
                        ConferenceObject.MemberName = "";
                        DeafResponse = GroupCallobj.DeafUndeaf(ConferenceObject, MyConf.MyConnectionString);
                        return DeafResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "UNDEAF_MEMBER")
                {
                    try
                    {
                        JObject UnDeafResponse = new JObject();
                        ConferenceObject.IsDeaf = false;
                        ConferenceObject.IsAll = false;
                        ConferenceObject.MemberName = member;
                        UnDeafResponse = GroupCallobj.DeafUndeaf(ConferenceObject, MyConf.MyConnectionString);
                        return UnDeafResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
                else if (_Action.ToUpper() == "UNDEAF_ALL")
                {
                    try
                    {
                        JObject UnDeafResponse = new JObject();
                        ConferenceObject.IsDeaf = false;
                        ConferenceObject.IsAll = true;
                        ConferenceObject.MemberName = "";
                        UnDeafResponse = GroupCallobj.DeafUndeaf(ConferenceObject, MyConf.MyConnectionString);
                        return UnDeafResponse;
                    }
                    catch (Exception ex)
                    {
                        logclass.LogRequest(ex.StackTrace);
                        Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                        return Jobj;
                    }
                }
            }
            catch (Exception ex)
            {
                Jobj = new JObject(new JProperty("Status", 0), new JProperty("ErrorReason", "Something Wrong With The Server"));
                return Jobj;
            }
            if (Jobj.SelectToken("Status") == null)
            {
                Jobj = new JObject(new JProperty("Status", 1), new JProperty("Message", "SUCCESS"));
            }
            return Jobj;
        }

        public void ValidateConference(grpcall ConferenceObject)
        {
	        string[] ValidateActions = {
		        "dial",
		        "mute_dial",
		        "unmute_dial_all",
		        "mute_dial_all",
		        "moderator_dial",
		        "MUTE_DIAL_ALL",
		        "UNMUTE_DIAL_ALL",
		        "MODERATOR_DIAL",
		        "MUTE_DIAL",
		        "DIAL"
	        };
	        if (ValidateActions.Contains(_Action)) {
		        if (_Action == "dial" || _Action == "mute_dial" || _Action == "moderator_dial") {
			        ConferenceObject.IsValidate = true;
			        ConferenceObject.ConferenceId = _ConferenceId;
			        ConferenceObject.Direction = "OUTBOUND";
			        ConferenceObject.ConferenceNumber = "";
			        ConferenceObject.ConferenceAccessKey = "";
			        ConferenceObject.TotalNumbers = 1;
		        } else {
			        ConferenceObject.IsValidate = true;
			        ConferenceObject.ConferenceId = _ConferenceId;
			        ConferenceObject.Direction = "OUTBOUND";
			        ConferenceObject.ConferenceNumber = "";
			        ConferenceObject.ConferenceAccessKey = "";
			        ConferenceObject.TotalNumbers = 0;
		        }
	        } else {
		        ConferenceObject.IsValidate = false;
		        ConferenceObject.ConferenceId = _ConferenceId;
		        ConferenceObject.Direction = "OUTBOUND";
		        ConferenceObject.ConferenceNumber = "";
		        ConferenceObject.ConferenceAccessKey = "";
		        ConferenceObject.TotalNumbers = 0;
	        }
            GroupCallBusiness GroupCallobj = new GroupCallBusiness();
            ConferenceValidateObject = GroupCallobj.Validate(MyConf.MyConnectionString,ConferenceObject);
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