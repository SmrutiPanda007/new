using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;




namespace GrpTalk.CommonClasses
{
    public static class MyConf
    {
        private static string myConnString;
        public static string MyConnectionString
        {
            get
            {
                if (myConnString == null)
                {
                    myConnString = ConfigurationManager.ConnectionStrings["GrpTalk"].ConnectionString;
                }
                return myConnString;
            }
        }
        //private static string userImage=null;
        //private static int imgStatus=0; 
        //public static string GetUserImage
        //{
        //    get 
        //    {
        //        if (userImage == null)
        //        { 
        //            string img = string.Empty;
        //            string imgStats = string.Empty;
        //            DAL.Sample userimg = new Sample();

                    
                
        //        }
        //        return 'sada1';

        //    }
        //}

        //public static string Get_Country_Name_By_Ip(string Ip_Address)
        //{

        //    string Cname = "";
        //    Cname = LoginBusiness.GetCountryName(MyConnectionString, Ip_Address, out Cname);
        //    return Cname;
        //}
    }
}