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
    
    public partial class OptedInsSet
    {
        public int id { get; set; }
        public string mobile { get; set; }
        public Nullable<byte> IsOptedIn { get; set; }
        public Nullable<System.DateTime> Created_at { get; set; }
        public Nullable<System.DateTime> updated_at { get; set; }
        public Nullable<int> UserId { get; set; }
    }
}
