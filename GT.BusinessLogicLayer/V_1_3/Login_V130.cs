using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using GT.DataAccessLayer;
using GTDataTypes;
using GT.Utilities;
using GT.Utilities.Properties;
using System.Net;
using System.Configuration;
using PusherServer;
using System.Drawing.Drawing2D;
using System.Web;
using ZXing;
using System.Drawing;
using ZXing.QrCode;
using ZXing.QrCode.Internal;


namespace GT.BusinessLogicLayer.V_1_3
{
    public class Login_V130
    {

        public JObject OtpCall(string sConnString, string mobile)
        {
            JObject responseObj = new JObject();
            DataSet Ds = new DataSet();
            int retVal;
            int isConfirmed;
            string retMsg = "";
            string otp = "";
            string fromNumber = "";
            BusinessHelper _helper = new BusinessHelper();
            _helper.GetOnlyNumeric(ref mobile);
            _helper.RemoveZeroPrefix(ref mobile);

            DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
            try
            {
                Ds = loginObj.OtpCall(mobile, out retVal, out retMsg, out isConfirmed, out otp);

                if (retVal != 1)
                {
                    responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", retMsg), new JProperty("ErrorCode", "110"),
                            new JProperty("MobileNumber", mobile));
                    return responseObj;
                }
                else
                {
                    if (isConfirmed == 1)
                    {
                        responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "User Already Confirmed"), new JProperty("ErrorCode", "109"),
                            new JProperty("MobileNumber", mobile));
                        return responseObj;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(otp))
                        {

                            if (!string.IsNullOrEmpty(otp))
                            {
                                if (mobile.Replace(" ", "").StartsWith("1"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["USACallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("971"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["UAECallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("973"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["BahrainCallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("968"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["OmanCallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("966"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["SaudiCallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("974"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["QatarCallMeNumber"].ToString();
                                }
                                else if (mobile.Replace(" ", "").StartsWith("965"))
                                {
                                    fromNumber = ConfigurationManager.AppSettings["KuwaitCallMeNumber"].ToString();
                                }
                                else
                                {
                                    fromNumber = ConfigurationManager.AppSettings["IndiaCallMeNumber"].ToString();
                                }
                                if (mobile.Replace(" ", "") == "18036584329")
                                {
                                    ApiOtpCall("919052485271", otp, ConfigurationManager.AppSettings["IndiaCallMeNumber"].ToString());
                                }
                                if (mobile.Replace(" ", "") == "18036102779")
                                {
                                    ApiOtpCall("919040767686", otp, ConfigurationManager.AppSettings["IndiaCallMeNumber"].ToString());
                                }

                            }
                            ApiOtpCall(mobile, otp, fromNumber);
                        }
                    }
                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", retMsg), new JProperty("ErrorCode", "117"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in otpcall :" + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "Something Went Wrong"),
                            new JProperty("ErrorCode", "101"));
            }




            return responseObj;
        }

        public bool ApiOtpCall(string MobileNumber, string Otp, string fromNumber)
        {

            bool IsSuccess = false;
            string numberClipUrl = "";
            string url = "";

            string otpString = "";
            for (int i = 0; i <= Otp.Length - 1; i++)
            {
                numberClipUrl = ConfigurationManager.AppSettings["OtpNumbersClipUrl"].ToString();
                url = ConfigurationManager.AppSettings["OtpNumbersClipUrl"].ToString();
                if (Otp[i] == '0')
                {
                    numberClipUrl = numberClipUrl + "Zero.mp3";
                }
                else if (Otp[i] == '1')
                {
                    numberClipUrl = numberClipUrl + "One.mp3";
                }
                else if (Otp[i] == '2')
                {
                    numberClipUrl = numberClipUrl + "Two.mp3";
                }
                else if (Otp[i] == '3')
                {
                    numberClipUrl = numberClipUrl + "Three.mp3";
                }
                else if (Otp[i] == '4')
                {
                    numberClipUrl = numberClipUrl + "Four.mp3";
                }
                else if (Otp[i] == '5')
                {
                    numberClipUrl = numberClipUrl + "Five.mp3";
                }
                else if (Otp[i] == '6')
                {
                    numberClipUrl = numberClipUrl + "Six.mp3";
                }
                else if (Otp[i] == '7')
                {
                    numberClipUrl = numberClipUrl + "Seven.mp3";
                }
                else if (Otp[i] == '8')
                {
                    numberClipUrl = numberClipUrl + "Eight.mp3";
                }
                else
                {
                    numberClipUrl = numberClipUrl + "Nine.mp3";
                }

                otpString = otpString + "<play>" + numberClipUrl + "</play>";
            }

            JObject requestJobj = new JObject();
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            string xml = "<Response><play>" + url + "GrpTalkOTP.mp3" + "</play>" + otpString + "<play>" + url + "OnceAgain.mp3" + "</play>" + otpString + "<play>" + url + "ThankYou.mp3" + "</play><Hangup/></Response>";
            requestJobj = new JObject(new JProperty("Number", MobileNumber),
                new JProperty("CallerId", fromNumber),
                new JProperty("RingUrl", "www.grptalk.com"),
                new JProperty("AnswerUrl", "www.grptalk.com"),
                new JProperty("HangupUrl", "www.grptalk.com"),
                new JProperty("HttpMethod", "POST"),
                new JProperty("Xml", xml));

            try
            {
                string stringResult = null;
                objWebRequest = (HttpWebRequest)WebRequest.Create("https://restapi.smscountry.com/v0.1/Accounts/sIkXPrYqDPe6xxjZgT1z/Calls/");
                objWebRequest.Method = "POST";
                objWebRequest.ContentType = "application/json";
                objWebRequest.Headers.Add("authorization", "Basic c0lrWFByWXFEUGU2eHhqWmdUMXo6aGQ1VzJkMmJjQXpzOHRtUk1hd3hXSlR3MXFXcFZlNW9nMHNNMkNNVg==");
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                Logger.TraceLog("CallMe requestObj :" + requestJobj.ToString());
                objStreamWriter.Write(requestJobj);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                IsSuccess = true;
                Logger.TraceLog("CallMe response :" + stringResult.ToString());
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In ApiOtpCall : " + ex.ToString());
                IsSuccess = false;
            }
            finally
            {
                if ((objStreamWriter != null))
                {
                    objStreamWriter.Close();
                }
                if ((objStreamReader != null))
                {
                    objStreamReader.Close();
                }
                objWebRequest = null;
                objWebResponse = null;
            }

            return IsSuccess;
            //request = "<request action=\"http://smscountry.com/testdr.aspx\" method=\"POST\"><to>" + MobileNumber + "</to><speak>Hello your group talk o t p is </speak> " + otpString + "<speak> once again your group talk o t p is </speak> " + otpString + " </request>";
            //stringpost = "api_key=" + api_key + "&access_key=" + access_key + "&xml=" + request;
            //HttpWebRequest objWebRequest = null;
            //HttpWebResponse objWebResponse = null;
            //StreamWriter objStreamWriter = null;
            //StreamReader objStreamReader = null;
            //try
            //{
            //    string stringResult = null;
            //    objWebRequest = (HttpWebRequest)WebRequest.Create("http://voiceapi.smscountry.com/api");
            //    objWebRequest.Method = "POST";
            //    objWebRequest.ContentType = "application/x-www-form-urlencoded";
            //    objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
            //    objStreamWriter.Write(stringpost);
            //    objStreamWriter.Flush();
            //    objStreamWriter.Close();
            //    objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
            //    objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
            //    stringResult = objStreamReader.ReadToEnd();
            //    objStreamReader.Close();
            //    IsSuccess = true;
            //}
            //catch (Exception ex)
            //{
            //    IsSuccess = false;
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
            //return IsSuccess;
        }

        public JObject ValidateOTP(string sConnString, string mobile, string OTP, string txnID, int isRegisteredFromWeb)
        {
            DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
            DataSet ds = new DataSet();
            JObject jObj;
            JArray jArr = new JArray();
            Int16 retVal = 0;
            string retMessage = "";
            int errorCode = 0;
            BusinessHelper businessHelperObj = new BusinessHelper();
            try
            {
                businessHelperObj.GetOnlyNumeric(ref mobile);
                businessHelperObj.RemoveZeroPrefix(ref mobile);
                ds = loginObj.ValidateOTP(mobile, OTP, txnID, isRegisteredFromWeb, out retVal, out retMessage, out errorCode);

                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                                     new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataColumn _Column in ds.Tables[0].Rows[0].Table.Columns)
                        {
                            jObj.Add(new JProperty(_Column.ColumnName, ds.Tables[0].Rows[0][_Column.ColumnName]));
                        }
                    }
                }
                else
                {
                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in loginBusiness is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"), new JProperty("ErrorCode", "101"));
                //new JProperty("Message", ex.ToString()));
            }
            return jObj;
        }


