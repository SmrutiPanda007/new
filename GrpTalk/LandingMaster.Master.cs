using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GT.Utilities;
using GrpTalk.CommonClasses;
using Newtonsoft.Json.Linq;
namespace GrpTalk
{
    public partial class LandingMaster : System.Web.UI.MasterPage
    {

        public string supportsNumber, missedCallNumber, address;
        public int countryId, maxLength, minLength;
        protected void Page_Load(object sender, EventArgs e)
        {
            JObject countrydetails = new JObject();
            GT.BusinessLogicLayer.Index obj = new GT.BusinessLogicLayer.Index();
            //country = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString,Request.UserHostAddress);
            countrydetails = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString, Request.UserHostAddress);
            missedCallNumber = Convert.ToString(countrydetails["MissedCallNumber"]);
            supportsNumber = Convert.ToString(countrydetails["SupportNumber"]);
            countryId = Convert.ToInt32(countrydetails["CountryId"]);
            maxLength = Convert.ToInt32(countrydetails["MaxLength"]);
            minLength = Convert.ToInt32(countrydetails["MinLength"]);


        }
    }
}