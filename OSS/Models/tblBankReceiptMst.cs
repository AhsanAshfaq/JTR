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
    
    public partial class tblBankReceiptMst
    {
        public int BankRecId { get; set; }
        public string BankRecNo { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string Narration { get; set; }
        public Nullable<bool> IsPosted { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public string AccountCode { get; set; }
        public Nullable<decimal> AccountBalance { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<decimal> TotalAmount { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> SessionID { get; set; }
    }
}
