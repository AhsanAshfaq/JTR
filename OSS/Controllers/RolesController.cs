using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSS.Models;
using Newtonsoft.Json;

namespace OSS.Controllers
{
    public class RolesController : Controller
    {
        private OssEntities db = new OssEntities();

        // GET: /Roles/

        public ActionResult Save(string Details, string Master)
        {
            try
            {
                dynamic master = JsonConvert.DeserializeObject(Master);
                dynamic details = JsonConvert.DeserializeObject(Details);

                tblRoles role = new tblRoles();
                foreach (var m in master)
                {
                    role.RoleName = m._RoleName;
                    role.CreateDate = portalutilities._GetDate;
                    role.CreateBy = portalutilities._username;
                    role.IsDelete = false;
                    role.IsActive = m._IsAcive;
                    role.SchoolID = portalutilities._schollid;
                    role.IsDelete = false;
                    db.tblRoles.Add(role);
                }
                db.SaveChanges();
                int RoleID = role.RoleID;


                foreach (var d in details)
                {
                    tblRights rights = new tblRights();
                    rights.RoleID = RoleID;
                    rights.FormID =d._formid;
                    rights.IsView = d._isview;
                    rights.IsSave = d._issave;
                    rights.IsUpdate = d._isupdate;
                    rights.IsDelete = d._isdelete;
                    rights.IsPrint = d._isprint;
                    rights.IsRestore = d._isrestore;
                    db.tblRights.Add(rights);

                }
                db.SaveChanges();
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditRecord(string Details, string Master)
        {
            try
            {
                dynamic master = JsonConvert.DeserializeObject(Master);
                dynamic details = JsonConvert.DeserializeObject(Details);

                int MSID = 0;
                foreach (var m in master)
                {
                    int id = Convert.ToInt32(m._RoleID);
                    tblRoles role = db.tblRoles.Find(id);
                    role.RoleName = m._RoleName;
                    role.IsActive = m._IsActive;

                    db.tblRoles.Add(role);
                    db.Entry(role).State = EntityState.Modified;
                    MSID = role.RoleID;
                }
                db.SaveChanges();
                
                foreach (var d in details)
                {
                    int id = Convert.ToInt32(d._RightID);
                    tblRights form = db.tblRights.Find(id);

                    form.RoleID = MSID;
                    form.FormID = d._formid;
                    form.IsView = d._isview;
                    form.IsSave = d._issave;
                    form.IsUpdate = d._isupdate;
                    form.IsDelete = d._isdelete;
                    form.IsPrint = d._isprint;
                    form.IsRestore = d._isrestore;
                    db.Entry(form).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch
            {

            }
            return RedirectToAction("Index");
        }

        public ActionResult Index()
        {
            return View(db.tblRoles.ToList());
        }

        // GET: /Roles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRoles tblrole = db.tblRoles.Find(id);
            if (tblrole == null)
            {
                return HttpNotFound();
            }
            return View(tblrole);
        }

        // GET: /Roles/Create
        public ActionResult Create()
        {
            tblForms form = new tblForms();
            var from = db.tblForms.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid && a.FormName !=null).ToList();

            return View(from);
        }

        // POST: /Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoleID,RoleName,SchoolID,IsActive,IsDelete,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate")] tblRoles tblrole)
        {
            if (ModelState.IsValid)
            {
                db.tblRoles.Add(tblrole);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblrole);
        }

        // GET: /Roles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.role = db.tblRoles.Where(a=>a.RoleID==id).ToList();

            var from = db.tblRights.Where(a=>a.RoleID==id).ToList();
            ViewBag.form = from;
            if (from == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: /Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoleID,RoleName,SchoolID,IsActive,IsDelete,CreateBy,CreateDate,UpdateBy,UpdateDate,DeleteBy,DeleteDate")] tblRoles tblrole)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblrole).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblrole);
        }

        // GET: /Roles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblRoles tblrole = db.tblRoles.Find(id);
            if (tblrole == null)
            {
                return HttpNotFound();
            }
            return View(tblrole);
        }

        // POST: /Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblRoles tblrole = db.tblRoles.Find(id);
            db.tblRoles.Remove(tblrole);
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
