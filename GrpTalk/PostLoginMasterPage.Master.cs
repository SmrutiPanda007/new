using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using GT.Utilities;

namespace GrpTalk
{
    public partial class PostLoginMasterPage : System.Web.UI.MasterPage
    {
        public string imgPath; public string nickName;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Cookies.Count > 1)
            {
                if (Request.Cookies["UserData"] != null)
                {

                    HttpContext.Current.Session["UserID"] = Request.Cookies["SessionId"]["UserID"];
                    HttpContext.Current.Session["Offset"] = Request.Cookies["SessionId"]["Offset"];
                    HttpContext.Current.Session["CountryID"] = Request.Cookies["SessionId"]["CountryID"];
                    HttpContext.Current.Session["HostMobile"] = Request.Cookies["UserData"]["HostMobile"];
                    HttpContext.Current.Session["UserType"] = Request.Cookies["UserData"]["UserType"];
                    HttpContext.Current.Session["QrCode"] = Request.Cookies["UserData"]["QrCode"];
                    
                }
            }
            if (Session["UserID"] == null)
            {
                Response.Redirect("/Home.aspx?msg=session expired please login again");
                return;
            }
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["GrpTalk"].ConnectionString);
            SqlCommand sqlCmd = new SqlCommand();
            SqlDataAdapter da = null;
            DataTable dt = new DataTable();
            int retVal = 0; string retMessage = "";
            try
            {
                sqlCmd = new SqlCommand("GT_GetSetProfileDetails", sqlCon);
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.Parameters.Add("@Mode", SqlDbType.Int).Value = 3;
                sqlCmd.Parameters.Add("@UserID", SqlDbType.Int).Value = Convert.ToInt32(Session["UserID"]);
                sqlCmd.Parameters.Add("@RetVal", SqlDbType.TinyInt).Direction = ParameterDirection.Output;
                sqlCmd.Parameters.Add("@RetMessage", SqlDbType.VarChar, 1000).Direction = ParameterDirection.Output;
                da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                Logger.ExceptionLog(ex.ToString());
            }

            retVal = Convert.ToInt32(sqlCmd.Parameters["@RetVal"].Value);
            retMessage = sqlCmd.Parameters["@RetMessage"].Value.ToString();

            if (retVal == 1)
            {
                if (dt.Rows.Count != 0)
                {
                    imgPath = Convert.ToString(dt.Rows[0]["DisplayPicPath"]);
                    nickName = Convert.ToString(dt.Rows[0]["NickName"]);
                }
            }
            else
            {
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
                Response.Redirect("/Home.aspx");
            }
            //}


        }
    }
}