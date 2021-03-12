using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSS.Models;
using Newtonsoft.Json;

namespace OSS.Controllers
{
    public class ClassController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /Class/
        public ActionResult Index()
        {
            return View(db.tblClassMst.ToList());
        }

        // GET: /Class/Details/5

        public ActionResult Save(string Details, string Master)
        {
            try
            {
                dynamic master = JsonConvert.DeserializeObject(Master);
                dynamic details = JsonConvert.DeserializeObject(Details);

                tblClassMst cls = new tblClassMst();
                foreach (var m in master)
                {
                    cls.ClassName = m._ClassName;
                    cls.StageID = m._StageID;
                    cls.Prefix = m._Prefix;
                    cls.IsActive = m._IsAcive;
                    cls.IsDarsgah = m._risdarasgah;
                    cls.GenerateFees = m._generatefees;
                    cls.SchoolID = portalutilities._schollid;
                    cls.CreateDate = portalutilities._GetDate;
                    cls.CreateBy = portalutilities._username;
                    cls.IsDelete = false;
                    db.tblClassMst.Add(cls);
                    db.SaveChanges();
                }

                int ClassID = cls.ClassID;
              //  List<tblClassDtl> cld1 = new List<tblClassDtl>();
                tblClassDtl cld=new tblClassDtl();
                foreach (var  d in details)
                {
                    
                    if (d._SectionID > 0) { 
                    cld.ClassID = ClassID;
                    cld.SectionID =d._SectionID;
                    db.tblClassDtl.Add(cld);
                    
                    }
                }
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditRecord(string Details, string Master)
        {
            try
            {
                dynamic master = JsonConvert.DeserializeObject(Master);
                dynamic details = JsonConvert.DeserializeObject(Details);

                int ClassID=0;
                
                foreach (var m in master)
                {
                    ClassID = Convert.ToInt32(m._ClassID);
                    tblClassMst cls = db.tblClassMst.Find(ClassID);
                    cls.ClassName = m._ClassName;
                    cls.StageID = m._StageID;
                    cls.Prefix = m._Prefix;
                    cls.IsActive = m._IsAcive;
                    cls.IsDarsgah = m._risdarasgah;
                    cls.GenerateFees = m._generatefees;
                    cls.SchoolID = portalutilities._schollid;
                    cls.CreateDate = portalutilities._GetDate;
                    cls.CreateBy = portalutilities._username;
                    cls.IsDelete = false;
                    db.tblClassMst.Add(cls);
                    db.Entry(cls).State = EntityState.Modified;
                    db.SaveChanges();
                    ClassID = cls.ClassID;
                }

                
              //  List<tblClassDtl> cld1 = new List<tblClassDtl>();
               // tblClassDtl cld = new tblClassDtl();
                int sectionid=0;
                foreach (var d in details)
                {
                    
                    if (d._SectionID > 0)
                    {
                        sectionid= Convert.ToInt32(d._SectionID);
                        tblClassDtl cld = db.tblClassDtl.Where(a => a.SectionID == sectionid && a.ClassID == ClassID).FirstOrDefault();
                        cld.ClassID = ClassID;
                        cld.SectionID = d._SectionID;
                        db.tblClassDtl.Add(cld);
                        if (cld != null)
                        {
                            db.Entry(cld).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            db.SaveChanges();
                        }
                    }
                }
                
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }



        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClassMst tblclassmst = db.tblClassMst.Find(id);
            if (tblclassmst == null)
            {
                return HttpNotFound();
            }
            return View(tblclassmst);
        }

        // GET: /Class/Create
        public ActionResult Create()
        {
            ViewBag.StageID = new SelectList(db.tblStage.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "StageID", "StageName");
            ViewBag.section = db.tblSection.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid).ToList();
            return View();
        }

        // POST: /Class/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ClassID,ClassName,StageID,CreateBy,CreateDate,DeleteBy,DeleteDate,UpdateBy,UpdateDate,IsDelete,IsActive,SchoolID,Prefix,IsDarsgah,GenerateFees")] tblClassMst tblclassmst)
        {
            if (ModelState.IsValid)
            {
                db.tblClassMst.Add(tblclassmst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblclassmst);
        }

        // GET: /Class/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClassMst tblclassmst = db.tblClassMst.Find(id);
            if (tblclassmst == null)
            {
                return HttpNotFound();
            }
            ViewBag.StageID = new SelectList(db.tblStage.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "StageID", "StageName", tblclassmst.StageID);
            ViewBag.section = db.tblSection.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid).ToList();
            return View(tblclassmst);
        }

        // POST: /Class/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ClassID,ClassName,StageID,CreateBy,CreateDate,DeleteBy,DeleteDate,UpdateBy,UpdateDate,IsDelete,IsActive,SchoolID,Prefix,IsDarsgah,GenerateFees")] tblClassMst tblclassmst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblclassmst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblclassmst);
        }

        // GET: /Class/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblClassMst tblclassmst = db.tblClassMst.Find(id);
            if (tblclassmst == null)
            {
                return HttpNotFound();
            }
            return View(tblclassmst);
        }

        // POST: /Class/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblClassMst tblclassmst = db.tblClassMst.Find(id);
            db.tblClassMst.Remove(tblclassmst);
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
