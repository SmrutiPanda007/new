using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using GT.BusinessLogicLayer;
using GrpTalk.CommonClasses;
using GT.Utilities;
using Microsoft.VisualBasic;
using System.Configuration;
namespace GrpTalk
{
    /// <summary>
    /// Summary description for GrpInboundCall
    /// </summary>

    public class GrpInboundCall : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = "text/plain";
            string inboundAnswerUrl = "";
            string preJoinCallBackUrl = "", postJoinCallBackUrl = "";
            GrpInboundCallBusiness grpInboundCallBusinessObj = new GrpInboundCallBusiness();
            JObject inboundCallResonse = new JObject();
            inboundAnswerUrl = ConfigurationManager.AppSettings["InboundAnswerUrl"].ToString();
            preJoinCallBackUrl = ConfigurationManager.AppSettings["grpCallCallBackUrl"].ToString();
            postJoinCallBackUrl = ConfigurationManager.AppSettings["InboundConferenceCallBackUrlPostJoin"].ToString();
            grpInboundCallBusinessObj.ConnString = MyConf.MyConnectionString;
            grpInboundCallBusinessObj.InboundAnswerUrl = inboundAnswerUrl;
            grpInboundCallBusinessObj.PreJoinCallBackUrl = inboundAnswerUrl;
            grpInboundCallBusinessObj.PostJoinCallBackUrl = postJoinCallBackUrl;
            grpInboundCallBusinessObj.WelcomeClip = ConfigurationManager.AppSettings["GrpWelcomeClip"].ToString();
            grpInboundCallBusinessObj.WaitClip = ConfigurationManager.AppSettings["GrpWaitClip"].ToString();
            string fromNumber = "";
            if (context.Request.HttpMethod.ToString().ToUpper() == "GET")
            {

                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[calluid]"]) == false)
                {
                    grpInboundCallBusinessObj.CallUuid = context.Request.QueryString["smscresponse[calluid]"];
                }
                else
                {
                    grpInboundCallBusinessObj.CallUuid = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[to]"]) == false)
                {
                    grpInboundCallBusinessObj.ToNumber = context.Request.QueryString["smscresponse[to]"];
                }
                else
                {
                    grpInboundCallBusinessObj.ToNumber = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[event]"]) == false)
                {
                    grpInboundCallBusinessObj.Event = context.Request.QueryString["smscresponse[event]"];
                }
                else
                {
                    grpInboundCallBusinessObj.Event = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[from]"]) == false)
                {
                    fromNumber = context.Request.QueryString["smscresponse[from]"].ToString();
                    Logger.TraceLog("grpInboundCallBusinessObj.ToNumber.Trim()" + grpInboundCallBusinessObj.ToNumber.Trim());
                    if (grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("+973") || grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("973"))
                    {
                        fromNumber = "973" + Strings.Right(fromNumber, 8);

                    }
                    else if (grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("+971") || grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("971"))
                    {
                        fromNumber = "971" + Strings.Right(fromNumber, 9);

                    }
                    else if (grpInboundCallBusinessObj.ToNumber.Trim().Contains("22314188"))
                    {
                        fromNumber = "968" + Strings.Right(fromNumber, 8);

                    }
                    else if (grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("44") | grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("+44"))
                    {
                        fromNumber = "44" + Strings.Right(fromNumber, 10);

                    }
                    else if (fromNumber.Length >= 10)
                    {

                        fromNumber = "91" + Strings.Right(fromNumber, 10);
                    }


                    Logger.TraceLog("from Number get:" + fromNumber);
                    grpInboundCallBusinessObj.FromNumber = fromNumber;
                }
                else
                {
                    grpInboundCallBusinessObj.FromNumber = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[direction]"]) == false)
                {
                    grpInboundCallBusinessObj.Direction = context.Request.QueryString["smscresponse[direction]"];
                }
                else
                {
                    grpInboundCallBusinessObj.Direction = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[callstatus]"]) == false)
                {
                    grpInboundCallBusinessObj.CallStatus = context.Request.QueryString["smscresponse[callstatus]"];
                }
                else
                {
                    grpInboundCallBusinessObj.CallStatus = "";
                };
                if (string.IsNullOrEmpty(context.Request.QueryString["smscresponse[digits]"]) == false)
                {
                    grpInboundCallBusinessObj.Digits = context.Request.QueryString["smscresponse[digits]"];
                    Logger.TraceLog("digits If : " + grpInboundCallBusinessObj.Digits);
                }
                else
                {
                    grpInboundCallBusinessObj.Digits = "";
                    Logger.TraceLog("digits else If : " + grpInboundCallBusinessObj.Digits);
                };
            }
            else
            {
                string jsonStr = "";
                JObject jsonObj = default(JObject);
                StreamReader inputStream = null;
                JObject jsonObjParaMeters = default(JObject);
                context.Request.InputStream.Position = 0;
                inputStream = new StreamReader(context.Request.InputStream);
                jsonObj = new JObject();
                jsonObj = JObject.Parse(jsonStr);
                jsonObjParaMeters = JObject.Parse(jsonObj.SelectToken("smscresponse").ToString());
                if (jsonObjParaMeters.SelectToken("calluid") != null)
                {
                    grpInboundCallBusinessObj.CallUuid = jsonObjParaMeters.SelectToken("calluid").ToString();
                }
                else
                {
                    grpInboundCallBusinessObj.CallUuid = "";
                }
                if (jsonObjParaMeters.SelectToken("to") != null)
                {
                    grpInboundCallBusinessObj.ToNumber = jsonObjParaMeters.SelectToken("to").ToString();
                }
                else
                {
                    grpInboundCallBusinessObj.ToNumber = "";
                }
                if (jsonObjParaMeters.SelectToken("from") != null)
                {
                    fromNumber = jsonObjParaMeters.SelectToken("from").ToString();
                    if (grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("+973") || grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("973"))
                    {
                        fromNumber = "973" + Strings.Right(fromNumber, 7);

                    }
                    else if (grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("+971") || grpInboundCallBusinessObj.ToNumber.Trim().StartsWith("971"))
                    {
                        fromNumber = "971" + Strings.Right(fromNumber, 9);

                    }
                    else if (grpInboundCallBusinessObj.ToNumber.Trim().Contains("22314188"))
                    {
                        fromNumber = "968" + Strings.Right(fromNumber, 8);

                    }
                    else
                    {

                        fromNumber = "91" + Strings.Right(fromNumber, 10);
                    }
                    Logger.TraceLog("from Number 4 post:" + fromNumber);
                    grpInboundCallBusinessObj.FromNumber = fromNumber;
                }
                else
                {
                    grpInboundCallBusinessObj.FromNumber = "";
                }
                if (jsonObjParaMeters.SelectToken("event") != null)
                {
                    grpInboundCallBusinessObj.Event = jsonObjParaMeters.SelectToken("event").ToString();
                }
                else
                {
                    grpInboundCallBusinessObj.Event = "";
                }
                if (jsonObjParaMeters.SelectToken("direction") != null)
                {
                    grpInboundCallBusinessObj.Direction = jsonObjParaMeters.SelectToken("direction").ToString();
                }
                else
                {
                    grpInboundCallBusinessObj.Direction = "";
                }
                if (jsonObjParaMeters.SelectToken("callstatus") != null)
                {
                    grpInboundCallBusinessObj.CallStatus = jsonObjParaMeters.SelectToken("callstatus").ToString();
                }
                else
                {
                    grpInboundCallBusinessObj.CallStatus = "";
                }
                if (jsonObjParaMeters.SelectToken("digits") != null)
                {
                    grpInboundCallBusinessObj.Digits = jsonObjParaMeters.SelectToken("digits").ToString();
                    Logger.TraceLog("Post digits If : " + grpInboundCallBusinessObj.Digits);
                }
                else
                {
                    grpInboundCallBusinessObj.Digits = "";
                }
            };
            context.Response.Write(grpInboundCallBusinessObj.ValidateInboundCall());
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