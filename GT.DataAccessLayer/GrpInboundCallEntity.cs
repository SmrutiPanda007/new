using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Threading.Tasks;
using GT.Utilities;
using GT.Utilities.Properties;
namespace GT.DataAccessLayer
{
    public class GrpInboundCallEntity : DataAccess
    {

        public GrpInboundCallEntity(string sConnString)
            : base(sConnString)
        {

        }
        public int CheckIsInterConnectCallEntity(string fromNumber, string toNumber)
        {
            SqlConnection sqlCon = null;
            sqlCon = Connection;
            //"Select top 1 conf_room From reports with(nolock) Where from_number like '%" & Right(_FromNumber, 9) & "' and to_number like '%" & Right(ConferenceNumber, 10) & "' and isInterConnect=1 and call_status <> 'completed' order by id desc", SqlConInt)
            SqlCommand sqlCmd = new SqlCommand("select 1 from gateways with(nolock) where Right(OriginationCallerId,10)= Right(@FromNumber,10);select GatewayId  from gateways with(nolock) where Right(OriginationCallerId,10)= Right(@ToNumber,10)", sqlCon);
            SqlDataAdapter sqlAdapter = new SqlDataAdapter();
            SqlDataAdapter daCheck = new SqlDataAdapter();
            DataSet dsCheck = new DataSet();

            try
            {
                sqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = fromNumber;
                sqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = toNumber;
                daCheck.SelectCommand = sqlCmd;
                daCheck.Fill(dsCheck);
                if (dsCheck.Tables[0].Rows.Count > 0)
                {
                    //return dsCheck.Tables[0].Rows[0]["conf_room"].ToString();
                    return Convert.ToInt16(dsCheck.Tables[1].Rows[0]["GateWayId"]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
                return 0;
            }
        }
        public DataSet GetInterConnectCallssDataEntity(int slNo)
        {
            SqlConnection sqlCon = null;
            sqlCon = Connection;
            SqlCommand sqlCmd = new SqlCommand("Select * from InterConnectCallsData with(nolock) where  slno = @slNo", sqlCon);
            SqlDataAdapter da = new SqlDataAdapter();
            DataSet ds = new DataSet();
            sqlCmd.Parameters.Add("@slNo", SqlDbType.BigInt).Value = Convert.ToInt32(slNo);
            try
            {
                da.SelectCommand = sqlCmd;
                da.Fill(ds);
                return ds;
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString() + "---" + "GetInterConnectCallssDataEntity");
                return ds;
            }
        }
        public DataSet GetInterConnectMembersDataEntity(string callUuid, int conferenceId, bool isAll, string mobileNumber, int nodeSlNo, int gatewayId, out int timeLimit)
        {
            SqlConnection sqlCon = null;
            sqlCon = Connection;
            DataSet interConnectDs = new DataSet();
            SqlDataAdapter interConnectDa = new SqlDataAdapter();
            SqlCommand sqlCmd = new SqlCommand("InterConnectCallsDial", sqlCon);
            try
            {
                sqlCmd.CommandTimeout = 0;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@callUuid", SqlDbType.VarChar, 100).Value = callUuid;
                sqlCmd.Parameters.Add("@conferenceId", SqlDbType.BigInt).Value = conferenceId;
                sqlCmd.Parameters.Add("@nodeSlNo", SqlDbType.BigInt).Value = nodeSlNo;
                sqlCmd.Parameters.Add("@gateWayId", SqlDbType.BigInt).Value = gatewayId;
                sqlCmd.Parameters.Add("@isAll", SqlDbType.BigInt).Value = isAll;
                sqlCmd.Parameters.Add("@mobileNumber", SqlDbType.VarChar, 100).Value = callUuid;
                sqlCmd.Parameters.Add("@DurationLimit", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@ismute", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@retMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@retVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                interConnectDa = new SqlDataAdapter();
                interConnectDs = new DataSet();
                interConnectDa.SelectCommand = sqlCmd;
                interConnectDa.Fill(interConnectDs);
                timeLimit = Convert.ToInt32(sqlCmd.Parameters["@DurationLimit"].Value);
                if (Convert.ToBoolean(sqlCmd.Parameters["@retVal"].Value) == true)
                {
                    return interConnectDs;
                }
                else
                {
                    Logger.ExceptionLog(sqlCmd.Parameters["@retMessage"].ToString());
                }
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString() + "---" + "GetInterConnectMembersDataEntity");
                throw ex;
            }
            return interConnectDs;
        }
        public DataSet NewInboundCallEntity(string _event, string toNumber, string fromNumber, string callUuid, string callStatus, string digits, out string retMessage)
        {
            Logger.TraceLog("NewInboundCallEntity DLL");
            SqlConnection sqlCon = null;
            sqlCon = Connection;
            SqlCommand sqlCmd = new SqlCommand("GT_InboundCall", sqlCon);
            DataSet ds = null;
            SqlDataAdapter da = null;
            Logger.TraceLog("_event : " + _event + " toNumber :" + toNumber + "fromNumber : " + fromNumber + " callUuid :" + callUuid + " callStatus : " + callStatus + " digits :" + digits);
            try
            {
                var _with1 = sqlCmd;
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Event", SqlDbType.VarChar, 30).Value = _event;
                sqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = toNumber;
                sqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = fromNumber;
                sqlCmd.Parameters.Add("@Digits", SqlDbType.VarChar, 20).Value = digits;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Bit).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter();
                ds = new DataSet();
                da.SelectCommand = sqlCmd;
                da.Fill(ds);
                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString() + "---" + "NewInboundCallEntity");
                retMessage = ex.ToString();
            }
            return ds;
        }

