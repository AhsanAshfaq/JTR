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
    
    public partial class Vu_PendingCheques
    {
        public int dtlID { get; set; }
        public string PartyName { get; set; }
        public string PartyType { get; set; }
        public string AccountName { get; set; }
        public Nullable<decimal> ChqAmount { get; set; }
        public string ChqNo { get; set; }
        public Nullable<System.DateTime> ChqDate { get; set; }
        public string ChqStatus { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> SessionID { get; set; }
    }
}
