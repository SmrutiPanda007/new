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
    
    public partial class ConferenceBalanceDeductionTrack
    {
        public decimal Slno { get; set; }
        public Nullable<decimal> UserID { get; set; }
        public Nullable<decimal> ConferenceId { get; set; }
        public Nullable<decimal> ReportId { get; set; }
        public string BatchId { get; set; }
        public Nullable<bool> IsInbound { get; set; }
        public Nullable<byte> PulseId { get; set; }
        public Nullable<int> TotalSeconds { get; set; }
        public Nullable<int> TotalPulses { get; set; }
        public Nullable<int> FreeMinutesLeftBeforeDeduction { get; set; }
        public Nullable<int> FreeMinutesDeducted { get; set; }
        public Nullable<double> BalanceLeftBeforeDeduction { get; set; }
        public Nullable<double> BalanceDeducted { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public Nullable<System.DateTime> InsertedTimeStamp { get; set; }
        public Nullable<System.DateTime> UpdatedTimeStamp { get; set; }
    }
}
