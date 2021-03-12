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
    public class CityController : Controller
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

                    if (db.tblCity.Any(a => a.CityID == restoreid && a.IsDelete == true))
                    {
                        tblCity tblCity = db.tblCity.Find(restoreid);
                        tblCity.IsDelete = false;
                        db.Entry(tblCity).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","City");
                    }
                    
                }
                list = db.tblCity.Where(a => a.IsDelete != true).OrderByDescending(a => a.CityID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblCity.Where(a=>a.IsDelete==true).OrderByDescending(a => a.CityID).ToList();
            }
            return View(list);
        }

        // GET: /City/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCity.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.RoleID = new SelectList(db.tblRoles, "CountryID", "CountryName");
            ViewBag.RoleID = new SelectList(db.tblRoles, "ProvinceID", "ProvinceName");
            return View();
        }

        // POST: /City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CityID,CityName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,CountryID,ProvinceID,DeleteBy,DeleteDate")] tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                db.tblCity.Add(tblCity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblCity);
        }

        // GET: /City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCity.Find(id);
            ViewBag.CountryID = new SelectList(db.tblCountry, "CountryID", "CountryName" ,tblCity.CountryID);
            ViewBag.ProvinceID = new SelectList(db.tblProvince, "ProvinceID", "ProvinceName", tblCity.ProvinceID);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        
        // POST: /City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CityID,CityName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,CountryID,ProvinceID,DeleteBy,DeleteDate")] tblCity tblCity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblCity).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblCity);
        }

        // GET: /City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblCity tblCity = db.tblCity.Find(id);
            if (tblCity == null)
            {
                return HttpNotFound();
            }
            return View(tblCity);
        }

        // POST: /City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCity tblCity = db.tblCity.Find(id);
            db.tblCity.Remove(tblCity);
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
