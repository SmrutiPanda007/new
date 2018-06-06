using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Xml;
using System.IO;
using log4net;
using Microsoft.VisualBasic;
using GT.BusinessLogicLayer;
using GT.Utilities;

namespace GrpTalk.CommonClasses
{

    public class ApiHelper
    {
        private string clientIpAddress = "";
        private string requestGUID = "";
        public const string XmlFormat = "XML";
        public const string JsonFormat = "JSON";
        private string format = "";
        public XmlDocument xmlDoc = null;
        public XmlElement rootElement = null;
        public JObject jObj = null;
        public JArray jArr = null;
        


        #region "ACTIONS"
        public const string GET_ALL_GROUP_CALLS = "GetAllGroupCallsNew";
        public const string GET_REPORTS_BY_BATCH_ID = "GetReportsByBatchIDNew";
        public const string REGISTER = "Register";
        public const string VALIDATE_OTP = "ValidateOtp";
        public const string GET_GROUPCALL_ROOM = "GetConferenceRoom";
        public const string CREATE_GROUPCALL = "CreateGroupCallNew";
        public const string EDIT_GROUPCALL = "EditGroupCallNew";
        public const string ADD_PARTICIPANT_IN_CONFERENCE = "AddParticipantInConference";
        public const string OTP_CALL = "OtpCall";
        public const string SEND_OPTIN_INSTRUCTIONS = "SendOptinInstructions";
        public const string GET_GROUPCALL_DETAILS_BYGROUPID = "GetConfDetailsByConfId";
        public const string USER_BALANCEINFO = "UserBalanceInfo";
        public const string PROFILE_IMAGE = "ProfileImage";
        public const string UPDATE_GROUPCALL_NAME = "UpdateConferenceName";
        public const string DELETE_GROUPCALL = "DeleteConference";
        public const string PUSHER_API = "PusherApi";
        public const string GET_CONFERENCE_BY_CONFID = "Getconferencedetailsbyconfid";
        public const string UPDATE_PROFILE = "UpdateProfile";
        public const string DIAL = "Dial";
        public const string MUTEUNMUTE = "MuteUnMute";
        public const string HANGUP = "Hangup";
        public const string USER_BALANCE = "UserBalance";
        public const string TERMS_AND_CONDITIONS = "TermsAndConditions";
        public const string INAPP_PURCAHSE = "IOSBuyCredits";
        public const string ANDROID_INAPP_PURCHASE = "AndroidBuyCredits";
        public const string GET_COUNTRIES = "GetCountries";
        public const string GRPCALL_CANCEL = "GrpCallCancel";
        public const string CHECK_USER_CONFIRMATION = "CheckUserConfirmation";
        public const string LEAVE_GROUP_CALL = "LeaveGroupCall";
        public const string RECHARGE_REQUEST = "RechargeRequest";
        public const string RECHARGE_HISTORY = "RechargeHistory";
        public const string INAPP_PURCHASE_HISTORY = "InAppPurchaseHistory";
        public const string PhoneContactsSync = "PhoneContactsSync";
        public const string GroupCallHistory = "GroupCallHistory";
        public const string Get_All_ContactsLists = "GetAllContactsLists";
        public const string Get_GroupCall_Room = "GetGroupCallRoom";
        public const string Phone_Call_HistorySync = "PhoneCallHistorySync";
        public const string Get_User_Details = "GetUserDetails";
        public const string QR_Code_Login = "QrCodeLogin";
        public const string QR_Code_Logout = "QrCodeLogout";
        public const string Check_Web_Login = "CheckWebLogin";
        public const string Add_Contact_To_Group = "AddContactToGroup";
        public const string ChangeTimeZone = "ChangeTimeZone";
        public const string UpdateGrpMemberStatus = "UpdateGroupMemberStatus";
        public const string GetAllGroupMembers = "GetAllGroupMembers";
        public const string UpdateGrpSettings = "UpdateGrpAdvancedSettings";
        public const string ClickToSendEmailForReports = "SendEmailReportOfGroup";




        
        //START_CONFERENCE_OUTBOUND As String = "StartConference_OutBound", _
        //START_CONFERENCE_INBOUND As String = "StartConference_InBound", _


        public const string HTTP_GET = "GET";
        public const string HTTP_POST = "POST";
        public const string HTTP_PUT = "PUT";
        public const string HTTP_DELETE = "DELETE";
        #endregion
        

        public Dictionary<string, object> ApiAuthValidate(string apiKey, Boolean isValidate, string apiSecret = "")
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            try
            {
                BusinessHelper businessHelperObj = new BusinessHelper();
                response = businessHelperObj.ApiAuthValidate(CommonClasses.MyConf.MyConnectionString, apiKey, isValidate, apiSecret);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ApiHelper " + ex.ToString());
            }
            return response;
        }


