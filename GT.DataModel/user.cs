//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GT.DataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class user
    {
        public int id { get; set; }
        public string mobile_number { get; set; }
        public string email { get; set; }
        public System.DateTime created_at { get; set; }
        public System.DateTime updated_at { get; set; }
        public string remember_me_token { get; set; }
        public Nullable<System.DateTime> remember_me_token_expires_at { get; set; }
        public Nullable<int> smsc_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string api_key { get; set; }
        public string access_key { get; set; }
        public string website_url { get; set; }
        public string company { get; set; }
        public string work_number { get; set; }
        public string profile_pic { get; set; }
        public string type { get; set; }
        public Nullable<int> admin_id { get; set; }
        public Nullable<float> balance { get; set; }
        public Nullable<int> pulse_id { get; set; }
        public Nullable<short> is_available { get; set; }
        public string reset_password_token { get; set; }
        public Nullable<System.DateTime> reset_password_token_expires_at { get; set; }
        public Nullable<System.DateTime> reset_password_email_sent_at { get; set; }
        public string soft_phone { get; set; }
        public Nullable<int> is_login { get; set; }
        public string user_password { get; set; }
        public byte[] user_image { get; set; }
        public string mobile_confirm { get; set; }
        public string email_confirm { get; set; }
        public string confirmed_at { get; set; }
        public string USERNAME { get; set; }
        public string email_token { get; set; }
        public Nullable<int> MinimumInboundMinutes { get; set; }
        public Nullable<int> MinimumOutboundMinutes { get; set; }
        public Nullable<byte> DNDAccess { get; set; }
        public Nullable<byte> TRAIAccess { get; set; }
        public string Role { get; set; }
        public Nullable<byte> IsDnd { get; set; }
        public string TimeZone { get; set; }
        public string AcManagerEmail { get; set; }
        public string devicetoken { get; set; }
        public string appsource { get; set; }
        public string deviceuniqueid { get; set; }
        public string androiddevicetoken { get; set; }
        public string countrycode { get; set; }
        public Nullable<int> RegisteredCountryID { get; set; }
    }
}
