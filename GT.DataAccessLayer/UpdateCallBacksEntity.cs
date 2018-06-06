using System;
using System.Collections;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using GT.DataAccessLayer;
using GT.Utilities.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using GT.Utilities;
using Microsoft.VisualBasic;


namespace GT.DataAccessLayer
{
    public class UpdateCallBacksEntity : DataAccess
    {
        SqlCommand sqlCmd = default(SqlCommand);
        SqlConnection sqlCon = null;
        SqlDataAdapter da = null;
        DataSet ds = null;
        public UpdateCallBacksEntity(string sConnectionString)
            : base(sConnectionString)
        {

        }
        public DataSet UpdateCallBacksOfGroupCall(CallBackVariable reportsObj, string callBackHostAddress, out int isRecording, out int notify, out int confID, out string confName, out int isInProgress)
        {
            sqlCon = new SqlConnection();
            sqlCon = Connection;
            sqlCmd = new SqlCommand("UpdateConferenceCall_OBD", sqlCon);
            da = new SqlDataAdapter();
            int retVal = 0;
            string retMessage = "";
            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandTimeout = 0;
                sqlCmd.Parameters.Add("@CallUUid", SqlDbType.VarChar, 100).Value = reportsObj.CallUUID;
                sqlCmd.Parameters.Add("@ConferenceUUID", SqlDbType.VarChar, 100).Value = reportsObj.ConferenceUUID;
                sqlCmd.Parameters.Add("@ConferenceSize", SqlDbType.Int).Value = reportsObj.ConferenceSize;
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

                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                
                if (retVal == 1)
                {
                    retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                    if (sqlCmd.Parameters["@IsInprogress"].Value != DBNull.Value)
                    {
                        isInProgress = int.Parse(sqlCmd.Parameters["@IsInprogress"].Value.ToString());
                    }
                    else
                    {
                        isInProgress = 0;
                    }

                    isRecording = 0;//Convert.ToInt32(sqlCmd.Parameters["@IsRecording"].Value);
                    notify = Convert.ToInt32(sqlCmd.Parameters["@notify"].Value);
                    confID = Convert.ToInt32(sqlCmd.Parameters["@conf_id"].Value);
                    confName = sqlCmd.Parameters["@confname"].Value.ToString();
                    
                }
                else
                {

                    isInProgress = 0;
                    isRecording = 0;
                    notify = 0;
                    confID = 0;
                    confName = "";

                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("exception at UpdateCallBacksOfGroupCall " + ex.ToString());
                isInProgress = 0;
                isRecording = 0;
                notify = 0;
                confID = 0;
                confName = "";
                throw ex;
            }
            return ds;
        }
    }
}
