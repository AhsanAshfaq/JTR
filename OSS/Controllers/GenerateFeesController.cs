using Newtonsoft.Json;
using OSS.Models;
using OSS.Models.viewmodel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
            var obj = (from q in db.tblGenerateFeesMst.Cast<tblGenerateFeesMst>()
                       where q.IsDelete == false && q.SchoolID == portalutilities._schollid && q.SessionID == portalutilities.ActiveSessionID
                       && q.UserID == portalutilities.LoginUserID
                       orderby q.GenFeesMstID descending
                       select new { q.GenFeesMstID, q.PostDate }).ToList();

            var result = new List<GenerateFeesViewModel>();

            for (int i = 0; i < obj.Count; i++)
            {
                /// TODO : This result list will contains the list (Record History Tab Data FROM Fees Receive Screen of Desktop App ) 
                result.Add(new GenerateFeesViewModel
                {
                    GenFeesMstID = obj[i].GenFeesMstID,
                    PostDate = obj[i].PostDate.Value.ToString(portalutilities.DateTimeFormat)
                });
            }

            return View(result);
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

        public ActionResult GetDefaulters(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate)
        {
            var obj = (from q in db.tblAdmission
                       join a in db.tblDiscount on q.DiscountID equals a.DiscountID
                       join s in db.tblStudentRegMst on q.RegID equals s.RegID
                       join v in db.Vu_Class on q.AdmissionID equals v.AdmissionID
                       where q.IsActive == true && q.IsDelete == false && q.SchoolID == portalutilities._schollid
                       && (stageId != 0 ? v.StageID == stageId : true)
                       && (classId != 0 ? v.ClassID == classId : true)
                       && (sectionId != 0 ? v.SectionID == sectionId : true) && s.IsAdmit == true
                       && v.SessionID == portalutilities.ActiveSessionID
                       && v.GenerateFees == true
                       orderby v.ClassID
                       select new
                       {
                           q.AdmissionID,
                           v.StageID,
                           v.StageName,
                           v.ClassID,
                           v.ClassName,
                           v.SectionID,
                           v.SectionName,
                           q.DiscountID,
                           a.DiscountName,
                           a.DiscountAmount,
                           a.DiscountType,
                           q.GRNo,
                           s.ApplicantName,
                           s.ApplicantNameInUrdu
                       }).ToList();

            var result = new List<object>();

            #region Defaulter Fees

            for (int i = 0; i < obj.Count; i++)
            {
                string ID = obj[i].ClassID.ToString();
                int CID = Convert.ToInt32(ID);

                string[] chargefee = chargeFeeList.Split(',');

                for (int j = 0; j < chargefee.Count(); j++)
                {
                    int feetype = Convert.ToInt32(chargefee[j]);

                    var objs = (from q in db.tblDefineFeesMst
                                join r in db.tblDefineFeesDtl on q.DefineFeesID equals r.DefineFeesID
                                join f in db.tblFeesType on r.FeesTypeID equals f.FeesTypeID
                                where q.ClassID == CID && f.FeesTypeID == feetype
                                && q.SchoolID == portalutilities._schollid && q.IsDelete == false
                                select new { f.FeesTypeID, f.FeesType, f.FeesAmount, f.ChargeFee }).FirstOrDefault();

                    if (objs != null)
                    {
                        int admissionid = Convert.ToInt32(obj[i].AdmissionID);
                        int feestypeid = Convert.ToInt32(objs.FeesTypeID);
                        //int CurrentMonth = DateTime.Parse(feeMonth).Month;
                        //int CurrentYear = DateTime.Parse(feeMonth).Year;

                        int CurrentMonth = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Month;
                        int CurrentYear = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Year;

                        var isDefaulter = (from q in db.Vu_StudentLedger
                                           select new { q.AdmissionID, q.FeesDate, q.Status, q.ClassSessionID }).Any(o => o.AdmissionID == admissionid &&
                                               o.FeesDate.Value.Month == CurrentMonth && o.FeesDate.Value.Year == CurrentYear
                                               && o.ClassSessionID == portalutilities.ActiveSessionID && o.Status != portalutilities.FeesStatus.Paid);

                        if (isDefaulter == true)
                        {
                            bool isExist = false;
                            if (objs.ChargeFee == portalutilities.ChargeFee.Monthly)
                            {
                                isExist = (from q in db.tblGenerateFeesMst
                                           join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                           select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o => o.FeesDate.Value.Month == CurrentMonth
                                 && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false && o.FeesDate.Value.Year == CurrentYear);
                            }
                            else if (objs.ChargeFee == portalutilities.ChargeFee.Yearly)
                            {
                                isExist = (from q in db.tblGenerateFeesMst
                                           join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                           select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete, q.SessionID }).Any(o => o.SessionID == portalutilities.ActiveSessionID
                                 && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                            }
                            else if (objs.ChargeFee == portalutilities.ChargeFee.OneTime)
                            {
                                isExist = (from q in db.tblGenerateFeesMst
                                           join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                           select new { a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o =>
                                 o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                            }

                            if (isExist == false)
                            {
                                string ApplicantName = "";
                                int? DiscID = 0;
                                string DiscName = "";
                                decimal? DiscAmount = 0;
                                decimal? NetFees = 0;
                                if (portalutilities.InEnglish == false)
                                {
                                    ApplicantName = obj[i].ApplicantNameInUrdu;
                                }
                                else
                                {
                                    ApplicantName = obj[i].ApplicantName;
                                }

                                var objdisc = (from q in db.tblFeesType where q.FeesTypeID == feestypeid && q.DiscountAble == true select q).Count();

                                if (objdisc != 0)
                                {
                                    if (obj[i].DiscountType == portalutilities.DiscountType.Percentage)
                                    {
                                        decimal discperc = decimal.Round((Convert.ToDecimal(objs.FeesAmount) * Convert.ToDecimal(obj[i].DiscountAmount)) / 100, 2, MidpointRounding.AwayFromZero);

                                        DiscID = obj[i].DiscountID;
                                        DiscName = obj[i].DiscountName;
                                        DiscAmount = discperc;
                                        NetFees = objs.FeesAmount - discperc;
                                    }
                                    else if (obj[i].DiscountType == portalutilities.DiscountType.Fixed)
                                    {
                                        DiscID = obj[i].DiscountID;
                                        DiscName = obj[i].DiscountName;
                                        DiscAmount = obj[i].DiscountAmount;
                                        NetFees = objs.FeesAmount - obj[i].DiscountAmount;
                                    }
                                }
                                else
                                {
                                    DiscID = obj[i].DiscountID;
                                    DiscName = "";
                                    DiscAmount = 0;
                                    NetFees = objs.FeesAmount;
                                }

                                result.Add(new
                                {
                                    AdmissionId = obj[i].AdmissionID,
                                    StudentName = ApplicantName,
                                    GRNo = obj[i].GRNo,
                                    StageID = obj[i].StageID,
                                    ClassID = obj[i].ClassID,
                                    SectionID = obj[i].SectionID,
                                    Class = obj[i].ClassName,
                                    Section = obj[i].SectionName,
                                    FeeTypeID = objs.FeesTypeID,
                                    FeeType = objs.FeesType,
                                    FeeAmount = objs.FeesAmount,
                                    DiscountID = DiscID,
                                    Discount = DiscName,
                                    DiscountAmount = DiscAmount,
                                    EditedDiscount = DiscAmount,
                                    NetFees = NetFees,
                                    Status = portalutilities.FeesStatus.UnPaid
                                });
                            }
                        }
                    }
                }
            }

            #endregion

            //result.Add(new
            //{
            //    AdmissionId = 0,
            //    GRNo = "1",
            //    StudentName = "1",
            //    Class = "1",
            //    Section = "1",
            //    FeeType = "1",
            //    FeeAmount = "1",
            //    Discount = "1",
            //    DiscountAmount = "1",
            //    EditedDiscount = "1",
            //    NetFees = "1",
            //});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Genereate(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate)
        {
            var obj = (from q in db.tblAdmission
                       join a in db.tblDiscount on q.DiscountID equals a.DiscountID
                       join s in db.tblStudentRegMst on q.RegID equals s.RegID
                       join v in db.Vu_Class on q.AdmissionID equals v.AdmissionID
                       where q.IsActive == true && q.IsDelete == false && q.SchoolID == portalutilities._schollid
                       && (stageId != 0 ? v.StageID == stageId : true)
                       && (classId != 0 ? v.ClassID == classId : true)
                       && (sectionId != 0 ? v.SectionID == sectionId : true) && s.IsAdmit == true
                       && v.SessionID == portalutilities.ActiveSessionID
                       && v.GenerateFees == true
                       orderby v.ClassID
                       select new
                       {
                           q.AdmissionID,
                           v.StageID,
                           v.StageName,
                           v.ClassID,
                           v.ClassName,
                           v.SectionID,
                           v.SectionName,
                           q.DiscountID,
                           a.DiscountName,
                           a.DiscountAmount,
                           a.DiscountType,
                           q.GRNo,
                           s.ApplicantName,
                           s.ApplicantNameInUrdu
                       }).ToList();

            var result = new List<object>();

            #region Fees

            for (int i = 0; i < obj.Count; i++)
            {
                string ID = obj[i].ClassID.ToString();
                int CID = Convert.ToInt32(ID);

                string[] chargefee = chargeFeeList.Split(',');

                for (int j = 0; j < chargefee.Count(); j++)
                {
                    int feetype = Convert.ToInt32(chargefee[j]);

                    var objs = (from q in db.tblDefineFeesMst
                                join r in db.tblDefineFeesDtl on q.DefineFeesID equals r.DefineFeesID
                                join f in db.tblFeesType on r.FeesTypeID equals f.FeesTypeID
                                where q.ClassID == CID && f.FeesTypeID == feetype
                                && q.SchoolID == portalutilities._schollid && q.IsDelete == false
                                select new { f.FeesTypeID, f.FeesType, f.FeesAmount, f.ChargeFee }).FirstOrDefault();

                    if (objs != null)
                    {
                        int admissionid = Convert.ToInt32(obj[i].AdmissionID);
                        int feestypeid = Convert.ToInt32(objs.FeesTypeID);
                        //int CurrentMonth = DateTime.Parse(feeMonth).Month;
                        //int CurrentYear = DateTime.Parse(feeMonth).Year;

                        int CurrentMonth = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Month;
                        int CurrentYear = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Year;

                        bool isExist = false;
                        if (objs.ChargeFee == portalutilities.ChargeFee.Monthly)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o => o.FeesDate.Value.Month == CurrentMonth
                             && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false && o.FeesDate.Value.Year == CurrentYear);
                        }
                        else if (objs.ChargeFee == portalutilities.ChargeFee.Yearly)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete, q.SessionID }).Any(o => o.SessionID == portalutilities.ActiveSessionID
                             && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                        }
                        else if (objs.ChargeFee == portalutilities.ChargeFee.OneTime)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o =>
                             o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                        }

                        if (isExist == false)
                        {
                            string ApplicantName = "";
                            int? DiscID = 0;
                            string DiscName = "";
                            decimal? DiscAmount = 0;
                            decimal? NetFees = 0;
                            if (portalutilities.InEnglish == false)
                            {
                                ApplicantName = obj[i].ApplicantNameInUrdu;
                            }
                            else
                            {
                                ApplicantName = obj[i].ApplicantName;
                            }

                            var objdisc = (from q in db.tblFeesType where q.FeesTypeID == feestypeid && q.DiscountAble == true select q).Count();

                            if (objdisc != 0)
                            {
                                if (obj[i].DiscountType == portalutilities.DiscountType.Percentage)
                                {
                                    decimal discperc = decimal.Round((Convert.ToDecimal(objs.FeesAmount) * Convert.ToDecimal(obj[i].DiscountAmount)) / 100, 2, MidpointRounding.AwayFromZero);

                                    DiscID = obj[i].DiscountID;
                                    DiscName = obj[i].DiscountName;
                                    DiscAmount = discperc;
                                    NetFees = objs.FeesAmount - discperc;
                                }
                                else if (obj[i].DiscountType == portalutilities.DiscountType.Fixed)
                                {
                                    DiscID = obj[i].DiscountID;
                                    DiscName = obj[i].DiscountName;
                                    DiscAmount = obj[i].DiscountAmount;
                                    NetFees = objs.FeesAmount - obj[i].DiscountAmount;
                                }
                            }
                            else
                            {
                                DiscID = obj[i].DiscountID;
                                DiscName = "";
                                DiscAmount = 0;
                                NetFees = objs.FeesAmount;
                            }

                            result.Add(new
                            {
                                AdmissionId = obj[i].AdmissionID,
                                StudentName = ApplicantName,
                                GRNo = obj[i].GRNo,
                                StageID = obj[i].StageID,
                                ClassID = obj[i].ClassID,
                                SectionID = obj[i].SectionID,
                                Class = obj[i].ClassName,
                                Section = obj[i].SectionName,
                                FeeTypeID = objs.FeesTypeID,
                                FeeType = objs.FeesType,
                                FeeAmount = objs.FeesAmount,
                                DiscountID = DiscID,
                                Discount = DiscName,
                                DiscountAmount = DiscAmount,
                                EditedDiscount = DiscAmount,
                                NetFees = NetFees,
                                Status = portalutilities.FeesStatus.UnPaid
                            });
                        }
                    }
                }
            }

            #endregion

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GenerateFixedFees(int stageId, int classId, int sectionId, string chargeFeeList, string feeMonth, string postDate)
        {
            var obj = (from q in db.tblAdmission
                       join a in db.tblDiscount on q.DiscountID equals a.DiscountID
                       join s in db.tblStudentRegMst on q.RegID equals s.RegID
                       join v in db.Vu_Class on q.AdmissionID equals v.AdmissionID
                       where q.IsActive == true && q.IsDelete == false && q.SchoolID == portalutilities._schollid
                       && (stageId != 0 ? v.StageID == stageId : true)
                       && (classId != 0 ? v.ClassID == classId : true)
                       && (sectionId != 0 ? v.SectionID == sectionId : true) && s.IsAdmit == true
                       && v.SessionID == portalutilities.ActiveSessionID
                       && v.GenerateFees == true
                       orderby v.ClassID
                       select new
                       {
                           q.AdmissionID,
                           v.StageID,
                           v.StageName,
                           v.ClassID,
                           v.ClassName,
                           v.SectionID,
                           v.SectionName,
                           q.DiscountID,
                           a.DiscountName,
                           a.DiscountAmount,
                           a.DiscountType,
                           q.GRNo,
                           s.ApplicantName,
                           s.ApplicantNameInUrdu
                       }).ToList();

            var result = new List<object>();

            #region Fixed Fees

            for (int i = 0; i < obj.Count; i++)
            {
                string ID = obj[i].ClassID.ToString();
                int CID = Convert.ToInt32(ID);

                string[] chargefee = chargeFeeList.Split(',');

                for (int j = 0; j < chargefee.Count(); j++)
                {
                    int feetype = Convert.ToInt32(chargefee[j]);
                    int admissionid = obj[i].AdmissionID;

                    var objs = (from q in db.tblDefineFeesStudentMst
                                join r in db.tblDefineFeesStudentDtl on q.DefineFeesStudentID equals r.DefineFeesStudentID
                                join f in db.tblFeesType on q.FeesTypeID equals f.FeesTypeID
                                where q.ClassID == CID && f.FeesTypeID == feetype && r.AdmissionID == admissionid
                                && q.SchoolID == portalutilities._schollid && q.IsDelete == false
                                select new { f.FeesTypeID, f.FeesType, f.FeesAmount, f.ChargeFee }).FirstOrDefault();

                    if (objs != null)
                    {
                        int feestypeid = Convert.ToInt32(objs.FeesTypeID);
                        //int CurrentMonth = DateTime.Parse(feeMonth).Month;
                        //int CurrentYear = DateTime.Parse(feeMonth).Year;

                        int CurrentMonth = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Month;
                        int CurrentYear = DateTime.ParseExact(feeMonth, portalutilities.MonthYearFomrat, CultureInfo.InvariantCulture).Year;

                        bool isExist = false;
                        if (objs.ChargeFee == portalutilities.ChargeFee.Monthly)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o => o.FeesDate.Value.Month == CurrentMonth
                             && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false && o.FeesDate.Value.Year == CurrentYear);
                        }
                        else if (objs.ChargeFee == portalutilities.ChargeFee.Yearly)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { q.FeesDate, a.FeesTypeID, a.AdmissionID, q.IsDelete, q.SessionID }).Any(o => o.SessionID == portalutilities.ActiveSessionID
                             && o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                        }
                        else if (objs.ChargeFee == portalutilities.ChargeFee.OneTime)
                        {
                            isExist = (from q in db.tblGenerateFeesMst
                                       join a in db.tblGenerateFeesDtl on q.GenFeesMstID equals a.GenFeesMstID
                                       select new { a.FeesTypeID, a.AdmissionID, q.IsDelete }).Any(o =>
                             o.FeesTypeID == feestypeid && o.AdmissionID == admissionid && o.IsDelete == false);
                        }

                        if (isExist == false)
                        {
                            string ApplicantName = "";
                            int? DiscID = 0;
                            string DiscName = "";
                            decimal? DiscAmount = 0;
                            decimal? NetFees = 0;
                            if (portalutilities.InEnglish == false)
                            {
                                ApplicantName = obj[i].ApplicantNameInUrdu;
                            }
                            else
                            {
                                ApplicantName = obj[i].ApplicantName;
                            }

                            var objdisc = (from q in db.tblFeesType where q.FeesTypeID == feestypeid && q.DiscountAble == true select q).Count();

                            if (objdisc != 0)
                            {
                                if (obj[i].DiscountType == portalutilities.DiscountType.Percentage)
                                {
                                    decimal discperc = decimal.Round((Convert.ToDecimal(objs.FeesAmount) * Convert.ToDecimal(obj[i].DiscountAmount)) / 100, 2, MidpointRounding.AwayFromZero);

                                    DiscID = obj[i].DiscountID;
                                    DiscName = obj[i].DiscountName;
                                    DiscAmount = discperc;
                                    NetFees = objs.FeesAmount - discperc;
                                }
                                else if (obj[i].DiscountType == portalutilities.DiscountType.Fixed)
                                {
                                    DiscID = obj[i].DiscountID;
                                    DiscName = obj[i].DiscountName;
                                    DiscAmount = obj[i].DiscountAmount;
                                    NetFees = objs.FeesAmount - obj[i].DiscountAmount;
                                }
                            }
                            else
                            {
                                DiscID = obj[i].DiscountID;
                                DiscName = "";
                                DiscAmount = 0;
                                NetFees = objs.FeesAmount;
                            }

                            result.Add(new
                            {
                                AdmissionId = obj[i].AdmissionID,
                                StudentName = ApplicantName,
                                GRNo = obj[i].GRNo,
                                StageID = obj[i].StageID,
                                ClassID = obj[i].ClassID,
                                SectionID = obj[i].SectionID,
                                Class = obj[i].ClassName,
                                Section = obj[i].SectionName,
                                FeeTypeID = objs.FeesTypeID,
                                FeeType = objs.FeesType,
                                FeeAmount = objs.FeesAmount,
                                DiscountID = DiscID,
                                Discount = DiscName,
                                DiscountAmount = DiscAmount,
                                EditedDiscount = DiscAmount,
                                NetFees = NetFees,
                                Status = portalutilities.FeesStatus.UnPaid
                            });
                        }
                    }
                }
            }

            #endregion

            //result.Add(new
            //{
            //    AdmissionId = 0,
            //    GRNo = "1",
            //    StudentName = "1",
            //    Class = "1",
            //    Section = "1",
            //    FeeType = "1",
            //    FeeAmount = "1",
            //    Discount = "1",
            //    DiscountAmount = "1",
            //    EditedDiscount = "1",
            //    NetFees = "1",
            //});
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: tblGenerateFeesMsts/Create
        public ActionResult Create()
        {
            ViewBag.StageList = db.tblStage.ToList();
            ViewBag.FeeList = db.tblFeesType.Where(o => o.SchoolID == portalutilities._schollid && o.IsDelete == false && o.IsActive == true).ToList();
            return View();
        }

        // POST: tblGenerateFeesMsts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(GenerateFeesCreateModel model)
        {
            var feesIndvList = GetFeeIndvList(model);
            var mstId = db.tblGenerateFeesMst.Add(new tblGenerateFeesMst
            {
                ClassID = model.classId,
                CreateBy = portalutilities._username,
                CreateDate = DateTime.Now,
                FeesDate = DateTime.Now,
                IsDelete = false,
                PostDate = model.postDate,
                SchoolID = portalutilities._schollid,
                SectionID = model.sectionId,
                SessionID = portalutilities.ActiveSessionID,
                StageID = model.stageId,
                TotalDiscountAmount = model.totalDiscount,
                TotalFeesAmount = model.totalFees,
                TotalNetFees = model.totalNetFees,
                TotalStudents = feesIndvList.Count,
            });
            db.SaveChanges();
            foreach (var item in feesIndvList)
            {
                db.tblGenerateFeesDtl.Add(new tblGenerateFeesDtl
                {
                    AdmissionID = item.admissionId,
                    ClassID = model.classId,
                    DiscountID = item.discountId,
                    EditedDiscAmount = item.editedDiscount,
                    FeesTypeID = item.feesTypeId,
                    GenFeesMstID = mstId.GenFeesMstID,
                    NetFees = item.netFees,
                    SectionID = model.sectionId,
                    StageID = model.stageId,
                    Status = portalutilities.FeesStatus.UnPaid,
                });
            }
            foreach (var item in model.chargeFees.Split(','))
            {
                var id = int.Parse(item);
                var currentFeeType = db.tblFeesType.FirstOrDefault(x => x.FeesTypeID == id).FeesType;
                db.tblGenerateFeesCFDtl.Add(new tblGenerateFeesCFDtl
                {
                    ChargeFee = currentFeeType,
                    GenFeesMstID = mstId.GenFeesMstID,
                });
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult GetListOfFees(int genFeesId) 
        {
            var tblGenerateFeesDtlList = db.tblGenerateFeesDtl.Where(x => x.GenFeesMstID == genFeesId).ToList();
            var result = new List<object>();
            foreach (var item in tblGenerateFeesDtlList)
            {
                result.Add(new
                {
                    AdmissionId = item.AdmissionID,
                    FeeTypeID = item.FeesTypeID,
                    DiscountID = item.DiscountID.Value,
                    FeeType = item.tblFeesType.FeesType
                });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        private static List<GenerateFeesIndv> GetFeeIndvList(GenerateFeesCreateModel model)
        {
            var feesIndvList = new List<GenerateFeesIndv>();
            foreach (var item in model.generateFeesList.Split('|'))
            {
                var itemArray = item.Split(',');
                feesIndvList.Add(new GenerateFeesIndv
                {
                    admissionId = int.Parse(itemArray[0]),
                    feesTypeId = int.Parse(itemArray[1]),
                    discountId = int.Parse(itemArray[2]),
                    editedDiscount = int.Parse(itemArray[3]),
                    netFees = int.Parse(itemArray[4])
                });
            }
            return feesIndvList;
        }

        // GET: tblGenerateFeesMsts/Edit/5
        public ActionResult Edit(int? id)
        {
            var result = new GenerateFeesEditViewModel();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            var tblGenerateFeesDtlList = db.tblGenerateFeesDtl.Where(x => x.GenFeesMstID == id).ToList();
            var tblGenerateFeesCfDtlList = db.tblGenerateFeesCFDtl.Where(x => x.GenFeesMstID == id).Select(x => x.ChargeFee).ToList();
            result.ChargeFeesIds = string.Join(",", tblGenerateFeesCfDtlList);
            result.ClassId = tblGenerateFeesMst.ClassID.Value;
            result.FeesMonth = tblGenerateFeesMst.FeesDate.Value.ToString("MMM");
            result.PostDate = tblGenerateFeesMst.PostDate.Value.ToString(portalutilities.DateTimeFormat);
            result.SectionId = tblGenerateFeesMst.SectionID.Value;
            result.StageId = tblGenerateFeesMst.StageID.Value;
            result.TotalEditedDiscount = tblGenerateFeesMst.TotalDiscountAmount;
            result.TotalFees = tblGenerateFeesMst.TotalFeesAmount;
            result.TotalNetFees = tblGenerateFeesMst.TotalNetFees;
            var classList = (from s in db.tblClassMst
                             where (s.StageID == tblGenerateFeesMst.StageID && s.IsDelete != true && s.IsActive == true && s.SchoolID == portalutilities._schollid)
                             select new IdName
                             {
                                 id = s.ClassID,
                                 name = s.ClassName
                             }).ToList();
            var sectionList = (from _tblClassDtls in db.tblClassDtl
                               join jsec in db.tblSection on _tblClassDtls.SectionID equals jsec.SectionID
                               where (_tblClassDtls.ClassID == tblGenerateFeesMst.ClassID && jsec.IsActive == true && jsec.IsDelete != true && jsec.SchoolID == portalutilities._schollid)
                               select new IdName
                               {
                                   id = jsec.SectionID,
                                   name = jsec.SectionName
                               }).ToList();
            ViewBag.StageList = db.tblStage.ToList();
            ViewBag.ClassList = classList;
            ViewBag.SectionList = sectionList;
            ViewBag.FeeList = db.tblFeesType.Where(o => o.SchoolID == portalutilities._schollid && o.IsDelete == false && o.IsActive == true).ToList();
            foreach (var item in tblGenerateFeesDtlList)
            {
                var discount = db.tblDiscount.FirstOrDefault(x => x.DiscountID == item.DiscountID.Value);
                var classobj = db.tblClassMst.FirstOrDefault(x=>x.ClassID == item.ClassID.Value);
                var feeobj = db.tblFeesType.FirstOrDefault(x => x.FeesTypeID == item.FeesTypeID);
                var admissionobj = db.tblAdmission.FirstOrDefault(x => x.AdmissionID == item.AdmissionID);
                var sectionobj = db.tblSection.FirstOrDefault(x => x.SectionID == item.SectionID);
                result.GridItems.Add(new GenerateFeesGridModel
                {
                    AdmissionId = item.AdmissionID.Value,
                    ClassId = item.ClassID.Value,
                    ClassName = classobj.ClassName,
                    DiscountId = item.DiscountID.Value,
                    DiscountAmount = discount.DiscountAmount,
                    DiscountName = discount.DiscountName,
                    EditedDiscount = item.EditedDiscAmount,
                    FeeAmount = feeobj.FeesAmount,
                    FeeTypeId = item.FeesTypeID.Value,
                    FeeTypeName = feeobj.FeesType,
                    GrNo = admissionobj.GRNo,
                    NetFees = item.NetFees,
                    SectionId = item.SectionID.Value,
                    SectionName = sectionobj.SectionName,
                    StudentName = portalutilities.InEnglish ? admissionobj.tblStudentRegMst.ApplicantName : admissionobj.tblStudentRegMst.ApplicantNameInUrdu
                });
            }
            
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
            return View(result);
        }

        // POST: tblGenerateFeesMsts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public string Edit(GenerateFeesCreateModel model, int id)
        {
            try
            {
                var feesIndvList = GetFeeIndvList(model);
                var tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
                var tblGenerateFeesDtlList = db.tblGenerateFeesDtl.Where(x => x.GenFeesMstID == id).ToList();
                tblGenerateFeesMst.UpdateBy = portalutilities._username;
                tblGenerateFeesMst.UpdateDate = DateTime.Now;
                tblGenerateFeesMst.TotalDiscountAmount = model.totalDiscount;
                tblGenerateFeesMst.TotalFeesAmount = model.totalFees;
                tblGenerateFeesMst.TotalNetFees = model.totalNetFees;
                tblGenerateFeesMst.TotalStudents = feesIndvList.Count;

                foreach (var item in tblGenerateFeesDtlList)
                {
                    foreach (var findv in feesIndvList)
                    {
                        if (findv.feesTypeId == item.FeesTypeID)
                        {
                            item.EditedDiscAmount = findv.editedDiscount;
                            item.NetFees = findv.netFees;
                        }
                    }
                }
                db.SaveChanges();
                return "Done";
            }
            catch (Exception e)
            {
                return e.InnerException.ToString();
            }
        }

        // GET: tblGenerateFeesMsts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblGenerateFeesMst tblGenerateFeesMst = db.tblGenerateFeesMst.Find(id);
            tblGenerateFeesMst.IsDelete = true;
            db.SaveChanges();
            if (tblGenerateFeesMst == null)
            {
                return HttpNotFound();
            }
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
