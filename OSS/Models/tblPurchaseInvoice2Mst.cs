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
    
    public partial class tblPurchaseInvoice2Mst
    {
        public long PI2MstID { get; set; }
        public string PI2Code { get; set; }
        public Nullable<int> VendorId { get; set; }
        public string ChallanNo { get; set; }
        public Nullable<decimal> GrandTotal { get; set; }
        public Nullable<int> TotalItems { get; set; }
        public Nullable<decimal> Freight { get; set; }
        public Nullable<decimal> Misc { get; set; }
        public Nullable<decimal> TotalTO { get; set; }
        public Nullable<decimal> TotalQty { get; set; }
        public Nullable<decimal> TotalGrossAmount { get; set; }
        public Nullable<decimal> TotalDiscount { get; set; }
        public Nullable<decimal> TotalNetAmount { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<decimal> TotalInvDisc { get; set; }
        public Nullable<decimal> TotalInvDiscPerc { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> SessionID { get; set; }
    }
}
