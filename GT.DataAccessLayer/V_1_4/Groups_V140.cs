using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using log4net;
using PusherServer;
using GT.DataModel;
using GT.Utilities;
using GT.Utilities.Properties;
using System.Web;

namespace GT.DataAccessLayer.V_1_4
{
   public class Groups_V140:DataAccess
    {
         public Groups_V140(string sConnString)
            : base(sConnString)
        {

        }

        public DataSet GetWebListContacts(int userId,string listId, out Int16 retVal,out string retMessage,out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet contactsDS = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetAllContactsFromLists", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 2;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@WebListIds", SqlDbType.VarChar, 100).Value = listId;
                sqlCmd.Parameters.Add("@TimeStamp", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                //HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                contactsDS = new DataSet();
                da.Fill(contactsDS);

                //HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            return contactsDS;
        }
        public DataSet CallRecordSubscription(int userId, int subscribeStatus, out int retVal, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;

                sqlCmd = new SqlCommand("GT_CallSubscriptionStatus", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;

                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@SubscribeStatus", SqlDbType.Int).Value = subscribeStatus;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMsg", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;

                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("CallRecordSubscription----" + ex.StackTrace);
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMsg = sqlCmd.Parameters["@RetMsg"].Value.ToString();
            return ds;
        }
        public DataSet GroupCallHistory(int userID, int grpCallID, int pageIndex, int pageSize,out int pageCount,out Int16 retVal)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet historyDS = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetGroupCallHistory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@GrpCallId", SqlDbType.Int).Value = grpCallID;
                
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
                sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                sqlCmd.Parameters.Add("@isCallSubscribed", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                historyDS = new DataSet();
                da.Fill(historyDS);

                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (sqlCmd.Parameters["@PageCount"].Value == DBNull.Value)
                pageCount = Convert.ToInt32(sqlCmd.Parameters["@PageCount"].Value);
            else
                pageCount = 0;

            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            return historyDS;
        }
        public DataSet MemberLeaveFromGrpCall(int userId, int grpTalkID, out short leaveStatus, out string grpTalkName, out string deviceToken, out short osID, out string hostMobile, out string schTime, out short retVal, out string retMessage, out string leaveMemName, out int errorCode)
        {
            SqlCommand leaveCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                leaveCmd = new SqlCommand("MemberLeaveFromGroupCall", sqlCon);
                leaveCmd.CommandType = CommandType.StoredProcedure;
                leaveCmd.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                leaveCmd.Parameters.Add("@GrpTalkID", SqlDbType.Int).Value = grpTalkID;
                leaveCmd.Parameters.Add("@LeaveMemberName", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@GrpTalkName", SqlDbType.NVarChar, 50).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@HostMobile", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@SchTime", SqlDbType.VarChar, 50).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@GrpTalkStatus", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@DeviceToken", SqlDbType.VarChar, -1).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@OsID", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                leaveCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                sqlCon.Open();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(leaveCmd);
                ds = new DataSet();
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in MemberLeaveFromGrpCall---" + ex.ToString());
                throw ex;
            }
            if (Convert.ToInt32(leaveCmd.Parameters["@RetVal"].Value) == 1)
            {

                retVal = Convert.ToInt16(leaveCmd.Parameters["@RetVal"].Value);
                leaveStatus = Convert.ToInt16(leaveCmd.Parameters["@GrpTalkStatus"].Value);
                osID = Convert.ToInt16(leaveCmd.Parameters["@OsID"].Value);
                grpTalkName = leaveCmd.Parameters["@GrpTalkName"].Value.ToString();
                schTime = leaveCmd.Parameters["@schTime"].Value.ToString();
                deviceToken = leaveCmd.Parameters["@DeviceToken"].Value.ToString();
                hostMobile = leaveCmd.Parameters["@GrpTalkStatus"].Value.ToString();
                leaveMemName = leaveCmd.Parameters["@LeaveMemberName"].Value.ToString();
                retMessage = "Success";
            }
            else
            {
                retVal = 0;
                leaveStatus = 0;
                grpTalkName = "";
                hostMobile = "";
                deviceToken = "";
                schTime = "";
                osID = 0;
                leaveMemName = "";
                retMessage = leaveCmd.Parameters["@RetMessage"].Value.ToString();
            }
            errorCode = Convert.ToInt32(leaveCmd.Parameters["@ErrorCode"].Value.ToString());
            return ds;
        }
        /// <summary>
        /// This Function is Used to Create GroupCall Entity
        /// </summary>
        public DataSet CreateGrpCall(DataTable conferenceMembers, int userId, grpcreate grpObj, int HostIsDND, out short retVal, out string retMessage, out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                grpObj.grpCallTime = grpObj.grpCallTime.Replace("a.m.", "am");
                grpObj.grpCallTime = grpObj.grpCallTime.Replace("p.m.", "pm");
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_CreateGroupCall", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                sqlCmd.Parameters.Add("@ConferenceName", SqlDbType.NVarChar, 100).Value = grpObj.grpCallName;
                sqlCmd.Parameters.Add("@Conferencedate", SqlDbType.VarChar, 50).Value = grpObj.grpCallDate;
                sqlCmd.Parameters.Add("@ConferenceTime", SqlDbType.VarChar, 50).Value = grpObj.grpCallTime;
                sqlCmd.Parameters.Add("@ManagerMobile", SqlDbType.VarChar, 50).Value = grpObj.managerMobile;
                sqlCmd.Parameters.Add("@ConferenceMembers", SqlDbType.Structured).Value = conferenceMembers;
                sqlCmd.Parameters.Add("@WebListIds", SqlDbType.VarChar, 100).Value = grpObj.WebListIds;
                sqlCmd.Parameters.Add("@SchType", SqlDbType.TinyInt).Value = grpObj.SchType;
                sqlCmd.Parameters.Add("@HostIsDnd", SqlDbType.TinyInt).Value = HostIsDND;
                sqlCmd.Parameters.Add("@WeekDays", SqlDbType.VarChar, 300).Value = grpObj.DaysInWeek;
                sqlCmd.Parameters.Add("@IsMuteDial", SqlDbType.TinyInt).Value = grpObj.IsMuteDial;
                sqlCmd.Parameters.Add("@IsOnlyDialIn", SqlDbType.TinyInt).Value = grpObj.IsOnlyDialIn;
                sqlCmd.Parameters.Add("@IsAllowNonMembers", SqlDbType.TinyInt).Value = grpObj.IsAllowNonMembers;
                sqlCmd.Parameters.Add("@OpenLineBefore", SqlDbType.TinyInt).Value = grpObj.OpenLineBefore;
                sqlCmd.Parameters.Add("@Reminder", SqlDbType.Int).Value = grpObj.ReminderMins;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                ds = new DataSet();
                da.Fill(ds);



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in CreateGroupCallEntity---" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString(); errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            return ds;

        }


        public DataSet GetGrpCallDetails(Int64 grpCallId,Int64 userId,out int retVal,out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
             
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetConferenceDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                sqlCmd.Parameters.Add("@ConfId", SqlDbType.BigInt).Value = grpCallId;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMsg", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                da.Fill(ds);



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetGrpCallDetails DLL---" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            retMsg = sqlCmd.Parameters["@RetMsg"].Value.ToString(); 
            return ds;

        }

        /// <summary>
        /// This Function is Used to Edit GroupCall Entity
        /// </summary>

        public DataSet EditGroupCall(DataTable grpCallMembers, int userId, grpEdit grpObj, Int16 webOrApp, out short retVal, out string retMessage, out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                Logger.TraceLog("FROM APP" + webOrApp.ToString());
                grpObj.grpCallTime = grpObj.grpCallTime.Replace("a.m.", "am");
                grpObj.grpCallTime = grpObj.grpCallTime.Replace("p.m.", "pm");
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_EditGroupCall", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserId", SqlDbType.BigInt).Value = userId;
                sqlCmd.Parameters.Add("@IsFromApp", SqlDbType.TinyInt).Value = webOrApp;
                sqlCmd.Parameters.Add("@ConferenceID", SqlDbType.BigInt).Value = grpObj.grpcallID;
                sqlCmd.Parameters.Add("@ConferenceName", SqlDbType.NVarChar, 100).Value = grpObj.grpCallName;
                sqlCmd.Parameters.Add("@Conferencedate", SqlDbType.VarChar, 50).Value = grpObj.grpCallDate;
                sqlCmd.Parameters.Add("@ConferenceTime", SqlDbType.VarChar, 50).Value = grpObj.grpCallTime;
                sqlCmd.Parameters.Add("@ConferenceMembers", SqlDbType.Structured).Value = grpCallMembers;
                sqlCmd.Parameters.Add("@ManagerMobile", SqlDbType.VarChar, 30).Value = grpObj.managerMobile;
                sqlCmd.Parameters.Add("@WebListIds", SqlDbType.VarChar).Value = grpObj.WebListIds;
                sqlCmd.Parameters.Add("@SchType", SqlDbType.TinyInt).Value = grpObj.SchType;
                sqlCmd.Parameters.Add("@DaysRecurs", SqlDbType.Int).Value = grpObj.RecurDays;
                sqlCmd.Parameters.Add("@WeekDays", SqlDbType.VarChar, 300).Value = grpObj.DaysInWeek;
                // sqlCmd.Parameters.Add("@MOnthDay", SqlDbType.VarChar, 50).Value = grpObj.DayOfMonth;
                // sqlCmd.Parameters.Add("@MonthRecur", SqlDbType.Int).Value = grpObj.WeekOfMonth;
                sqlCmd.Parameters.Add("@IsOnlyDialIn", SqlDbType.TinyInt).Value = grpObj.IsOnlyDialIn;
                sqlCmd.Parameters.Add("@IsAllowNonMembers", SqlDbType.TinyInt).Value = grpObj.IsAllowNonMembers;
                sqlCmd.Parameters.Add("@OpenLineBefore", SqlDbType.TinyInt).Value = grpObj.OpenLineBefore;
                sqlCmd.Parameters.Add("@IsMuteDial", SqlDbType.TinyInt).Value = grpObj.IsMuteDial;
                sqlCmd.Parameters.Add("@Reminder", SqlDbType.Int).Value = grpObj.ReminderMins;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in EditGroupCallEntity---" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            return ds;




        }
        /// <summary>
        /// This Function is Used to Get All GroupCalls Entity
        /// </summary>

