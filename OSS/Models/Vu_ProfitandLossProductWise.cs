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
    
    public partial class Vu_ProfitandLossProductWise
    {
        public long ProductID { get; set; }
        public string ProductName { get; set; }
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public Nullable<System.DateTime> Dated { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<decimal> Profit { get; set; }
        public Nullable<int> SessionID { get; set; }
    }
}
