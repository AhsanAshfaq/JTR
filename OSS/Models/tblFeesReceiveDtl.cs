//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OSS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblFeesReceiveDtl
    {
        public long FeesReceiveDtlID { get; set; }
        public Nullable<long> FeesReceiveMstID { get; set; }
        public Nullable<int> GenFeesDtlID { get; set; }
        public Nullable<decimal> AdjustmentAmount { get; set; }
        public Nullable<decimal> ReceivedAmount { get; set; }
        public string Status { get; set; }
        public Nullable<decimal> DueDateDisc { get; set; }
    
        public virtual tblGenerateFeesDtl tblGenerateFeesDtl { get; set; }
        public virtual tblFeesReceiveMst tblFeesReceiveMst { get; set; }
    }
}
