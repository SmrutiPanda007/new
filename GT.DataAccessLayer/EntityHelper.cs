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

namespace GT.DataAccessLayer
{
    public class EntityHelper:DataAccess
    {
        public EntityHelper(string sConnString)
            : base(sConnString)
        {

        }
        public DataSet GetCountryDetails(int userId)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            string query = "";

            try
            {
                query += "Select CountryID, CountryName, CountryCode, MinLength, MaxLength From CountryMaster with(nolock) Where IsActive = 1;";
                query += "Select a.CountryID, b.CallerIDnumber From Gateways a with(nolock), CallerIDs b with(nolock) Where a.GatewayID = b.GatewayID And b.IsActive = 1";
                query += "select RiskProfileStatus,MobileNumber,a.CountryID,b.CountryCode from UserMaster a With(nolock),CountryMaster b with(nolock) where a.CountryID=b.CountryID and UserId=" + userId.ToString() + " and a.IsActive=1";
                sqlCmd = new SqlCommand(query, sqlCon);
                da = new SqlDataAdapter();
                da.SelectCommand = sqlCmd;
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in groupMasterEntity.GetCountryDetails is ==>" + ex.StackTrace);
                throw ex;
            }
            return ds;
        }
        public void UserOptOutRegistrationEnt(string fromNumber ,string toNumber,string optinNumber, out int RetVal, out string RetMessage, out short isDnd, out int optInSent)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection SqlCon = Connection;
            try
            {
                var _with1 = sqlCmd;
                _with1 = new SqlCommand("UserOptinOutRegistration", SqlCon);
                _with1.CommandType = CommandType.StoredProcedure;
                _with1.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = fromNumber;
                _with1.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = toNumber;
                if (toNumber.Contains(optinNumber))
                {
                    _with1.Parameters.Add("@IsOptedIn", SqlDbType.Bit).Value = true;
                }
                else
                {
                    _with1.Parameters.Add("@IsOptedIn", SqlDbType.Bit).Value = false;
                }
                _with1.Parameters.Add("@IsDND", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@OptedInstructionsSent", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@RetMessage", SqlDbType.VarChar, 300).Direction = ParameterDirection.Output;
                SqlCon.Open();
                _with1.ExecuteNonQuery();
                SqlCon.Close();
                RetVal = Convert.ToInt32(_with1.Parameters["@RetVal"].Value);
                optInSent = Convert.ToInt32(_with1.Parameters["@OptedInstructionsSent"].Value);
                RetMessage = _with1.Parameters["@RetMessage"].Value.ToString();
                isDnd = Convert.ToInt16(_with1.Parameters["@IsDND"].Value);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                optInSent = 0;
                RetVal = 0;
                RetMessage = "Something Went Wrong";
                isDnd = 0;
            }

        }

        public void SendAppDownloadLink(string number, out int retVal, out string retMessage)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection SqlCon = Connection;
            try
            {
                var _with1 = sqlCmd;
                sqlCmd = new SqlCommand("SendAppDownloadLink", SqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Value = number;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 300).Direction = ParameterDirection.Output;
                SqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                SqlCon.Close();
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in SendAppDownloadLink DAL " + ex.ToString());
                retVal = 0;
                retMessage = "Something Went Wrong";
            }

        }

        public DataSet ValidateAccessToken(string accessToken, out int statusCode, out int subStatusCode, out string statusDescription, out int retVal, out string retMsg)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                sqlCmd = new SqlCommand("ValidateAccessToken", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@AccessToken", SqlDbType.VarChar, 100).Value = accessToken;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@StatusCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@SubStatusCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@StatusDescription", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;


                da.SelectCommand = sqlCmd;
                da.Fill(ds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                statusCode = Convert.ToInt16(sqlCmd.Parameters["@StatusCode"].Value);
                if (statusCode != 200)
                {
                    subStatusCode = Convert.ToInt16(sqlCmd.Parameters["@SubStatusCode"].Value);
                    statusDescription = sqlCmd.Parameters["@StatusDescription"].Value.ToString();
                }
                else
                {
                    subStatusCode = 0;
                    statusDescription = "";
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in EntityHelper is " + ex.ToString());
                throw ex;
            }
            return ds;
        }
    }
}
