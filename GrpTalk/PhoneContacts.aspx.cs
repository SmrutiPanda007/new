﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GrpTalk
{
    public partial class PhoneContacts : System.Web.UI.Page
    {
        public int countryID = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserID"] != null)
            {
                countryID = Convert.ToInt32(Session["CountryID"]);   
            }
            
            
        }
    }
}