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
    
    public partial class tblOpeningQtyMst
    {
        public long OQMstID { get; set; }
        public string OQCode { get; set; }
        public Nullable<decimal> TotalQty { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<decimal> TotalPurPrice { get; set; }
    }
}
