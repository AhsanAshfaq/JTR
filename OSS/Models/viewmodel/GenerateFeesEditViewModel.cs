using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSS.Models.viewmodel
{
    public class GenerateFeesEditViewModel
    {
        public GenerateFeesEditViewModel()
        {
            GridItems = new List<GenerateFeesGridModel>();
        }
        public int StageId { get; set; }
        public int ClassId { get; set; }
        public int SectionId { get; set; }
        public string PostDate { get; set; }
        public string FeesMonth { get; set; }
        public string ChargeFeesIds { get; set; }
        public List<GenerateFeesGridModel> GridItems { get; set; }
        public decimal? TotalEditedDiscount { get; set; }
        public decimal? TotalFees { get; set; }
        public decimal? TotalNetFees { get; set; }
    }

    public class GenerateFeesGridModel {
        public string GrNo { get; set; }
        public int AdmissionId { get; set; }
        public string StudentName { get; set; }
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public int FeeTypeId { get; set; }
        public string FeeTypeName { get; set; }
        public decimal? FeeAmount { get; set; }
        public int DiscountId { get; set; }
        public string DiscountName { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal? EditedDiscount { get; set; }
        public decimal? NetFees { get; set; }
    }

    public class IdName 
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}