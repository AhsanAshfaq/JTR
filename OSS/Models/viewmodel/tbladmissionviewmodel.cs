using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OSS.Models.viewmodel
{
    public class tbladmissionviewmodel
    {
        public int AdmissionID { get; set; }
        public string GRNo { get; set; }
        public Nullable<int> RegID { get; set; }
        public Nullable<int> DiscountID { get; set; }
        public Nullable<int> RollNo { get; set; }
        public string RollNoGenerated { get; set; }
        public string Remarks { get; set; }
        public string DeleteBy { get; set; }
        public Nullable<System.DateTime> DeleteDate { get; set; }
        public string CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<System.DateTime> PostDate { get; set; }
        public Nullable<int> SchoolID { get; set; }
        public Nullable<int> BankID { get; set; }
        public string ChallanNoGenerated { get; set; }
        public Nullable<int> ChallanNo { get; set; }
        public Nullable<bool> Hostel { get; set; }
    }
}