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
using PusherServer;
using System.Web;

namespace GT.DataAccessLayer.V_1_5
{
    public class GroupCall : DataAccess
    {
        public GroupCall(string sConnString)
            : base(sConnString)
        {

        }

        /// <summary>
        /// This function is used to validate the Autodial request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="grpCallId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public string ValidateRequest(int userId, int grpCallId, string token)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            string resp = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("RequestValidate", sqlCon);
                var _with1 = sqlCmd;
                _with1.CommandType = CommandType.StoredProcedure;
                _with1.CommandTimeout = 0;
                _with1.Parameters.Add("@Conf_id", SqlDbType.Int).Value = grpCallId;
                _with1.Parameters.Add("@user_id", SqlDbType.Int).Value = userId;
                _with1.Parameters.Add("@token", SqlDbType.VarChar, 20).Value = token;
                _with1.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@RetMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCon.Open();
                _with1.ExecuteNonQuery();
                sqlCon.Close();
                if (Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value.ToString()) == 1)
                {
                    resp = "SUCCESS";
                }
                else
                {
                    resp = "FAIL";
                }

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(" ValidateRequest in GroupCallEntity : " + ex.ToString());
                resp = "Exception at Validating Grpcall";
                throw ex;
            }
            return resp;
        }
        /// <summary>
        /// This function is used to validate the groupcall, which means a groupcall with this id is exists and user is allowed to run this call etc..
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="grpCallId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public DataSet ValidateGrpCall(grpcall grpCallProp, out short retVal, out string retMsg, out string callUUID, out int errorCode, out long instanceId)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                Logger.TraceLog("Validate grpcall Dial DAL" + DateTime.Now.ToString());
                sqlCon = Connection;
                sqlCmd = new SqlCommand("ValidateConference", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@IsValidate", SqlDbType.TinyInt).Value = grpCallProp.IsValidate;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = grpCallProp.ConferenceId;
                sqlCmd.Parameters.Add("@ConferenceNumber", SqlDbType.VarChar).Value = grpCallProp.ConferenceNumber;
                sqlCmd.Parameters.Add("@UserIdTriggered", SqlDbType.Int).Value = grpCallProp.UserId;
                sqlCmd.Parameters.Add("@IsMute", SqlDbType.Bit).Value = grpCallProp.IsMute;
                sqlCmd.Parameters.Add("@TotalNumbers", SqlDbType.Int).Value = grpCallProp.TotalNumbers;
                sqlCmd.Parameters.Add("@IsCallFromBonus", SqlDbType.Bit).Value = grpCallProp.IsCallFromBonus;
                sqlCmd.Parameters.Add("@AccessKey", SqlDbType.VarChar, 10).Value = grpCallProp.ConferenceAccessKey;
                sqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@InstanceIdOut", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ErrorCode", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                Logger.TraceLog("Validate grpcall Dial before calling Sp" + DateTime.Now.ToString());
                da.Fill(ds);
                Logger.TraceLog("Validate grpcall Dial After callhing Sp" + DateTime.Now.ToString());
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                errorCode = Convert.ToInt32(sqlCmd.Parameters["@ErrorCode"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                callUUID = sqlCmd.Parameters["@CallUUID"].Value.ToString();
                instanceId = Convert.ToInt64(sqlCmd.Parameters["@InstanceIdOut"].Value);
            }
            catch (Exception ex)
            {
                retVal = 0;
                retMsg = "Exception at Validating GrpCall";
                errorCode = 101;
                callUUID = "";
                Logger.ExceptionLog("ValidateGrpCall in GrpCallEnt : " + ex.ToString());
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// This Is Used to Get The data from Db To make GrpCall Dial
        /// </summary>
        /// <param name="grpCallPropObj"></param>
        /// <param name="retVal"></param>
        /// <param name="retMsg"></param>
        /// <param name="sendDigits"></param>
        /// <returns></returns>
        public DataSet GrpCallDialEnt(grpcall grpCallPropObj, int isCallFromWeb, out short retVal, out string retMsg, out int durationLimit, out short isCallFromBonus, out int isPaidClient, out string sendDigitsString)
        {
            SqlConnection sqlConDial = null;
            SqlCommand sqlCmdDial = null;
            SqlDataAdapter sqlDa = null;
            DataSet sqlDs = null;
            isPaidClient = 0;
            try
            {
                sqlConDial = Connection;
                sqlCmdDial = new SqlCommand("ConferenceDialGpApp", sqlConDial);
                sqlCmdDial.CommandType = CommandType.StoredProcedure;
                sqlCmdDial.CommandTimeout = 0;
                sqlCmdDial.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = grpCallPropObj.ConferenceId;
                sqlCmdDial.Parameters.Add("@IsCallFromWeb", SqlDbType.Int).Value = isCallFromWeb;
                Logger.TraceLog("Mobile Number in Dataaccess for dial" + grpCallPropObj.MobileNumber + "grpCallPropObj.ConferenceId" + grpCallPropObj.ConferenceId + "grpCallPropObj.IsModerator" + grpCallPropObj.IsModerator);
                Logger.TraceLog("grpCallPropObj.IsMute" + grpCallPropObj.IsMute + "grpCallPropObj.IsAll" + grpCallPropObj.IsAll + " grpCallPropObj.UserId" + grpCallPropObj.UserId + "grpCallPropObj.ConferenceAction" + grpCallPropObj.ConferenceAction);
                sqlCmdDial.Parameters.Add("@MobileNumbers", SqlDbType.VarChar, -1).Value = grpCallPropObj.MobileNumber;
                sqlCmdDial.Parameters.Add("@IsModerator", SqlDbType.Bit).Value = grpCallPropObj.IsModerator;
                sqlCmdDial.Parameters.Add("@IsMute", SqlDbType.Bit).Value = grpCallPropObj.IsMute;
                sqlCmdDial.Parameters.Add("@IsAll", SqlDbType.Bit).Value = grpCallPropObj.IsAll;
                sqlCmdDial.Parameters.Add("@Direction", SqlDbType.VarChar, 10).Value = "OUTBOUND";
                sqlCmdDial.Parameters.Add("@ConferenceAction", SqlDbType.VarChar, 20).Value = grpCallPropObj.ConferenceAction;
                sqlCmdDial.Parameters.Add("@ConferenceRoom", SqlDbType.VarChar, 50).Value = grpCallPropObj.ConferenceRoom;
                sqlCmdDial.Parameters.Add("@CallUUID", SqlDbType.VarChar, 200).Value = grpCallPropObj.CallUUID;
                sqlCmdDial.Parameters.Add("@InstanceId", SqlDbType.BigInt).Value = grpCallPropObj.InstanceId;
                sqlCmdDial.Parameters.Add("@IsRetry", SqlDbType.Bit).Value = grpCallPropObj.IsRetry;
                sqlCmdDial.Parameters.Add("@UserId", SqlDbType.BigInt).Value = grpCallPropObj.UserId;
                sqlCmdDial.Parameters.Add("@IsAutodial", SqlDbType.TinyInt).Value = grpCallPropObj.IsAutodial;
                sqlCmdDial.Parameters.Add("@IsCallFromBonus", SqlDbType.Bit).Direction = ParameterDirection.InputOutput;
                sqlCmdDial.Parameters["@IsCallFromBonus"].Value = grpCallPropObj.IsCallFromBonus;
                sqlCmdDial.Parameters.Add("@DurationLimit", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmdDial.Parameters.Add("@IsinterConnect", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmdDial.Parameters.Add("@IsPaidClient", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmdDial.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmdDial.Parameters.Add("@SendDigitsSting", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmdDial.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlDa = new SqlDataAdapter(sqlCmdDial);
                sqlDs = new DataSet();
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                Logger.TraceLog("Dial grpcall before executing sp" + DateTime.Now.ToString());
                sqlDa.Fill(sqlDs);
                Logger.TraceLog("Dial grpcall after executing sp" + DateTime.Now.ToString());
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmdDial.Parameters["@RetVal"].Value);
                retMsg = sqlCmdDial.Parameters["@RetMessage"].Value.ToString();
                sendDigitsString = Convert.ToString(sqlCmdDial.Parameters["@SendDigitsSting"].Value);
                durationLimit = 0;
                isCallFromBonus = 0;
                Logger.TraceLog("Retmsg in dial sp" + retMsg);
                if (retVal == 1)
                {
                    if (sqlCmdDial.Parameters["@DurationLimit"].Value != DBNull.Value)
                        durationLimit = Convert.ToInt32(sqlCmdDial.Parameters["@DurationLimit"].Value);

                    isCallFromBonus = Convert.ToInt16(sqlCmdDial.Parameters["@IsCallFromBonus"].Value);
                    isPaidClient = Convert.ToInt16(sqlCmdDial.Parameters["@IsPaidClient"].Value);
                    Logger.TraceLog("Duration Limite" + durationLimit + " isCallFromBonus " + isCallFromBonus + " isPaidClient " + isPaidClient);
                }



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception at GrpCall Dialing : " + ex.ToString());
                retVal = 0;
                retMsg = "Some thing went wrong ";
                isCallFromBonus = 0;
                isPaidClient = 0;
                sqlDs = null;
                throw ex;
            }
            finally
            {
                if (sqlConDial.State == ConnectionState.Open)
                {
                    sqlConDial.Close();
                }
                sqlConDial = null;
                sqlCmdDial = null;
            }

            return sqlDs;
        }
        /// <summary>
        /// This function is used to get the data for grpcall retry
        /// </summary>
        /// <param name="callUUID"></param>
        /// <param name="requestUUId"></param>
        /// <param name="retVal"></param>
        /// <param name="retMsg"></param>
        /// <param name="grpCallID"></param>
        /// <param name="isModerator"></param>
        /// <param name="isMute"></param>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        public DataSet GrpCallRetry(string callUUID, string requestUUId, int callDrop, out short retVal, out string retMsg, out int grpCallID, out bool isModerator, out bool isMute, out string mobileNumber)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GetConferenceCallRetryData", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 200).Value = callUUID;
                sqlCmd.Parameters.Add("@RequestUUID", SqlDbType.VarChar, 100).Value = requestUUId;
                sqlCmd.Parameters.Add("@CallDrop", SqlDbType.Int).Value = callDrop;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@IsMute", SqlDbType.Bit).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@IsModerator", SqlDbType.Bit).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ConferenceID", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                da.Fill(ds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
                grpCallID = Convert.ToInt32(sqlCmd.Parameters["@ConferenceID"].Value.ToString());
                isModerator = Convert.ToBoolean(sqlCmd.Parameters["@IsModerator"].Value);
                isMute = Convert.ToBoolean(sqlCmd.Parameters["@IsMute"].Value);
                mobileNumber = sqlCmd.Parameters["@MobileNumber"].Value.ToString();
                
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                retVal = 0;
                isMute = false;
                isModerator = false;
                mobileNumber = "";
                grpCallID = 0;
                retMsg = "Something Went Wrong";
                throw ex;
            }
            finally
            {

            }
            return ds;
        }

        public void UpdateAutodialInfo(grpcall grpcall)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("update conferenceautodialinfo with(rowlock) set isstarted=1 where token=@token  and confid=@confid and userid=@userid", sqlCon);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.Add("@userid", SqlDbType.Int).Value = grpcall.UserId;
                sqlCmd.Parameters.Add("@confid", SqlDbType.Int).Value = grpcall.ConferenceId;
                sqlCmd.Parameters.Add("@token", SqlDbType.VarChar, 20).Value = grpcall.AutoDialTocken;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("conference autodial update conferenceautodialinfo" + ex.StackTrace);
                throw ex;
            }
        }
        /// <summary>
        /// This function is used to update info abt when remote server is not responding
        /// </summary>
        /// <param name="ReportIds"></param>
        /// <param name="EndReason"></param>
        public void UpdateSystemDownReports(DataTable ReportIds, string EndReason)
        {

            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("UpdateSystemDownReports", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ReportIds", SqlDbType.Structured).Value = ReportIds;
                sqlCmd.Parameters.Add("@EndReason", SqlDbType.VarChar, 100).Value = EndReason;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception At UpdateSystemDownReports() Method " + ex.ToString());
                throw ex;
            }
            finally
            {
                sqlCon = null;
                sqlCon = null;
            }
        }

        /// <summary>
        /// This function is used to update the dial status after gettin successfull response from api
        /// </summary>
        /// <param name="grpCallProp"></param>
        /// <param name="apiId"></param>
        /// <param name="reportIdRequestUUIDTable"></param>
        public void UpdateDialResponse(grpcall grpCallProp, String apiId, DataTable reportIdRequestUUIDTable)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("UpdateDialResponse", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = grpCallProp.ConferenceId;
                sqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 200).Value = grpCallProp.CallUUID;
                if (grpCallProp.GatewayID == 4)
                {
                    sqlCmd.Parameters.Add("@PlivoApiID", SqlDbType.VarChar, 100).Value = apiId;
                }
                sqlCmd.Parameters.Add("@RequestUUIDs", SqlDbType.Structured).Value = reportIdRequestUUIDTable;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception At UpdateSystemDownReports() Method " + ex.ToString());
                throw ex;
            }
            finally
            {
                sqlCon = null;
                sqlCon = null;
            }
        }

        /// <summary>
        /// This function is used to update call status to Hangup Which means hangup the calls
        /// </summary>
        /// <param name="grpCallProp"></param>
        /// <param name="retVal"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public DataSet Hangup(grpcall grpCallProp, out short retVal, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("ConferenceHangupGt", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = grpCallProp.ConferenceId;
                sqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 200).Value = grpCallProp.CallUUID;
                sqlCmd.Parameters.Add("@IsAll", SqlDbType.Bit).Value = grpCallProp.IsAll;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Value = grpCallProp.MobileNumber;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter();
                ds = new DataSet();
                da.SelectCommand = sqlCmd;

                da.Fill(ds);

                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                retVal = 0;
                retMsg = "Database level exception at Hanup the call";
                throw ex;
            }
            finally
            {

            }
            return ds;
        }
        /// <summary>
        /// This function is used to get the plivocalluuids from reports table 
        /// </summary>
        /// <param name="InPut"></param>
        /// <param name="_Type"></param>
        /// <param name="grpcall"></param>
        /// <returns></returns>
        public DataSet GetPlivoCallUUIDs(string InPut, short _Type, grpcall grpcall)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {

                if (_Type == 0)
                {
                    sqlCmd = new SqlCommand("Select PlivoCallUUID From reports with(nolock) Where requestuuid In (Select * From dbo.Split(@InPut, ',')) And PlivoCallUUID Is Not Null", sqlCon);
                }
                else
                {
                    sqlCmd = new SqlCommand("Select PlivoCallUUID From reports with(nolock) Where conf_id = " + grpcall.ConferenceId + " And member_id In (Select * From dbo.Split(@InPut, ',')) And PlivoCallUUID Is Not Null", sqlCon);
                }
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.Add("@InPut", SqlDbType.VarChar).Value = InPut;
                da = new SqlDataAdapter();
                da.SelectCommand = sqlCmd;
                ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Error Getting Plivo CallUUIDs, Reason : " + ex.ToString());
                throw ex;

            }
            return ds;
        }
        /// <summary>
        /// This function used to update call status to mute/unmute
        /// </summary>
        /// <param name="grpCallProp"></param>
        /// <param name="retVal"></param>
        /// <param name="retMsg"></param>
        /// <returns></returns>
        public DataSet MuteUnmute(grpcall grpCallProp, out int retVal, out string retMsg)
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = null;
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("ConferenceMuteUnmuteGt", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = grpCallProp.ConferenceId;
                sqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar).Value = grpCallProp.CallUUID;
                sqlCmd.Parameters.Add("@InstanceId", SqlDbType.BigInt).Value = grpCallProp.InstanceId;
                sqlCmd.Parameters.Add("@IsMute", SqlDbType.Bit).Value = grpCallProp.IsMute;
                sqlCmd.Parameters.Add("@IsAll", SqlDbType.Bit).Value = grpCallProp.IsAll;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, -1).Value = grpCallProp.MobileNumber;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter();
                ds = new DataSet();
                da.SelectCommand = sqlCmd;
                HttpContext.Current.Items.Add("DbStartTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                da.Fill(ds);
                HttpContext.Current.Items.Add("DbEndTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
                retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
                retMsg = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                retVal = 0;
                retMsg = "Something Went Wrong";
                throw ex;
            }
            return ds;
        }

        /// <summary>
        /// This function is used to get the device info
        /// </summary>
        /// <param name="pushObj"></param>
        /// <param name="retVal"></param>
        /// <param name="osID"></param>
        /// <param name="grpCallName"></param>
        /// <returns></returns>
        public DataSet MobileNotifier(PusherNotifier pushObj, out Int32 retVal, out string grpCallName, out string hostName)
        {
            SqlConnection sqlCon = null;
            SqlCommand sqlCmd = null;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            JObject pushJobject = new JObject();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("GetDeviceKey", sqlCon);
                var _with1 = sqlCmd;
                _with1.CommandType = CommandType.StoredProcedure;
                _with1.Parameters.Add("@mode", SqlDbType.Int).Value = 1;
                _with1.Parameters.Add("@confid", SqlDbType.Int).Value = pushObj.GrpCallID;
                _with1.Parameters.Add("@CName", SqlDbType.NVarChar, 100).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@HostName", SqlDbType.NVarChar, 20).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@retval", SqlDbType.Int).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value.ToString());
                grpCallName = sqlCmd.Parameters["@CName"].Value.ToString();
                hostName = sqlCmd.Parameters["@HostName"].Value.ToString();
            }
            catch (Exception ex)
            {
                retVal = 0;
                grpCallName = "";
                throw ex;
            }
            finally
            {
                sqlCon = null;
                sqlCmd = null;
            }
            return ds;
        }

        public DataSet PrivateRoom(grpcall grpCallObj, out int retVal, out string retMsg)
        {

            SqlConnection sqlCon = null;
            SqlCommand sqlCmd = null;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            JObject pushJobject = new JObject();
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("TRANFERCALL", sqlCon);
                var _with1 = sqlCmd;
                _with1.CommandType = CommandType.StoredProcedure;
                _with1.Parameters.Add("@ConferenceId", SqlDbType.Int).Value = grpCallObj.ConferenceId;
                _with1.Parameters.Add("@IsPrivate", SqlDbType.Bit).Value = grpCallObj.IsInPrivate;
                _with1.Parameters.Add("@IsAll", SqlDbType.Bit).Value = grpCallObj.IsAll;
                _with1.Parameters.Add("@InstanceId", SqlDbType.BigInt).Value = grpCallObj.InstanceId;
                _with1.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 20).Value = grpCallObj.MobileNumber;
                _with1.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                _with1.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value.ToString());
                retMsg = sqlCmd.Parameters["@RetVal"].Value.ToString();
                Logger.TraceLog("DAL Transfer call" + grpCallObj.ConferenceId + grpCallObj.IsInPrivate + grpCallObj.CallUUID + grpCallObj.MobileNumber + ds.Tables.Count);
            }
            catch (Exception ex)
            {
                retVal = 0;
                Logger.ExceptionLog("Exception in PrivateRoom DAL" + ex.ToString());
                throw;
            }
            finally
            {
                sqlCon = null;
                sqlCmd = null;
            }
            return ds;
        }

        public void SendEMailOnException(string fromAddress, int userId, string emailIds, string ccmailIds, string subject, string body, string replyTo)
        {

            SqlConnection sqlCon = null;
            SqlCommand sqlCmd = null;
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter();
            JObject pushJobject = new JObject();
            string retMsg = "";
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("Email_Save_Send", sqlCon);
                var _with1 = sqlCmd;
                _with1.CommandType = CommandType.StoredProcedure;
                _with1.Parameters.Add("@From_address", SqlDbType.VarChar, 100).Value = fromAddress;
                _with1.Parameters.Add("@UserId", SqlDbType.Int).Value = userId;
                _with1.Parameters.Add("@Email_ids", SqlDbType.VarChar, 150).Value = emailIds;
                _with1.Parameters.Add("@cc", SqlDbType.VarChar, 5000).Value = ccmailIds;
                _with1.Parameters.Add("@bcc", SqlDbType.VarChar, -1).Value = "";
                _with1.Parameters.Add("@subject", SqlDbType.VarChar, 200).Value = subject;
                _with1.Parameters.Add("@reply_to", SqlDbType.VarChar, 100).Value = replyTo;
                _with1.Parameters.Add("@template_content", SqlDbType.VarChar, -1).Value = body;
                _with1.Parameters.Add("@Lang_Type", SqlDbType.Int).Value = 0;
                _with1.Parameters.Add("@priority", SqlDbType.Int).Value = 1;
                _with1.Parameters.Add("@attach", SqlDbType.VarChar, 200).Value = DBNull.Value;
                _with1.Parameters.Add("@Category", SqlDbType.Int).Value = 1;
                _with1.Parameters.Add("@ret_val", SqlDbType.VarChar, 6000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                retMsg = sqlCmd.Parameters["@ret_val"].Value.ToString();

            }
            catch (Exception ex)
            {

                Logger.ExceptionLog("Exception in SendEMailOnException DAL" + ex.ToString());
                throw;
            }
            finally
            {
                sqlCon = null;
                sqlCmd = null;
            }

        }

    }
}
