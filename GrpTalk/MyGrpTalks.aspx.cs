using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;
namespace GrpTalk
{
    public partial class MyGrpTalks : System.Web.UI.Page
    {
        public short userType;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                userType = Convert.ToInt16(Session["UserType"]);

                if (Convert.ToInt32(Session["UserType"]) == 1)
                {
                    Response.Redirect("/MyGroup.aspx");
                }
            }

          

        }
    }
}