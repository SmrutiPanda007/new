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
    
    public partial class Services_HeartBeat_Info_Conf
    {
        public int ServiceId { get; set; }
        public string Service_Name { get; set; }
        public string Source { get; set; }
        public Nullable<System.DateTime> Last_Beat { get; set; }
        public Nullable<int> Queue_Count { get; set; }
        public string Err_Trace { get; set; }
        public long Err_Count { get; set; }
    }
}
