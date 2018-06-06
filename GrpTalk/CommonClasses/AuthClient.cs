using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GT.Utilities;

namespace GrpTalk.CommonClasses
{

    public class AuthClient
    {
        
        private string apiKey = "";
        private string apiSecret = "";
        public Dictionary<string, object> authValidateResponse = new Dictionary<string, object>();


        public bool Authenticate()
        {
            ApiHelper helperObj = new ApiHelper();
            bool isValidate = false;
            string url = HttpContext.Current.Request.RawUrl;
            authValidateResponse = helperObj.ApiAuthValidate(apiKey: apiKey, isValidate: isValidate, apiSecret: apiSecret);
            HttpContext.Current.Items.Add("AuthCompleteTime", (DateTime.Now - new DateTime(1970, 1, 1)).TotalMilliseconds);
            if (Convert.ToInt32(authValidateResponse["StatusCode"]) == 200)
            {
                return true;
            }
            return false;
        }
        public string ApiKey
        {
            get { return apiKey; }
            set { apiKey = value; }
        }
        public string ApiSecret
        {
            get { return apiSecret; }
            set { apiSecret = value; }
        }
    }
}