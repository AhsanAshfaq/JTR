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
    
    public partial class Vu_LoanAdvance
    {
        public long TransactionID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<decimal> OriginalAmount { get; set; }
        public Nullable<decimal> ApprovedAmount { get; set; }
        public Nullable<decimal> PaidAmount { get; set; }
        public string RefCode { get; set; }
        public Nullable<int> EmployeeID { get; set; }
        public string TransactionType { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string Remarks { get; set; }
        public int Days { get; set; }
        public int TotalSalary { get; set; }
        public int TotalAllowances { get; set; }
        public Nullable<decimal> Deduction { get; set; }
        public int NetSalary { get; set; }
    }
}
