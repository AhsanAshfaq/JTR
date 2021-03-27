using System;
using System.Collections.Generic;

namespace OSS.Models.viewmodel
{
    public class GenerateFeesCreateModel
    {
        public string generateFeesList { get; set; }
        public int totalDiscount { get; set; }
        public int totalFees { get; set; }
        public int totalNetFees { get; set; }
        public int stageId { get; set; }
        public int classId { get; set; }
        public int sectionId { get; set; }
        public string feeMonth { get; set; }
        public DateTime postDate { get; set; }
        public string chargeFees { get; set; }
    }
    public class GenerateFeesIndv {
        public int admissionId { get; set; }
        public int feesTypeId { get; set; }
        public int discountId { get; set; }
        public int editedDiscount { get; set; }
        public int netFees { get; set; }
    }
}