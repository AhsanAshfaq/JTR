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
    
    public partial class tblRequisition
    {
        public long RequisitionID { get; set; }
        public string RequisitionNo { get; set; }
        public Nullable<int> EmpID { get; set; }
        public string Type { get; set; }
        public string Remarks { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public Nullable<decimal> ApprovedAmount { get; set; }
        public string ApprovedBy { get; set; }
        public Nullable<System.DateTime> ApprovedDate { get; set; }
        public Nullable<System.DateTime> PendingDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<bool> IsApproved { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsCancelled { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string CancelledBy { get; set; }
        public Nullable<System.DateTime> CancelledDate { get; set; }
    }
}
