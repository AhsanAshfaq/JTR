using Newtonsoft.Json;
using OSS.Models;
using OSS.Models.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class FeesReceiveController : Controller
    {
        private OssEntities db = new OssEntities();
        // GET: FeesReceive
        public ActionResult Index()
        {
            var obj = (dynamic)null;
            if (portalutilities.InEnglish == false)
            {
                if (portalutilities.LoginUserID != portalutilities.AdminUserID)
                {
                    obj = (from q in db.tblFeesReceiveMst.Cast<tblFeesReceiveMst>()
                           join e in db.tblAdmission on q.AdmissionID equals e.AdmissionID
                           join st in db.tblStudentRegMst on e.RegID equals st.RegID
                           join d in db.Vu_Class on e.AdmissionID equals d.AdmissionID
                           where q.IsDelete == false && q.SessionID == portalutilities.ActiveSessionID && d.SessionID == portalutilities.ActiveSessionID
                            && q.SchoolID == portalutilities._schollid && q.UserID == portalutilities.LoginUserID
                           orderby q.FeesReceiveMstID descending
                           select
                               new { q.FeesReceiveMstID, q.FeesReceiveNo, e.AdmissionID, ApplicantName = st.ApplicantNameInUrdu, d.ClassName, d.SectionName, q.TotalAdjustmentAmount, q.PostDate }).ToList();

                }
                else
                {
                    obj = (from q in db.tblFeesReceiveMst.Cast<tblFeesReceiveMst>()
                           join e in db.tblAdmission on q.AdmissionID equals e.AdmissionID
                           join st in db.tblStudentRegMst on e.RegID equals st.RegID
                           join d in db.Vu_Class on e.AdmissionID equals d.AdmissionID
                           where q.IsDelete == false && q.SessionID == portalutilities.ActiveSessionID && d.SessionID == portalutilities.ActiveSessionID
                            && q.SchoolID == portalutilities._schollid
                           orderby q.FeesReceiveMstID descending
                           select
                               new { q.FeesReceiveMstID, q.FeesReceiveNo, e.AdmissionID, ApplicantName = st.ApplicantNameInUrdu, d.ClassName, d.SectionName, q.TotalAdjustmentAmount, q.PostDate }).ToList();

                }
            }
            else
            {
                if (portalutilities.LoginUserID != portalutilities.AdminUserID)
                {
                    obj = (from q in db.tblFeesReceiveMst.Cast<tblFeesReceiveMst>()
                           join e in db.tblAdmission on q.AdmissionID equals e.AdmissionID
                           join st in db.tblStudentRegMst on e.RegID equals st.RegID
                           join d in db.Vu_Class on e.AdmissionID equals d.AdmissionID
                           where q.IsDelete == false && q.SessionID == portalutilities.ActiveSessionID && d.SessionID == portalutilities.ActiveSessionID
                            && q.SchoolID == portalutilities._schollid && q.UserID == portalutilities.LoginUserID
                           orderby q.FeesReceiveMstID descending
                           select
                               new { q.FeesReceiveMstID, q.FeesReceiveNo, e.AdmissionID, st.ApplicantName, d.ClassName, d.SectionName, q.TotalAdjustmentAmount, q.PostDate }).ToList();

                }
                else
                {
                    obj = (from q in db.tblFeesReceiveMst.Cast<tblFeesReceiveMst>()
                           join e in db.tblAdmission on q.AdmissionID equals e.AdmissionID
                           join st in db.tblStudentRegMst on e.RegID equals st.RegID
                           join d in db.Vu_Class on e.AdmissionID equals d.AdmissionID
                           where q.IsDelete == false && q.SessionID == portalutilities.ActiveSessionID && d.SessionID == portalutilities.ActiveSessionID
                            && q.SchoolID == portalutilities._schollid
                           orderby q.FeesReceiveMstID descending
                           select
                               new { q.FeesReceiveMstID, q.FeesReceiveNo, e.AdmissionID, st.ApplicantName, d.ClassName, d.SectionName, q.TotalAdjustmentAmount, q.PostDate }).ToList();

                }
            }

            var result = new List<FeesReceiveIndexViewModel>();

            for (int i = 0; i < obj.Count; i++)
            {
                /// TODO : This result list will contains the list (Record History Tab Data FROM Fees Receive Screen of Desktop App ) 
                result.Add(new FeesReceiveIndexViewModel
                {
                    FeesRecNo = obj[i].FeesReceiveNo,
                    ClassName = obj[i].ClassName,
                    StudentName = obj[i].ApplicantName,
                    FeesRecId = obj[i].FeesReceiveMstID,// fetch this id for selection a record in table
                    PostDate = Convert.ToDateTime(obj[i].PostDate).ToString(portalutilities.DateTimeFormat),
                    SectionName = obj[i].SectionName,
                    TotalAdjAmount = Convert.ToDecimal(obj[i].TotalAdjustmentAmount),
                    AdmissionId = obj[i].AdmissionID //TODO: This Admission id is not useful here bcoz FeesRecId is Primary key
                });
            }

            return View(result);
        }

        // GET: FeesReceive/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FeesReceive/Create
        public ActionResult Create(long? feesRecId)
        {
            var result = new FeesReceiveCreateViewModel();
            List<FeesReceiveStudentViewModel> studentList = GetStudentsList();

            ViewBag.StudentList = studentList;
            ViewBag.StudentId = feesRecId ?? 0;

            // If studentId has a value it means it was clicked from index page grid and we need to show the form in edit mode.... If not then normal create form
            if (feesRecId.HasValue)
                result = GetStudentDetailInformation(feesRecId.Value);

            return View(result);
        }

        private static List<FeesReceiveStudentViewModel> GetStudentsList()
        {
            OssEntities cntx = new OssEntities();
            var obj = (from q in cntx.tblConfiguration where q.SchoolID == portalutilities._schollid select q).FirstOrDefault();
            var objdep = (dynamic)null;
            if (portalutilities.InEnglish == false)
            {
                if (obj.IsSearchGRNoInUrdu == true)
                {
                    objdep = (from q in cntx.tblAdmission.Cast<tblAdmission>()
                              join a in cntx.tblStudentRegMst on q.RegID equals a.RegID
                              join c in cntx.Vu_Class on q.AdmissionID equals c.AdmissionID
                              where q.IsDelete == false
                                  && q.SchoolID == portalutilities._schollid && a.IsAdmit == true
                                  && c.SessionID == portalutilities.ActiveSessionID
                              orderby q.GRNo ascending
                              select new { q.AdmissionID, ApplicantName = q.GRNo }).ToList();
                }
            }
            else
            {
                if (obj.IsSearchGRNo == false)
                {
                    objdep = (from q in cntx.tblAdmission
                              join a in cntx.tblStudentRegMst on q.RegID equals a.RegID
                              join c in cntx.Vu_Class on q.AdmissionID equals c.AdmissionID
                              where q.IsDelete == false
                                  && q.SchoolID == portalutilities._schollid && a.IsAdmit == true
                                  && c.SessionID == portalutilities.ActiveSessionID
                              orderby a.ApplicantName ascending
                              select new { q.AdmissionID, ApplicantName = a.ApplicantName + " " + q.GRNo }).ToList();

                }
                else
                {
                    objdep = (from q in cntx.tblAdmission
                              join a in cntx.tblStudentRegMst on q.RegID equals a.RegID
                              join c in cntx.Vu_Class on q.AdmissionID equals c.AdmissionID
                              where q.IsDelete == false
                                  && q.SchoolID == portalutilities._schollid && a.IsAdmit == true
                                  && c.SessionID == portalutilities.ActiveSessionID
                              orderby q.GRNo ascending
                              select new { q.AdmissionID, ApplicantName = q.GRNo }).ToList();
                }
            }

            var result = new List<FeesReceiveStudentViewModel>();
            for (int i = 0; i < objdep.Count; i++)
            {
                /// TODO : Simple list of all student's id along with the name (1440/0321/0105) we want to show
                /// 
                result.Add(new FeesReceiveStudentViewModel
                {
                    AdmissionId = objdep[i].AdmissionID,
                    StudentName = objdep[i].ApplicantName
                });

                //return new List<FeesReceiveStudentViewModel> {
                //new FeesReceiveStudentViewModel{
                //    AdmissionId = objdep[i].AdmissionID,
                //    StudentName = "1440/0321/0105"
                //    }
                //};
            }

            return result;
        }

        private string CreateFeesRecNumber() 
        {
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var count = db.tblFeesReceiveMst.Count(x => x.CreateDate.Value.Year == year && x.CreateDate.Value.Month == month);
            return month + "" + year % 100 + "-" + (count + 1);
        }

        // POST: FeesReceive/Create
        [HttpPost]
        public string Create(FeesReceivePostModel model, int? id)
        {
            if (id.HasValue)
            {
                try
                {
                    var feeRecId = long.Parse(id.Value.ToString());
                    var feesRecMst = db.tblFeesReceiveMst.FirstOrDefault(x => x.FeesReceiveMstID == feeRecId);
                    feesRecMst.TotalAdjustmentAmount = model.TotalAdjustmentAmount;
                    feesRecMst.TotalDiscountAmount = model.TotalDiscount;
                    feesRecMst.TotalFeesAmount = model.TotalFees;
                    feesRecMst.TotalNetFees = model.TotalNetFees;
                    var feeRecDtlList = db.tblFeesReceiveDtl.Where(x => x.FeesReceiveMstID == feeRecId).ToList();
                    var feeList = model.FeesList.Split('|');
                    foreach (var item in feeRecDtlList)
                    {
                        foreach (var feeDtl in feeList)
                        {
                            var ii = feeDtl.Split(',');
                            if (item.GenFeesDtlID.Value == int.Parse(ii[0].ToString()))
                            {
                                item.AdjustmentAmount = Decimal.Parse(ii[1]);
                                item.ReceivedAmount = Decimal.Parse(ii[2]);
                                item.Status = ii[1] != ii[3] ? portalutilities.FeesStatus.Partial : portalutilities.FeesStatus.Paid;
                            }
                        }
                    }
                    db.SaveChanges();
                    return "Done";

                }
                catch (Exception e)
                {
                    return "Error";
                    throw;
                }

            }
            else
            {
                try
                {
                    var mstobj = db.tblFeesReceiveMst.Add(new tblFeesReceiveMst
                    {
                        AdmissionID = model.AdmissionId,
                        CreateBy = portalutilities._username,
                        CreateDate = DateTime.Now,
                        FeesReceiveNo = CreateFeesRecNumber(),
                        IsDelete = false,
                        PostDate = model.PostDate,
                        Remarks = string.Empty,
                        SchoolID = portalutilities._schollid,
                        SessionID = portalutilities.ActiveSessionID,
                        TotalAdjustmentAmount = model.TotalAdjustmentAmount,
                        TotalDiscountAmount = model.TotalDiscount,
                        TotalFeesAmount = model.TotalFees,
                        TotalNetFees = model.TotalNetFees,
                        UserID = portalutilities.LoginUserID,
                    });
                    db.SaveChanges();
                    var feeList = model.FeesList.Split('|');
                    foreach (var item in feeList)
                    {
                        var ii = item.Split(',');
                        db.tblFeesReceiveDtl.Add(new tblFeesReceiveDtl
                        {
                            AdjustmentAmount = Decimal.Parse(ii[1]),
                            ReceivedAmount = Decimal.Parse(ii[2]),
                            GenFeesDtlID = int.Parse(ii[0]),
                            Status = ii[1] != ii[3] ? portalutilities.FeesStatus.Partial : portalutilities.FeesStatus.Paid,
                            FeesReceiveMstID = mstobj.FeesReceiveMstID,
                        });
                        db.SaveChanges();
                    }
                    return "Done";
                }
                catch (Exception e)
                {
                    return "Error";
                    throw;
                }

            }
        }

        public ActionResult GetStudentDetails(int admissionId)
        {
            FeesReceiveCreateViewModel result = GetStudentDetailInformation(admissionId);
            var json = JsonConvert.SerializeObject(result);
            return new ContentResult { Content = json, ContentType = "application/json" };
        }

        private static FeesReceiveCreateViewModel GetStudentDetailInformation(int AdmissionID)
        {
            OssEntities cntx = new OssEntities();
            var obj = (from q in cntx.tblStudentRegMst
                       join a in cntx.tblAdmission on q.RegID equals a.RegID
                       join c in cntx.Vu_Class on a.AdmissionID equals c.AdmissionID
                       where a.AdmissionID == AdmissionID && c.SessionID == portalutilities.ActiveSessionID
                       select new { q.ApplicantName, q.ApplicantNameInUrdu, q.FatherName, q.FatherNameInUrdu, c.StageName, c.ClassName, c.SectionName }).FirstOrDefault();

            var objc = (from q in cntx.tblConfiguration where q.SchoolID == portalutilities._schollid select q).FirstOrDefault();
            string ApplicantName = "";
            string FatherName = "";
            if (portalutilities.InEnglish == false)
            {
                FatherName = obj.FatherNameInUrdu;
                if (objc.IsSearchGRNoInUrdu == true)
                {
                    ApplicantName = obj.ApplicantNameInUrdu;
                }
            }
            else
            {
                FatherName = obj.FatherName;
                if (objc.IsSearchGRNo == true)
                {
                    ApplicantName = obj.ApplicantName;
                }
            }

            var objdtl = (from q in cntx.Vu_StudentLedger
                          where q.AdmissionID == AdmissionID && q.SchoolID == portalutilities._schollid && q.ClassSessionID == portalutilities.ActiveSessionID
                          && q.Status != portalutilities.FeesStatus.Paid && q.TransactionType == portalutilities.TransactionTypeFees.GenerateFees
                          orderby q.FeesDate ascending
                          select new
                          {
                              q.RefCode,
                              q.FeesTypeID,
                              FeesMonth = q.FeesDate,
                              q.FeesType,
                              q.FeesAmount,
                              q.DiscountAble,
                              q.DiscountID,
                              q.DiscountName,
                              DiscountAmount = q.EditedDiscAmount,
                              q.NetFees,
                              q.Status
                          }).ToList();

            var feesListForStudent = new List<FeesReceiveGridViewModel>();

            for (int i = 0; i < objdtl.Count; i++)
            {
                string refcode = objdtl[i].RefCode;
                int feestypeid = Convert.ToInt32(objdtl[i].FeesTypeID);
                DateTime feesdate = Convert.ToDateTime(objdtl[i].FeesMonth);
                int GenFeesDtlID = 0;
                string DiscName = "";
                decimal? DueDateDisc = 0;
                decimal? DiscAmount = 0;
                decimal? RecAmount = 0;
                decimal? AdjAmount = 0;
                decimal? NetFees = 0;

                var objgen = (from q in cntx.tblGenerateFeesMst
                              join a in cntx.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                              where q.IsDelete == false && q.SchoolID == portalutilities._schollid && q.GenFeesNo == refcode
                              && a.FeesTypeID == feestypeid && a.AdmissionID == AdmissionID
                              select new { a.GenFeesDtlID }).FirstOrDefault();

                if (objgen != null)
                {
                    GenFeesDtlID = objgen.GenFeesDtlID;
                }

                var objAdj = (from q in cntx.Vu_StudentLedger
                              where q.AdmissionID == AdmissionID && q.FeesTypeID == feestypeid && q.ClassSessionID == portalutilities.ActiveSessionID &&
                              q.SchoolID == portalutilities._schollid && q.Status != portalutilities.FeesStatus.Paid
                              && q.FeesDate == feesdate
                              select new { q.AdjustmentAmount }).Sum(a => a.AdjustmentAmount);

                var objs = (from q in cntx.tblSchool where q.SchoolID == portalutilities._schollid select q).FirstOrDefault();
                var DeductDiscount = (from q in cntx.tblConfiguration where q.SchoolID == portalutilities._schollid select q.DeductDiscount).FirstOrDefault();

                DateTime today = DateTime.Now.Date;
                DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, objs.LateFeesDate.Value.Day);
                if ((today <= dt || feesdate.Month > today.Month) && feesdate.Year == today.Year && DeductDiscount == true)
                {
                    if (objdtl[i].DiscountAmount == 0 && objdtl[i].DiscountAble == true && feesdate.Month >= DateTime.Now.Month)
                    {
                        DiscName = portalutilities.DueDateDiscName;
                        DiscAmount = objs.LateFeesAmount;
                        NetFees = objdtl[i].NetFees - objs.LateFeesAmount;
                        DueDateDisc = objs.LateFeesAmount;

                        if (objAdj != null)
                        {
                            AdjAmount = objAdj - objs.LateFeesAmount;
                        }
                    }
                    else
                    {
                        DiscName = objdtl[i].DiscountName;
                        DiscAmount = objdtl[i].DiscountAmount;
                        NetFees = objdtl[i].NetFees;
                        DueDateDisc = 0;

                        if (objAdj != null)
                        {
                            AdjAmount = objAdj;
                        }
                    }
                }
                else
                {
                    DiscName = objdtl[i].DiscountName;
                    DiscAmount = objdtl[i].DiscountAmount;
                    NetFees = objdtl[i].NetFees;
                    DueDateDisc = 0;

                    if (objAdj != null)
                    {
                        AdjAmount = objAdj;
                    }
                }

                var objRec = (from q in cntx.Vu_StudentLedger
                              where q.AdmissionID == AdmissionID && q.FeesTypeID == feestypeid && q.ClassSessionID == portalutilities.ActiveSessionID &&
                              q.SchoolID == portalutilities._schollid && q.Status != portalutilities.FeesStatus.Paid
                              && q.FeesDate == feesdate
                              select new { q.ReceivedAmount }).Sum(a => a.ReceivedAmount);

                if (objRec != null)
                {
                    RecAmount = objRec;
                }

                feesListForStudent.Add(new FeesReceiveGridViewModel
                {
                    FeesMonth = objdtl[i].FeesMonth.Value.ToString(portalutilities.MonthYearFomrat),
                    FeesTypeName = objdtl[i].FeesType,
                    FeesAmount = objdtl[i].FeesAmount,
                    DiscountName = DiscName,
                    DiscountAmount = DiscAmount,
                    NetFees = NetFees,
                    ReceivedAmount = RecAmount,
                    DueDateDisc = DueDateDisc,
                    AdjustmentAmount = AdjAmount,
                    GenFeesDtlID = GenFeesDtlID,
                    FeesReceiveDtlID = 0,///// generate when user save record
                    Status = objdtl[i].Status
                });
            }

            var result = new FeesReceiveCreateViewModel
            {
                PostDate = DateTime.Now.ToString(),
                AdmissionId = AdmissionID,
                StageName = obj.StageName,
                ClassName = obj.ClassName,
                SectionName = obj.SectionName,
                StudentName = ApplicantName,
                FatherName = FatherName,
                FeesDetails = feesListForStudent, // List of all the fees a student have 
            };
            return result;
        }

        private static FeesReceiveCreateViewModel GetStudentDetailInformation(long feesRecId)
        {
            OssEntities cntx = new OssEntities();
            var obj = (from q in cntx.tblFeesReceiveMst
                       join a in cntx.tblAdmission on q.AdmissionID equals a.AdmissionID
                       join st in cntx.tblStudentRegMst on a.RegID equals st.RegID
                       join d in cntx.Vu_Class on a.AdmissionID equals d.AdmissionID
                       where q.FeesReceiveMstID == feesRecId && d.SessionID == portalutilities.ActiveSessionID && q.SessionID == portalutilities.ActiveSessionID
                       select new
                       {
                           st.RegID,
                           q.FeesReceiveNo,
                           q.AdmissionID,
                           d.StageName,
                           d.StageID,
                           d.ClassName,
                           d.ClassID,
                           d.SectionName,
                           d.SectionID,
                           st.FatherName,
                           st.FatherNameInUrdu,
                           st.ApplicantName,
                           st.ApplicantNameInUrdu,
                           a.IsActive,
                           q.Remarks,
                           q.PostDate,
                           q.TotalFeesAmount,
                           q.TotalDiscountAmount,
                           q.TotalNetFees,
                           q.TotalAdjustmentAmount
                       }).FirstOrDefault();

            var objdtl = (from q in cntx.tblFeesReceiveMst
                          join r in cntx.tblFeesReceiveDtl on q.FeesReceiveMstID equals r.FeesReceiveMstID
                          join a in cntx.tblAdmission on q.AdmissionID equals a.AdmissionID
                          join st in cntx.tblStudentRegMst on a.RegID equals st.RegID
                          join dtl in cntx.tblGenerateFeesDtl on r.GenFeesDtlID equals dtl.GenFeesDtlID
                          join mst in cntx.tblGenerateFeesMst on dtl.GenFeesMstID equals mst.GenFeesMstID
                          join f in cntx.tblFeesType on dtl.FeesTypeID equals f.FeesTypeID
                          join c in cntx.tblDiscount on dtl.DiscountID equals c.DiscountID
                          where q.FeesReceiveMstID == feesRecId
                          select new
                          {
                              r.GenFeesDtlID,
                              r.FeesReceiveDtlID,
                              FeesMonth = mst.FeesDate,
                              f.FeesType,
                              f.FeesAmount,
                              c.DiscountName,
                              DiscountAmount = dtl.EditedDiscAmount,
                              NetFees = f.FeesAmount - dtl.EditedDiscAmount,
                              r.ReceivedAmount,
                              r.AdjustmentAmount,
                              r.DueDateDisc,
                              r.Status
                          }).ToList();

            var feesListForStudent = new List<FeesReceiveGridViewModel>();

            for (int i = 0; i < objdtl.Count; i++)
            {
                feesListForStudent.Add(new FeesReceiveGridViewModel
                {
                    FeesMonth = objdtl[i].FeesMonth.Value.ToString(portalutilities.MonthYearFomrat),
                    FeesTypeName = objdtl[i].FeesType,
                    FeesAmount = objdtl[i].FeesAmount,
                    DiscountName = objdtl[i].DiscountName,
                    DiscountAmount = objdtl[i].DiscountAmount,
                    NetFees = objdtl[i].NetFees,
                    ReceivedAmount = objdtl[i].ReceivedAmount,
                    DueDateDisc = objdtl[i].DueDateDisc,
                    AdjustmentAmount = objdtl[i].AdjustmentAmount,
                    GenFeesDtlID = objdtl[i].GenFeesDtlID,
                    FeesReceiveDtlID = objdtl[i].FeesReceiveDtlID,
                    Status = objdtl[i].Status
                });
            }

            var obja = (from q in cntx.tblConfiguration where q.SchoolID == portalutilities._schollid select q).FirstOrDefault();
            string ApplicantName = "";
            string FatherName = "";
            int? admissionid = 0;
            if (portalutilities.InEnglish == false)
            {
                FatherName = obj.FatherNameInUrdu;
                if (obja.IsSearchGRNoInUrdu == true)
                {
                    admissionid = Convert.ToInt32(obj.AdmissionID);
                    //cmbStudent.SelectedValue = obj.AdmissionID;
                    ApplicantName = obj.ApplicantNameInUrdu;
                }
                else
                {
                    ApplicantName = obj.ApplicantNameInUrdu + "-" + obj.AdmissionID;
                }
            }
            else
            {
                admissionid = obj.AdmissionID;
                FatherName = obj.FatherName;
                if (obja.IsSearchGRNo == true)
                {
                    ApplicantName = obj.ApplicantName;
                }
            }

            var result = new FeesReceiveCreateViewModel
            {
                PostDate = obj.PostDate.Value.ToString(portalutilities.DateTimeFormat),
                AdmissionId = admissionid,
                StageName = obj.StageName,
                ClassName = obj.ClassName,
                SectionName = obj.SectionName,
                StageId = obj.StageID,
                SectionId = obj.SectionID,
                ClassId = obj.ClassID,
                StudentName = ApplicantName,
                FatherName = FatherName,
                FeesDetails = feesListForStudent, // List of all the fees a student have 
            };
            return result;
        }

        // GET: FeesReceive/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FeesReceive/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: FeesReceive/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var feeRecive = db.tblFeesReceiveMst.Find(id);
            if (feeRecive == null)
            {
                return HttpNotFound();
            }
            feeRecive.IsDelete = true;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: FeesReceive/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
