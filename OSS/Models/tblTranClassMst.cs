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
    
    public partial class tblTranClassMst
    {
        public int TranClassMstID { get; set; }
        public string TranClassNo { get; set; }
        public Nullable<int> FromStageID { get; set; }
        public Nullable<int> ToStageID { get; set; }
        public Nullable<int> FromSectionID { get; set; }
        public Nullable<int> ToSectionID { get; set; }
        public Nullable<int> FromSessionID { get; set; }
        public Nullable<int> ToSessionID { get; set; }
        public Nullable<int> FromClassID { get; set; }
        public Nullable<int> ToClassID { get; set; }
        public Nullable<int> TotalStudentsLoad { get; set; }
        public Nullable<int> TotalStudentsTrans { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
    }
}
