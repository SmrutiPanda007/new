using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using GT.Utilities;


namespace GT.DataAccessLayer
{

    public class UsersInfoEntity:DataAccess 
    {
        public UsersInfoEntity(string sConnString)
            : base(sConnString)
        {

        }
        SqlCommand SqlCmd = default(SqlCommand);
        SqlConnection SqlCon = null;
        SqlDataAdapter Da = null;
        DataSet Ds = null;
        public DataSet GetUserProfileImage(int userid)
        {
        SqlCommand SqlCmd = default(SqlCommand);
        SqlConnection SqlCon = null;
        SqlDataAdapter Da = null;
        DataSet Ds = null;
        try
        {
          SqlCmd = new SqlCommand("Select user_image,case  when user_image=0x then 0 when user_image is null then 0 else 1 end as imgstat from users with(nolock) where id=@userid", SqlCon);
            SqlCmd.CommandType = CommandType.Text;
            SqlCmd.Parameters.Add("@userid", SqlDbType.VarChar).Value = userid;
            Da = new SqlDataAdapter();
            Da.SelectCommand = SqlCmd;
            Ds = new DataSet();
            Da.Fill(Ds);
             }
        catch (Exception ex)
        {
            Logger.ExceptionLog("Exception in UserInfoEntity is  " + ex.ToString());
            throw ex;
           
        }
        return Ds;

        }
  
       
    }
}
