using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;
using GrpTalk.CommonClasses;
using Newtonsoft.Json.Linq;
using System.Net;
namespace GrpTalk
{
    public partial class PreLoginMasterPage : System.Web.UI.MasterPage
    {
        JObject countrydetails = new JObject();
        public string supportsNumber, missedCallNumber, address;
        public int countryId, maxLength, minLength;
        protected void Page_Load(object sender, EventArgs e)
        {
            GT.BusinessLogicLayer.Index obj = new GT.BusinessLogicLayer.Index();

            //country = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString,Request.UserHostAddress);
            countrydetails = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString,"183.82.2.22");

            supportsNumber = Convert.ToString(countrydetails["SupportNumber"]);
            missedCallNumber = Convert.ToString(countrydetails["MissedCallNumber"]);
            address = Convert.ToString(countrydetails["Address"]);
            countryId = Convert.ToInt32(countrydetails["CountryId"]);
            maxLength = Convert.ToInt32(countrydetails["MaxLength"]);
            minLength = Convert.ToInt32(countrydetails["MinLength"]);
        }
    }
}