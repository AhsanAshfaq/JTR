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
    
    public partial class tblGenerateSalaryMst
    {
        public int GenSalaryMstID { get; set; }
        public string GenSalaryNo { get; set; }
        public Nullable<int> DesignationID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public Nullable<int> TotalEmployees { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<System.DateTime> SalaryDate { get; set; }
        public Nullable<decimal> TotalNetSalary { get; set; }
        public Nullable<System.DateTime> PayrollPeriodFromDate { get; set; }
        public Nullable<System.DateTime> PayrollPeriodToDate { get; set; }
    }
}
