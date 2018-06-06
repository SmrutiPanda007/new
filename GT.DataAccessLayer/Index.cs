using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using GT.Utilities;

namespace GT.DataAccessLayer
{
    public class Index : DataAccess
    {
        SqlConnection sqlCon = new SqlConnection();
        SqlCommand sqlCmd = default(SqlCommand);
        public Index(string sConnString)
            : base(sConnString)
        {

        }
        public int SendAppDownloadLink(int countryId, string MobileNumber, string email, int leadType, out string retMessage)
        {
            try
            {
                sqlCon = Connection;
                sqlCmd = new SqlCommand("SendAppDownloadLinkLatest", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@CountryId", SqlDbType.Int, 100).Value = countryId;
                sqlCmd.Parameters.Add("@MobileNumber", SqlDbType.VarChar, 100).Value = MobileNumber;
                sqlCmd.Parameters.Add("@Email", SqlDbType.VarChar, 100).Value = email;
                sqlCmd.Parameters.Add("@LeadType", SqlDbType.Int).Value = leadType;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();

                retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();
            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception SendDownload Link DAL :" + ex.ToString());
                throw ex;
            }
            return Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
        }

        public DataSet Get_Country_Name_By_Ip(string Ip_Address, out int retVal, out string retMessage)
        {
            string[] IP_Addr_Array;
            UInt64 IP_Range_Value;
            IP_Addr_Array = Ip_Address.Split('.');

            IP_Range_Value = (Convert.ToUInt64(IP_Addr_Array[0]) * 256 * 256 * 256) + (Convert.ToUInt64(IP_Addr_Array[1]) * 256 * 256) + (Convert.ToUInt64(IP_Addr_Array[2]) * 256) + Convert.ToUInt64(IP_Addr_Array[3]);
            SqlCommand sqlCmd = default(SqlCommand);
            sqlCon = Connection; ;
            SqlDataAdapter da = null;
            DataSet ds = null;
            string Country = "";

            sqlCmd = new SqlCommand("GT_GetCountryNameByIp", sqlCon);
            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Ip_Range", SqlDbType.BigInt).Value = IP_Range_Value;
                //sqlCmd.Parameters.Add("@country", SqlDbType.VarChar, 10).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                ds = new DataSet();
                da.Fill(ds);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            retVal = Convert.ToInt16(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = Convert.ToString(sqlCmd.Parameters["@RetMessage"].Value);
            return ds;
        }

    }
}
