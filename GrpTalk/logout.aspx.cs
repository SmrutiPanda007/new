using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using GT.Utilities;
using GT.Utilities.Properties;
using GrpTalk.CommonClasses;
using PusherServer;

namespace GrpTalk
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                UpdateQrCodeLogout(Convert.ToInt32(Session["UserID"]),0);
                Session.Abandon();

                if (HttpContext.Current.Request.Cookies["SessionId"] != null || HttpContext.Current.Request.Cookies["UserData"] != null)
                {
                    HttpCookie authCookie = new HttpCookie("SessionId", "");
                    authCookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(authCookie);
                    HttpCookie userData = new HttpCookie("UserData", "");
                    userData.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(userData);

                }

            }
            Response.Redirect("/Home.aspx?msg=Session expired");

        }

        public void UpdateQrCodeLogout(int userId,short sessionEnd)
        {
            SqlConnection sqlCon = new SqlConnection(MyConf.MyConnectionString);
            SqlCommand sqlCmd = default(SqlCommand);
            int retVal = 0;
            try
            {
                sqlCmd = new SqlCommand("GT_ValidateQrCodeLogin", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = userId;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 1;
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.Int).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                sqlCon.Open();
                sqlCmd.ExecuteNonQuery();
                sqlCon.Close();
                retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
                if (retVal == 1 && sessionEnd ==0)
                {
                    string pusherAppId = System.Configuration.ConfigurationManager.AppSettings["pusherAppId"].ToString();
                    string pusherAppKey = System.Configuration.ConfigurationManager.AppSettings["pusherAppKey"].ToString();
                    string pusherAppsecret = System.Configuration.ConfigurationManager.AppSettings["pusherAppsecret"].ToString();
                    string channelName = Convert.ToString(Session["QrCode"]);
                    Pusher pusher = new Pusher(pusherAppId, pusherAppKey, pusherAppsecret);
                    ITriggerResult pusherResult = null;
                    pusherResult = pusher.Trigger(channelName, "Logout", new
                    {
                        IsloggedIn = 0,
                    });


                }



            }
            catch (Exception ex)
            {
                Logger.ExceptionLog("Exception in UpdateQrCodeLogout Logout is " + ex.ToString());
                throw ex;
            }



        }
    }
}