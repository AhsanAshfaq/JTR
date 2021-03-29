using System;
using System.Collections.Generic;

namespace OSS.Models.viewmodel
{
    public class FeesReceiveIndexViewModel
    {
        public string FeesRecNo { get; set; }
        public long FeesRecId { get; set; }
        public string StudentName { get; set; }
        public int AdmissionId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public decimal TotalAdjAmount { get; set; }
        public string PostDate { get; set; }
    }
    public class FeesReceiveCreateViewModel {
        public int? AdmissionId { get; set; }
        public string PostDate { get; set; }
        public int ClassId { get; set; }
        public string StageName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public List<FeesReceiveGridViewModel> FeesDetails { get; set; }
    }

    public class FeesReceiveStudentViewModel 
    {
        public int AdmissionId { get; set; }
        public string StudentName { get; set; }
    }

    public class FeesReceiveGridViewModel 
    {
        public string FeesMonth { get; set; }
        public string FeesTypeName { get; set; }
        public long FeesReceiveDtlID { get; set; }
        public int? GenFeesDtlID { get; set; }
        public decimal? FeesAmount { get; set; }
        public string DiscountName { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? DueDateDisc { get; set; }
        public decimal? NetFees { get; set; }
        public decimal? ReceivedAmount { get; set; }
        public decimal? AdjustmentAmount { get; set; }
        public string Status { get; set; }
    }
}