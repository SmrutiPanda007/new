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
using GT.DataAccessLayer;
using GT.Utilities;
using System.Text;


namespace GT.BusinessLogicLayer
{
    public class BusinessHelper
    {
        public string connString = "";

        public JObject jObj = new JObject();
        public Dictionary<string, object> ApiAuthValidate(string sConnString, string apiKey, bool isValidate, string apiSecret = "")
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, string> authorizedUsers = new Dictionary<string, string>();
            int statusCode = 0, subStatusCode = 0, retVal = 0;
            string statusDescription = "", retMsg = "";
            DataSet ds = new DataSet();
            EntityHelper entitiHelperObj = new EntityHelper(sConnString);
            authorizedUsers.Add("S2m8HRmwf5", "alR8pQok8ll2zCYmZL4R");
            authorizedUsers.Add("fDZjHHybyM", "zu5nKlRQFbPCQrihQn51");
            authorizedUsers.Add("smscountry", "smsc");
            authorizedUsers.Add("asda", "sadasda");
            authorizedUsers.Add("gQ4uXstuq8", "ysRcBcoMNoSAQ68OBte");
            try
            {
                if (string.IsNullOrEmpty(apiKey) || string.IsNullOrWhiteSpace(apiKey))
                {
                    response.Add("StatusCode", 401);
                    response.Add("SubStatusCode", 0);
                    response.Add("StatusDescription", "Unauthorized");
                    response.Add("Success", false);
                    response.Add("Message", "ApiKey Is Missing");
                    return response;
                }
                else if (string.IsNullOrEmpty(apiSecret) || string.IsNullOrWhiteSpace(apiSecret))
                {
                    response.Add("StatusCode", 401);
                    response.Add("SubStatusCode", 0);
                    response.Add("StatusDescription", "Unauthorized");
                    response.Add("Success", false);
                    response.Add("Message", "ApiSecret Is Missing");
                    return response;
                }
                bool res = authorizedUsers.ContainsKey(apiKey);
                if (authorizedUsers.ContainsKey(apiKey) != true)
                {
                    response.Add("Success", false);
                    response.Add("Message", "Unauthorized");
                    response.Add("StatusCode", 401);
                    response.Add("SubStatusCode", 0);
                    response.Add("StatusDescription", "Unauthorized");
                    return response;
                }
                if (authorizedUsers[apiKey] != apiSecret)
                {
                    response.Add("Success", false);
                    response.Add("Message", "Invalid Combination of ApiKey and ApiSecret");
                    response.Add("StatusCode", 401);
                    response.Add("SubStatusCode", 0);
                    response.Add("StatusDescription", "Unauthorized");
                    return response;
                }


                if (HttpContext.Current.Request.Headers["AccessToken"]  != null)
                {
                    string accessToken = HttpContext.Current.Request.Headers["AccessToken"];
                    
                    ds = entitiHelperObj.ValidateAccessToken(accessToken, out statusCode, out subStatusCode, out statusDescription, out retVal, out retMsg);
                    if (retVal == 1)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            response.Add("Success", true);
                            response.Add("StatusCode", 200);
                            foreach (DataColumn column in ds.Tables[0].Rows[0].Table.Columns)
                            {
                                response.Add(column.ColumnName, ds.Tables[0].Rows[0][column.ColumnName]);
                            }
                        }
                        else
                        {
                            response.Clear();
                            response.Add("StatusCode", statusCode);
                            response.Add("SubStatusCode", subStatusCode);
                            response.Add("StatusDescription", statusDescription);
                            response.Add("Success", false);
                            response.Add("Message", "User Not Found");
                        }

                    }
                    else
                    {
                        response.Clear();
                        response.Add("StatusCode", statusCode);
                        response.Add("SubStatusCode", subStatusCode);
                        response.Add("StatusDescription", statusDescription);
                        response.Add("Success", false);
                        response.Add("Message", retMsg);
                    }

                }
                else
                {
                    response.Add("StatusCode", 200);
                }
            }
            catch (Exception ex)
            {
                response.Clear();
                response.Add("StatusCode", 500);
                response.Add("SubStatusCode", -1);
                response.Add("StatusDescription", ex.Message);
                response.Add("Success", false);
                response.Add("Message", "Server Error");
                Logger.ExceptionLog("Exception in BusinessHelper is " + ex.ToString());
            }

            return response;
        }

        public string UserOptInOutRegistraton(string sConnn, HttpContext parameterContext,string optinNumber,string optoutClip)
        {
            string fromNumber = "";
            string toNumber = "";
            bool isOpedIn = false;
            int retVal = 0;
            string retMsg = "";
            short isDnd = 0;
            int optinSent = 0;
            string responseString = "";
            JObject parametersObj;
            JObject optOutResObj = new JObject();
            try
            {
                if (parameterContext.Request.HttpMethod.ToString().ToUpper() == "GET")
                {
                    if (parameterContext.Request["smscresponse[from]"] != null)
                    {
                        fromNumber = parameterContext.Request["smscresponse[from]"];
                    }
                    if (parameterContext.Request["smscresponse[to]"] != null)
                    {
                        toNumber = parameterContext.Request["smscresponse[to]"];
                    }
                }
                else if (parameterContext.Request.HttpMethod.ToString().ToUpper() == "POST")
                {
                    string _JsonStr = "";
                    parameterContext.Request.InputStream.Position = 0;
                    _JsonStr = new System.IO.StreamReader(parameterContext.Request.InputStream).ReadToEnd();
                    parametersObj = JObject.Parse(_JsonStr);
                    parametersObj = JObject.Parse(parametersObj.SelectToken("smscresponse").ToString());
                    if (parametersObj.SelectToken("from") != null)
                    {
                        fromNumber = parametersObj.SelectToken("from").ToString();
                    }
                    if (parametersObj.SelectToken("to") != null)
                    {
                        toNumber = parametersObj.SelectToken("to").ToString();
                    }
                }
                else
                {
                    optOutResObj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", "Invalida Data"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception While Parsing the data : " + ex.ToString());
                optOutResObj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", "Invalida Data"));
            }
            try
            {
                EntityHelper helperEntObj = new EntityHelper(sConnn);
                helperEntObj.UserOptOutRegistrationEnt(fromNumber, toNumber, optinNumber, out retVal, out retMsg, out isDnd, out optinSent);
                if (retVal == 1)
                {
                    if (isOpedIn == true)
                    {
                        responseString = "<Response><Hangup/></Response>";
                        //GetHostDeviceTokens(FromNumber);
                    }
                    else
                    {
                        responseString = "<Response><PreAnswer><Play>" + optoutClip  + "</Play></PreAnswer><Hangup/></Response>";
                    }
                   // responseString = retMsg;
                }
                else
                {
                    optOutResObj = new JObject(new JProperty("Success", true),
                                       new JProperty("Message", retMsg));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception While Parsing the data : " + ex.ToString());
                responseString = "Something Went Wrong";
            }
            return responseString;
        }

        public string SendAppDownloadLink(string sConn, HttpContext parameterContext)
        {
            string number = "";
            int retVal = 0;
            string retMessage = "";
            JObject parametersObj = new JObject();
            JObject responseObj = new JObject();
            try
            {
                if (parameterContext.Request.HttpMethod.ToString().ToUpper() == "GET")
                {

                    if (parameterContext.Request["smscresponse[from]"] != null)
                    {

                        number = parameterContext.Request["smscresponse[from]"];

                    }
                    else
                    {
                        responseObj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", "Invalida Data"));
                        return responseObj.ToString();
                    }

                }
                else if (parameterContext.Request.HttpMethod.ToString().ToUpper() == "POST")
                {
                    string _JsonStr = "";
                    parameterContext.Request.InputStream.Position = 0;
                    _JsonStr = new System.IO.StreamReader(parameterContext.Request.InputStream).ReadToEnd();
                    parametersObj = JObject.Parse(_JsonStr);
                    parametersObj = JObject.Parse(parametersObj.SelectToken("smscresponse").ToString());
                    if (parametersObj.SelectToken("from") != null)
                    {
                        number = parametersObj.SelectToken("from").ToString();
                    }
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false),
                                        new JProperty("Message", "Invalida Data"));
                    return responseObj.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception In SendAppDownloadLink BLL : " + ex.ToString());
            }


            try
            {
                EntityHelper helperEntObj = new EntityHelper(sConn);
                helperEntObj.SendAppDownloadLink(number, out retVal, out retMessage);
                if (retVal == 1)
                {
                    responseObj = new JObject(new JProperty("Success", true),
                                        new JProperty("Message", retMessage));
                }
                else
                {
                    responseObj = new JObject(new JProperty("Success", false),
                                       new JProperty("Message", retMessage));
                }
            }
            catch (Exception ex)
            {
                responseObj = new JObject(new JProperty("Success", false),
                                       new JProperty("Message", "Server error"));
            }
            return responseObj.ToString();
        }
        public void NewProperty(string key, object value)
        {

            if (jObj == null)
            {
                jObj = null;
            }
            if (jObj.SelectToken(key) != null)
            {
                jObj.Remove(key);
            }
            jObj.Add(new JProperty(key, value));
        }

        public void GetOnlyNumeric(ref string input)
        {
            char[] numericArray = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            foreach (char _Char in input)
            {
                int pos = Array.IndexOf(numericArray, _Char);

                if (pos == -1)
                {
                    input = input.Replace(_Char.ToString(), "");
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
                input = input.Substring(1, input.Length - 1);

            }
        }
        public JObject GetResponse()
        {
            return jObj;
        }

        public DataTable GetConferenceMebers(DataTable members, int userId,string version, out int HostIsDND)
        {
            
            string tempName = "", tempMobile = "", HostMobile = "";
            bool DndFlag;
            int hostCountryId = 0,hostCountryCode =0;
            Boolean isCountryPreficExists = false;
            string currenctCountryPrefix = "", callerIDFinal = "", userDefaultCountryCode = "91";
            int currentCountryID = 0;
            DataTable callerIDs = new DataTable(), finalConferenceMembers = new DataTable(), countryMaster = new DataTable();
            DataAccessLayer.EntityHelper groupsObj = new DataAccessLayer.EntityHelper(connString);
            DataSet ds = new DataSet();

            try
            {
                ds = groupsObj.GetCountryDetails(userId);
                countryMaster = ds.Tables[0];
                callerIDs = ds.Tables[1];
                HostMobile = ds.Tables[2].Rows[0]["MobileNumber"].ToString();
                hostCountryId = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
                hostCountryCode = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryCode"]);
                userDefaultCountryCode = hostCountryCode.ToString();
                finalConferenceMembers = new DataTable();
                finalConferenceMembers.Columns.Add("Name", typeof(string));
                finalConferenceMembers.Columns.Add("Mobile", typeof(string));
                finalConferenceMembers.Columns.Add("CallerID", typeof(string));
                finalConferenceMembers.Columns.Add("CountryID", typeof(int));
                finalConferenceMembers.Columns.Add("IsInDND", typeof(Boolean));
                finalConferenceMembers.Columns.Add("IsOptedIn", typeof(Boolean));
                finalConferenceMembers.Columns.Add("IsDndCheck", typeof(int));

                DataTable conferenceMembersTemp = new DataTable();

                conferenceMembersTemp.Columns.Add("Name", typeof(string));
                conferenceMembersTemp.Columns.Add("Mobile", typeof(string));
                conferenceMembersTemp.Columns.Add("CallerID", typeof(string));
                conferenceMembersTemp.Columns.Add("CountryID", typeof(int));
                conferenceMembersTemp.Columns.Add("IsInDND", typeof(Boolean));
                conferenceMembersTemp.Columns.Add("IsOptedIn", typeof(Boolean));
                conferenceMembersTemp.Columns.Add("IsDndCheck", typeof(int));

                foreach (DataRow _member in members.Rows)
                {
                    tempName = _member["Name"].ToString();
                    tempMobile = _member["Mobile"].ToString();
                    if ( version == "v1.0.0" )
                    {
                        DndFlag = false;
                    }
                    else
                    {
                        DndFlag = Convert.ToBoolean(_member["IsDndCheck"]);
                    }

                    GetOnlyNumeric(ref tempMobile);
                    RemoveZeroPrefix(ref tempMobile);

                    isCountryPreficExists = false;
                    currenctCountryPrefix = userDefaultCountryCode;
                    foreach (DataRow _Country in countryMaster.Rows)
                    {
                        if (tempMobile.StartsWith(_Country["CountryCode"].ToString()) && tempMobile.Length == Int16.Parse(_Country["MaxLength"].ToString()))
                        {
                            isCountryPreficExists = true;
                            currenctCountryPrefix = _Country["CountryCode"].ToString();
                            currentCountryID = Int16.Parse(_Country["CountryID"].ToString());
                            break;
                        }
                    }
                    if (isCountryPreficExists == false)
                    {
                        tempMobile = userDefaultCountryCode + tempMobile;
                        currentCountryID = hostCountryId;
                    }

                    conferenceMembersTemp.Rows.Add(tempName, tempMobile, "", currentCountryID, false, false, DndFlag);
                }
                finalConferenceMembers = RemoveDuplicates(conferenceMembersTemp, "Mobile");
                foreach (DataRow _member in finalConferenceMembers.Rows)
                {
                    currentCountryID = int.Parse(_member["CountryID"].ToString());
                    DataRow[] dr = callerIDs.Select("CountryID =" + currentCountryID + " ");
                    if (dr.Length > 0)
                    {
                        callerIDFinal = callerIDs.Select("CountryID =" + currentCountryID + " ")[0]["CallerIDNumber"].ToString();
                    }
                    else
                    {
                        callerIDFinal = "";
                    }

                    _member["CallerID"] = callerIDFinal;
                }
                if (hostCountryId == 108)
                {
                    HostIsDND = CheckHostDND(HostMobile);
                    CheckDND(ref finalConferenceMembers, version);
                }
                else
                {
                    HostIsDND = 0;
                }
                                
                LogTable(finalConferenceMembers);
                return finalConferenceMembers;

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.GetConferenceMebers is ==>" + ex.ToString());
                throw ex;
            }
        }


        public DataTable GetConferenceMebersNew(DataTable members, int userId, string version, out int HostIsDND)
        {
            
            string tempName = "", tempMobile = "", HostMobile = "";
            bool DndFlag;
            int hostCountryId = 0, hostCountryCode = 0;
            Boolean isCountryPreficExists = false;
            string currenctCountryPrefix = "", callerIDFinal = "", userDefaultCountryCode = "91";
            int currentCountryID = 0;
            DataTable callerIDs = new DataTable(), finalConferenceMembers = new DataTable(), countryMaster = new DataTable();
            DataAccessLayer.EntityHelper groupsObj = new DataAccessLayer.EntityHelper(connString);
            DataSet ds = new DataSet();

            try
            {
                ds = groupsObj.GetCountryDetails(userId);
                countryMaster = ds.Tables[0];
                callerIDs = ds.Tables[1];
                HostMobile = ds.Tables[2].Rows[0]["MobileNumber"].ToString();
                hostCountryId = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryID"]);
                hostCountryCode = Convert.ToInt32(ds.Tables[2].Rows[0]["CountryCode"]);
                userDefaultCountryCode = hostCountryCode.ToString();
                finalConferenceMembers = new DataTable();
                finalConferenceMembers.Columns.Add("Name", typeof(string));
                finalConferenceMembers.Columns.Add("Mobile", typeof(string));
                finalConferenceMembers.Columns.Add("CallerID", typeof(string));
                finalConferenceMembers.Columns.Add("CountryID", typeof(int));
                finalConferenceMembers.Columns.Add("IsInDND", typeof(Boolean));
                finalConferenceMembers.Columns.Add("IsOptedIn", typeof(Boolean));
                finalConferenceMembers.Columns.Add("IsDndCheck", typeof(int));
                finalConferenceMembers.Columns.Add("ListID", typeof(int));

                DataTable conferenceMembersTemp = new DataTable();

                conferenceMembersTemp.Columns.Add("Name", typeof(string));
                conferenceMembersTemp.Columns.Add("Mobile", typeof(string));
                conferenceMembersTemp.Columns.Add("CallerID", typeof(string));
                conferenceMembersTemp.Columns.Add("CountryID", typeof(int));
                conferenceMembersTemp.Columns.Add("IsInDND", typeof(Boolean));
                conferenceMembersTemp.Columns.Add("IsOptedIn", typeof(Boolean));
                conferenceMembersTemp.Columns.Add("IsDndCheck", typeof(int));
                conferenceMembersTemp.Columns.Add("ListID", typeof(int));

                foreach (DataRow _member in members.Rows)
                {
                    
                    tempName = _member["Name"].ToString();
                    
                    tempMobile = _member["Mobile"].ToString();
                    if (version == "v1.0.0")
                    {
                        DndFlag = false;
                    }
                    else
                    {
                        DndFlag = Convert.ToBoolean(_member["IsDndCheck"]);
                    }

                    GetOnlyNumeric(ref tempMobile);
                    RemoveZeroPrefix(ref tempMobile);

                    isCountryPreficExists = false;
                    currenctCountryPrefix = userDefaultCountryCode;
                    foreach (DataRow _Country in countryMaster.Rows)
                    {
                        if (tempMobile.StartsWith(_Country["CountryCode"].ToString()) && tempMobile.Length == Int16.Parse(_Country["MaxLength"].ToString()))
                        {
                            isCountryPreficExists = true;
                            currenctCountryPrefix = _Country["CountryCode"].ToString();
                            currentCountryID = Int16.Parse(_Country["CountryID"].ToString());
                            break;
                        }
                    }
                    if (isCountryPreficExists == false)
                    {
                        tempMobile = userDefaultCountryCode + tempMobile;
                        currentCountryID = hostCountryId;
                    }

                    conferenceMembersTemp.Rows.Add(tempName, tempMobile, "", currentCountryID, false, false, DndFlag, Convert.ToInt32(_member["ListID"]));
                }
                finalConferenceMembers = RemoveDuplicates(conferenceMembersTemp, "Mobile");
                foreach (DataRow _member in finalConferenceMembers.Rows)
                {
                    currentCountryID = int.Parse(_member["CountryID"].ToString());
                    DataRow[] dr = callerIDs.Select("CountryID =" + currentCountryID + " ");
                    if (dr.Length > 0)
                    {
                        callerIDFinal = callerIDs.Select("CountryID =" + currentCountryID + " ")[0]["CallerIDNumber"].ToString();
                    }
                    else
                    {
                        callerIDFinal = "";
                    }

                    _member["CallerID"] = callerIDFinal;
                }
                if (hostCountryId == 108)
                {
                    HostIsDND = CheckHostDND(HostMobile);
                    CheckDND(ref finalConferenceMembers, version);
                }
                else
                {
                    HostIsDND = 0;
                }

                LogTable(finalConferenceMembers);
                return finalConferenceMembers;

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.GetConferenceMebers is ==>" + ex.ToString());
                throw ex;
            }
        }

        public DataTable RemoveDuplicates(DataTable inputTable, string columnName)
        {
            List<DataRow> duplicateRows = new List<DataRow>();
            List<Object> distinctColumnValues = new List<object>();
            try
            {
                foreach (DataRow _Row in inputTable.Rows)
                {
                    if (distinctColumnValues.Contains(_Row[columnName]))
                    {
                        duplicateRows.Add(_Row);
                    }
                    else
                    {
                        distinctColumnValues.Add(_Row[columnName]);
                    }
                }
                foreach (DataRow _dataRow in duplicateRows)
                {
                    inputTable.Rows.Remove(_dataRow);
                }
                return inputTable;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.RemoveDuplicates is ==>" + ex.ToString());
                return inputTable;
            }

        }

        public void CheckDND(ref DataTable inputTable,string version)
        {
            StringBuilder mobilesToCheck = new StringBuilder();
            DataTable mobileInputToDB = new DataTable();
            //string retMsg = "";
            //int retVal = 0;
            DataSet ds = new DataSet();

            try
            {
                mobileInputToDB.Columns.Add("Mobile", typeof(string));
                for (int i = 0; i < inputTable.Rows.Count; i++)
                {
                    mobilesToCheck.Append(inputTable.Rows[i]["Mobile"].ToString()+',');
                }
                
                if (mobilesToCheck.Length > 0)
                {
                    JObject dndCheckResponse = new JObject();

                    dndCheckResponse = DndCheckApiCall(mobilesToCheck.ToString());

                    if (dndCheckResponse.SelectToken("items") != null)
                    {
                        foreach (JObject _Mob in (JArray)dndCheckResponse.SelectToken("items"))
                        {
                            if (_Mob.SelectToken("Is_DND").ToString() == "1")
                            {
                                inputTable.Select("Mobile='" + _Mob.SelectToken("Mobile_Number").ToString() + "'")[0]["IsInDnd"] = true;
                            }
                            else
                            {
                                if (version != "v1.0.0")
                                {
                                    inputTable.Select("Mobile='" + _Mob.SelectToken("Mobile_Number").ToString() + "'")[0]["IsDndCheck"] = 1;
                                }
                            }
                        }
                        
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.CheckDND is ==>" + ex.ToString());
            }
        }

        public JObject DndCheckApiCall(string mobiles)
        {
            HttpWebRequest req = null;
            HttpWebResponse resp = null;
            StreamReader sReader = null;
            StreamWriter sWriter = null;
            JObject responseObj = new JObject();
            try
            {
                req = (HttpWebRequest)WebRequest.Create("http://www.smscountry.com/api360invite_dnd_check.aspx");
                req.Method = "POST";
                req.ContentType = "application/x-www-form-urlencoded";
                sWriter = new StreamWriter(req.GetRequestStream());
                sWriter.Write("Mobile_Number=" + mobiles);
                sWriter.Flush();
                sWriter.Close();
                resp = (HttpWebResponse)req.GetResponse();
                sReader = new StreamReader(resp.GetResponseStream());
                responseObj = JObject.Parse(sReader.ReadToEnd());
                return responseObj;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.DndCheckApiCall is ==>" + ex.ToString());
                return null;
            }
        }

        public void LogTable(DataTable table)
        {
            Logger.TraceLog("Logging Entire Table");
            foreach (DataRow _Row in table.Rows)
            {
                Logger.TraceLog("New Row");
                foreach (DataColumn _Column in table.Columns)
                {
                    Logger.TraceLog(_Column.ColumnName + ":" + _Row[_Column.ColumnName]);
                }
            }
        }
        public int CheckHostDND(string Hostmobile)
        {
            int HostDnd = 0;
            try
            {
                JObject _DndCheckResponse = new JObject();
                _DndCheckResponse = DndCheckApiCall(Hostmobile.ToString());

                if (_DndCheckResponse.SelectToken("items") != null)
                {
                    foreach (JObject _Mob in JArray.Parse(_DndCheckResponse.SelectToken("items").ToString()))
                    {
                        if (_Mob.SelectToken("Is_DND").ToString() == "1") { HostDnd = 1; }
                        else { HostDnd = 0; }
                    }

                }
            }


            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in BusinessHelper.CheckHostDND is ==>" + ex.ToString());
                return HostDnd;
            }

            return HostDnd;

        }

        

    }

}
