using System.Collections.Generic;

namespace OSS.Models.viewmodel
{
    public class FeeVoucherReportModel
    {
        public int? AdmissionId { get; set; }
        public string StudentName { get; set; }
        public string StudentFatherName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string GrNo { get; set; }
        public string RollNumber { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolLogoUrl { get; set; }
        public List<FeeVoucherFees> feeList { get; set; }

    }
    public class FeeVoucherFees 
    {
        public string FeeType { get; set; }
        public string FeeMonth { get; set; }
        public int Amount { get; set; }
    }
}