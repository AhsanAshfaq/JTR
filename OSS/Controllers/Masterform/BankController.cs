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
    public class BankController : Controller
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

                    if (db.tblBank.Any(a => a.BankID == restoreid && a.IsDelete == true))
                    {
                        tblBank tblBank = db.tblBank.Find(restoreid);
                        tblBank.IsDelete = false;
                        db.Entry(tblBank).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Bank");
                    }
                    
                }
                list = db.tblBank.Where(a => a.IsDelete != true).OrderByDescending(a => a.BankID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblBank.Where(a=>a.IsDelete==true).OrderByDescending(a => a.BankID).ToList();
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
            tblBank tblBank = db.tblBank.Find(id);
            if (tblBank == null)
            {
                return HttpNotFound();
            }
            return View(tblBank);
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
        public ActionResult Create([Bind(Include="BankID,BankName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblBank tblBank)
        {
            if (ModelState.IsValid)
            {
                db.tblBank.Add(tblBank);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblBank);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBank tblBank = db.tblBank.Find(id);
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName" ,tblBank.BankID);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tblBank.SchoolID);
            if (tblBank == null)
            {
                return HttpNotFound();
            }
            return View(tblBank);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="BankID,BankName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblBank tblBank)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblBank).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblBank);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblBank tblBank = db.tblBank.Find(id);
            if (tblBank == null)
            {
                return HttpNotFound();
            }
            return View(tblBank);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblBank tblBank = db.tblBank.Find(id);
            db.tblBank.Remove(tblBank);
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
