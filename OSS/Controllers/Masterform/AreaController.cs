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
    public class AreaController : Controller
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

                    if (db.tblArea.Any(a => a.AreaID == restoreid && a.IsDelete == true))
                    {
                        tblArea tblArea = db.tblArea.Find(restoreid);
                        tblArea.IsDelete = false;
                        db.Entry(tblArea).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","Area");
                    }
                    
                }
                list = db.tblArea.Where(a => a.IsDelete != true).OrderByDescending(a => a.AreaID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblArea.Where(a=>a.IsDelete==true).OrderByDescending(a => a.AreaID).ToList();
            }
            return View(list);
        }

        // GET: /Area/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArea tblArea = db.tblArea.Find(id);
            if (tblArea == null)
            {
                return HttpNotFound();
            }
            return View(tblArea);
        }

        // GET: /Area/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.tblCountry, "CountryID", "CountryName");
            ViewBag.ProvinceID = new SelectList(db.tblProvince, "PrvinceID", "ProvinceName");
            ViewBag.CityID = new SelectList(db.tblCity, "CityID", "CityName");
            return View();
        }

        // POST: /Area/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="AreaID,AreaName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,CountryID,ProvinceID,CityID,DeleteBy,DeleteDate")] tblArea tblArea)
        {
            if (ModelState.IsValid)
            {
                db.tblArea.Add(tblArea);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblArea);
        }

        // GET: /Area/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArea tblArea = db.tblArea.Find(id);
            ViewBag.CountryID = new SelectList(db.tblCountry, "CountryID", "CountryName" ,tblArea.CountryID);
            ViewBag.ProvinceID = new SelectList(db.tblProvince, "ProvinceID", "ProvinceName", tblArea.ProvinceID);
            ViewBag.CityID = new SelectList(db.tblCity, "CityID", "CityName", tblArea.CityID);
            if (tblArea == null)
            {
                return HttpNotFound();
            }
            return View(tblArea);
        }

        
        // POST: /Area/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="AreaID,AreaName,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsLogin,IsDelete,IsActive,CountryID,ProvinceID,CityID,DeleteBy,DeleteDate")] tblArea tblArea)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblArea).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblArea);
        }

        // GET: /Area/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblArea tblArea = db.tblArea.Find(id);
            if (tblArea == null)
            {
                return HttpNotFound();
            }
            return View(tblArea);
        }

        // POST: /Area/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblArea tblArea = db.tblArea.Find(id);
            db.tblArea.Remove(tblArea);
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
