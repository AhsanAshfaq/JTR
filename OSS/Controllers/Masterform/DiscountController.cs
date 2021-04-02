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
    public class DiscountController : Controller
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
                    
                    if (db.tblDiscount.Any(a => a.DiscountID == restoreid && a.IsDelete == true))
                    {
                        tblDiscount tbldiscount = db.tblDiscount.Find(restoreid);
                        tbldiscount.IsDelete = false;
                        db.Entry(tbldiscount).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                       // return View("Index","User");
                    }
                }
                list = db.tblDiscount.Where(a => a.IsDelete != true).OrderByDescending(a => a.DiscountID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblDiscount.Where(a=>a.IsDelete==true).OrderByDescending(a => a.DiscountID).ToList();
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
            tblDiscount tbldiscount = db.tblDiscount.Find(id);
            if (tbldiscount == null)
            {
                return HttpNotFound();
            }
            return View(tbldiscount);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            ViewBag.DiscountType = new SelectList(db.tblDiscount, "DiscountID", "DiscountType");
            return View();
        }

        // POST: /Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "DiscountID,DiscountCode,DiscountType,DiscountName,DiscountAmount,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsDelete,IsActive,SchoolID,DeleteBy,DeleteDate")] tblDiscount tbldiscount)
        {
            if (ModelState.IsValid)
            {
                db.tblDiscount.Add(tbldiscount);
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tbldiscount);
        }

        // GET: /Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiscount tbldiscount = db.tblDiscount.Find(id);

            ViewBag.DiscountType = new SelectList(db.tblDiscount, "DiscountID", "DiscountType", tbldiscount.DiscountID);
            if (tbldiscount == null)
            {
                return HttpNotFound();
            }
            return View(tbldiscount);
        }
        
        // POST: /Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountID,DiscountCode,DiscountType,DiscountName,DiscountAmount,CreatedBy,UpdatedBy,CreateDate,UpdateDate,IsDelete,IsActive,SchoolID,DeleteBy,DeleteDate")] tblDiscount tbldiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tbldiscount).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tbldiscount);
        }

        // GET: /Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblDiscount tbldiscount = db.tblDiscount.Find(id);
            if (tbldiscount == null)
            {
                return HttpNotFound();
            }
            return View(tbldiscount);
        }

        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblDiscount tbldiscount = db.tblDiscount.Find(id);
            db.tblDiscount.Remove(tbldiscount);
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
