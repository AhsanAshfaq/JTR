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
    public class FeesTypeController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /Users/
        public ActionResult Index(int isdelete = 0, int restoreid = 0)
        {
            var list = (dynamic)null;
            TempData["restore"] = "";
            if (isdelete == 0)
            {
                if (restoreid > 0)
                {
                    if (db.tblFeesType.Any(a => a.FeesTypeID == restoreid && a.IsDelete == true))
                    {
                        tblFeesType tblFeesType = db.tblFeesType.Find(restoreid);
                        tblFeesType.IsDelete = false;
                        db.Entry(tblFeesType).State = EntityState.Modified;
                        db.SaveChanges();
                        TempData["msg"] = "Record Restore Successfully";
                        // return View("Index","FeesType");
                    }

                }
                list = db.tblFeesType.Where(a => a.IsDelete != true).OrderByDescending(a => a.FeesTypeID).ToList();
            }
            else
            {
                TempData["restore"] = "restore";
                list = db.tblFeesType.Where(a => a.IsDelete == true).OrderByDescending(a => a.FeesTypeID).ToList();
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
            tblFeesType tblFeesType = db.tblFeesType.Find(id);
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            //ViewBag.ChargeFee = new SelectList(db.tblRoles, "CountryID", "CountryName");
            ViewBag.ChargeFee = portalutilities.BindChargeFee();
            return View();
        }

        // POST: /City/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FeesTypeID,FeesType,FeesTypeInUrdu,FeesAmount,ChargeFee,DiscountAble,RefundAble,CreatedBy,CreateDate,DeleteBy,DeleteDate,,UpdatedBy,UpdateDate,IsDelete,IsActive,SchoolID")] tblFeesType tblFeesType)
        {
            if (ModelState.IsValid)
            {
                db.tblFeesType.Add(tblFeesType);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblFeesType);
        }

        // GET: /City/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFeesType tblFeesType = db.tblFeesType.Find(id);
            //ViewBag.ChargeFee = new SelectList(db.tblRoles, "CountryID", "CountryName");
            ViewBag.ChargeFee = portalutilities.BindChargeFee();
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // POST: /City/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FeesTypeID,FeesType,FeesTypeInUrdu,FeesAmount,ChargeFee,DiscountAble,RefundAble,CreatedBy,CreateDate,DeleteBy,DeleteDate,,UpdatedBy,UpdateDate,IsDelete,IsActive,SchoolID")] tblFeesType tblFeesType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblFeesType).State = EntityState.Modified;
                db.SaveChanges();
                TempData["msg"] = "Record Update Successfully";
                return RedirectToAction("Index");
            }
            return View(tblFeesType);
        }

        // GET: /City/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblFeesType tblFeesType = db.tblFeesType.Find(id);
            if (tblFeesType == null)
            {
                return HttpNotFound();
            }
            return View(tblFeesType);
        }

        // POST: /City/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblFeesType tblFeesType = db.tblFeesType.Find(id);
            db.tblFeesType.Remove(tblFeesType);
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

        //public object BindChargeFee()
        //{
        //    List<string> obj = new List<string>();
        //    portalutilities.ChargeFee pMS = new portalutilities.ChargeFee();

        //    FieldInfo[] fields = pMS.GetType().GetFields();
        //    int k = 0;
        //    foreach (var xInfo in fields)
        //    {
        //        //cmbChargeFee.Items.Insert(k, xInfo.GetValue(pMS).ToString());
        //        obj.Insert(k, xInfo.GetValue(pMS).ToString());
        //        k++;
        //    }

        //    return obj.ToList();
        //}
    }
}
