using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace GrpTalk
{
    public partial class OtpLive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindOtpDetails();
            }
        }
        private void BindOtpDetails()
        {
            SqlCommand sqlCmd = default(SqlCommand);
            SqlConnection sqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["GrpTalk"].ConnectionString);
            SqlDataAdapter da = null;
            DataSet Ds = new DataSet();
            int userId = Convert.ToInt32(Request.QueryString["userid"]);
            try
            {

                if (userId == 0)
                {

                    Response.Write("NO Data Found");
                }
                else
                {
                    sqlCmd = new SqlCommand("select top 5 MobileNumber,OTP,DateAdd(MINUTE,330,InsertedTimeStamp) as InsertedTimeStamp from OTPMASTER WITH(NOLOCK) where UserId='" + userId + "' order by Slno desc", sqlCon);
                    da = new SqlDataAdapter(sqlCmd);
                    da.Fill(Ds);
                    if (Ds.Tables.Count != 0)
                    {
                        if (Ds.Tables[0].Rows.Count != 0)
                        {

                            gv1.DataSource = Ds.Tables[0];
                            gv1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}