        public DataSet InBoundCallBack(string conferenceUUID, int conferenceSize, string callUUID, string fromNumber, string toNumber, string eventName, string callStatus, string requestUUID, int grpCallMemberId, string action, string digits, Int64 startTime, Int64 endTime, string endReason)        
        {
            Logger.TraceLog("end reason Dal " + endReason);
            SqlConnection SqlCon = new SqlConnection();
            SqlCommand SqlCmd = new SqlCommand();
            SqlDataAdapter Da = new SqlDataAdapter();
            DataSet Ds = new DataSet();
            try
            {
                SqlCon = Connection;
                SqlCmd = new SqlCommand("InboundConferenceCallback", SqlCon);
                var _with1 = SqlCmd;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 100).Value = callUUID;
                SqlCmd.Parameters.Add("@ConferenceUUID", SqlDbType.VarChar, 100).Value = conferenceUUID;
                SqlCmd.Parameters.Add("@ConferenceSize", SqlDbType.Int).Value = conferenceSize;
                SqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = fromNumber;
                SqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = toNumber;
                SqlCmd.Parameters.Add("@Event", SqlDbType.VarChar, 20).Value = eventName;
                SqlCmd.Parameters.Add("@CallStatus", SqlDbType.VarChar, 100).Value = callStatus;
                SqlCmd.Parameters.Add("@RequestUUID", SqlDbType.VarChar, 100).Value = requestUUID;
                SqlCmd.Parameters.Add("@MemberId", SqlDbType.Int).Value = grpCallMemberId;
                SqlCmd.Parameters.Add("@ConferenceAction", SqlDbType.VarChar, 20).Value = action;
                SqlCmd.Parameters.Add("@Digits", SqlDbType.VarChar, 20).Value = digits;
                SqlCmd.Parameters.Add("@StartTime", SqlDbType.BigInt).Value = Convert.ToInt64(startTime);
                SqlCmd.Parameters.Add("@EndTime", SqlDbType.BigInt).Value = Convert.ToInt64(endTime);
                SqlCmd.Parameters.Add("@EndReason", SqlDbType.VarChar, 30).Value = endReason;
                SqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@notify", SqlDbType.Int).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@conf_id", SqlDbType.BigInt).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@confname", SqlDbType.VarChar, 200).Direction = ParameterDirection.Output;


                Da = new SqlDataAdapter();
                Da.SelectCommand = SqlCmd;
                Ds = new DataSet();
                Da.Fill(Ds);


            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception In InBoundCallBack : " + ex.ToString());
                throw ex;
            }
            return Ds;
        }

        public DataSet ConnectInBoundToGroupCall(Int64 conferenceId, string fromNumber, string toNumber, string callUUID,int mode, out Int16 retVal, out string retMsg,out long durationLimit)
        {
            DataSet ds = new DataSet();
            SqlConnection SqlCon = new SqlConnection();
            SqlCommand SqlCmd = new SqlCommand();
            SqlDataAdapter Da = new SqlDataAdapter();
            DataSet Ds = new DataSet();
            try
            {
                SqlCon = Connection;
                SqlCmd = new SqlCommand("GT_INSERTInboundCallDetails", SqlCon);
                var _with1 = SqlCmd;
                SqlCmd.CommandType = CommandType.StoredProcedure;
                SqlCmd.Parameters.Add("@Mode", SqlDbType.BigInt).Value = mode;
                SqlCmd.Parameters.Add("@ConferenceId", SqlDbType.BigInt).Value = conferenceId;
                SqlCmd.Parameters.Add("@FromNumber", SqlDbType.VarChar, 20).Value = fromNumber;
                SqlCmd.Parameters.Add("@ToNumber", SqlDbType.VarChar, 20).Value = toNumber;
                SqlCmd.Parameters.Add("@CallUUID", SqlDbType.VarChar, 50).Value = callUUID;
                SqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                SqlCmd.Parameters.Add("@DurationLimitOut", SqlDbType.BigInt).Direction = ParameterDirection.Output;
              
                Da = new SqlDataAdapter();
                Da.SelectCommand = SqlCmd;
                Ds = new DataSet();
                Da.Fill(Ds);
                retMsg = SqlCmd.Parameters["@RetMessage"].Value.ToString();
                retVal = Convert.ToInt16(SqlCmd.Parameters["@RetVal"].Value.ToString());
                if (SqlCmd.Parameters["@DurationLimitOut"].Value == DBNull.Value)
                    durationLimit = 3;
                else
                    durationLimit = Convert.ToInt64(SqlCmd.Parameters["@DurationLimitOut"].Value.ToString());

            }
            catch (Exception ex)
            {
                Logger.TraceLog("Exception In ConnectInBoundToConference : " + ex.ToString());
                throw ex;
            }
            return Ds;
        }
    }
}
