using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GT.Utilities;
using System.Data;
using System.Data.SqlClient;


namespace GT.BusinessLogicLayer
{
    public class Index
    {
        public JObject SendAppDownloadLink(string sConnString, int countryId, string mobileNumber, string email, int leadType)
        {
            string retMessage = "";
            JObject responseJobj = new JObject();
            try
            {
                GT.DataAccessLayer.Index obj = new GT.DataAccessLayer.Index(sConnString);
                int retVal = obj.SendAppDownloadLink(countryId, mobileNumber, email, leadType, out retMessage);
                if (retVal == 1)
                {
                    responseJobj = new JObject(new JProperty("Success", 1),
                                    new JProperty("Message", "Success"));

                }
                else
                {
                    responseJobj = new JObject(new JProperty("Success", 0),
                                    new JProperty("Message", "Failed"));
                }

            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception In Index BLL :" + ex.ToString());
                responseJobj = new JObject(new JProperty("Success", 0),
                                    new JProperty("Message", "Failed"));
            }
            return responseJobj;

        }

        public JObject Get_Country_Name_By_Ip(string sConnString, string Ip_Address)
        {
            string retMessage = "";
            int retVal;
            JObject responseJobj = new JObject();
            try
            {
                GT.DataAccessLayer.Index obj = new GT.DataAccessLayer.Index(sConnString);
                DataSet ds = obj.Get_Country_Name_By_Ip(Ip_Address, out retVal, out retMessage);
                if (retVal == 1)
                {
                    foreach (DataColumn dsColumn in ds.Tables[0].Columns)
                    {
                        responseJobj.Add(new JProperty(dsColumn.ColumnName, ds.Tables[0].Rows[0][dsColumn.ColumnName]));
                        // responseJobj.Add(new JProperty(dsColumn.));
                    }
                    //responseJobj = Add(new JObject(new JProperty("Success", 1),
                    //                new JProperty("Message", "Success")));

                }
                else
                {
                    //responseJobj = new JObject(new JProperty("Success", 0),
                    //                new JProperty("Message", "Failed"));
                }

            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception In Index BLL :" + ex.ToString());
                responseJobj = new JObject(new JProperty("Success", 0),
                                    new JProperty("Message", "Failed"));
            }
            return responseJobj;


        }
    }
}
