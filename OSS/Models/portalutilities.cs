using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using OSS.Models;

namespace OSS.Models
{
    //Session["employee"] = emp;
    //Session["UserID"] = emp.UserID;
    //Session["name"] = emp.UserName;
    //Session["schollid"] = emp.SchoolID;
    //Session["roleid"] = emp.RoleID;

    static class portalutilities
    {

        //  public static int _schollid = Convert.ToInt32(System.Web.HttpContext.Current.Session["schollid"]);
       
        public static int ActiveSessionID = 16;
        public static int AdminUserID = 1;
        public static int _schollid = 1;
        public static int LoginUserID = 1;
        public static bool InEnglish = false;
        //public static string _username =(System.Web.HttpContext.Current.Session["name"]).ToString();
        public static string _username = "admin1";
        public static DateTime _GetDate = DateTime.Now;
        //public static int _RoleID = Convert.ToInt32(System.Web.HttpContext.Current.Session["roleid"]);
        public static int _RoleID = 1;

        //public static object BindStage()
        //{
        //    using (OssEntities cntx = new OssEntities())
        //    {
        //        var obj = (from q in cntx.tblStage
        //                   where q.IsActive == true && q.IsDelete == false
        //                       && q.SchoolID == portalutilities._schollid
        //                   select new { q.StageID, q.StageName }).ToList();

        //        return obj;
        //        //if (obj != null)
        //        //{
        //        //    cmb.DataSource = obj;
        //        //    cmb.DisplayMember = "StageName";
        //        //    cmb.ValueMember = "StageID";
        //        //}

        //        //cmb.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        //cmb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        //AutoCompleteStringCollection data = new AutoCompleteStringCollection();

        //        //for (int i = 0; i <= obj.Count - 1; i++)
        //        //{
        //        //    data.Add(obj[i].StageName);
        //        //}

        //        //cmb.AutoCompleteCustomSource = data;
        //    }
        //}

        #region Fee Module

        public static string DueDateDiscName = "Due Date Discount";
        public static string MonthYearFomrat = "MMM-yy";
        public static string DateTimeFormat = "dd-MMM-yyyy";

        public struct DiscountType
        {
            public static string Percentage = "Percentage";
            public static string Fixed = "Fixed";
        }

        public struct FeesStatus
        {
            public static string Paid = "Paid";
            public static string UnPaid = "UnPaid";
            public static string Partial = "Partial";
            public static string Received = "Received";
        }

        public struct TransactionTypeFees
        {
            public static string FeesReceive = "Fees Receive";
            public static string GenerateFees = "Generate Fees";
        }

        public struct ChargeFee
        {
            public static string OneTime = "One Time";
            public static string Monthly = "Monthly";
            public static string HalfYearly = "Half Yearly";
            public static string Quaterly = "Quaterly";
            public static string Yearly = "Yearly";
        }

        public static object BindChargeFee()
        {
            List<string> obj = new List<string>();
            portalutilities.ChargeFee pMS = new portalutilities.ChargeFee();

            FieldInfo[] fields = pMS.GetType().GetFields();
            int k = 0;
            foreach (var xInfo in fields)
            {
                //cmbChargeFee.Items.Insert(k, xInfo.GetValue(pMS).ToString());
                obj.Insert(k, xInfo.GetValue(pMS).ToString());
                k++;
            }

            return obj;
        }

        #endregion

    }

}