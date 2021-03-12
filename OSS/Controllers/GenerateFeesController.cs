using OSS.Models;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class GenerateFeesController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: tblGenerateFeesMsts
        public ActionResult Index()
        {
            ViewBag.StageList = db.tblStage.ToList();

            return View();
        }

        // GET: tblGenerateFeesMsts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        // GET: tblGenerateFeesMsts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: tblGenerateFeesMsts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenFeesMstID,GenFeesNo,StageID,ClassID,SectionID,TotalStudents,TotalFeesAmount,TotalDiscountAmount,TotalNetFees,IsDelete,SchoolID,PostDate,FeesDate,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate,SessionID,UserID")] tblGenerateFeesMst tblGenerateFeesMst)
        {
            if (ModelState.IsValid)
            {
                db.tblGenerateFeesMst.Add(tblGenerateFeesMst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblGenerateFeesMst);
        }

        // GET: tblGenerateFeesMsts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        // POST: tblGenerateFeesMsts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GenFeesMstID,GenFeesNo,StageID,ClassID,SectionID,TotalStudents,TotalFeesAmount,TotalDiscountAmount,TotalNetFees,IsDelete,SchoolID,PostDate,FeesDate,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate,SessionID,UserID")] tblGenerateFeesMst tblGenerateFeesMst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblGenerateFeesMst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblGenerateFeesMst);
        }

        // GET: tblGenerateFeesMsts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(tblGenerateFeesMst);
        }

        // POST: tblGenerateFeesMsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            db.tblGenerateFeesMst.Remove(tblGenerateFeesMst);
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
