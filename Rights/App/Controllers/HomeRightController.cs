using Langben.BLL;
using Langben.DAL;
using Langben.IBLL;
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
            //var person = Utils.ReadCookieAsObj("SysPerson");
            //if (person == null)
            //{
            //    return Redirect("/Account");
            //}
            ViewBag.MyName = "person.MyName";

            ISysAnnouncementBLL announcementBLL = new SysAnnouncementBLL();
            SysAnnouncement announcement = announcementBLL.GetTop(1);
            if (announcement!=null)
            {
                ViewBag.Announcement = announcement.Message;
            }
            
            

            return View();
        }
       
    }
}

