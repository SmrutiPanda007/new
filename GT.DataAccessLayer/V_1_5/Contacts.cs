using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using GT.DataModel;
using GT.Utilities;
using GT.Utilities.Properties;

namespace GT.DataAccessLayer.V_1_5
{
    public class Contacts : DataAccess
    {
        public Contacts(string sConnectionString)
            : base(sConnectionString)
        { }
        public int PhoneCallHistorySync(int userID, string phoneNumbers, out string retMessage, out int errorCode)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            int retVal = 0;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_PhoneCallHistorySync", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@PhoneNumbers", SqlDbType.VarChar, -1).Value = phoneNumbers;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            return retVal;

        }
        public DataSet GetAllContactsList(int userID, out int retVal, out string retMessage, out string timeStamp)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet listsds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetAllContactsLists", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@TimeStamp", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                listsds = new DataSet();
                da.Fill(listsds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            timeStamp = sqlCmd.Parameters["@TimeStamp"].Value.ToString();
            return listsds;

        }

        public DataSet GetMobileContactDetails(int userId, int source, int mode, int pageIndex, out int retVal, out string retMessage, out int pageCount, out int defaultLines)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_NewGetContacts", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 180;
                SqlDataAdapter da = new SqlDataAdapter();
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Source", SqlDbType.Int).Value = source;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@DefaultLines", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetMobileContactDetails DAL" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            pageCount = Convert.ToInt32(sqlCmd.Parameters["@PageCount"].Value);
            if (sqlCmd.Parameters["@DefaultLines"].Value != DBNull.Value)
            {
                defaultLines = Convert.ToInt32(sqlCmd.Parameters["@DefaultLines"].Value);
            }
            else
            {
                defaultLines = 0;
            }
            return ds;

        }

        public DataSet GetMobileContactDetailsSort(int userId, int source, int mode, string alphabet, int pageIndex, int listId, out int retVal, out string retMessage, out int pageCount, out int contactsCount)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_NewGetContacts", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 180;
                SqlDataAdapter da = new SqlDataAdapter();
                sqlCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Source", SqlDbType.Int).Value = source;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@Aplhabet", SqlDbType.VarChar, -1).Value = alphabet;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@AllContactsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetMobileContactDetailsSort DAL" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            pageCount = Convert.ToInt32(sqlCmd.Parameters["@PageCount"].Value);
            contactsCount = Convert.ToInt32(sqlCmd.Parameters["@AllContactsCount"].Value);
            return ds;

        }

        public DataSet DeleteWebContact(int userID, int listId, int contactID, int mode, out int returnValue, out string returnMessage)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebContactsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = contactID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@ListId", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in DeleteWebContact DAl :" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }

        public DataSet WebContactsList(int userID, int listId, int pageIndex, out int returnValue, out string returnMessage, out int pageCount, out int allContactsCount)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_NewGetContacts", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@Source", SqlDbType.Int).Value = 2;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@AllContactsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in WebContactsList DAL :" + ex.ToString());
                throw ex;

            }

            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            allContactsCount = Convert.ToInt32(sqlCmd.Parameters["@AllContactsCount"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            pageCount = Convert.ToInt32(sqlCmd.Parameters["@PageCount"].Value);
            allContactsCount = Convert.ToInt32(sqlCmd.Parameters["@AllContactsCount"].Value);

            return ds;
        }

        public DataSet ManageWebContacts(int userID, string name, string mobileNumber, string prefix, int contactID, int mode, int listID, string listName, out int returnValue, out string returnMessage, string path, out int contactId)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                if (path != "")
                {
                    path = "ContactImages/" + path;
                }
                else
                {
                    path = "";
                }
                Logger.ExceptionLog("DLL IMAGE PATH " + path);
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebContactsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Name", SqlDbType.NVarChar, 200).Value = name;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 30).Value = mobileNumber;
                sqlCmd.Parameters.Add("@Prefix", SqlDbType.VarChar, 20).Value = prefix;
                sqlCmd.Parameters.Add("@ImgPath", SqlDbType.VarChar, 1000).Value = path;
                sqlCmd.Parameters.Add("@ContactID", SqlDbType.Int).Value = contactID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listID;
                sqlCmd.Parameters.Add("@ListName", SqlDbType.NVarChar, 50).Value = listName;
                sqlCmd.Parameters.Add("@ListContactsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in ManageWebContacts DAL :" + ex.ToString());
                throw ex;
            }
            if (mode != 2)
            {
                contactId = Convert.ToInt32(sqlCmd.Parameters["@ListContactsCount"].Value);
            }
            else { contactId = 0; }

            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }

        public DataSet UploadExcelData(int userID, DataTable dt, DataTable excelDuplicates, out int returnValue, out string returnMessage, int listId, string listName, int mode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_UploadExcelData", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@ContactsData", SqlDbType.Structured).Value = dt;
                sqlCmd.Parameters.Add("@ExcelDuplicates", SqlDbType.Structured).Value = excelDuplicates;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@ListName", SqlDbType.NVarChar, 30).Value = listName;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in UploadExcelData DAL :" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }

        public int AddAllContactsToParticularList(int userId, int listId, string contactId, int mode, out string returnMessage, out int listContactsCount)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            int returnValue = 0;
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebContactsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@ListId", SqlDbType.VarChar, 20).Value = listId;
                sqlCmd.Parameters.Add("@ContactIds", SqlDbType.VarChar, 2000).Value = contactId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                // sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ListContactsCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in AddAllContactsToParticularList DAL :" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            listContactsCount = Convert.ToInt32(sqlCmd.Parameters["@ListContactsCount"].Value);
            return returnValue;
        }
        public int AddWebContactList(int userID, string listname, int mode, out string returnMessage, out int listId)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebListsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@ListName", SqlDbType.NVarChar, 50).Value = listname;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ListIdOut", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in AddWebContactList DAL :" + ex.ToString());
                throw ex;

            }

            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            listId = Convert.ToInt32(sqlCmd.Parameters["@ListIdOut"].Value);
            return Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
        }

        public int EditWebContactList(int userID, int listId, string listname, int mode, out string returnMessage)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebListsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@ListName", SqlDbType.VarChar, 50).Value = listname;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in EditWebContactList :" + ex.ToString());
                throw ex;

            }

            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
        }
        public DataSet WebContactsList(int userID, int mode, int listId, int source, int pageIndex, string alphabetSort, string searchValue, out int returnValue, out string returnMessage, out int pageCount)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetContactsForWeb", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@ListID", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@Source", SqlDbType.Int).Value = source;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@Aplhabet", SqlDbType.VarChar, 10).Value = alphabetSort;
                sqlCmd.Parameters.Add("@SearchValue", SqlDbType.VarChar, 100).Value = searchValue;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in WebContactsList :" + ex.ToString());
                throw ex;

            }

            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            pageCount = Convert.ToInt32(sqlCmd.Parameters["@PageCount"].Value);
            return ds;
        }
        public int DeleteListIdNListIdContacts(int userId, int listId, int mode, out string returnMessage)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            int returnValue = 0;
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_WebListsManagement", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@ListId", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = mode;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in DeleteListIdNListIdContacts :" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            returnMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return returnValue;
        }

        public int SaveContactToGroup(int userId, long conferenceId, string mobileNumber, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            int returnValue = 0;
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_AddContactToGroup", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = conferenceId;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 15).Value = mobileNumber;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in SaveContactToGroup DLL:" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return returnValue;
        }

        public DataSet AddNonMember(int userId, int listId, string mobile, int mode, out int returnValue, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter sqlda;
            DataSet ds = new DataSet();

            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("AddNonMember", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@ListId", SqlDbType.Int).Value = listId;
                sqlCmd.Parameters.Add("@Mobile", SqlDbType.VarChar, 15).Value = mobile;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int, 15).Value = mode;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlda = new SqlDataAdapter(sqlCmd);
                sqlda.Fill(ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in SaveContactToGroup DLL:" + ex.ToString());
                throw ex;
            }
            returnValue = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }
    }
}
