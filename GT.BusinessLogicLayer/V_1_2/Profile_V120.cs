using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using GT.Utilities;
using GT.DataAccessLayer;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace GT.BusinessLogicLayer.V_1_2
{
    public class Profile_V120
    {
        public JObject UserBalance(string sConnString, int userId)
        {
            JObject jObj = new JObject();
            string retMsg = "";
            int retVal = 0;
            double balance;
            DataSet ds = new DataSet();
            try
            {
                DataAccessLayer.V_1_2.Profile_V120 profileObj = new DataAccessLayer.V_1_2.Profile_V120(sConnString);
                ds = profileObj.UserBalance(userId, out retMsg, out retVal, out balance);
                if (retVal == 1)
                {
                    jObj = new JObject(new JProperty("Success", true),
                                     new JProperty("Message", retMsg),
                                     new JProperty("UserBalance", balance));


                }
                else
                {

                    jObj = new JObject(new JProperty("Success", false),
                                     new JProperty("Message", retMsg));
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ProfileBusiness.UserBalance is ==>" + ex.ToString());
                jObj = new JObject(new JProperty("Success", false),
                     new JProperty("Message", "Something Went Wrong"));
                //new JProperty("Message", ex.ToString()));

            }

            return jObj;
        }


        public JObject ProfileImage(string profileImg, string tempStoragePath, string tempFileName, string sConnString, int userId)
        {
            JObject jObj = new JObject();
            Image _image = null;
            MemoryStream mStream = new MemoryStream();
            byte[] byteArr, data;
            DataAccessLayer.V_1_2.Profile_V120 profileObj = new DataAccessLayer.V_1_2.Profile_V120(sConnString);
            int result;
            try
            {
                byteArr = Convert.FromBase64String(profileImg.Replace(" ", "+"));
                mStream = new MemoryStream(byteArr);
                _image = Image.FromStream(mStream);
                _image.Save(tempStoragePath + tempFileName);

                data = ReadFile(tempStoragePath + tempFileName);
                result = profileObj.ProfileImage(userId, tempFileName, data);
                if (result > 0)
                {

                    jObj = new JObject(new JProperty("Status", true),
                                     new JProperty("Message", "Success"),
                                     new JProperty("imagepath", "https://new.grptalk.com/TempImages/" + tempFileName.ToString() + ""));
                }
                else
                {

                    jObj = new JObject(new JProperty("Status", false),
                                     new JProperty("Message", "Failed"));
                }
            }
            catch (Exception ex)
            {
                jObj = new JObject(new JProperty("Status", false),
                     new JProperty("Message", "Something Went Wrong"));
                //new JProperty("Message", ex.ToString()));
                Logger.ExceptionLog("Exception in ProfileBusiness.ProfileImage is ==>" + ex.ToString());
            }

            return jObj;

        }
        public JObject UpdateProfile(string sConnString, int userID, string nickName, string emailID, string workNumber, string webSiteURL, string company)
        {
            JObject resultJobj = new JObject();
            JObject tempObj = new JObject();
            int retVal = 0;
            string retMsg = "";
            DataSet ds = new DataSet();
            try
            {
                DataAccessLayer.V_1_2.Profile_V120 profileObj = new DataAccessLayer.V_1_2.Profile_V120(sConnString);
                ds = profileObj.UpdateProfile(userID, nickName, emailID, workNumber, webSiteURL, company, out retVal, out retMsg);
                if (retVal == 1)
                {

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        resultJobj = new JObject(new JProperty("Success", true),
                                           new JProperty("Message", "Success"),
                                           new JProperty("userid", ds.Tables[0].Rows[0]["UserID"]),
                                            new JProperty("Nickname", ds.Tables[0].Rows[0]["Nickname"]),
                                            new JProperty("EmailID", ds.Tables[0].Rows[0]["EmailID"]),
                                            new JProperty("WebsiteURL", ds.Tables[0].Rows[0]["WebsiteURL"]),
                                            new JProperty("WorkNumber", ds.Tables[0].Rows[0]["WorkNumber"]),
                                            new JProperty("Company", ds.Tables[0].Rows[0]["Company"]),
                                            new JProperty("imagepath", ds.Tables[0].Rows[0]["DisplayPicPath"]),
                                            new JProperty("Currency", ds.Tables[0].Rows[0]["CurrencyName"]),
                                            new JProperty("RequestedAmount", ds.Tables[0].Rows[0]["PaidAmount"]),
                                            new JProperty("RequestedMinutes", ds.Tables[0].Rows[0]["AddedAmount"]),
                                            new JProperty("InAppPurchase", true),
                                            new JProperty("SupportEmailID", "hello@grptalk.com"));

                    }


                }
                else
                {
                    resultJobj = new JObject(new JProperty("Success", false),
                                             new JProperty("Message", "User Not Available"));
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Error :" + ex.ToString());
                resultJobj = new JObject(new JProperty("Success", false),
                    new JProperty("Message", "Something Went Wrong"),
                    //new JProperty("Message", ex.ToString()),
                                         new JProperty("ErrorCode", "E0002"));
            }
            return resultJobj;
        }
        private byte[] ReadFile(string filePath)
        {
            byte[] data = null;

            FileInfo fileInformation = new FileInfo(filePath);
            long numOfBytes = fileInformation.Length;
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            BinaryReader _binaryReader = new BinaryReader(fStream);
            data = _binaryReader.ReadBytes(int.Parse(numOfBytes.ToString()));
            return data;
        }

        public JObject GetCountries(string sConnString)
        {
            JObject countriesJObj = new JObject();
            JObject TempJobj = new JObject();
            JArray CountriesJarr = new JArray();
            DataSet Ds = new DataSet();
            DataAccessLayer.V_1_2.Profile_V120 profileObj = new DataAccessLayer.V_1_2.Profile_V120(sConnString);
            String retMsg = "";
            Ds = profileObj.GetCountries(out retMsg);

            if (Ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow _row in Ds.Tables[0].Rows)
                {
                    TempJobj = new JObject();
                    foreach (DataColumn _column in Ds.Tables[0].Columns)
                    {
                        TempJobj.Add(new JProperty(_column.ColumnName, _row[_column.ColumnName]));
                    }
                    CountriesJarr.Add(TempJobj);
                }
                countriesJObj = new JObject(new JProperty("Success", true),
                        new JProperty("Message", retMsg),
                        new JProperty("Countries", CountriesJarr));
            }
            return countriesJObj;
        }
    }
}
