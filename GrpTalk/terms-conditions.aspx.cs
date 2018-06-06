using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using GT.Utilities;
using GrpTalk.CommonClasses;

namespace GrpTalk
{
    public partial class terms_conditions : System.Web.UI.Page
    {
        public string supportsNumber, address;
        public int countryId;
        protected void Page_Load(object sender, EventArgs e)
        {
            JObject countrydetails = new JObject();
            GT.BusinessLogicLayer.Index obj = new GT.BusinessLogicLayer.Index();
            countrydetails = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString, Request.UserHostAddress);
            supportsNumber = Convert.ToString(countrydetails["SupportNumber"]);
            address = Convert.ToString(countrydetails["Address"]);
            countryId = Convert.ToInt32(countrydetails["CountryId"]);

        }
    }
}