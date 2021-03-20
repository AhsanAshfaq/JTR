namespace OSS.Models.viewmodel
{
    public class GenerateFeesViewModel
    {
        public int  AdmissionId { get; set; }
        public string GRNo { get; set; }
        public string StudentName { get; set; }
        public  int ClassId { get; set; }
        public int SectionId { get; set; }
        public int FeeTypeId { get; set; }
        public int FeeAmount { get; set; }
        public int Discount { get; set; }
        public int DiscountAmount { get; set; }
        public int EditedDiscount { get; set; }
        public int NetFees { get; set; }

    }
}