using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class EditGroupTalk : System.Web.UI.Page
    {
        public string hostNumber = "";
        public int countryID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                hostNumber = Session["HostMobile"].ToString();
                countryID = Convert.ToInt32(Session["CountryID"]);

                if (Convert.ToInt32(Session["UserType"]) == 1)
                {
                    Response.Redirect("/EditGroup.aspx");
                }

            }
            else
            {
                Response.Redirect("/Home.aspx?msg=Session Expired");
            }
        }
    }
}