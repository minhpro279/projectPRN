using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectPRN.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;


namespace projectPRN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            var view = View("Views/Login.cshtml");
            return view;
        }

        //public IActionResult Mainpage(string pass, string user)
        //{
        //    FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();

        //    if (HttpContext.Session.GetString("currentUsername") != null && HttpContext.Session.GetString("currentPassword") != null)
        //    {
        //        user = HttpContext.Session.GetString("currentUsername");
        //        pass = HttpContext.Session.GetString("currentPassword");
        //    }

        //    List<Account> login = context.Accounts.ToList<Account>().FindAll(x => x.Username.Equals(user));
        //    var view = View("Views/Login.cshtml");
        //    if (login.Count < 1)
        //    {
        //        view = View("Views/LoginFailed.cshtml");
        //    }
        //    else
        //    {
        //        foreach (Account Account in login)
        //        {
        //            if (Account.Username.Equals(user) && Account.Password.Equals(pass))
        //            {
        //                HttpContext.Session.SetString("currentUsername", user);
        //                HttpContext.Session.SetString("currentPassword", pass);
        //                string currentId = context.Accounts.Where(x => x.Username.Equals(user)).First().StudentId;
        //                HttpContext.Session.SetString("currentId", currentId);
        //                view = (ViewResult)IndexPage(HttpContext);
        //            }
        //            else
        //            {
        //                view = View("Views/LoginFailed.cshtml");
        //            }
        //        }
        //    }
        //    return view;
        //}

        public IActionResult indexPage()
        {
            var view = (ViewResult)IndexPage(HttpContext);
            if (HttpContext.Session.GetString("loginState").Equals(false.ToString()))
            {
                view = View("Views/Login.cshtml");
            }
            return view;
        }

        public IActionResult Mainpage(string user, string pass)
        {
            FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();

            if (HttpContext.Session.GetString("currentUsername") != null && HttpContext.Session.GetString("currentPassword") != null)
            {
                user = HttpContext.Session.GetString("currentUsername");
                pass = HttpContext.Session.GetString("currentPassword");
            }

            List<Account> login = context.Accounts.ToList<Account>().FindAll(x => x.Username.Equals(user));
            if (login.Count < 1)
            {
                HttpContext.Session.SetString("loginState", false.ToString());
                ViewBag.loginCheck = false;
            }
            else
            {
                foreach (Account Account in login)
                {
                    if (Account.Username.Equals(user) && Account.Password.Equals(pass))
                    {
                        HttpContext.Session.SetString("currentUsername", user);
                        HttpContext.Session.SetString("currentPassword", pass);
                        string currentId = context.Accounts.Where(x => x.Username.Equals(user)).First().StudentId;
                        HttpContext.Session.SetString("currentId", currentId);
                        HttpContext.Session.SetString("loginState", true.ToString());
                    }
                    else
                    {
                        HttpContext.Session.SetString("loginState", false.ToString());
                        ViewBag.loginCheck = false;
                    }
                }
            }
            return indexPage();
        }

        public IActionResult IndexPage(HttpContext hcontext)
        {
            var view = View("Views/Index.cshtml");
            ViewBag.currentUsername = getCurrentUsername(hcontext);
            return view;
        }

        public string getCurrentUsername(HttpContext hcontext)
        {
            string currentUsername = hcontext.Session.GetString("currentUsername");
            return currentUsername;
        }

        public IActionResult logOut()
        {
            var view = View("Views/Login.cshtml");
            HttpContext.Session.Clear();
            HttpContext.Session.SetString("loginState", false.ToString());
            return view;
        }

        /// Exam Schedule

        public IActionResult ExamSchedule()
        {
            var view = View("Views/ExamSchedule.cshtml");
            if (HttpContext.Session.GetString("loginState") != null)
            {
                if (HttpContext.Session.GetString("loginState").Equals(true.ToString()))
                {
                    FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();
                    string currentId = HttpContext.Session.GetString("currentId");
                    ViewBag.subjectDetails = context.Subjects.ToList<Subject>();
                    ViewBag.courseDetails = context.Courses.ToList<Course>();
                    view.ViewData.Model = context.ExamSchedules.Include(x => x.Course).ToList<ExamSchedule>().FindAll(x => x.Course.StudentId.Equals(currentId));
                }
                else
                {
                    view = View("Views/InvalidRequest.cshtml");
                }
            }
            else { view = View("Views/InvalidRequest.cshtml"); }
            return view;
        }

        public IActionResult GradeReport(int termID, string subjectID)
        {
            var view = View("Views/GradeReport.cshtml");
            if (HttpContext.Session.GetString("loginState") != null)
            {
                if (HttpContext.Session.GetString("loginState").Equals(true.ToString()))
                {

                }
                else { view = View("Views/InvalidRequest.cshtml"); }
            }
            else { view = View("Views/InvalidRequest.cshtml"); }
            return view;
        }
    }
}
