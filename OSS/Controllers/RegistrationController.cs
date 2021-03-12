using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OSS.Models;
using OSS.Models.viewmodel;

namespace OSS.Controllers
{
    public class RegistrationController : Controller
    {
        private OssEntities db = new OssEntities();
        
        // GET: /Registration/
       // int portalutilities._schollid = Convert.ToInt32(portalutilities._schollid);
        public ActionResult Index()
        {
            
            return View(db.tblStudentRegMst.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid).OrderByDescending(a => a.RegID).Take(100).ToList());
        }
        public void dropdownlist()
        {
            ViewBag.ReligionID = new SelectList(db.tblReligion.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ReligionID", "Religion");
            ViewBag.StageID = new SelectList(db.tblStage.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "StageID", "StageName");
            ViewBag.SessionID = new SelectList(db.tblSession.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "SessionID", "SessionName");
            ViewBag.CountryID = new SelectList(db.tblCountry.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "CountryID", "CountryName");
            ViewBag.BankID = new SelectList(db.tblBank.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "BankID", "BankName");
            ViewBag.DiscountID = new SelectList(db.tblDiscount.Where(a => a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "DiscountID", "DiscountName");
        }
        public ActionResult fill_dropdown(int id ,string name)
        {
            var result=(dynamic)null;
            switch (name)
            {
                case "StageID":
                   // bind stage to class
                     result = (from s in db.tblClassMst
                               where (s.StageID == id && s.IsDelete != true && s.IsActive == true && s.SchoolID == portalutilities._schollid)
                          select new
                          {
                              id = s.ClassID,
                              name = s.ClassName
                          }).ToList();
                        
                    break;
                case "ClassID":
                    // bind class to section
                    result = (from _tblClassDtls in db.tblClassDtl
                              join jsec in db.tblSection on _tblClassDtls.SectionID equals jsec.SectionID
                              where (_tblClassDtls.ClassID == id && jsec.IsActive == true && jsec.IsDelete != true && jsec.SchoolID == portalutilities._schollid)
                                  select new
                                  {
                                      id = jsec.SectionID,
                                      name = jsec.SectionName
                                  }).ToList();
                    break;
                case "CountryID":
                    // bind country to province
                    result = (from _tblProvinces in db.tblProvince
                              where (_tblProvinces.CountryID == id && _tblProvinces.IsActive == true && _tblProvinces.IsDelete != true && _tblProvinces.SchoolID == portalutilities._schollid)
                              select new
                              {
                                  id = _tblProvinces.ProvinceID,
                                  name = _tblProvinces.ProvinceName
                              }).ToList();
                    break;
                case "ProvinceID":
                    // bind ProvinceID to CityID
                    result = (from _tblCities in db.tblCity

                              where (_tblCities.ProvinceID == id && _tblCities.IsActive == true && _tblCities.IsDelete != true && _tblCities.SchoolID == portalutilities._schollid)
                              select new
                              {
                                  id = _tblCities.CityID,
                                  name = _tblCities.CityName
                              }).ToList();
                    break;

                case "CityID":
                    // bind CityID to AreaID
                    result = (from _tblAreas in db.tblArea

                              where (_tblAreas.ProvinceID == id && _tblAreas.IsActive == true && _tblAreas.IsDelete != true && _tblAreas.SchoolID == portalutilities._schollid)
                              select new
                              {
                                  id = _tblAreas.AreaID,
                                  name = _tblAreas.AreaName
                              }).ToList();
                    break;
                default:
                //    Console.WriteLine("Default case");
                    break;
                    
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        // GET: /Registration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudentRegMst tblstudentregmst = db.tblStudentRegMst.Find(id);
            if (tblstudentregmst == null)
            {
                return HttpNotFound();
            }
            return View(tblstudentregmst);
        }

        // GET: /Registration/Create
        public ActionResult Create()
        {
            dropdownlist();
            return View();
        }

        // POST: /Registration/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RegID,RegNo,ApplicantName,ApplicantNameInUrdu,FatherNameInUrdu,AddressInUrdu,DateOfBirth,ImagePath,LOGO,StageID,ClassID,DarsgahID,SectionID,Age,BloodGroup,BFormNo,PhoneNo,MobileNo,TemporaryAddress,PermanentAddress,Gender,FatherName,FatherNIC,FatherMobileNo,FatherOccupation,FatherQualification,FatherMonthlySalary,FatherOfficeAddress,FatherOfficePhoneNo,MotherName,MotherNIC,MotherMobileNo,MotherOccupation,MotherQualification,MotherMonthlySalary,MotherOfficeAddress,MotherOfficePhoneNo,DeleteBy,DeleteDate,CreateBy,CreateDate,UpdateBy,UpdateDate,IsDelete,IsActive,IsAdmit,PostDate,portalutilities._schollid,SessionID,Remarks,ReligionID,AreaID,CityID,ProvinceID,CountryID,GardianName,LastClassAttended,LastSchoolAttended,NativeLang,RelationtoStu,NameofSignatory,IsSendMsgtoFather,IsSendMsgtoMother,PortalID,DateOfAdmission,DateOfRemoval,ClassAtTimeOfRemoval,CauseOfRemoval")] tblStudentRegMst tblstudentregmst)
        {
            //if (ModelState.IsValid)
            
          #region Registration
                tblstudentregmst.CreateDate = portalutilities._GetDate;
                tblstudentregmst.CreateBy = portalutilities._username;
                tblstudentregmst.IsDelete = false;
                tblstudentregmst.IsActive = true;
                tblstudentregmst.SchoolID = portalutilities._schollid;
                tblstudentregmst.IsDelete = false;
                db.tblStudentRegMst.Add(tblstudentregmst);
                db.SaveChanges();
          #endregion

          #region Adminssion

                tblAdmission adm = new tblAdmission();
                    adm.RegID = tblstudentregmst.RegID;
                    adm.Remarks = tblstudentregmst.Remarks;
                    adm.RollNo = Convert.ToInt32(Request["RollNo"]);
                    adm.GRNo = (Request["GRNo"]).ToString();
                    adm.BankID = Convert.ToInt32(Request["BankID"]);
                    adm.ChallanNo = Convert.ToInt32(Request["ChallanNo"]);
                    adm.DiscountID = Convert.ToInt32(Request["DiscountID"]);
                    adm.Hostel = Convert.ToBoolean(Request["Hostel"]);
                    adm.PostDate = tblstudentregmst.PostDate;
                    adm.CreateDate = portalutilities._GetDate;
                    adm.CreateBy = portalutilities._username;
                    adm.IsDelete = false;
                    adm.IsActive = true;
                    adm.SchoolID = portalutilities._schollid;
                    adm.IsDelete = false;
                    db.tblAdmission.Add(adm);
                    db.SaveChanges();
           #endregion
               
             //   
                return RedirectToAction("Index");
            

            return View(tblstudentregmst);
        }

        // GET: /Registration/Edit/5
        public ActionResult Edit(int? id)
        {
            dropdownlist();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblStudentRegMst tblstudentregmst = db.tblStudentRegMst.Find(id);
            ViewBag.tbladmission = db.tblAdmission.Where(a => a.RegID == tblstudentregmst.RegID).FirstOrDefault();
          //  ViewBag.ClassID = new SelectList(from s in db.tblClassMst where (s.StageID == tblstudentregmst.StageID && s.IsDelete != true && s.IsActive == true && s.SchoolID == portalutilities._schollid) select new { s.ClassID, s.ClassName }, "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            ViewBag.ClassID = new SelectList(db.tblClassMst.Where(a => a.StageID == tblstudentregmst.StageID && a.IsDelete != true && a.IsActive == true && a.SchoolID == portalutilities._schollid), "ClassID", "ClassName", tblstudentregmst.ClassID);
            
            if (tblstudentregmst == null)
            {
                return HttpNotFound();
            }
            return View(tblstudentregmst);
        }

        // POST: /Registration/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RegID,RegNo,ApplicantName,ApplicantNameInUrdu,FatherNameInUrdu,AddressInUrdu,DateOfBirth,ImagePath,LOGO,StageID,ClassID,DarsgahID,SectionID,Age,BloodGroup,BFormNo,PhoneNo,MobileNo,TemporaryAddress,PermanentAddress,Gender,FatherName,FatherNIC,FatherMobileNo,FatherOccupation,FatherQualification,FatherMonthlySalary,FatherOfficeAddress,FatherOfficePhoneNo,MotherName,MotherNIC,MotherMobileNo,MotherOccupation,MotherQualification,MotherMonthlySalary,MotherOfficeAddress,MotherOfficePhoneNo,DeleteBy,DeleteDate,CreateBy,CreateDate,UpdateBy,UpdateDate,IsDelete,IsActive,IsAdmit,PostDate,portalutilities._schollid,SessionID,Remarks,ReligionID,AreaID,CityID,ProvinceID,CountryID,GardianName,LastClassAttended,LastSchoolAttended,NativeLang,RelationtoStu,NameofSignatory,IsSendMsgtoFather,IsSendMsgtoMother,PortalID,DateOfAdmission,DateOfRemoval,ClassAtTimeOfRemoval,CauseOfRemoval")] tblStudentRegMst tblstudentregmst)
        {
           
            if (ModelState.IsValid)
            {
                db.Entry(tblstudentregmst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblstudentregmst);
        }

        // GET: /Registration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblStudentRegMst tblstudentregmst = db.tblStudentRegMst.Find(id);
            if (tblstudentregmst == null)
            {
                return HttpNotFound();
            }
            return View(tblstudentregmst);
        }

        // POST: /Registration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStudentRegMst tblstudentregmst = db.tblStudentRegMst.Find(id);
            db.tblStudentRegMst.Remove(tblstudentregmst);
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