        public DataSet GetAllGroupCalls(int userId, int AppSource, string DeviceToken, string AppVersion, string TimeStamp, out string outTimeStamp, out int count, out double userBal, out string profileImagePath, out string userCurrentDate, out string userRegTimeStamp, out int retVal, out string retMessage,out string webListsTimeStamp,out int errorCode)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetAllGroupCalls", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 120;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@osid", SqlDbType.Int).Value = AppSource;
                sqlCmd.Parameters.Add("@devicetoken", SqlDbType.VarChar, 10000).Value = DeviceToken;
                sqlCmd.Parameters.Add("@appversion", SqlDbType.VarChar, 50).Value = AppVersion;
                sqlCmd.Parameters.Add("@timestamp", SqlDbType.VarChar, 50).Value = TimeStamp;
                sqlCmd.Parameters["@timestamp"].Direction = ParameterDirection.InputOutput;
                sqlCmd.Parameters.Add("@WebListsTimeStamp", SqlDbType.VarChar, 50).Value = TimeStamp;
                sqlCmd.Parameters["@WebListsTimeStamp"].Direction = ParameterDirection.InputOutput;
                sqlCmd.Parameters.Add("@totcount", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@userbal", SqlDbType.Float).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@profileimagepath", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@usercurrentdate", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@userregtimestamp", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("GetAllGroupCalls----" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            outTimeStamp = sqlCmd.Parameters["@timestamp"].Value.ToString();
            //count = Convert.ToInt32(sqlCmd.Parameters["@totcount"].Value);
            count = 0;
            userBal = Convert.ToDouble(sqlCmd.Parameters["@userbal"].Value);
            profileImagePath = sqlCmd.Parameters["@profileimagepath"].Value.ToString();
            userCurrentDate = sqlCmd.Parameters["@usercurrentdate"].Value.ToString();
            userRegTimeStamp = sqlCmd.Parameters["@userregtimestamp"].Value.ToString();
            webListsTimeStamp = sqlCmd.Parameters["@WebListsTimeStamp"].Value.ToString();
            return ds;

        }
        /// <summary>
        /// This Function is Used to Delete GroupCall Entity
        /// </summary>

        public string DeleteGroupCall(int groupCallID, out int retVal,out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("DeleteConference", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.Int).Value = groupCallID;
                sqlCmd.Parameters.Add("@type", SqlDbType.Int).Value = 0;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCmd.ExecuteNonQuery();
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in DeleteGroupCallEntity" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            return sqlCmd.Parameters["@RetMessage"].Value.ToString();
        }
        /// <summary>
        /// This Function is Used to Update GroupCall Name Entity
        /// </summary>

        public string UpdateGroupCallName(int userID, int groupCallID, string groupCallName, out int retVal)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("update_conferencename_app", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                var _with1 = sqlCmd;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@conf_id", SqlDbType.Int).Value = groupCallID;
                sqlCmd.Parameters.Add("@conferencename", SqlDbType.NVarChar, 50).Value = groupCallName;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCon.Open();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCmd.ExecuteNonQuery();
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in UpdateGroupCallNameEntity" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            return sqlCmd.Parameters["@RetMessage"].Value.ToString();

        }
        /// <summary>
        /// This Function is Used to GetGroupCall Details Entity
        /// </summary>

        public DataSet GetGroupCallDetailsByGroupCallID(int userID, int groupCallID, out int retVal, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("Getconferencedetailsbyconfid", sqlCon);
                var _with1 = sqlCmd;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@confid", SqlDbType.Int).Value = groupCallID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                ds = new DataSet();
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GetGroupCallDetailsByGroupCallID" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            return ds;
        }
        /// <summary>
        /// This Function is Used to Get ConferenceRoom Entity
        /// </summary>

        public DataSet GetConferenceRoomBackUp(int confID, int userID, out short retVal, out string retMsg, out string serverTime, out string startTime,out int errorCode)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                sqlCmd = new SqlCommand("Getconferenceroomdetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@confid", SqlDbType.Int).Value = confID;
                sqlCmd.Parameters.Add("@userid", SqlDbType.VarChar, 5000).Value = userID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@servertime", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@starttime", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                da.SelectCommand = sqlCmd;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                if (retVal == 1)
                {
                    serverTime = sqlCmd.Parameters["@servertime"].Value.ToString();
                    startTime = sqlCmd.Parameters["@starttime"].Value.ToString();
                }
                else
                {
                    serverTime = "";

                    startTime = "";
                }



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in groupMasterEntity.GetConferenceRoom is ==>" + ex.ToString());
                throw ex;
            }
            return ds;
        }

        public DataSet GetConferenceRoom(int confID, int userID,int type,int pageSize,int pageNumber,string searchText, out short retVal, out string retMsg, out string serverTime, out string startTime,out int totalMembersCount,out int errorCode)
        {
            SqlConnection sqlCon = Connection; //System.Configuration.ConfigurationManager.ConnectionStrings("smscconferenceconnectionstring"));
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                sqlCmd = new SqlCommand("GT_GetGroupCallRoom", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@confid", SqlDbType.Int).Value = confID;
                sqlCmd.Parameters.Add("@userid", SqlDbType.VarChar, 5000).Value = userID;
                sqlCmd.Parameters.Add("@Type", SqlDbType.Int).Value = type;
                sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageNumber;
                sqlCmd.Parameters.Add("@SearchText", SqlDbType.VarChar, 100).Value = searchText;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@servertime", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@starttime", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                da.SelectCommand = sqlCmd;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
                if (retVal == 1)
                {
                    serverTime = sqlCmd.Parameters["@servertime"].Value.ToString();
                    startTime = sqlCmd.Parameters["@starttime"].Value.ToString();
                    totalMembersCount = 0;
                }
                else
                {
                    serverTime = "";
                    startTime = "";
                    totalMembersCount = 0;
                }



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in groupMasterEntity.GetConferenceRoom is ==>" + ex.ToString());
                throw ex;
            }
            return ds;
        }

        public DataSet SaveContactsToConference(DataTable members, int userID, int confID,string webListIds, out int retVal, out string retMessage,out int errorCode)
        {
            SqlConnection sqlCon = Connection;
            SqlCommand sqlCmd = default(SqlCommand);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            try
            {
                sqlCmd = new SqlCommand("GT_AddParticipantInGroupCall2", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@conferenceId", SqlDbType.Int).Value = confID;
                sqlCmd.Parameters.Add("@WebListIds", SqlDbType.VarChar, 500).Value = webListIds;
                sqlCmd.Parameters.Add("@ConferenceMembers", SqlDbType.Structured).Value = members;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da.SelectCommand = sqlCmd;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in groupMasterEntity.SaveContactsToConference is ==>" + ex.ToString());
                throw ex;

            }
            return ds;
        }

        public DataSet InAppBuyCredits(int osID, string accessToken, string transactionID, string txnID, string paymentStatus, out int retVal, out double availableAmount, out string retMsg)
        {
            
            SqlCommand SqlCmd = default(SqlCommand);
            SqlConnection SqlCon = null;
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();


            try
            {
                Logger.TraceLog("Android InAppBuyCredits DAL STARTED");
                SqlCon = Connection;
                SqlCmd = new SqlCommand("AddBalnceThroughInAppPurchase", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@OsID", SqlDbType.Int).Value = osID;
                SqlCmd.Parameters.Add("@AccessToken", SqlDbType.VarChar, 100).Value = accessToken;
                SqlCmd.Parameters.Add("@TransactionID", SqlDbType.VarChar, 100).Value = transactionID;
                SqlCmd.Parameters.Add("@TxnID", SqlDbType.VarChar, 100).Value = txnID;
                SqlCmd.Parameters.Add("@PaymentStatus", SqlDbType.VarChar, 100).Value = paymentStatus;
                SqlCmd.Parameters.Add("@AvailableBalance", SqlDbType.Float).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(SqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                ds = new DataSet();
                da.Fill(ds);
                retVal = Convert.ToInt32(SqlCmd.Parameters["@RetVal"].Value);
                retMsg = SqlCmd.Parameters["@RetMessage"].Value.ToString();
                availableAmount = Convert.ToDouble(SqlCmd.Parameters["@AvailableBalance"].Value);
                Logger.TraceLog("retVal : " + retVal + " retMsg " + retMsg + " availableAmount ");

            }
            catch (Exception ex)
            {
                retVal = 0;
                retMsg = "Something went Wrong";
                Logger.ExceptionLog("Exception in GroupmasterEntity is " + ex.ToString());
                throw ex;
            }
            return ds;
        }

        public string GetAppversions(string accessToken, out string version, out string buildId)
        {
            SqlCommand SqlCmd = default(SqlCommand);
            SqlConnection SqlCon = null;
            try
            {
                SqlCon = Connection;
                SqlCmd = new SqlCommand("GetAppVersionBuildNumber", SqlCon);
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@AccessTOken", SqlDbType.VarChar, 100).Value = accessToken;
                SqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@AppVersion", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@BuildNumber", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                SqlCon.Open();
                SqlCmd.ExecuteNonQuery();
                SqlCon.Close();
                if (Convert.ToInt32(SqlCmd.Parameters["@RetVal"].Value) == 1)
                {
                    version = SqlCmd.Parameters["@AppVersion"].Value.ToString();
                    buildId = SqlCmd.Parameters["@BuildNumber"].Value.ToString();
                }
                else
                {
                    version = "";
                    buildId = "";
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception GroupmasterEntuty.GetAppVersions :" + ex.ToString());
                throw ex;
            }
            finally
            {
                if (SqlCon != null)
                {
                    SqlCon = null;
                }
                if (SqlCmd != null)
                {
                    SqlCmd = null;
                }
            }
            return "";
        }


        public string GrpCallCancel(int ConferenceID, out int retVal,out string LastCallDate,out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                
                sqlCon = Connection;
                sqlCmd = new SqlCommand("CancelGrpCall", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                var _with1 = sqlCmd;
                sqlCmd.Parameters.Add("@ConferenceID", SqlDbType.Int).Value = ConferenceID;
                sqlCmd.Parameters.Add("@LastCalldate", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@retVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@retMsg", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                sqlCon.Open();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCmd.ExecuteNonQuery();
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in GrpCallCancel" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@retVal"].Value);
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            LastCallDate = sqlCmd.Parameters["@LastCalldate"].Value.ToString();
            return sqlCmd.Parameters["@retMsg"].Value.ToString();

        }

        public DataSet CheckUserConfirmation(string Mobile, out int retVal, out string RetMessage, out  bool isConfirmed, out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            DataSet ds = new DataSet();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("CheckUserConfirmation", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();

                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 15).Value = Mobile;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.TinyInt).Value = 2;
                sqlCmd.Parameters.Add("@IsConfirmed", SqlDbType.Bit).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@OTP", SqlDbType.VarChar, 6).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in CheckUserConfirmation" + ex.ToString());
                throw ex;
            }
            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
            RetMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            isConfirmed = Convert.ToBoolean(sqlCmd.Parameters["@IsConfirmed"].Value);
            return ds;



        }
        public DataSet InAppPurchaseHistory(int userID)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet Ds = new DataSet();
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GetInAppPurchaseHistory", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = userID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                Ds = new DataSet();
                da.Fill(Ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in RechargeHistory :" + ex.ToString());
                throw ex;

            }


            return Ds;
        }
        public DataSet LiveCallDetails(int userID)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet Ds = new DataSet();
            try
            {
                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GT_GetLiveCallDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userid", SqlDbType.VarChar, 20).Value = userID;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                Ds = new DataSet();
                da.Fill(Ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception in RechargeHistory :" + ex.ToString());
                throw ex;

            }


            return Ds;
        }
    }
}
