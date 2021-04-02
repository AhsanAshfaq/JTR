using iText.Html2pdf;
using OSS.Models;
using OSS.Models.viewmodel;
using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class FeeVoucherController : Controller
    {
        // GET: FeeVoucher
        private OssEntities db = new OssEntities();
        public ActionResult Create()
        {
            ViewBag.StageList = db.tblStage.Where(o => o.SchoolID == portalutilities._schollid && o.IsDelete == false && o.IsActive == true).ToList();
            return View();
        }
        public ActionResult LoadStudents(int stageId, int classId, int sectionId, string selectedPeriod, string activeness) 
        {
            var llist = db.tblAdmission.Where(x => x.tblStudentRegMst.SchoolID == portalutilities._schollid && x.tblStudentRegMst.StageID == stageId 
                            && x.tblStudentRegMst.ClassID == classId && x.tblStudentRegMst.SectionID == sectionId).ToList();


            var result = new List<object>();
            foreach (var item in llist)
            {
                result.Add(new
                {
                    AdmissionId = item.AdmissionID,
                    StudentName = portalutilities.InEnglish ? item.tblStudentRegMst.ApplicantName : item.tblStudentRegMst.ApplicantNameInUrdu,
                    GrNo = item.GRNo
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FeeVoucherReport(string studentIds)
        {
            var model = new FeeVoucherReportModel
            {
                AdmissionId = 1,
                StudentName = "Test",
                StudentFatherName = "Test",
                ClassName = "Owla",
                GrNo = "1440/0321/0005",
                SectionName = "AWWAL",
                RollNumber = "13",
                SchoolAddress = "PLOT # 1,ST 4, PHASE1, SECTOR 4, SCHEME 33 AHSANABAD KARACHI",
                SchoolLogoUrl = "https://ibb.co/Sv0GJRW",
                SchoolName = "JAMIA-TUR-RASHEED",
                SchoolPhone = "03340555850"

            };
            HtmlConverter.ConvertToPdf("<h1>Test</h1>", new FileStream(@"c:\>File.pdf", FileMode.CreateNew));
            return View(model);
        }
        public ActionResult GenerateReport(string studentIds) 
        {
            return RedirectToAction("FeeVoucherReport", new { studentIds = studentIds });
        }
    }
}
