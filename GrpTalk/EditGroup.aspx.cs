using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class EditGroup : System.Web.UI.Page
    {
        public string hostNumber = "";
        public int countryID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                hostNumber = Session["HostMobile"].ToString();
                countryID = Convert.ToInt32(Session["CountryID"]);
                if (countryID == 108)
                {
                    hostNumber = hostNumber.Substring(2, 10);
                }
                else if (countryID == 1)
                { hostNumber = hostNumber.Substring(1, 10); }
                else if (countryID == 19)
                { hostNumber = hostNumber.Substring(3, 8); }
                else if (countryID == 239)
                { hostNumber = hostNumber.Substring(3, 12); }

                if (Convert.ToInt32(Session["UserType"]) == 2)
                {
                    Response.Redirect("/EditGroupTalk.aspx");
                }
            }
            else
            {
                Response.Redirect("/Home.aspx?msg=Session Expired");
            }
        }
    }
}