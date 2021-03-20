using System;
using System.Collections.Generic;

namespace OSS.Models.viewmodel
{
    public class FeesReceiveIndexViewModel
    {
        public string FeesRecNo { get; set; }
        public int FeesRecId { get; set; }
        public string StudentName { get; set; }
        public int StudentId { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public int TotalAdjAmount { get; set; }
        public string PostDate { get; set; }
    }
    public class FeesReceiveCreateViewModel {
        public int StudentId { get; set; }
        public string PostDate { get; set; }
        public int ClassId { get; set; }
        public int StageId { get; set; }
        public int SectionId { get; set; }
        public string StageName { get; set; }
        public string ClassName { get; set; }
        public string SectionName { get; set; }
        public string StudentName { get; set; }
        public string FatherName { get; set; }
        public List<FeesReceiveGridViewModel> FeesDetails { get; set; }
    }

    public class FeesReceiveStudentViewModel 
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
    }

    public class FeesReceiveGridViewModel 
    {
        public string FeesMonth { get; set; }
        public string FeesTypeName { get; set; }
        public int FeesTypeId { get; set; }
        public int FeesAmount { get; set; }
        public string DiscountName { get; set; }
        public int DiscountTableId { get; set; }
        public int DiscountAmount { get; set; }
        public int NetFees { get; set; }
        public int ReceivedAmount { get; set; }
        public int AdjustmentAmount { get; set; }
        public int GenerateFeesId { get; set; }
    }
}