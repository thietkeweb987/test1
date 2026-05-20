using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pro.Models;

namespace pro.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            // Trả về đúng file giao diện Views/Home/Login.cshtml
            // Truyền vào một đối tượng User trống để né lỗi @Model.Email bị null
            return View(new pro.Models.User());
        }
        [HttpPost]
        public ActionResult Login(User model)
        {
            if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin!");
                return View(new User());
            }

            string txtRole = (model.Role ?? "").ToString().Trim().ToLower();
            string txtEmail = (model.Email ?? "").ToString().Trim().ToLower();
            string txtPassword = (model.Password ?? "").ToString().Trim();

            // 1. Kiểm tra tài khoản Admin
            if (txtRole == "admin")
            {
                if (txtEmail != "admin@company.com")
                {
                    ModelState.AddModelError("Email", "Email Admin không chính xác hoặc chưa được đăng ký!");
                    return View(model);
                }
                if (txtPassword != "123456")
                {
                    ModelState.AddModelError("Password", "Mật khẩu Admin nhập vào không đúng!");
                    return View(model);
                }

                Session["UserRole"] = "admin";
                return RedirectToAction("Page1");
            }

            // 2. Kiểm tra tài khoản Mentor
            else if (txtRole == "mentor")
            {
                if (txtEmail != "mentor@company.com")
                {
                    ModelState.AddModelError("Email", "Email Mentor không chính xác!");
                    return View(model);
                }
                if (txtPassword != "123456")
                {
                    ModelState.AddModelError("Password", "Mật khẩu Mentor không đúng!");
                    return View(model);
                }

                Session["UserRole"] = "mentor";
                return RedirectToAction("Page2");
            }

            // 3. Kiểm tra tài khoản Intern
            else if (txtRole == "intern")
            {
                if (txtEmail != "intern@company.com")
                {
                    ModelState.AddModelError("Email", "Email Thực tập sinh không chính xác!");
                    return View(model);
                }
                if (txtPassword != "123456")
                {
                    ModelState.AddModelError("Password", "Mật khẩu Thực tập sinh không đúng!");
                    return View(model);
                }

                Session["UserRole"] = "intern";
                return RedirectToAction("Page3");
            }

            // Nếu không chọn vai trò nào hợp lệ
            ModelState.AddModelError("Role", "Vui lòng chọn đúng vai trò hệ thống!");
            return View(model);
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