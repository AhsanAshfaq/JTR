using OSS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace OSS.Controllers
{
    public class LoginController : Controller
    {
        private OssEntities db = new OssEntities();
        //
        // GET: /Login/
        public ActionResult Login(string Username, string Password)
        {
            using (db)
            {
                tblUser emp = db.tblUser.Where(x => x.UserName == Username && x.Password == Password && x.IsActive == true && x.IsDelete !=true).FirstOrDefault();
                if (emp != null)
                {
                    Session["employee"] = emp;
                    Session["UserID"] = emp.UserID;
                    Session["name"] = emp.UserName;
                    Session["schollid"] = emp.SchoolID;
                    Session["roleid"] = emp.RoleID;


                    //string auth = "-N-Y".ToString();
                    String ThisMachine;
                    ThisMachine = WindowsIdentity.GetCurrent().Name.ToString();
                    TempData["msg"] = "Welcome to Login";
                    //if (ThisMachine == emp.SA1)
                    //{
                        return RedirectToAction("Index", "Home");


                   // return Content("User authorization failed --------!  Illegal software use ErrorCode." + ThisMachine);
                    // return Response.Write("User Authrization failed");
                }
                else if (Username != null || Password != null )
                {
                    ViewBag.msg = "Incorrect Login Attempt!";
                }
                

            }
            return View();
        }
        public ActionResult Logout()
        {
            Session["employee"] = null;
            return RedirectToAction("Login", "Login");
        }
	}
}