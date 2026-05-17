using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace pro.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        // Hàm này để sau này gọi trang Login (Views/Home/Login.cshtml)
        public ActionResult Login()
        {
            return View();
        }

        // Hàm này để gọi trang Mentor (Views/Home/Page2.cshtml)
        public ActionResult Page1()
        {
            return View();
        }
        // Hàm này để gọi trang Mentor (Views/Home/Page2.cshtml)
        public ActionResult Page2()
        {
            return View();
        } // Hàm này để gọi trang Mentor (Views/Home/Page2.cshtml)
        public ActionResult Page3()
        {
            return View();
        }
    }
}