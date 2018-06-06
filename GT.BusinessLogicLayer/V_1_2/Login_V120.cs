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

namespace GT.BusinessLogicLayer.V_1_2
{
    public class Login_V120
    {
        public JObject OtpCall(string sConnString, string mobile)
        {
            JObject responseObj = new JObject();
            DataSet Ds = new DataSet();
            int retVal;
            int isConfirmed;
            string retMsg = "";
            string otp = "";
            BusinessHelper _helper = new BusinessHelper();
            _helper.GetOnlyNumeric(ref mobile);
            _helper.RemoveZeroPrefix(ref mobile);

            DataAccessLayer.V_1_2.Login_V120 loginObj = new DataAccessLayer.V_1_2.Login_V120(sConnString);
            try
            {
                Ds = loginObj.OtpCall(mobile, out retVal, out retMsg, out isConfirmed, out otp);

                if (retVal != 1)
                {
                    responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", retMsg),
                            new JProperty("MobileNumber", mobile));
                    return responseObj;
                }
                else
                {
                    if (isConfirmed == 1)
                    {
                        responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "User Already Confirmed"),
                            new JProperty("MobileNumber", mobile));
                        return responseObj;

                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(otp))
                        {
                            ApiOtpCall(mobile, otp);
                        }
                    }
                    responseObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", retMsg));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in otpcall :" + ex.ToString());
                responseObj = new JObject(new JProperty("Success", false),
                            new JProperty("Message", "User Already Confirmed"),
                            new JProperty("ErrorCode", "E0002"));
            }




            return responseObj;
        }

        public bool ApiOtpCall(string MobileNumber, string Otp)
        {
            string api_key = "botuWZooZDamqVdGfXns";
            string access_key = "nnsLKPzjklbhBZilBZjTEANUwZVyTYWjgJDGIEnV";
            bool IsSuccess = false;

            string request = "";
            string FinalOtp = "";
            string otpString = "";
            string stringpost = null;
            for (int i = 0; i <= Otp.Length - 1; i++)
            {
                FinalOtp = FinalOtp + " " + Otp[i];
                otpString = otpString + "<speak>" + Otp[i] + "</speak>";
            }
            request = "<request action=\"http://smscountry.com/testdr.aspx\" method=\"POST\"><to>" + MobileNumber + "</to><speak>Hello your group talk o t p is </speak> " + otpString + "<speak> once again your group talk o t p is </speak> " + otpString + " </request>";
            stringpost = "api_key=" + api_key + "&access_key=" + access_key + "&xml=" + request;
            HttpWebRequest objWebRequest = null;
            HttpWebResponse objWebResponse = null;
            StreamWriter objStreamWriter = null;
            StreamReader objStreamReader = null;
            try
            {
                string stringResult = null;
                objWebRequest = (HttpWebRequest)WebRequest.Create("http://voiceapi.smscountry.com/api");
                objWebRequest.Method = "POST";
                objWebRequest.ContentType = "application/x-www-form-urlencoded";
                objStreamWriter = new StreamWriter(objWebRequest.GetRequestStream());
                objStreamWriter.Write(stringpost);
                objStreamWriter.Flush();
                objStreamWriter.Close();
                objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
                objStreamReader = new StreamReader(objWebResponse.GetResponseStream());
                stringResult = objStreamReader.ReadToEnd();
                objStreamReader.Close();
                IsSuccess = true;
            }
            catch (Exception ex)
            {
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
        }

        public JObject ValidateOTP(string sConnString, string mobile, string OTP, string txnID)
        {
            DataAccessLayer.V_1_2.Login_V120 loginObj = new DataAccessLayer.V_1_2.Login_V120(sConnString);
            DataSet ds = new DataSet();
            JObject jObj;
            JArray jArr = new JArray();
            Int16 retVal = 0;
            string retMessage = "";
            BusinessHelper businessHelperObj = new BusinessHelper();
            try
            {
                businessHelperObj.GetOnlyNumeric(ref mobile);
                businessHelperObj.RemoveZeroPrefix(ref mobile);
                ds = loginObj.ValidateOTP(mobile, OTP, txnID, out retVal, out retMessage);

                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                                     new JProperty("Message", retMessage));
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
                                     new JProperty("Message", retMessage));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in loginBusiness is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"));
                //new JProperty("Message", ex.ToString()));
            }
            return jObj;
        }


        public JObject Registration(string sConnString, JObject paramObj, string clientIpAddr, string txnID, Boolean isResend,int countryID)
        {
            DataAccessLayer.V_1_2.Login_V120 loginObj = new DataAccessLayer.V_1_2.Login_V120(sConnString);
            DataSet ds = new DataSet();
            JObject jObj;
            JArray jArr = new JArray();
            Int16 retVal = 0, isExisted = 0;
            string retMessage = "", mobile = "";
            BusinessHelper businessHelperObj = new BusinessHelper();
            string missedCallNumber = "";
            
            if (countryID==19)
            {
                missedCallNumber = ConfigurationManager.AppSettings["BahrainMissedCallNumber"].ToString();
            }
            else if (countryID==239)
            {
                missedCallNumber = ConfigurationManager.AppSettings["UAEMissedCallNumber"].ToString();
            }
            else if (countryID == 241)
            {
                missedCallNumber = ConfigurationManager.AppSettings["USAMissedCallNumber"].ToString();
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
                                     new JProperty("ErrorCode", "E0001"));
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


                ds = loginObj.Registration(detailsObj, out isExisted, out retVal, out retMessage);




                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                            new JProperty("IsExisted", isExisted),
                            new JProperty("MissedCallNumber", missedCallNumber),
                            new JProperty("Message", retMessage));
                }
                else
                {
                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", retMessage));
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in LoginBusiness is " + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                                   new JProperty("Message", "Something Went Wrong"),
                                   new JProperty("ErrorCode", "E0002"));


            }
            return jObj;
        }
    }
}
