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
        public static int _schollid = 1;
        //public static string _username =(System.Web.HttpContext.Current.Session["name"]).ToString();
        public static string _username = "admin1";
        public static DateTime _GetDate = DateTime.Now;
        //public static int _RoleID = Convert.ToInt32(System.Web.HttpContext.Current.Session["roleid"]);
        public static int _RoleID = 1;

        //var obj = (from q in cntx.tblStudentRegMst
        //           join a in cntx.tblAdmission on q.RegID equals a.RegID
        //           join c in cntx.Vu_Class on a.AdmissionID equals c.AdmissionID
        //           where q.IsActive == true && q.IsDelete == false && q.SchoolID == PortalUtilities.loginschoolid
        //           && c.StageID == StgID && c.SessionID == PortalUtilities.ActiveSessionID
        //           select new
        //           {
        //               a.AdmissionID,
        //               a.GRNo,
        //               q.ApplicantName,
        //               q.ApplicantNameInUrdu,
        //               q.FatherName,
        //               q.FatherNameInUrdu
        //           }).ToList();

    }
    
}