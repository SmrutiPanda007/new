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
    
    public partial class AdminUserInfo
    {
        public int AdminId { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<System.DateTime> InsertedTimeStamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimeStamp { get; set; }
    }
}
