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
    
    public partial class Vu_Class
    {
        public int TransactionID { get; set; }
        public Nullable<int> AdmissionID { get; set; }
        public Nullable<int> StageID { get; set; }
        public string StageName { get; set; }
        public Nullable<int> ClassID { get; set; }
        public string ClassName { get; set; }
        public string ClassPrefix { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string SectionName { get; set; }
        public string SectionPrefix { get; set; }
        public Nullable<int> SessionID { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public string RollNoGenerated { get; set; }
        public string ChallanNoGenerated { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> Hostel { get; set; }
        public Nullable<int> DarsgahID { get; set; }
        public string Darsgah { get; set; }
        public Nullable<bool> GenerateFees { get; set; }
    }
}
