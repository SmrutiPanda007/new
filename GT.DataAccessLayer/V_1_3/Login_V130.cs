﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GTDataTypes;
using GT.Utilities;
using GT.Utilities.Properties;
using System.Web;

namespace GT.DataAccessLayer.V_1_3
{
    public class Login_V130 : DataAccess
    {
        public Login_V130(string sConnString)
            : base(sConnString)
        {

        }
        public DataSet OtpCall(string mobileNumber, out int retVal, out string retMsg, out int isConfirmed, out string Otp)
        {
            SqlConnection sCon = new SqlConnection();
            SqlCommand sCmd = new SqlCommand();
            SqlDataAdapter RegDa = null;
            DataSet RegDs = null;
            try
            {
                sCon = Connection;
                sCmd = new SqlCommand("CheckUserConfirmation", sCon);
                sCmd.CommandType = CommandType.StoredProcedure;
                sCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 15).Value = mobileNumber;
                sCmd.Parameters.Add("@Mode", SqlDbType.TinyInt).Value = 1;
                sCmd.Parameters.Add("@TxnID", SqlDbType.VarChar, 100).Value = HttpContext.Current.Request.Headers["TxnID"].ToString();
                sCmd.Parameters.Add("@IsConfirmed", SqlDbType.Bit).Direction = ParameterDirection.Output;
                sCmd.Parameters.Add("@OTP", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                sCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                RegDa = new SqlDataAdapter();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                RegDa.SelectCommand = sCmd;
                RegDs = new DataSet();
                RegDa.Fill(RegDs);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in LoginEntity is " + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sCmd.Parameters["@RetVal"].Value);
            retMsg = sCmd.Parameters["@RetMessage"].Value.ToString();
            isConfirmed = Convert.ToInt32(sCmd.Parameters["@IsConfirmed"].Value);
            Otp = sCmd.Parameters["@OTP"].Value.ToString();
            return RegDs;

        }

        /// <summary>
        /// Function for validating OTP
        /// </summary>
        /// <returns></returns>
        public DataSet ValidateOTP(string mobile, string OTP, string txnID, int isRegisteredFromWeb, out short retVal, out string retMsg, out int errorCode)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = new DataSet();
            try
            {

                sqlCmd = new SqlCommand("Registration", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.TinyInt).Value = 2;
                sqlCmd.Parameters.Add("@IsRegisteredFromWeb", SqlDbType.TinyInt).Value = isRegisteredFromWeb;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Value = mobile;
                sqlCmd.Parameters.Add("@Otp", SqlDbType.VarChar, 10).Value = OTP;
                sqlCmd.Parameters.Add("@TxnID", SqlDbType.VarChar, 100).Value = txnID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in LoginEntity is " + ex.ToString());
                throw ex;
            }


            return ds;
        }

        /// <summary>
        /// Function for registration 
        /// </summary>
        /// <returns></returns>
        public DataSet Registration(RegistrationDetails details, out short isExisted, out short retVal, out string retMsg, out int errorCode)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = new DataSet();
            try
            {

                sqlCmd = new SqlCommand("Registration", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.TinyInt).Value = 1;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Value = details.Mobile;
                sqlCmd.Parameters.Add("@DeviceUniqueUID", SqlDbType.VarChar, 500).Value = details.DeviceUniqueID;
                sqlCmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar, 500).Value = details.DeviceToken;
                sqlCmd.Parameters.Add("@OsID", SqlDbType.Int).Value = details.OsID;

                sqlCmd.Parameters.Add("@RegisteredIpAddress", SqlDbType.VarChar, 100).Value = details.ClientIpAddress;
                sqlCmd.Parameters.Add("@TxnID", SqlDbType.VarChar, 100).Value = details.TxnID;
                if (details.IsResend != "")
                {
                    sqlCmd.Parameters.Add("@IsResendOtp", SqlDbType.VarChar, 30).Value = details.IsResend;
                }
                sqlCmd.Parameters.Add("@IsExisted", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
                isExisted = Convert.ToInt16(sqlCmd.Parameters["@IsExisted"].Value);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in LoginEntity is " + ex.StackTrace);
                throw ex;
            }
            return ds;
        }

        public DataSet QRCodeChecking(int userID, string qrCode, string deviceUniqueId, string deviceToken, Int32 osID,out int retVal,out string retMsg)
        {
            
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = new DataSet();
            try
            {

                sqlCmd = new SqlCommand("ValidateQrCodeLogin", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@QrCode", SqlDbType.VarChar, 100).Value = qrCode;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.Add("@DeviceUniqueId", SqlDbType.VarChar, 100).Value = deviceUniqueId;
                sqlCmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar, 100).Value = deviceToken;
                sqlCmd.Parameters.Add("@OsId", SqlDbType.TinyInt).Value = osID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds);

                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                Logger.TraceLog(retVal.ToString());
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ValidateQrCode DAL is " + ex.ToString());
                throw ex;
            }


            return ds;
        }

        public DataSet ValidateQrCode(string qrCodeAccessToken, string qrCode, string deviceUniqueId, string deviceToken, string loginIp, string location, string browserName, string osName, Int16 osID, out short retVal, out string retMsg)
        {
            Logger.TraceLog("QRCODE DAL" + qrCodeAccessToken);
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataSet ds = new DataSet();
            try
            {

                sqlCmd = new SqlCommand("ValidateQrCodeLogin", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@QrCode", SqlDbType.VarChar, 100).Value = qrCode;
                sqlCmd.Parameters.Add("@QrCodeAccessTokenReq", SqlDbType.VarChar,50).Value = qrCodeAccessToken;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 2;                
                sqlCmd.Parameters.Add("@DeviceUniqueId", SqlDbType.VarChar, 100).Value = deviceUniqueId;
                sqlCmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar, 100).Value = deviceToken;
                sqlCmd.Parameters.Add("@LoginIp", SqlDbType.VarChar, 50).Value = loginIp;
                sqlCmd.Parameters.Add("@Location", SqlDbType.VarChar, 100).Value = location;
                sqlCmd.Parameters.Add("@OsName", SqlDbType.VarChar, 50).Value = osName;
                sqlCmd.Parameters.Add("@LoginBrowser", SqlDbType.VarChar, 50).Value = browserName;
                sqlCmd.Parameters.Add("@OsId", SqlDbType.TinyInt).Value = osID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;

                da = new SqlDataAdapter(sqlCmd);

                da.Fill(ds);

                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                Logger.TraceLog(retVal.ToString());
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ValidateQrCode DAL is " + ex.ToString());
                throw ex;
            }


            return ds;
        }

        public DataTable WebLoginCheck(int userId, out int retVal, out string retMessage)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                sqlCmd = new SqlCommand("GT_ValidateQrCodeLogin", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 2;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in WebLoginCheck DAL is " + ex.ToString());
                throw ex;
            }
            return dt;
        }

        public int QrCodeLogOut(int userId, out string retMessage)
        {
            int retVal = 0;
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = default(SqlDataAdapter);
            DataTable dt = new DataTable();

            try
            {
                sqlCmd = new SqlCommand("GT_ValidateQrCodeLogin", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in QrCodeLogOut DAL is " + ex.ToString());
                throw ex;
            }
            return retVal;

        }
    }


}
