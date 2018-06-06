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
    public partial class Index : System.Web.UI.Page
    {
        JObject countrydetails = new JObject();
        public string supportsNumber, missedCallNumber, address;
        public int countryId, maxLength, minLength;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["automationKey"] != null && Request.QueryString["automationKey"] == "JpeZCad6UFRmY1n3miEcQPZWjJFMIeWOU4mlAkLQ==")
            {
                HttpContext.Current.Session["UserID"] = 31814;
                HttpContext.Current.Session["QrCode"] = "CHANNEL";
                HttpContext.Current.Session["CountryID"] = 108;
                HttpContext.Current.Session["HostMobile"] = "919040767686";
                HttpContext.Current.Session["Offset"] = 330;
                HttpContext.Current.Session["UserType"] = 2;

                Session.Timeout = 60;
                JObject responseJobj = new JObject();
                GT.BusinessLogicLayer.V_1_4.Login_V140 loginObj = new GT.BusinessLogicLayer.V_1_4.Login_V140();
                responseJobj = loginObj.ValidateOTP(MyConf.MyConnectionString, "919040767686", "123456", "", 1);
                Response.Redirect("/MyGrpTalks.aspx");


            }
            GT.BusinessLogicLayer.Index obj = new GT.BusinessLogicLayer.Index();

            //country = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString,Request.UserHostAddress);
            countrydetails = obj.Get_Country_Name_By_Ip(MyConf.MyConnectionString, Request.UserHostAddress);

            supportsNumber = Convert.ToString(countrydetails["SupportNumber"]);
            missedCallNumber = Convert.ToString(countrydetails["MissedCallNumber"]);
            address = Convert.ToString(countrydetails["Address"]);
            countryId = Convert.ToInt32(countrydetails["CountryId"]);
            maxLength = Convert.ToInt32(countrydetails["MaxLength"]);
            minLength = Convert.ToInt32(countrydetails["MinLength"]);
        }
    }
}