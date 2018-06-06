using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CommonClasses.ApiHelper helperObj =new CommonClasses.ApiHelper();
            helperObj.SaveRequest("AdWordRequests");
        }
    }
}