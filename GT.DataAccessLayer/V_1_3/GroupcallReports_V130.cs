using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using GT.Utilities;
using GT.Utilities.Properties;
using log4net;
using PusherServer;
using System.Web;

namespace GT.DataAccessLayer.V_1_3
{
    public class GroupcallReports_V130 : DataAccess
    {
        SqlCommand sqlCmd = default(SqlCommand);
        SqlConnection sqlCon = null;
        SqlDataAdapter da = null;
        DataSet ds = null;

        JObject jObj = new JObject();
        public GroupcallReports_V130(string sConnString)
            : base(sConnString)
        {

        }

        public DataSet UpdateCallBacksOfGroupCall(CallBackVariable reportsObj, string callBackHostAddress, out int isRecording, out int notify, out int confID, out string confName, out int isInProgress)
        {
            sqlCon = new SqlConnection();
            sqlCon = Connection;
            da = new SqlDataAdapter();
            ds = new DataSet();
            try
            {

                sqlCmd = new SqlCommand("UpdateConferenceCall_OBD", sqlCon);

                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.Add("@CallUUid", SqlDbType.VarChar, 100).Value = reportsObj.CallUUID;
                sqlCmd.Parameters.Add("@RequestUuid", SqlDbType.VarChar, 100).Value = reportsObj.RequestUUID;
                sqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 100).Value = reportsObj.From;
                sqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 100).Value = reportsObj.To;
                sqlCmd.Parameters.Add("@MemberId", SqlDbType.VarChar, 20).Value = reportsObj.grpCallMemberID;
                sqlCmd.Parameters.Add("@CallStatus", SqlDbType.VarChar, 100).Value = reportsObj.CallStatus;
                sqlCmd.Parameters.Add("@Event", SqlDbType.VarChar, 100).Value = reportsObj.Event;
                sqlCmd.Parameters.Add("@Digits", SqlDbType.VarChar, 100).Value = reportsObj.Digits;
                sqlCmd.Parameters.Add("@Direction", SqlDbType.VarChar, 100).Value = reportsObj.Direction;
                sqlCmd.Parameters.Add("@ConferenceName", SqlDbType.VarChar, 1000).Value = reportsObj.GrpCallName;
                sqlCmd.Parameters.Add("@ConferenceAction", SqlDbType.VarChar, 100).Value = reportsObj.GrpCallAction;
                sqlCmd.Parameters.Add("@ConferenceDigits", SqlDbType.VarChar, 100).Value = reportsObj.Digits;
                sqlCmd.Parameters.Add("@EndReason", SqlDbType.VarChar, 100).Value = reportsObj.EndReason;
                sqlCmd.Parameters.Add("@StartTime", SqlDbType.BigInt).Value = reportsObj.StartTime;
                sqlCmd.Parameters.Add("@EndTime", SqlDbType.BigInt).Value = reportsObj.EndTime;
                sqlCmd.Parameters.Add("@SeqNumber", SqlDbType.BigInt).Value = reportsObj.SeqNumber;
                sqlCmd.Parameters.Add("@NodeIp", SqlDbType.VarChar, 30).Value = callBackHostAddress;
                sqlCmd.Parameters.Add("@IsRecording", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@IsInprogress", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@notify", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@conf_id", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@confname", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                da.SelectCommand = sqlCmd;
                ds = new DataSet();
                da.Fill(ds);
                if (sqlCmd.Parameters["@IsInprogress"].Value != DBNull.Value)
                {
                    isInProgress = int.Parse(sqlCmd.Parameters["@IsInprogress"].Value.ToString());
                }
                else
                {
                    isInProgress = 0;
                }

                isRecording = Convert.ToInt32(sqlCmd.Parameters["@IsRecording"].Value);
                notify = Convert.ToInt32(sqlCmd.Parameters["@notify"].Value);
                confID = Convert.ToInt32(sqlCmd.Parameters["@conf_id"].Value);
                confName = sqlCmd.Parameters["@confname"].Value.ToString();
                int retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                string retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in UpdateConferenceCall_OBD DAL " + ex.ToString());
                throw ex;
            }

            return ds;
        }
        //public DataSet GetReportsByBatchIdNew(int userID, int grpCallID, string batchID, int pageIndex, int pageSize, out int pageCount, out int retVal, out string retMessage, out short IsCallFromBonus)
        //{
        //    SqlCommand sqlCmd = default(SqlCommand);
        //    SqlConnection sqlCon = null;
        //    SqlDataAdapter da = null;
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        sqlCon = new SqlConnection();
        //        sqlCon = Connection;
        //        sqlCmd = new SqlCommand("GT_GrptalkReports", sqlCon);
        //        sqlCmd.CommandType = CommandType.StoredProcedure;
        //        sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userID;
        //        sqlCmd.Parameters.Add("@ConfId", SqlDbType.Int).Value = grpCallID;
        //        sqlCmd.Parameters.Add("@PageIndex", SqlDbType.Int).Value = pageIndex;
        //        sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
        //        sqlCmd.Parameters.Add("@BatchId", SqlDbType.VarChar, 1000).Value = batchID;
        //        sqlCmd.Parameters.Add("@PageCount", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
        //        sqlCmd.Parameters.Add("@IsCallFromBonus", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
        //        sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
        //        sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
        //        da = new SqlDataAdapter(sqlCmd);
        //        HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        //        da.Fill(ds);
        //        HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
        //        retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
        //        retMessage = sqlCmd.Parameters["@RetVal"].Value.ToString();
        //        pageCount = Convert.ToInt16(sqlCmd.Parameters["@PageCount"].Value);
        //        IsCallFromBonus = Convert.ToInt16(sqlCmd.Parameters["@IsCallFromBonus"].Value.ToString());


        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.ExceptionLog("Exception in GroupCallReportsEntity is " + ex.ToString());
        //        throw ex;
        //    }

        //    return ds;
        //}

        public DataSet GetReportsByBatchIdNew(int userID, int grpCallID, string batchID, int pageIndex,int pageSize,string searchtext, out int pageCount, out int retVal, out string retMessage, out short isCallFromBonus,out int errorCode)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = new DataSet();
            try
            {

                sqlCon = new SqlConnection();
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GrptalkReportsOffset", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = userID;
                sqlCmd.Parameters.Add("@COnfId", SqlDbType.Int).Value = grpCallID;
                sqlCmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
                sqlCmd.Parameters.Add("@BatchId", SqlDbType.VarChar, 1000).Value = batchID;
                sqlCmd.Parameters.Add("@PageIndex", SqlDbType.TinyInt).Value = pageIndex;
                sqlCmd.Parameters.Add("@SearchText", SqlDbType.VarChar, 1000).Value = searchtext;
                sqlCmd.Parameters.Add("@PageCount", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@IsCallFromBonus", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 800).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                pageCount = Convert.ToInt16(sqlCmd.Parameters["@PageCount"].Value);
                //pageCount = 0;
                isCallFromBonus = Convert.ToInt16(sqlCmd.Parameters["@IsCallFromBonus"].Value.ToString());


            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in GroupCallReportsEntity is " + ex.ToString());
                throw ex;
            }

            return ds;
        }
    }
}