        public ILog TraceLogger = log4net.LogManager.GetLogger("TraceLogger");
        public ApiHelper()
        {
            clientIpAddress = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();
        }
        public void InitializeXmlDoc()
        {
            xmlDoc = new XmlDocument();
            rootElement = xmlDoc.CreateElement("Response");
            xmlDoc.AppendChild(rootElement);
        }
        public void LogTrace(string methodName, string msg, short code)
        {
            if (code == 1)
            {
                TraceLogger.Info(requestGUID + " - " + methodName + " - " + msg);
            }
            else
            {
                TraceLogger.Error(requestGUID + " - " + methodName + " - " + msg);
            }
            try
            {
                string requestSavePath = "D:\\Websites\\GrpTalk\\Logs\\ErrorRequests\\";
                if (!System.IO.Directory.Exists(requestSavePath))
                {
                    System.IO.Directory.CreateDirectory(requestSavePath);
                }
                HttpContext.Current.Request.SaveAs(requestSavePath + requestGUID + "_" + HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"] + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt", true);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ApiHelper " + ex.ToString());
            }
        }
        public void SaveRequest(string prefixString = "")
        {
            try
            {
                string requestSavePath = HttpContext.Current.Server.MapPath("Logs");
                requestSavePath = requestSavePath + "\\";
                if (!System.IO.Directory.Exists(requestSavePath))
                {
                    System.IO.Directory.CreateDirectory(requestSavePath);
                }
                string fileName = requestSavePath + prefixString + "_" + requestGUID + "_" + Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString() + "_"  + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
                HttpContext.Current.Request.SaveAs(fileName, true);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in saving file is " + ex.ToString());

            }
        }

