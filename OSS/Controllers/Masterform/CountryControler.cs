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
    public class CountryController : Controller
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

                    if (db.tblCountry.Any(a => a.CountryID == restoreid && a.IsDelete == true))
                    {
                        tblCountry tblCountry = db.tblCountry.Find(restoreid);
                        tblCountry.IsDelete = false;
                        db.Entry(tblCountry).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Country");
                    }
                    
                }
                list = db.tblCountry.Where(a => a.IsDelete != true).OrderByDescending(a => a.CountryID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblCountry.Where(a=>a.IsDelete==true).OrderByDescending(a => a.CountryID).ToList();
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
            tblCountry tblCountry = db.tblCountry.Find(id);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
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
        public ActionResult Create([Bind(Include="CountryID,CountryName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblCountry tblCountry)
        {
            if (ModelState.IsValid)
            {
                db.tblCountry.Add(tblCountry);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCountry);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCountry tblCountry = db.tblCountry.Find(id);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tblCountry.SchoolID);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CountryID,CountryName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblCountry tblCountry)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCountry).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblCountry);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCountry tblCountry = db.tblCountry.Find(id);
            if (tblCountry == null)
            {
                return HttpNotFound();
            }
            return View(tblCountry);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCountry tblCountry = db.tblCountry.Find(id);
            db.tblCountry.Remove(tblCountry);
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
