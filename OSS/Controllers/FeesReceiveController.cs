using Newtonsoft.Json;
using OSS.Models;
using OSS.Models.viewmodel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class FeesReceiveController : Controller
    {
        private OssEntities db = new OssEntities();
        // GET: FeesReceive
        public ActionResult Index()
        {
            var result = new List<FeesReceiveIndexViewModel>();
            /// TODO : This result list will contains the list (Record History Tab Data FROM Fees Receive Screen of Desktop App ) 
            result.Add(new FeesReceiveIndexViewModel
            {
                FeesRecNo = "1220-911",
                ClassName = "Sabiya",
                StudentName = "Tahir Khan",
                FeesRecId = 1,
                PostDate = DateTime.Now.ToString("dd-MMMM-yyyy"),
                SectionName = "ALIF",
                TotalAdjAmount = 5000,
                AdmissionId = 1
            });
            return View(result);
        }

        // GET: FeesReceive/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeesReceive/Create
        public ActionResult Create(int? studentId)
        {
            var result = new FeesReceiveCreateViewModel();
            List<FeesReceiveStudentViewModel> studentList = GetStudentsList();
            
            ViewBag.StudentList = studentList;
            ViewBag.StudentId = studentId ?? 0;

            // If studentId has a value it means it was clicked from index page grid and we need to show the form in edit mode.... If not then normal create form
            if (studentId.HasValue)
                result = GetStudentDetailInformation(studentId.Value);

            return View(result);
        }

        private static List<FeesReceiveStudentViewModel> GetStudentsList()
        {
            /// TODO : Simple list of all student's id along with the name (1440/0321/0105) we want to show
            return new List<FeesReceiveStudentViewModel> {
            new FeesReceiveStudentViewModel{
                AdmissionId = 1,
                StudentName = "1440/0321/0105"
                },
            new FeesReceiveStudentViewModel{
                AdmissionId = 2,
                StudentName = "1440/0321/0106"
                }
            };
        }

        // POST: FeesReceive/Create
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            try
            {
                // TODO: Add insert logic here
                var stageId = int.Parse(form["stageId"]);
                var classId = int.Parse(form["classId"]);
                var sectionId = int.Parse(form["sectionId"]);
                var postDate = DateTime.Parse(form["postDate"]);
                var selectedFeesIds = form["selectedFees"];  // These are GenerateFeesId for each fees item selected and its comma separated.
                var StudentName = form["name"];
                var FatherName = form["fname"];
                var selectedAdmissionId = form["selectedAdmissionId"];

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult GetStudentDetails(int admissionId)
        {
            FeesReceiveCreateViewModel result = GetStudentDetailInformation(admissionId);
            var json = JsonConvert.SerializeObject(result);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        private static FeesReceiveCreateViewModel GetStudentDetailInformation(int admissionId)
        {
            var feesListForStudent = new List<FeesReceiveGridViewModel> {
            new FeesReceiveGridViewModel{
                    FeesMonth = "Nov-2020",
                    FeesTypeName = "Monthly Fees",
                    FeesAmount = 1000,
                    DiscountName = "Disc 5000",
                    DiscountAmount = 5000,
                    NetFees = 5000,
                    ReceivedAmount = 0,
                    AdjustmentAmount = 2500,
                    DiscountTableId = 1,
                    FeesTypeId = 1,
                    GenerateFeesId = 1,
            },
            new FeesReceiveGridViewModel{
                    FeesMonth = "Nov-2020",
                    FeesTypeName = "Monthly Fees",
                    FeesAmount = 1000,
                    DiscountName = "Disc 5000",
                    DiscountAmount = 5000,
                    NetFees = 5000,
                    ReceivedAmount = 0,
                    AdjustmentAmount = 2500,
                    DiscountTableId = 1,
                    FeesTypeId = 1,
                    GenerateFeesId = 2,
            }



            };

            var result = new FeesReceiveCreateViewModel
            {
                PostDate = DateTime.Now.ToString("dd-MMMM-yyyy"),
                SectionId = 1,
                StageId = 1,
                AdmissionId = admissionId,
                StageName = "Dars-e-Nizami",
                ClassName = "Owla",
                SectionName = "SAALIS",
                StudentName = "Tahir Khan",
                FatherName = "Ali Khan",
                ClassId = 1,
                FeesDetails = feesListForStudent, // List of all the fees a student have 
            };
            return result;
        }

        // GET: FeesReceive/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeesReceive/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FeesReceive/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FeesReceive/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
