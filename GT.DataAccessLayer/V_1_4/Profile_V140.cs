using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GT.Utilities;
using System.Web;

namespace GT.DataAccessLayer.V_1_4
{

    public class Profile_V140 : DataAccess
    {
        public Profile_V140(string sConnString)
            : base(sConnString)
        {

        }

        public DataSet GetDahsboardProfile(int userId, int mode, out int retVal, out string RetMessage)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetSetProfileDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();

                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in CheckUserConfirmation" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            RetMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;

        }


        public DataSet GetRechargeDetails(int userId, out int retVal, out string RetMessage)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetRechargeDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 180;
                SqlDataAdapter da = new SqlDataAdapter();
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMsg", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetRechargeDetailsBackend" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            RetMessage = sqlCmd.Parameters["@RetMsg"].Value.ToString();
            return ds;

        }

        public DataSet SetProfileDetailsUpdate(int userId, int mode, out int retVal, out string retmsg, HttpContext con, byte[] data, string path)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetSetProfileDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Nickname", SqlDbType.NVarChar, 50).Value = con.Request["nickname"];
                sqlCmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 50).Value = con.Request["email_id"];
                sqlCmd.Parameters.Add("@DispayPicPath", SqlDbType.VarChar, 300).Value = path;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in ProfileUpdateBackend" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retmsg = Convert.ToString(sqlCmd.Parameters["@RetMessage"].Value);
            return ds;

        }

        public DataSet SetProfileNameEmailUpdate(int userId, int mode, out int retVal, out string retmsg, HttpContext con)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                Logger.TraceLog("SetProfileNameEmailUpdate" + con.Request["nickname"]);
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetSetProfileDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Nickname", SqlDbType.NVarChar, 500).Value = con.Request["nickname"];
                sqlCmd.Parameters.Add("@EmailId", SqlDbType.NVarChar, 500).Value = con.Request["email_id"];
                sqlCmd.Parameters.Add("@DispayPicPath", SqlDbType.VarChar, 300).Value = "";
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in ProfileUpdateBackend" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retmsg = Convert.ToString(sqlCmd.Parameters["@RetMessage"].Value);
            return ds;

        }
        public int PhoneContactsSync(DataTable phoneContacts, DataTable secondaryContacts, DataTable editContacts, DataTable editSecondaryContacts, string deleteContacts, string deviceUniqueID, string deviceName, int userID, int isFirstTime, out string retMsg)
        {
            SqlConnection sqlCon = Connection;
            SqlCommand sqlCmd = default(SqlCommand);
            try
            {
                sqlCmd = new SqlCommand("GT_UploadPhoneContacts", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@IsFirstTime", SqlDbType.Int).Value = isFirstTime;
                sqlCmd.Parameters.Add("@PhoneContacts", SqlDbType.Structured).Value = phoneContacts;
                sqlCmd.Parameters.Add("@SecondaryContacts", SqlDbType.Structured).Value = secondaryContacts;
                sqlCmd.Parameters.Add("@EditContacts", SqlDbType.Structured).Value = editContacts;
                sqlCmd.Parameters.Add("@EditSecondaryContacts", SqlDbType.Structured).Value = editSecondaryContacts;
                sqlCmd.Parameters.Add("@DeleteContacts", SqlDbType.VarChar, -1).Value = deleteContacts;
                sqlCmd.Parameters.Add("@DeviceUniqueID", SqlDbType.VarChar, 200).Value = deviceUniqueID;
                sqlCmd.Parameters.Add("@DeviceName", SqlDbType.VarChar, -1).Value = deviceName;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Profile entity is " + ex.ToString());
                throw ex;
            }
            return Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
        }
        /// <summary>
        /// Function for getting balance information
        /// </summary>
        /// <returns></returns>
        public DataSet UserBalance(int userId, out string retMsg, out int retVal, out double balance)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            try
            {
                sqlCmd = new SqlCommand("UserBalance", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@UserBal", SqlDbType.Float).Direction = ParameterDirection.Output;

                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                if (retVal == 1)
                {
                    balance = Convert.ToDouble(sqlCmd.Parameters["@UserBal"].Value);
                }
                else
                {
                    balance = 0.0;
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in Profile entity is " + ex.ToString());
                throw ex;
            }
            return ds;

        }

        /// <summary>
        /// Function for updating user profile
        /// </summary>
        /// <returns></returns>
        public DataSet UpdateProfile(int userID, string nickName, string emailID, string offSet, string workNumber, string webSiteURL, string company, out int retVal, out string retMsg, out int errorCode)
        {
            

            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                
                Logger.TraceLog("UpdateProfile" + nickName);
                sqlCmd = new SqlCommand("UpdateUserDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@nickname", SqlDbType.NVarChar, 500).Value = nickName;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 500).Value = emailID;
                sqlCmd.Parameters.Add("@Offset", SqlDbType.VarChar, 20).Value = offSet;
                sqlCmd.Parameters.Add("@WorkNumber", SqlDbType.VarChar, 100).Value = workNumber;
                sqlCmd.Parameters.Add("@WebsiteUrl", SqlDbType.VarChar, 100).Value = webSiteURL;
                sqlCmd.Parameters.Add("@company", SqlDbType.VarChar, 100).Value = company;
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
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ProfileEntity is " + ex.ToString());
                throw ex;
            }
            return ds;

        }

        /// <summary>
        /// Function to get user image
        /// </summary>
        /// <returns></returns>
        public int ProfileImage(int userId, string tempFileName, byte[] data)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            int result = 0;
            try
            {
                sqlCmd = new SqlCommand("user_profile", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@user_image", SqlDbType.VarBinary, -1).Value = data;
                sqlCmd.Parameters.Add("@profile_pic", SqlDbType.VarChar, 200).Value = "TempImages/" + tempFileName.ToString() + "";
                sqlCmd.Parameters.Add("@mode", SqlDbType.Int).Value = 4;
                sqlCon.Open();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                result = sqlCmd.ExecuteNonQuery();
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in ProfileEntity is " + ex.ToString());
                throw ex;
            }
            return result;
        }
        public DataSet GetCountries(out string retMsg)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                sqlCmd = new SqlCommand("GetAvailableCountryList", sqlCon);
                var _with1 = sqlCmd;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                throw ex;
            }
            retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }
        public int SetUserUtcOffSetTime(int userId, int mode, string offSetValue, out string RetMessage)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            int retVal = 0;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetSetProfileDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@OffsetValue", SqlDbType.VarChar, 20).Value = offSetValue;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in SetUserUtcOffSetTime" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            RetMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return retVal;

        }
    }
}
