using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
       
        public static int ActiveSessionID = 1;
        public static int _schollid = 1;
        public static bool InEnglish = true;
        //public static string _username =(System.Web.HttpContext.Current.Session["name"]).ToString();
        public static string _username = "admin1";
        public static DateTime _GetDate = DateTime.Now;
        //public static int _RoleID = Convert.ToInt32(System.Web.HttpContext.Current.Session["roleid"]);
        public static int _RoleID = 1;

        #region Fee Module

        public static string MonthYearFomrat = "MMM-yyyy";

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

        public struct ChargeFee
        {
            public static string OneTime = "One Time";
            public static string Monthly = "Monthly";
            public static string HalfYearly = "Half Yearly";
            public static string Quaterly = "Quaterly";
            public static string Yearly = "Yearly";
        }

        #endregion

    }

}