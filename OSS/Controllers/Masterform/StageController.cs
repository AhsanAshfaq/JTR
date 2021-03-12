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
    public class ControllerController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /Users/
        public ActionResult Index(int isdelete=0, int restoreid=0)
        {
            var list =(dynamic)null;
            TempData["restore"] = "";
            if (isdelete == 0)
            {
                if(restoreid>0){

                    if (db.tblStage.Any(a => a.StageID == restoreid && a.IsDelete == true))
                    {
                        tblStage tblStage = db.tblStage.Find(restoreid);
                        tblStage.IsDelete = false;
                        db.Entry(tblStage).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Stage");
                    }
                    
                }
                list = db.tblStage.Where(a => a.IsDelete != true).OrderByDescending(a => a.StageID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblStage.Where(a=>a.IsDelete==true).OrderByDescending(a => a.StageID).ToList();
            }
            return View(list);
        }

        // GET: /Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStage tblStage = db.tblStage.Find(id);
            if (tblStage == null)
            {
                return HttpNotFound();
            }
            return View(tblStage);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName");
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="StageID,StageName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblStage tblStage)
        {
            if (ModelState.IsValid)
            {
                db.tblStage.Add(tblStage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblStage);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStage tblStage = db.tblStage.Find(id);
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName" ,tblStage.StageID);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tblStage.SchoolID);
            if (tblStage == null)
            {
                return HttpNotFound();
            }
            return View(tblStage);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="StageID,StageName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblStage tblStage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblStage).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblStage);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStage tblStage = db.tblStage.Find(id);
            if (tblStage == null)
            {
                return HttpNotFound();
            }
            return View(tblStage);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStage tblStage = db.tblStage.Find(id);
            db.tblStage.Remove(tblStage);
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
