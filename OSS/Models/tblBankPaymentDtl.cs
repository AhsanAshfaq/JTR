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
    
    public partial class tblBankPaymentDtl
    {
        public int BankPayDtlId { get; set; }
        public Nullable<int> BankPayId { get; set; }
        public Nullable<decimal> CurrentAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public Nullable<decimal> BalanceAmount { get; set; }
        public string ShortNarration { get; set; }
        public string ChqNo { get; set; }
        public Nullable<System.DateTime> ChqDate { get; set; }
        public string AccountCodeDtl { get; set; }
        public string ChqStatus { get; set; }
    
        public virtual tblBankPaymentDtl tblBankPaymentDtl1 { get; set; }
        public virtual tblBankPaymentDtl tblBankPaymentDtl2 { get; set; }
    }
}
