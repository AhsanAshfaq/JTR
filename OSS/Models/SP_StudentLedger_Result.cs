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
    
    public partial class SP_StudentLedger_Result
    {
        public long Glid { get; set; }
        public string ApplicantName { get; set; }
        public string VoucherNo { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public string AccountCode { get; set; }
        public string VoucherDate { get; set; }
        public Nullable<decimal> Debit { get; set; }
        public Nullable<decimal> Credit { get; set; }
    }
}
