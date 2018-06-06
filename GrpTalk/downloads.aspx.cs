using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class downloads : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().Contains("android"))
            {
                Response.Redirect("https://play.google.com/store/apps/details?id=com.mobile.android.grptalk");
            }
            else if (Request.ServerVariables["HTTP_USER_AGENT"].ToString().ToLower().Contains("iphone"))
            {
                Response.Redirect("https://itunes.apple.com/in/app/grptalk/id1074172134?ls=1&amp;mt=8");
            }
        }
    }
}