using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OSS
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void session_start()
        {
            if (Session != null)
            {
                //create cookie with session id name
                Session.Timeout = 24 * 6000;
                HttpCookie cookie = new HttpCookie("ASP.NET_SessionId");
                //add the session id
                cookie.Value = Session.SessionID;
                //set expire to 12 hours
                cookie.Expires = DateTime.Now.AddHours(23).AddMinutes(59);
                //add the cookie to the response
                Response.Cookies.Add(cookie);
            }

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
        }
    }
}
