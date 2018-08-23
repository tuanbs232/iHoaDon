using iHoaDon.Business;
using iHoaDon.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WebDemoSigning.Filter;

namespace iHoaDon.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
       
        public HomeController()
        {
            
        }
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToAction("Dashboard");
            }
            else
            {
                return View();
            }
        }

        [PublicWebAuthentication]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
