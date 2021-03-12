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
    public class definefeesController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /definefees/
        public ActionResult Index()
        {
            var tbldefinefeesmst = db.tblDefineFeesMst.Include(t => t.tblClassMst).Include(t => t.tblSection).Include(t => t.tblStage);
            return View(tbldefinefeesmst.Where(a => a.IsDelete != true  && a.SchoolID == portalutilities._schollid).OrderByDescending(x=>x.PostDate).Take(10).ToList());
        }

        // GET: /definefees/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefineFeesMst tbldefinefeesmst = db.tblDefineFeesMst.Find(id);
            if (tbldefinefeesmst == null)
            {
                return HttpNotFound();
            }
            return View(tbldefinefeesmst);
        }

        // GET: /definefees/Create
        public ActionResult Create()
        {
            ViewBag.StageID = new SelectList(db.tblStage.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "StageID", "StageName");
            var feeTypes = db.tblFeesType.ToList();
            ViewBag.feeTypes = feeTypes;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblDefineFeesMst tbldefinefeesmst, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                tbldefinefeesmst.TotalFees = int.Parse(form["totalFee"]);
                tbldefinefeesmst.IsDelete = false;
                var defineFee = db.tblDefineFeesMst.Add(tbldefinefeesmst);
                db.SaveChanges();
                var dtlObjList = new List<tblDefineFeesDtl>();
                var detailsData = form["feedata"].Split('|');
                foreach (var d in detailsData)
                {
                    var currentItem = d.Split(',');
                    int id = int.Parse(currentItem[0]);
                    var obj = db.tblFeesType.Where(x => x.FeesTypeID == id).FirstOrDefault();

                    dtlObjList.Add(new tblDefineFeesDtl { 
                    DefineFeesID = defineFee.DefineFeesID,
                    FeesTypeID = obj.FeesTypeID,
                    tblFeesType = obj,
                    });
                }
                db.tblDefineFeesDtl.AddRange(dtlObjList);
                
                return RedirectToAction("Index");
            }

            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tbldefinefeesmst.ClassID);
            ViewBag.SectionID = new SelectList(db.tblSection.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "SectionID", "SectionName", tbldefinefeesmst.SectionID);
            ViewBag.StageID = new SelectList(db.tblStage.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "StageID", "StageName", tbldefinefeesmst.StageID);
            return View(tbldefinefeesmst);
        }

        // GET: /definefees/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDefineFeesMst tbldefinefeesmst = db.tblDefineFeesMst.Include(t=> t.tblDefineFeesDtl).Where(x=>x.DefineFeesID == id).FirstOrDefault();
            if (tbldefinefeesmst == null)
            {
                return HttpNotFound();
            }

            var feeTypes = db.tblFeesType.ToList();
            ViewBag.feeTypes = feeTypes;
            ViewBag.ClassList = db.tblClassMst.ToList();
            ViewBag.SectionList = db.tblSection.ToList();
            ViewBag.StageList = db.tblStage.ToList();
            return View(tbldefinefeesmst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, tblDefineFeesMst tbldefinefeesmst, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                tbldefinefeesmst.TotalFees = int.Parse(form["totalFee"]);
                db.SaveChanges();
                var dtlObjList = new List<tblDefineFeesDtl>();
                var detailsData = form["feedata"].Split('|');
                var existingDtlObjList = db.tblDefineFeesDtl.Where(x => x.DefineFeesID == id).ToList();
                db.tblDefineFeesDtl.RemoveRange(existingDtlObjList);
                db.SaveChanges();
                foreach (var d in detailsData)
                {
                    var currentItem = d.Split(',');
                    int feeTypeID = int.Parse(currentItem[0]);
                    var obj = db.tblFeesType.Where(x => x.FeesTypeID == feeTypeID).FirstOrDefault();

                    dtlObjList.Add(new tblDefineFeesDtl
                    {
                        DefineFeesID = tbldefinefeesmst.DefineFeesID,
                        FeesTypeID = obj.FeesTypeID,
                        tblFeesType = obj,
                    });
                }
                db.tblDefineFeesDtl.AddRange(dtlObjList);
                db.Entry(tbldefinefeesmst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClassID = new SelectList(db.tblClassMst, "ClassID", "ClassName", tbldefinefeesmst.ClassID);
            ViewBag.SectionID = new SelectList(db.tblSection, "SectionID", "SectionName", tbldefinefeesmst.SectionID);
            ViewBag.StageID = new SelectList(db.tblStage, "StageID", "StageName", tbldefinefeesmst.StageID);
            return View(tbldefinefeesmst);
        }

        public ActionResult Delete(long? id)
        {
            tblDefineFeesMst tbldefinefeesmst = db.tblDefineFeesMst.Find(id);
            tbldefinefeesmst.IsDelete = true;
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
