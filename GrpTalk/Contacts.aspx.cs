using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;

namespace GrpTalk
{
    public partial class Contacts : System.Web.UI.Page
    {
        public int countryID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            countryID = Convert.ToInt32(Session["CountryID"]);
        }
    }
}