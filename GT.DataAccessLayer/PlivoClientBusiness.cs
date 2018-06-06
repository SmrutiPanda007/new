using System;
using System.Collections.Generic;
using Plivo.API;
using RestSharp;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Data;
using System.Net;
using GT.Utilities;

namespace GT.BusinessLogicLayer
{

    class PlivoClientBusiness
    {
        RestAPI clientObj = new RestAPI("MAZTA1MGQ0MDY1YTQZN2", "ZWJkY2FhOGQ0MzBjZjA2Nzg2NDNiZjM0ZTJiYmEx");
        String answerUrl = "http://new.grpTalk.com/PlivoTestYConf.aspx";
        String ringUrl = "http://new.grpTalk.com/PlivoRingUrl.aspx";
        public JObject MakeCall(string from, string to)
        {
            IRestResponse<Call> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            JArray requestUUIDs = null;
            try
            {
                parameters.Add("from", from);
                if (to == "18143209997")
                {
                    parameters.Add("to", "918143209997");
                }
                else
                {
                    parameters.Add("to", to);
                }
                parameters.Add("answer_url", answerUrl);
                parameters.Add("ring_url", ringUrl);
                parameters.Add("answer_method", "GET");
                restResp = clientObj.make_call(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    requestUUIDs = new JArray();
                    requestUUIDs.Add(restResp.Data.request_uuid);
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", restResp.Data.message), new JProperty("RequestUUIDs", requestUUIDs), new JProperty("ApiID", restResp.Data.api_id));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used  for making bulkm calls
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public JObject MakeBulkCall(string from, string to, string delimiter)
        {
            IRestResponse<Call> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            Dictionary<string, string> tempParameters = new Dictionary<string, string>();
            JArray requestUUIDs = null;
            try
            {
                parameters.Add("from", from);
                parameters.Add("answer_url", answerUrl);
                parameters.Add("ring_url", ringUrl);
                parameters.Add("answer_method", "GET");
                foreach (string _Mobile in to.Split(delimiter.ToCharArray()))
                {
                    if (_Mobile == "18143209997")
                    {
                        tempParameters.Add("918143209997", "");
                    }
                    else
                    {
                        tempParameters.Add(_Mobile, "");
                    }
                }
                restResp = clientObj.make_bulk_call(parameters, tempParameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    requestUUIDs = new JArray();
                    foreach (string UUID in restResp.Data.request_uuid.Replace("[", "").Replace("]", "").Replace("\"", "").Split(','))
                    {
                        requestUUIDs.Add(UUID);
                    }
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", restResp.Data.message), new JProperty("RequestUUIDs", requestUUIDs), new JProperty("ApiID", restResp.Data.api_id));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used to make hangup for conference members
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public JObject HangupConferenceMembers(string conferenceName, string memberIDs)
        {
            Logger.TraceLog("Plivo Hangup ConferenceMembers, ConferenceName : " + conferenceName + ", MemberIDs : " + memberIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("conference_name", "");
                parameters.Add("member_id", memberIDs);
                restResp = clientObj.hangup_member(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", restResp.Data.message));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used for making hangup calls
        /// </summary>
        /// <param name="requestUUIDs"></param>
        /// <returns></returns>
        public JObject HangupCalls(string requestUUIDs)
        {

            Logger.TraceLog("Plivo HangupCalls, RequestUUIDs : " + requestUUIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("call_uuid", requestUUIDs);
                restResp = clientObj.hangup_call(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", "Hangup Success"));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used to make conference as mute
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public JObject ConferenceMute(string conferenceName, string memberIDs)
        {

            Logger.TraceLog("Plivo MuteApi, ConferenceName : " + conferenceName + ", MemberIDs : " + memberIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                if (memberIDs.EndsWith(","))
                {
                    //memberIDs = memberIDs.Right(memberIDs.Length - 1);
                    memberIDs = memberIDs.Substring(1, memberIDs.Length - 1);
                    
                }
                parameters.Add("conference_name", conferenceName);
                parameters.Add("member_id", memberIDs);
                restResp = clientObj.mute_member(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.Accepted)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Error In Mute"));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used to make unmute
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public JObject ConferenceUnMute(string conferenceName, string memberIDs)
        {

            Logger.TraceLog("Plivo UnMuteApi, ConferenceName : " + conferenceName + ", MemberIDs : " + memberIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                if (memberIDs.EndsWith(","))
                {
                    
                    //memberIDs = memberIDs.Right(memberIDs.Length - 1);
                    memberIDs = memberIDs.Substring(1, memberIDs.Length - 1);
                }
                parameters.Add("conference_name", conferenceName);
                parameters.Add("member_id", memberIDs);
                restResp = clientObj.unmute_member(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", "OK"));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Error In UnMute"));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Satus", 0), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used to make conference as deaf
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public JObject ConferenceDeaf(string conferenceName, string memberIDs)
        {

            Logger.TraceLog("Plivo dEAFapi, ConferenceName : " + conferenceName + ", MemberIDs : " + memberIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("conference_name", conferenceName);
                parameters.Add("member_id", memberIDs);
                restResp = clientObj.deaf_member(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", restResp.Data.message));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used for making conference as undeaf
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <param name="memberIDs"></param>
        /// <returns></returns>
        public JObject ConferenceUnDeaf(string conferenceName, string memberIDs)
        {

            Logger.TraceLog("Plivo UnDeafApi, ConferenceName : " + conferenceName + ", MemberIDs : " + memberIDs);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("conference_name", conferenceName);
                parameters.Add("member_id", memberIDs);
                restResp = clientObj.undeaf_member(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", restResp.Data.message));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", "Server Internal Error"));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }

        /// <summary>
        /// This function is used to kill conferences
        /// </summary>
        /// <param name="conferenceName"></param>
        /// <returns></returns>
        public JObject KillConference(string conferenceName)
        {
            Logger.TraceLog("Plivo KillConference, ConferenceName : " + conferenceName);
            IRestResponse<GenericResponse> restResp = null;
            JObject responseObj = null;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            try
            {
                parameters.Add("conference_name", conferenceName);
                restResp = clientObj.hangup_conference(parameters);
                if (restResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    responseObj = new JObject(new JProperty("Success", true), new JProperty("Message", "Conference Hungup"));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false), new JProperty("Message", restResp.Data.error));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Server Internal Error : "));
                Logger.ExceptionLog("Exception in PlivoClientBusiness is " + ex.ToString());
            }
            return responseObj;
        }
       


    }
}
