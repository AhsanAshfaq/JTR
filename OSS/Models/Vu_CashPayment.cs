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
    
    public partial class Vu_CashPayment
    {
        public int GLid { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<decimal> AmountDrCr { get; set; }
        public string RefCode { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<System.DateTime> TransactionDateTime { get; set; }
        public int TotalDebit { get; set; }
        public Nullable<decimal> TotalCredit { get; set; }
        public int AccountID { get; set; }
        public string AccountCode { get; set; }
        public string AccountType { get; set; }
        public string AccountNo { get; set; }
        public string AccountName { get; set; }
        public string Remarks { get; set; }
        public string TransactionType { get; set; }
    }
}