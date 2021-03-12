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
    public class ProvinceController : Controller
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

                    if (db.tblProvince.Any(a => a.ProvinceID == restoreid && a.IsDelete == true))
                    {
                        tblProvince tblProvince = db.tblProvince.Find(restoreid);
                        tblProvince.IsDelete = false;
                        db.Entry(tblProvince).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Province");
                    }
                    
                }
                list = db.tblProvince.Where(a => a.IsDelete != true).OrderByDescending(a => a.ProvinceID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblProvince.Where(a=>a.IsDelete==true).OrderByDescending(a => a.ProvinceID).ToList();
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
            tblProvince tblProvince = db.tblProvince.Find(id);
            if (tblProvince == null)
            {
                return HttpNotFound();
            }
            return View(tblProvince);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.tblCountry, "CountryID", "CountryName");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProvinceID,ProvinceName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,CountryID,DeleteBy,DeleteDate")] tblProvince tblProvince)
        {
            if (ModelState.IsValid)
            {
                db.tblProvince.Add(tblProvince);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblProvince);
        }

        // GET: /Country/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProvince tblProvince = db.tblProvince.Find(id);
            ViewBag.CountryID = new SelectList(db.tblCountry, "CountryID", "CountryName" ,tblProvince.CountryID);
            if (tblProvince == null)
            {
                return HttpNotFound();
            }
            return View(tblProvince);
        }

        
        // POST: /Country/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProvinceID,ProvinceName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,ProvinceID,DeleteBy,DeleteDate")] tblProvince tblProvince)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblProvince).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblProvince);
        }

        // GET: /Country/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblProvince tblProvince = db.tblProvince.Find(id);
            if (tblProvince == null)
            {
                return HttpNotFound();
            }
            return View(tblProvince);
        }

        // POST: /Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblProvince tblProvince = db.tblProvince.Find(id);
            db.tblProvince.Remove(tblProvince);
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
