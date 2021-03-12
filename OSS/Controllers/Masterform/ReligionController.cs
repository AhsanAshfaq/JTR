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
    public class ReligionController : Controller
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

                    if (db.tblReligion.Any(a => a.ReligionID == restoreid && a.IsDelete == true))
                    {
                        tblReligion tblReligion = db.tblReligion.Find(restoreid);
                        tblReligion.IsDelete = false;
                        db.Entry(tblReligion).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Religion");
                    }
                    
                }
                list = db.tblReligion.Where(a => a.IsDelete != true).OrderByDescending(a => a.ReligionID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblReligion.Where(a=>a.IsDelete==true).OrderByDescending(a => a.ReligionID).ToList();
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
            tblReligion tblReligion = db.tblReligion.Find(id);
            if (tblReligion == null)
            {
                return HttpNotFound();
            }
            return View(tblReligion);
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
        public ActionResult Create([Bind(Include="ReligionID,ReligionName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblReligion tblReligion)
        {
            if (ModelState.IsValid)
            {
                db.tblReligion.Add(tblReligion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblReligion);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReligion tblReligion = db.tblReligion.Find(id);
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName" ,tblReligion.ReligionID);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tblReligion.SchoolID);
            if (tblReligion == null)
            {
                return HttpNotFound();
            }
            return View(tblReligion);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ReligionID,ReligionName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblReligion tblReligion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblReligion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblReligion);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblReligion tblReligion = db.tblReligion.Find(id);
            if (tblReligion == null)
            {
                return HttpNotFound();
            }
            return View(tblReligion);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblReligion tblReligion = db.tblReligion.Find(id);
            db.tblReligion.Remove(tblReligion);
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