        public JObject Registration(string sConnString, JObject paramObj, string clientIpAddr, string txnID, Boolean isResend, int countryID)
        {
            DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
            DataSet ds = new DataSet();
            JObject jObj;
            JArray jArr = new JArray();
            Int16 retVal = 0, isExisted = 0;
            int errorCode = 0;
            string retMessage = "", mobile = "";
            BusinessHelper businessHelperObj = new BusinessHelper();
            string missedCallNumber = "";

            if (countryID == 19)
            {
                missedCallNumber = ConfigurationManager.AppSettings["BahrainMissedCallNumber"].ToString();
            }
            else if (countryID == 239)
            {
                missedCallNumber = ConfigurationManager.AppSettings["UAEMissedCallNumber"].ToString();
            }
            else if (countryID == 241)
            {
                missedCallNumber = ConfigurationManager.AppSettings["USAMissedCallNumber"].ToString();
            }
            else if (countryID == 173)
            {
                missedCallNumber = ConfigurationManager.AppSettings["OmanMissedCallNumber"].ToString();
            }
            else if (countryID == 199)
            {
                missedCallNumber = ConfigurationManager.AppSettings["SaudiMissedCallNumber"].ToString();
            }
            else if (countryID == 124)
            {
                missedCallNumber = ConfigurationManager.AppSettings["KuwaitMissedCallNumber"].ToString();
            }
            else if (countryID == 188)
            {
                missedCallNumber = ConfigurationManager.AppSettings["QatarMissedCallNumber"].ToString();
            }
            else
            {
                missedCallNumber = ConfigurationManager.AppSettings["IndiaMissedCallNumber"].ToString();
            }
            try
            {
                RegistrationDetails detailsObj = new RegistrationDetails();


                if (paramObj.SelectToken("MobileNumber") == null)
                {
                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", "Unable to read MobileNumber"),
                                     new JProperty("ErrorCode", "107"));
                    return jObj;


                }
                mobile = paramObj.SelectToken("MobileNumber").ToString();
                businessHelperObj.GetOnlyNumeric(ref mobile);
                businessHelperObj.RemoveZeroPrefix(ref mobile);


                detailsObj.Mobile = mobile;
                detailsObj.DeviceUniqueID = paramObj.SelectToken("DeviceUniqueID").ToString();
                detailsObj.DeviceToken = paramObj.SelectToken("DeviceToken").ToString();
                detailsObj.OsID = Int16.Parse(paramObj.SelectToken("OsID").ToString());
                detailsObj.ClientIpAddress = clientIpAddr;
                detailsObj.TxnID = txnID;

                if (isResend)
                {
                    detailsObj.IsResend = "Yes";
                }
                else
                {
                    detailsObj.IsResend = "";
                }


                ds = loginObj.Registration(detailsObj, out isExisted, out retVal, out retMessage, out errorCode);




                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                            new JProperty("IsExisted", isExisted),
                            new JProperty("MissedCallNumber", missedCallNumber),
                            new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                }
                else
                {
                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", retMessage), new JProperty("ErrorCode", errorCode));
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in LoginBusiness is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "Something Went Wrong"),
                                   new JProperty("ErrorCode", "101"));


            }
            return jObj;
        }

        public JObject ValidateAccessToken(string sConnString, string accessToken)
        {
            JObject jObj = new JObject();
            int statusCode = 0, subStatusCode = 0, retVal = 0;
            string statusDescription = "", retMsg = "";
            DataSet ds = new DataSet();
            EntityHelper entitiHelperObj = new EntityHelper(sConnString);
            try
            {
                ds = entitiHelperObj.ValidateAccessToken(accessToken, out statusCode, out subStatusCode, out statusDescription, out retVal, out retMsg);
                if (retVal == 1)
                {
                    if (ds != null || ds.Tables.Count != 0)
                    {

                        DataRow dr = ds.Tables[0].Rows[0];
                        jObj.Add(new JProperty("Success", true));
                        foreach (DataColumn dc in ds.Tables[0].Columns)
                        {
                            jObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                        }


                    }
                }
                else
                {
                    jObj = new JObject(new JProperty("Success", false),
                                         new JProperty("Message", retMsg));
                }
            }

            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ValidateAccessToken BAL is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "Something Went Wrong"),
                                   new JProperty("ErrorCode", "101"));
            }
            return jObj;

        }

        public JObject QR_Code_Check(string sConnString, JObject jObj, int userID)
        {

            JObject responseJobj = new JObject();
            string qrCode = Convert.ToString(jObj.SelectToken("QrCode"));
            string deviceId = Convert.ToString(jObj.SelectToken("DeviceUniqueId"));
            string deviceToken = Convert.ToString(jObj.SelectToken("DeviceToken"));
            int osId = Convert.ToInt32(jObj.SelectToken("OsId"));
            int retVal = 0;
            string retMsg = ""; string qrCodeAccessToken = "";
            DataSet ds = new DataSet();
            string pusherAppId = System.Configuration.ConfigurationManager.AppSettings["pusherAppId"].ToString();
            string pusherAppKey = System.Configuration.ConfigurationManager.AppSettings["pusherAppKey"].ToString();
            string pusherAppsecret = System.Configuration.ConfigurationManager.AppSettings["pusherAppsecret"].ToString();

            DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
            ds = loginObj.QRCodeChecking(userID, qrCode, deviceId, deviceToken, osId, out retVal, out retMsg);
            if (retVal == 1)
            {
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        qrCodeAccessToken = ds.Tables[0].Rows[0]["QrCodeAccessToken"].ToString();
                    }

                }
                
                Pusher pusherObj = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);
                ITriggerResult PusherResponse = null;
                PusherResponse = pusherObj.Trigger(qrCode, "Login", new
                {
                    IsloggedIn = 0,
                    QrCode = qrCode,
                    QrCodeAccessToken = qrCodeAccessToken,
                    DeviceUniqueId = deviceId,
                    DeviceToken = deviceToken,
                    OsId = osId
                });
                responseJobj = new JObject(new JProperty("Success", true),
                                       new JProperty("Message", "Succes"));
            }
            else
            {
                responseJobj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", retMsg));
            }
            return responseJobj;

        }

        public JObject ValidateQrCode(string sConnString, string qrCodeAccessToken, string qrCode, string deviceUniqueId, string deviceToken, string loginIp, string browserName, Int16 osID, string osName)
        {
            Logger.TraceLog("QrCode retval BAl " + qrCodeAccessToken);
            JObject jresponseObj = new JObject();
            Int16 retVal = 0;
            string retMessage = "";
            DataSet ds = new DataSet();
            string pusherAppId = System.Configuration.ConfigurationManager.AppSettings["pusherAppId"].ToString();
            string pusherAppKey = System.Configuration.ConfigurationManager.AppSettings["pusherAppKey"].ToString();
            string pusherAppsecret = System.Configuration.ConfigurationManager.AppSettings["pusherAppsecret"].ToString();
            try
            {
                DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
                string locationAddress = GetLocationName(loginIp);
                ds = loginObj.ValidateQrCode(qrCodeAccessToken, qrCode, deviceUniqueId, deviceToken, loginIp, locationAddress, browserName, osName, osID, out retVal, out retMessage);
                Logger.TraceLog("QrCode retval" + retVal.ToString());
                if (retVal == 1)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            jresponseObj.Add(new JProperty("Success", true));
                            jresponseObj.Add(new JProperty("Message", "Success"));
                            jresponseObj.Add(new JProperty("RedirectURL", ConfigurationManager.AppSettings["WebUrl"] + "mygrptalks.aspx"));
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                foreach (DataColumn dc in ds.Tables[0].Columns)
                                {
                                    jresponseObj.Add(new JProperty(dc.ColumnName, dr[dc.ColumnName]));
                                }
                            }

                            Pusher pusherObj = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);
                            ITriggerResult PusherResponse = null;



                            PusherResponse = pusherObj.Trigger(qrCode, "Login", new
                            {
                                IsloggedIn = 1,
                                OsName = osName,
                                browserName = ConfigurationManager.AppSettings["WebUrl"].ToString() + "/images/" + browserName + ".png",
                                Location = locationAddress
                            });



                        }
                        else
                        {
                            jresponseObj.Add(new JProperty("Success", false));
                            jresponseObj.Add(new JProperty("Message", "Failed"));

                        }
                    }
                    else
                    {

                        jresponseObj.Add(new JProperty("Success", false));
                        jresponseObj.Add(new JProperty("Message", "Failed"));
                    }
                }
                else
                {
                    jresponseObj.Add(new JProperty("Success", false));
                    jresponseObj.Add(new JProperty("Message", retMessage));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ValidateQrCode BAL is " + ex.ToString());
                jresponseObj = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "Something Went Wrong"),
                                   new JProperty("ErrorCode", "101"));
            }

            return jresponseObj;
        }

        private string GetLocationName(string ipAddress)
        {
            WebClient wc = new WebClient();
            string locationName = "";
            try
            {
                Stream data = wc.OpenRead("http://www.freegeoip.net/json/" + ipAddress);
                StreamReader sr = new StreamReader(data);
                string response = sr.ReadToEnd().ToString();

                JObject jsonResponse = JObject.Parse(response);
                if (jsonResponse.SelectToken("city") != null || jsonResponse.SelectToken("region_code") != null)
                {
                    locationName = Convert.ToString(jsonResponse.SelectToken("city")) + "-" + jsonResponse.SelectToken("region_code");
                }
            }
            catch (Exception ex)
            { Logger.ExceptionLog("Exception in GetLocationName() BAL is " + ex.ToString()); }

            return locationName;



        }

        public JObject QrCodeLogout(JObject jobj, string sConnString, int userId)
        {
            JObject jresponse = new JObject();
            string qrCode = string.Empty;
            int retVal = 0; string message = string.Empty;
            try
            {
                string pusherAppId = System.Configuration.ConfigurationManager.AppSettings["pusherAppId"].ToString();
                string pusherAppKey = System.Configuration.ConfigurationManager.AppSettings["pusherAppKey"].ToString();
                string pusherAppsecret = System.Configuration.ConfigurationManager.AppSettings["pusherAppsecret"].ToString();

                if (jobj.SelectToken("QrCode") != null)
                {
                    GT.DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
                    retVal = loginObj.QrCodeLogOut(userId, out message);
                    if (retVal == 1)
                    {
                        qrCode = Convert.ToString(jobj.SelectToken("QrCode"));

                        Pusher pusherObj = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);
                        ITriggerResult pusherResponse = null;
                        pusherResponse = pusherObj.Trigger(qrCode, "Logout", new
                        {
                            QrCode = qrCode,
                            IsLoggedOut = 1
                        });
                        jresponse = new JObject(new JProperty("Success", true),
                            new JProperty("Message", "Success"));
                        if (HttpContext.Current.Request.Cookies["SessionId"] != null || HttpContext.Current.Request.Cookies["UserData"] != null)
                        {
                            HttpContext.Current.Request.Cookies["SessionId"].Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Current.Request.Cookies["UserData"].Expires = DateTime.Now.AddDays(-1);
                        }

                    }
                    else
                    {
                        jresponse = new JObject(new JProperty("Success", false),
                             new JProperty("Message", "Log Out Failed"));
                    }
                }
                else
                {
                    jresponse = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Invalid QrCode"));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in QrCodeLogout : " + ex.ToString());
                jresponse = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"));
            }
            return jresponse;

        }

        public JObject WebLoginCheck(string sConnString, int userId)
        {
            JObject jresponse = new JObject();
            int retVal = 0;
            string retMessage = string.Empty;
            int isLoggedIn = 0;
            try
            {
                GT.DataAccessLayer.V_1_3.Login_V130 loginObj = new DataAccessLayer.V_1_3.Login_V130(sConnString);
                DataTable dt = loginObj.WebLoginCheck(userId, out retVal, out retMessage);
                if (retVal == 1)
                {

                    if (dt.Rows.Count != 0)
                    {
                        jresponse = new JObject();
                        jresponse.Add(new JProperty("Success", true));
                        jresponse.Add(new JProperty("Message", "Success"));


                        foreach (DataColumn dc in dt.Columns)
                        {
                            if (dc.ColumnName != "LoginBrowser")
                            {
                                jresponse.Add(new JProperty(dc.ColumnName, dt.Rows[0][dc.ColumnName]));
                            }
                            else { jresponse.Add(new JProperty("browserName", ConfigurationManager.AppSettings["WebUrl"].ToString() + "/images/" + dt.Rows[0][dc.ColumnName] + ".png")); }
                        }


                    }

                }
                else
                {
                    jresponse = new JObject(new JProperty("Success", false),
                     new JProperty("Message", retMessage));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in WebLoginCheck : " + ex.ToString());
                jresponse = new JObject(new JProperty("Success", false),
                        new JProperty("Message", "Something Went Wrong"));

            }
            return jresponse;
        }

        public JObject QrCodeGenerator()
        {
            JObject jobj;
            string baseString = "";
            string conf_code = "";
            string overlayImagePath = System.Web.HttpContext.Current.Server.MapPath("/images/") + ConfigurationManager.AppSettings["QrCodeOverlayImage"].ToString();
            conf_code = "grpTalk" + System.Guid.NewGuid().ToString();
            conf_code = conf_code.Replace("-", "").Replace(" ", "");
            //GENERATE_QR_CODE("12345678910111213456789987456321456987456321");
            ZXing.Common.BitMatrix bitmatrix = default(ZXing.Common.BitMatrix);
            Bitmap bmp_image = default(Bitmap);
            string qr_to_str = null;
            IBarcodeWriter writer = default(IBarcodeWriter);


            try
            {
                writer = new BarcodeWriter
                {
                    Format = BarcodeFormat.QR_CODE,
                    Options = new ZXing.QrCode.QrCodeEncodingOptions
                    {
                        Margin = 0,
                        Width = 500,
                        Height = 500,
                        ErrorCorrection = ZXing.QrCode.Internal.ErrorCorrectionLevel.L
                    }
                };
                qr_to_str = conf_code;//CryptoUtil.Encrypt(conf_code);
                bitmatrix = writer.Encode(qr_to_str);
                bmp_image = new Bitmap(bitmatrix.Width, bitmatrix.Height);
                for (int i = 0; i <= bmp_image.Height - 1; i++)
                {
                    for (int j = 0; j <= bmp_image.Width - 1; j++)
                    {
                        if (bitmatrix[i, j])
                        {
                            bmp_image.SetPixel(i, j, Color.Black);
                        }
                        else
                        {
                            bmp_image.SetPixel(i, j, Color.White);
                        }
                    }
                }
                Bitmap overlay = new Bitmap(overlayImagePath);

                int deltaHeigth = bmp_image.Height - overlay.Height;
                int deltaWidth = bmp_image.Width - overlay.Width;

                Graphics g = Graphics.FromImage(bmp_image);
                g.DrawImage(overlay, new Point(deltaWidth / 2, deltaHeigth / 2));

                MemoryStream stream = new MemoryStream();
                bmp_image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = stream.ToArray();
                baseString = Convert.ToBase64String(imageBytes);
                jobj = new JObject(
                    new JProperty("Success", true),
                    new JProperty("Message", "Success"),
                    new JProperty("ConfCode", conf_code),
                    new JProperty("BaseString", baseString));


            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.ToString());
                jobj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"));
            }
            Pusher pusherObj = new Pusher("99686", "ed522d982044e2680be6", "2d2388bc3972643d5b5b");
            ITriggerResult PusherResponse = null;

            PusherResponse = pusherObj.Trigger(conf_code, "client-myEvent", new
            {
                AccessToken = "outbound"

            });
            Logger.TraceLog("pusher response :" + PusherResponse.ToString());
            return jobj;
        }




    }
}
