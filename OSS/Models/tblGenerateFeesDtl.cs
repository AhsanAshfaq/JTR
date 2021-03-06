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
    
    public partial class tblGenerateFeesDtl
    {
        public tblGenerateFeesDtl()
        {
            this.tblFeesReceiveDtl = new HashSet<tblFeesReceiveDtl>();
        }
    
        public int GenFeesDtlID { get; set; }
        public Nullable<int> GenFeesMstID { get; set; }
        public Nullable<int> AdmissionID { get; set; }
        public Nullable<int> FeesTypeID { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<decimal> EditedDiscAmount { get; set; }
        public Nullable<decimal> NetFees { get; set; }
        public string Status { get; set; }
        public Nullable<int> StageID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<int> ChallanID { get; set; }
    
        public virtual tblAdmission tblAdmission { get; set; }
        public virtual tblClassMst tblClassMst { get; set; }
        public virtual tblDiscount tblDiscount { get; set; }
        public virtual ICollection<tblFeesReceiveDtl> tblFeesReceiveDtl { get; set; }
        public virtual tblFeesType tblFeesType { get; set; }
        public virtual tblGenerateFeesMst tblGenerateFeesMst { get; set; }
        public virtual tblSection tblSection { get; set; }
        public virtual tblStage tblStage { get; set; }
    }
}
