using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Langben.App.Controllers
{
    public class HomeRightController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var person = Utils.ReadCookieAsObj("SysPerson");
            if (person == null)
            {
                return Redirect("/Account");
            }
            ViewBag.MyName = person.MyName;
            return View();
        }
       
    }
}

