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
    
    public partial class UserCallerIDsTriggerTable
    {
        public decimal Slno { get; set; }
        public Nullable<decimal> UserCallerIdSlno { get; set; }
        public Nullable<decimal> UserID { get; set; }
        public Nullable<int> CallerIdSlno { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<System.DateTime> InsertedTimeStamp { get; set; }
        public Nullable<int> AvailableMinutes { get; set; }
        public string TriggerType { get; set; }
        public string TransactionID { get; set; }
    }
}
