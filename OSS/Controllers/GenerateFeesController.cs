using OSS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class GenerateFeesController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: tblGenerateFeesMsts
        public ActionResult Index()
        {
            ViewBag.StageList = db.tblStage.ToList();
            ViewBag.FeeList = db.tblFeesType.ToList();
            return View();
        }

        // GET: tblGenerateFeesMsts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        public ActionResult GetDefaulters(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate) 
        {
            var result = new List<object>();
            result.Add(new {
                AdmissionId = 0,
                GRNo = "1",
                StudentName = "1",
                Class = "1",
                Section = "1",
                FeeType = "1",
                FeeAmount = "1",
                Discount = "1",
                DiscountAmount = "1",
                EditedDiscount = "1",
                NetFees = "1",
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Genereate(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate)
        {
            var result = new List<object>();
            result.Add(new
            {
                AdmissionId = 0,
                GRNo = "1",
                StudentName = "1",
                Class = "1",
                Section = "1",
                FeeType = "1",
                FeeAmount = "1",
                Discount = "1",
                DiscountAmount = "1",
                EditedDiscount = "1",
                NetFees = "1",
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateFixedFees(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate)
        {
            var result = new List<object>();
            result.Add(new
            {
                AdmissionId = 0,
                GRNo = "1",
                StudentName = "1",
                Class = "1",
                Section = "1",
                FeeType = "1",
                FeeAmount = "1",
                Discount = "1",
                DiscountAmount = "1",
                EditedDiscount = "1",
                NetFees = "1",
            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: tblGenerateFeesMsts/Create
        public ActionResult Create()
        {
            ViewBag.StageList = db.tblStage.ToList();
            ViewBag.FeeList = db.tblFeesType.ToList();
            return View();
        }

        // POST: tblGenerateFeesMsts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection form)
        {
            var stageId = int.Parse(form["stage"]);
            var classId = int.Parse(form["class"]);
            var sectionId = int.Parse(form["section"]);
            var postDate = DateTime.Parse(form["postDatee"]);
            var ChargeFees = form["chargeFees"];
            var feeMonth = form["feesMonth"];
            var selectedStudents = form["selectedStudentsId"];
            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: tblGenerateFeesMsts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        // POST: tblGenerateFeesMsts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenFeesMstID,GenFeesNo,StageID,ClassID,SectionID,TotalStudents,TotalFeesAmount,TotalDiscountAmount,TotalNetFees,IsDelete,SchoolID,PostDate,FeesDate,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate,SessionID,UserID")] tblGenerateFeesMst tblGenerateFeesMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGenerateFeesMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGenerateFeesMst);
        }

        // GET: tblGenerateFeesMsts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        // POST: tblGenerateFeesMsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            db.tblGenerateFeesMst.Remove(tblGenerateFeesMst);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
