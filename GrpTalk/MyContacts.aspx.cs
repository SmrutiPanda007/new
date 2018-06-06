using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;

namespace GrpTalk
{
    public partial class MyContacts : System.Web.UI.Page
    {
        public int countryID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.TraceLog("mycontacts page");
              countryID = Convert.ToInt32(Session["CountryID"]);
              Logger.TraceLog(countryID.ToString());
        }
    }
}