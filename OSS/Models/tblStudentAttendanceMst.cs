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
    
    public partial class tblStudentAttendanceMst
    {
        public long StudAttendanceMstID { get; set; }
        public Nullable<int> StageID { get; set; }
        public Nullable<int> ClassID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> TotalStudents { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> SessionID { get; set; }
        public string Createby { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string AttendanceType { get; set; }
    }
}
