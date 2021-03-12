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
    public class UsersController : Controller
    {
        private OssEntities db = new OssEntities();
        public RegistrationController ab = new RegistrationController();
        
        // GET: /Users/
        public ActionResult Index(int isdelete=0, int restoreid=0)
        {
            
            var list =(dynamic)null;
            TempData["restore"] = "";
            if (isdelete == 0)
            {
                if(restoreid>0){

                    if (db.tblUser.Any(a => a.UserID == restoreid && a.IsDelete == true))
                    {
                        tblUser tbluser = db.tblUser.Find(restoreid);
                        tbluser.IsDelete = false;
                        db.Entry(tbluser).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","User");
                    }
                }
                list = db.tblUser.Where(a => a.IsDelete != true).OrderByDescending(a => a.UserID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblUser.Where(a=>a.IsDelete==true).OrderByDescending(a => a.UserID).ToList();
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
            tblUser tbluser = db.tblUser.Find(id);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View(tbluser);
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
        public ActionResult Create([Bind(Include="UserID,UserName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblUser tbluser)
        {
            if (ModelState.IsValid)
            {
                db.tblUser.Add(tbluser);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbluser);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tbluser = db.tblUser.Find(id);
            ViewBag.RoleID = new SelectList(db.tblRoles, "RoleID", "RoleName" ,tbluser.RoleID);
            ViewBag.SchoolID = new SelectList(db.tblSchool, "SchoolID", "SchoolName", tbluser.SchoolID);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View(tbluser);
        }

        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserID,UserName,Password,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,RoleID,SchoolID,DeleteBy,DeleteDate")] tblUser tbluser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbluser).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tbluser);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblUser tbluser = db.tblUser.Find(id);
            if (tbluser == null)
            {
                return HttpNotFound();
            }
            return View(tbluser);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblUser tbluser = db.tblUser.Find(id);
            db.tblUser.Remove(tbluser);
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
