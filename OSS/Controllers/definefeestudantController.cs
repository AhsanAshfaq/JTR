using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSS.Models;

namespace OSS.Controllers
{
    public class definefeestudantController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /definefeestudant/
        public ActionResult Index()
        {
            var tbldefinefeesstudentmst = db.tblDefineFeesStudentMst.Include(t => t.tblClassMst).Include(t => t.tblSection).Include(t => t.tblStage);
            ViewBag.FeeTypeList = db.tblFeesType.ToList();
            return View(tbldefinefeesstudentmst.Where(x => x.IsDelete == false).OrderByDescending(x => x.PostDate).Take(10).ToList());
        }

        // GET: /definefeestudant/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefineFeesStudentMst tbldefinefeesstudentmst = db.tblDefineFeesStudentMst.Find(id);
            if (tbldefinefeesstudentmst == null)
            {
                return HttpNotFound();
            }
            return View(tbldefinefeesstudentmst);
        }

        // GET: /definefeestudant/Create
        public ActionResult Create()
        {
            ViewBag.ClassID = new SelectList(db.tblClassMst, "ClassID", "ClassName");
            ViewBag.SectionID = new SelectList(db.tblSection, "SectionID", "SectionName");
            ViewBag.StageID = new SelectList(db.tblStage, "StageID", "StageName");
            var feeTypes = db.tblFeesType.ToList();
            ViewBag.feeTypes = feeTypes;
            return View();
        }
        
        public ActionResult GetSelectedStudentsList(long defineFeeStudentId)
        {
            var studentDtlList = db.tblDefineFeesStudentDtl.Where(x => x.DefineFeesStudentID == defineFeeStudentId).ToList();
            var result = new List<object>();
            foreach (var item in studentDtlList)
            {
                var ad = db.tblAdmission.Include(x => x.tblStudentRegMst).FirstOrDefault(x => x.AdmissionID == item.AdmissionID);
                result.Add(new { id = ad.AdmissionID, GRNo = ad.GRNo, FirstName = ad.tblStudentRegMst.ApplicantNameInUrdu, LastName = ad.tblStudentRegMst.FatherNameInUrdu });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStudentsList(int stageId, int classId, int sectionId) 
        {
            var studentsList = (from tsr in db.tblStudentRegMst
                                join tad in db.tblAdmission on tsr.RegID equals tad.RegID
                                where tsr.StageID == stageId && tsr.ClassID == classId && tsr.SectionID == sectionId
                                select new { id = tad.AdmissionID , GRNo = tad.GRNo, FirstName = tsr.ApplicantNameInUrdu, LastName = tsr.FatherNameInUrdu }).ToList();
            return Json(studentsList, JsonRequestBehavior.AllowGet);
        }
        // POST: /definefeestudant/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblDefineFeesStudentMst tbldefinefeesstudentmst, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var selectedStudentIds = form["selectedStudentList"].Split(',');
                tbldefinefeesstudentmst.TotalStudents = int.Parse(form["totalStudentNumber"]);
                tbldefinefeesstudentmst.FeesTypeID = int.Parse(form["feeType"]);
                tbldefinefeesstudentmst.IsDelete = false;
                tbldefinefeesstudentmst.PostDate = DateTime.Now;

                var obj = db.tblDefineFeesStudentMst.Add(tbldefinefeesstudentmst);
                db.SaveChanges();
                foreach (var studentId in selectedStudentIds)
                {
                    var admissionId = int.Parse(studentId);
                    db.tblDefineFeesStudentDtl.Add(new tblDefineFeesStudentDtl
                    {
                        DefineFeesStudentID = obj.DefineFeesStudentID,
                        AdmissionID = admissionId
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.tblClassMst, "ClassID", "ClassName", tbldefinefeesstudentmst.ClassID);
            ViewBag.SectionID = new SelectList(db.tblSection, "SectionID", "SectionName", tbldefinefeesstudentmst.SectionID);
            ViewBag.StageID = new SelectList(db.tblStage, "StageID", "StageName", tbldefinefeesstudentmst.StageID);
            return View(tbldefinefeesstudentmst);
        }

        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefineFeesStudentMst tbldefinefeesstudentmst = db.tblDefineFeesStudentMst.Find(id);
            if (tbldefinefeesstudentmst == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClassList = db.tblClassMst.ToList();
            ViewBag.StageList = db.tblStage.ToList();
            var feeTypes = db.tblFeesType.ToList();
            ViewBag.feeTypes = feeTypes;
            var sectionList = new List<tblSection>();
            var result = (from _tblClassDtls in db.tblClassDtl
                      join jsec in db.tblSection on _tblClassDtls.SectionID equals jsec.SectionID
                      where _tblClassDtls.ClassID == tbldefinefeesstudentmst.ClassID && jsec.IsActive == true && jsec.IsDelete != true && jsec.SchoolID == portalutilities._schollid
                      select new
                      {
                          id = jsec.SectionID,
                          name = jsec.SectionName
                      }).ToList();
            foreach (var sec in result)
            {
                sectionList.Add(new tblSection { 
                    SectionID = sec.id,
                    SectionName = sec.name
                });
            }
            ViewBag.SectionList = sectionList;
            return View(tbldefinefeesstudentmst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblDefineFeesStudentMst tbldefinefeesstudentmst, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                var defineFeesStudentId = int.Parse(form["defineFeeStudentId"]);
                var obj = db.tblDefineFeesStudentMst.FirstOrDefault(x => x.DefineFeesStudentID == defineFeesStudentId);
                var selectedStudentIds = form["selectedStudentList"].Split(',');
                obj.FeesTypeID = int.Parse(form["feeType"]);
                obj.SectionID = tbldefinefeesstudentmst.SectionID;
                obj.StageID = tbldefinefeesstudentmst.StageID;
                obj.ClassID = tbldefinefeesstudentmst.ClassID;
                obj.UpdateDate = DateTime.Now;
                db.SaveChanges();
                db.tblDefineFeesStudentDtl.RemoveRange(db.tblDefineFeesStudentDtl.Where(x => x.DefineFeesStudentID == defineFeesStudentId));
                db.SaveChanges();
                foreach (var studentId in selectedStudentIds)
                {
                    var admissionId = int.Parse(studentId);
                    db.tblDefineFeesStudentDtl.Add(new tblDefineFeesStudentDtl
                    {
                        DefineFeesStudentID = defineFeesStudentId,
                        AdmissionID = admissionId
                    });
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.tblClassMst, "ClassID", "ClassName", tbldefinefeesstudentmst.ClassID);
            ViewBag.SectionID = new SelectList(db.tblSection, "SectionID", "SectionName", tbldefinefeesstudentmst.SectionID);
            ViewBag.StageID = new SelectList(db.tblStage, "StageID", "StageName", tbldefinefeesstudentmst.StageID);
            return View(tbldefinefeesstudentmst);
        }

        // GET: /definefeestudant/Delete/5
        public ActionResult Delete(long? id)
        {
            tblDefineFeesStudentMst tbldefinefeesstudentmst = db.tblDefineFeesStudentMst.Find(id);
            tbldefinefeesstudentmst.IsDelete = true;
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
