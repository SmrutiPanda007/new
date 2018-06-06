using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GT.Utilities;
using System.Web;

namespace GT.DataAccessLayer.V_1_2
{
    public class Profile_V120 : DataAccess
    {
        public Profile_V120(string sConnString)
            : base(sConnString)
        {

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
        public DataSet UpdateProfile(int userID, string nickName, string emailID, string workNumber, string webSiteURL, string company, out int retVal, out string retMsg)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            try
            {
                sqlCmd = new SqlCommand("UpdateUserDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@nickname", SqlDbType.NVarChar, 500).Value = nickName;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 500).Value = emailID;
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
    }
}
