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
    
    public partial class error_log
    {
        public int sno { get; set; }
        public string proc_name { get; set; }
        public string errors { get; set; }
        public Nullable<System.DateTime> time_stamp { get; set; }
        public Nullable<int> err_line { get; set; }
    }
}