        public void sms_api_call(string mobileNumber, string message)
        {
            mobileNumber = mobileNumber.Replace(" ", "").Replace(System.Environment.NewLine, "");
            mobileNumber = mobileNumber.Replace("+", "");
            HttpWebRequest objWebRequest = default(HttpWebRequest);
            HttpWebResponse objWebResponse = default(HttpWebResponse);
            string[] smsRefID ={
                                    "error",
                                    "0"
                                };
            StreamWriter objStreamWriter = default(StreamWriter);
            StreamReader objStreamReader = default(StreamReader);
            string smsRespNo = null;
            string stringPost = null;
            string msg = "";
            string jobID = null;
            bool status = false;
            CredentialCache credentials = new CredentialCache();

            try
            {
                credentials.Add(new Uri("http://unifiedapi.smscountry.com/v1.1/SendSms/"), "Basic", new NetworkCredential("tWncaEz7GPJtwIcR8QAi", "L74NKAAw5eHpXVFsrSb6LRAtZ8ZDTYjCTzb3CX"));

                stringPost = "MobileNumbers=" + mobileNumber + "&message=" + HttpUtility.UrlEncode(message) + "&MessageType=N&SenderID=GRPTLK";
                objWebRequest = (HttpWebRequest)WebRequest.Create("http://unifiedapi.smscountry.com/v1.1/SendSms/");
                objWebRequest.Method = "POST";
                objWebRequest.Credentials = credentials;
                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringPost);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                smsRespNo = "";
                smsRespNo = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                JObject jObj = new JObject();
                jObj = JObject.Parse(smsRespNo);

                msg = jObj.SelectToken("Message").ToString();
                status = Convert.ToBoolean(jObj.SelectToken("Success"));
                jobID = jObj.SelectToken("JobID").ToString();


            }
            catch (Exception ex)
            {
                //log.Info("Error for slno : " & slno & " at sms_api_call by smsc : " & ex.ToString())
                //HtpContext.Current.Response.Write(ex.ToString())
            }
        }
        public void GetOnlyNumeric(ref string input)
        {
            string[] numericArray = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            foreach (char _char in input)
            {
                int pos = Array.IndexOf(numericArray, _char);
                if (pos > -1)
                {
                    input = input.Replace(_char.ToString(), "");
                }
                //if (_NumericArray.Contains(_Char))
                //{
                //    _Input = _Input.Replace(_Char, "");
                //}
            }
            input = input.Replace(" ", "");
            input = input.Replace(Environment.NewLine, "");
        }
        public void RemoveZeroPrefix(ref string input)
        {
            while (input.StartsWith("0"))
            {
                input = input.Substring(input.Length - 1);
                //_Input = String.Right(_Input, (_Input.Length - 1));
            }
        }
        public void InitializeJSonObjects()
        {
            jObj = new JObject();
            jArr = new JArray();
        }
        public string KillChars(string strWords)
        {
            string[] badChars = null;
            string newChars = null;
            int i = 0;

            badChars = new string[12];
            badChars[0] = "SELECT";
            badChars[1] = "DROP ";
            badChars[2] = ";";
            badChars[3] = "--";
            badChars[4] = "INSERT ";
            badChars[5] = "DELETE ";
            badChars[6] = "XP_";
            badChars[7] = " SELECT";
            badChars[8] = " DROP";
            badChars[9] = " INSERT";
            badChars[10] = " DELETE";

            newChars = strWords;

            for (i = 0; i <= badChars.GetUpperBound(1); i++)
            {
                newChars = newChars.Replace(badChars[i], "");
                //newChars = Strings.Replace(newChars, badChars(i), "", 1, -1, 1);
            }
            badChars = null;
            newChars = newChars.Replace(System.Environment.NewLine, "\n");
            //newChars = Strings.Replace(newChars, Constants.vbCrLf, Constants.vbLf);
            newChars = newChars.Trim();
            //newChars = String.LTrim(Strings.RTrim(newChars));
            return newChars;
        }
        public string EscapedXmlContent(string content)
        {
            return "";
        }
        public void NewProperty(string key, object value, ref JObject destJSonObj, ref XmlElement xmlRootEle, ref XmlDocument doc)
        {
            //if (this.IsXmlFormat)
            //{
            //    if (xmlRootEle == null)
            //    {
            //        xmlRootEle = rootElement;
            //    }
            //    if (doc == null)
            //    {
            //        doc = xmlDoc;
            //    }
            //    foreach (XmlNode _Child in xmlRootEle.SelectNodes("//" + key))
            //    {
            //        if (_Child != null)
            //        {
            //            _Child.RemoveAll();
            //        }
            //    }
            //    XmlElement tempEle = doc.CreateElement(key);
            //    tempEle.InnerText = value.ToString();
            //    xmlRootEle.AppendChild(tempEle);
            //}
            //else
            //{
            if (destJSonObj == null)
            {
                destJSonObj = jObj;
            }
            if (destJSonObj.SelectToken(key) != null)
            {
                destJSonObj.Remove(key);
            }
            destJSonObj.Add(new JProperty(key, value));
            //}
        }
        //public string Format
        //{
        //    get { return _Format.ToUpper(); }
        //    set { _Format = value.ToUpper(); }
        //}

        public string GetResponse()
        {
            //if (this.IsXmlFormat)
            //{
            //    return XmlDoc.InnerXml;
            //}
            //else
            //{
            return jObj.ToString();
            //}
        }
        //public int IsUnicode(string s)
        //{
        //    int found = 0;
        //    int i = 0;
        //    for (i = 1; i <= s.Length; i++)
        //    {
        //        if (Strings.InStr("0123456789ABCDEFabcdef", Strings.Mid(s, i, 1)))
        //        {
        //        }

        //        else
        //        {
        //            found = 1;
        //            return 0;
        //        }
        //    }
        //    if (found == 1)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        return 1;
        //    }
        //}
        //public string GetStringFromHex(string str1)
        //{
        //    string strRetVal = "";
        //    int i = 0;
        //    if (IsUnicode(str1))
        //    {
        //        for (i = 1; i <= str1.Length; i += 2)
        //        {
        //            strRetVal = strRetVal + Strings.Chr(Convert.ToInt64("&H" + Strings.Mid(str1, i, 2)));
        //        }
        //    }
        //    return strRetVal;
        //}
        //public static string DecodeUCS(string str1)
        //{
        //    string functionReturnValue = null;
        //    short i = 0;
        //    string strTemp = "";
        //    string strHEX = "";
        //    short j = 0;
        //    string strTemp1 = "";
        //    j = 0;
        //    for (i = 1; i <= str1.Length; i++)
        //    {
        //        if (j == 0)
        //            strHEX = "&#";
        //        strTemp1 = strTemp1 + Strings.Right("0" + Conversion.Hex(Strings.Asc(Strings.Mid(str1, i, 1))), 2);
        //        if (j == 1)
        //        {
        //            strTemp1 = "0000" + Convert.ToInt64("&H" + strTemp1);
        //            strTemp1 = strTemp1.Substring(strTemp1.Length - 1, 6);
        //            //strTemp1 = Strings.Right("0000" + Convert.ToInt64("&H" + strTemp1), 6);
        //            strTemp = strTemp + strHEX + strTemp1 + ";";
        //            strTemp1 = "";
        //            j = 0;
        //        }
        //        else
        //        {
        //            j = 1;
        //        }
        //    }
        //    functionReturnValue = strTemp;
        //    return functionReturnValue;
        //    return functionReturnValue;
        //}
        //public bool IsUnicodeMessage(string str1)
        //{
        //    int i = 0;
        //    try
        //    {
        //        for (i = 1; i <= str1.Length; i++)
        //        {
        //            if (Strings.AscW(Strings.Mid(str1, i, 1)) > 255)
        //            {
        //                return true;
        //            }
        //        }
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        //MsgBox("Error Finding Unicode.")
        //        return false;
        //    }
        //}
        //public string UDH_con_7bit_To_8bit(string str1)
        //{
        //    string[] strTemp = null;
        //    int i = 0;
        //    string strBinData = "";
        //    byte bytBitsReq = 0;

        //    strTemp = new string[str1.Length * 7 / 7 + 1];
        //    for (i = 0; i <= str1.Length - 1; i++)
        //    {
        //        strTemp[i] = Get7bitBinary(Strings.Mid(str1, i + 1, 1));
        //    }
        //    bytBitsReq = 1;
        //    for (i = 0; i <= Information.UBound(strTemp) - 1; i++)
        //    {
        //        if (i < Information.UBound(strTemp))
        //        {
        //            if (!string.IsNullOrEmpty(strTemp[i]))
        //            {
        //                if (bytBitsReq == 8)
        //                    bytBitsReq = 1;
        //                if (i < Information.UBound(strTemp) - 1)
        //                {
        //                    strBinData=strBinData+strTemp[i + 1].Substring(strTemp[i + 1].Length-1,bytBitsReq)+ strTemp[i];
        //                    //strBinData = strBinData + Strings.Right(strTemp[i + 1], bytBitsReq) + strTemp[i];
        //                    strTemp[i + 1] = Strings.Mid(strTemp[i + 1], 1,(strTemp[i + 1]).Length - bytBitsReq);
        //                    bytBitsReq = Convert.ToByte(bytBitsReq + 1);
        //                }
        //                else
        //                {
        //                    strBinData=strBinData+("00000000" + strTemp[i]).Substring(("00000000" + strTemp[i]).Length-1,8)
        //                    //strBinData = strBinData + Strings.Right("00000000" + strTemp[i], 8);
        //                }
        //            }
        //        }
        //        else
        //        {
                    
        //            strBinData = strBinData + Strings.Right("00000000" + strTemp[i], 8);
        //            strBinData=strBinData+ ("00000000" + strTemp[i]).Substring(("00000000" + strTemp[i]).Length-1,8);
        //        }
        //    }
        //    strTemp = null;
        //    return strBinData;
        //}
        //public string Get7bitBinary(string str1)
        //{
        //    string strRetval = "";
        //    int j = 0;

        //    j = Strings.Asc(str1);
        //    while (1)
        //    {
        //        if (j == 0)
        //            break; // TODO: might not be correct. Was : Exit Do
        //        if (j == 1)
        //        {
        //            strRetval = strRetval + j;
        //        }
        //        else
        //        {
        //            strRetval = strRetval + j % 2;
        //        }
        //        j = j / 2;
        //    }
        //    strRetval = Strings.Mid(strRetval + "000000", 1, 7);
        //    return Strings.StrReverse(strRetval);
        //}
        //public char getChar(string str1)
        //{
        //    int i = 0;
        //    int j = 0;
        //    byte bytByteCal = 0;
        //    j = 256;
        //    for (i = 1; i <= 8; i++)
        //    {
        //        j = j / 2;
        //        bytByteCal = bytByteCal + Strings.Mid(str1, i, 1) * j;
        //    }
        //    return Strings.Chr(bytByteCal);
        //}

        public void FlushErrorResponse(int statusCode, int subCode = -1, string statusDescription = "")
        {
            HttpContext.Current.Response.StatusCode = statusCode;
            if (subCode >= 0)
            {
                HttpContext.Current.Response.SubStatusCode = subCode;
            }
            if (!string.IsNullOrEmpty(statusDescription))
            {
                HttpContext.Current.Response.StatusDescription = statusDescription;
            }
            HttpContext.Current.Response.End();
            //HttpContext.Current.ApplicationInstance.CompleteRequest()
        }
#region "PROPERTIES"
        public string ClientIpAddress
        {
            get { return clientIpAddress; }
            set { clientIpAddress = value; }
        }
        public string Format
        {
            get { return format.ToUpper(); }
            set { format = value.ToUpper(); }
        }
        public bool IsXmlFormat
        {
            get
            {
                if (this.Format == ApiHelper.XmlFormat)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

    }

}