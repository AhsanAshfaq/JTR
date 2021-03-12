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
    
    public partial class tblProvince
    {
        public tblProvince()
        {
            this.tblArea = new HashSet<tblArea>();
            this.tblCity = new HashSet<tblCity>();
            this.tblStudentRegMst = new HashSet<tblStudentRegMst>();
        }
    
        public int ProvinceID { get; set; }
        public string ProvinceName { get; set; }
        public Nullable<int> CountryID { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    
        public virtual ICollection<tblArea> tblArea { get; set; }
        public virtual ICollection<tblCity> tblCity { get; set; }
        public virtual tblCountry tblCountry { get; set; }
        public virtual ICollection<tblStudentRegMst> tblStudentRegMst { get; set; }
    }
}
