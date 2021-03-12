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
    
    public partial class tblSection
    {
        public tblSection()
        {
            this.tblClassDtl = new HashSet<tblClassDtl>();
            this.tblDefineFeesMst = new HashSet<tblDefineFeesMst>();
            this.tblDefineFeesStudentMst = new HashSet<tblDefineFeesStudentMst>();
            this.tblGenerateFeesDtl = new HashSet<tblGenerateFeesDtl>();
            this.tblStudentRegMst = new HashSet<tblStudentRegMst>();
        }
    
        public int SectionID { get; set; }
        public string SectionName { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string Prefix { get; set; }
    
        public virtual ICollection<tblClassDtl> tblClassDtl { get; set; }
        public virtual ICollection<tblDefineFeesMst> tblDefineFeesMst { get; set; }
        public virtual ICollection<tblDefineFeesStudentMst> tblDefineFeesStudentMst { get; set; }
        public virtual ICollection<tblGenerateFeesDtl> tblGenerateFeesDtl { get; set; }
        public virtual ICollection<tblStudentRegMst> tblStudentRegMst { get; set; }
    }
}
