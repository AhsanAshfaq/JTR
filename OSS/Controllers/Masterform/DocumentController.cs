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
    public class DocumentController : Controller
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

                    if (db.tblDocument.Any(a => a.DocumentID == restoreid && a.IsDelete == true))
                    {
                        tblDocument tblDocument = db.tblDocument.Find(restoreid);
                        tblDocument.IsDelete = false;
                        db.Entry(tblDocument).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Document");
                    }
                    
                }
                list = db.tblDocument.Where(a => a.IsDelete != true).OrderByDescending(a => a.DocumentID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblDocument.Where(a=>a.IsDelete==true).OrderByDescending(a => a.DocumentID).ToList();
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
            tblDocument tblDocument = db.tblDocument.Find(id);
            if (tblDocument == null)
            {
                return HttpNotFound();
            }
            return View(tblDocument);
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
        public ActionResult Create([Bind(Include="DocumentID,DocumentName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblDocument tblDocument)
        {
            if (ModelState.IsValid)
            {
                db.tblDocument.Add(tblDocument);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblDocument);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDocument tblDocument = db.tblDocument.Find(id);
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName" ,tblDocument.DocumentID);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tblDocument.SchoolID);
            if (tblDocument == null)
            {
                return HttpNotFound();
            }
            return View(tblDocument);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="DocumentID,DocumentName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblDocument tblDocument)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblDocument).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblDocument);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDocument tblDocument = db.tblDocument.Find(id);
            if (tblDocument == null)
            {
                return HttpNotFound();
            }
            return View(tblDocument);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDocument tblDocument = db.tblDocument.Find(id);
            db.tblDocument.Remove(tblDocument);
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
