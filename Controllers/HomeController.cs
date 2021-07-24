using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using projectPRN.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace projectPRN.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            var view = View("Views/Login.cshtml");
            if (HttpContext.Session.GetString("RegisterSuccess") != null)
            {
                ViewBag.success = true;
            }
            return view;
        }

        public IActionResult indexPage()
        {
            var view = (ViewResult)Homepage(HttpContext);
            if (HttpContext.Session.GetString("loginState").Equals(false.ToString()))
            {
                view = View("Views/Login.cshtml");
            }
            return view;
        }

        public IActionResult Mainpage(string user, string pass)
        {
            FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();

            //if (HttpContext.Session.GetString("currentUsername") != null && HttpContext.Session.GetString("currentPassword") != null)
            //{
            //    user = HttpContext.Session.GetString("currentUsername");
            //    pass = HttpContext.Session.GetString("currentPassword");
            //}

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
                        //HttpContext.Session.SetString("currentPassword", pass);
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

        public IActionResult Homepage(HttpContext hcontext)
        {
            var view = View("Views/newIndex.cshtml");
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
                    int? termID = context.StudentInfos.ToList().FindAll(x => x.StudentId.Equals(currentId)).First().CurrentTerm;
                    ViewBag.currentUsername = getCurrentUsername(HttpContext);
                    view.ViewData.Model = context.ExamSchedules.Include(x => x.Course.Subject).ToList<ExamSchedule>().FindAll(x => x.Course.StudentId.Equals(currentId) && x.Course.TermId.Equals(termID));
                }
                else
                {
                    view = View("Views/InvalidRequest.cshtml");
                }
            }
            else { view = View("Views/InvalidRequest.cshtml"); }
            return view;
        }

        public IActionResult GradeReport(int termID, int courseID)
        {
            var view = View("Views/GradeReport.cshtml");
            if (HttpContext.Session.GetString("loginState") != null)
            {
                if (HttpContext.Session.GetString("loginState").Equals(true.ToString()))
                {
                    FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();
                    string currentId = HttpContext.Session.GetString("currentId");
                    ViewBag.currentUsername = getCurrentUsername(HttpContext);
                    ViewBag.terms = context.TermStudents.Include(x => x.Term).ToList().FindAll(x => x.StudentId.Equals(currentId));
                    if (termID != 0)
                    {
                        ViewBag.tempTermID = termID;
                        ViewBag.courses = context.Courses.Include(x => x.Subject).Include(x => x.Term).ToList().Where(x => x.StudentId.Equals(currentId) && x.TermId.Equals(termID));
                        if (courseID != 0)
                        {
                            List<Grade> list = context.Grades.ToList().FindAll(x => x.CourseId.Equals(courseID));
                            if(list.Count < 1)
                            {
                                ViewBag.GradeError = true;
                            }
                            else { ViewBag.grade = list; }
                        }
                    }
                }
                else { view = View("Views/InvalidRequest.cshtml"); }
            }
            else { view = View("Views/InvalidRequest.cshtml"); }
            return view;
        }

        public IActionResult Register()
        {
            var view = View("Views/Register.cshtml");
            return view;
        }

        public IActionResult RegisterConfirm(string fullname, string username, string password, string major)
        {
            FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();
            List<Account> accounts = context.Accounts.ToList<Account>();

            var view = View("Views/Register.cshtml");
            if (fullname == null)
            {
                ViewBag.fullnameInvalid = true;
            }
            if (username == null)
            {
                ViewBag.usernameInvalid = true;
            }
            else
            {
                foreach (Account acc in accounts)
                {
                    if (acc.Username.Trim().Equals(username.Trim()))
                    {
                        ViewBag.duplicateUsername = true;
                    }
                }
            }

            if (password == null)
            {
                ViewBag.passwordInvalid = true;
            }

            if (major.Trim().Equals("0"))
            {
                ViewBag.majorInvalid = true;
            }


            if (ViewBag.fullnameInvalid != true && ViewBag.usernameInvalid != true && ViewBag.passwordInvalid != true
                && ViewBag.duplicateUsername != true && ViewBag.majorInvalid != true)
            {
                StudentInfo student = new StudentInfo();
                int newID = Convert.ToInt32(context.StudentInfos.Max(x => x.StudentId).Trim()) + 1;
                student.StudentId = newID.ToString();
                student.Major = major;
                student.StudentName = fullname;
                student.CurrentTerm = null;
                context.StudentInfos.Add(student);
                Account account = new Account();
                account.StudentId = student.StudentId;
                account.Username = username.Trim();
                account.Password = password.Trim();
                context.Accounts.Add(account);
                context.SaveChanges();
                HttpContext.Session.SetString("RegisterSuccess", true.ToString());
                view = (ViewResult)Login();
            }
            return view;
        }

        public IActionResult Profile()
        {
            var view = View("Views/Profile.cshtml");
            if (HttpContext.Session.GetString("loginState") != null)
            {
                if (HttpContext.Session.GetString("loginState").Equals(true.ToString()))
                {
                    FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();
                    string currentId = HttpContext.Session.GetString("currentId");
                    ViewBag.currentUsername = getCurrentUsername(HttpContext);
                    ViewBag.terms = context.Terms.ToList();
                    //if (HttpContext.Session.GetString("UpdateSuccess").Equals(true.ToString()))
                    //{
                    //    ViewBag.UpdateSuccess = true;
                    //}
                    view.ViewData.Model = context.StudentInfos.Where(x => x.StudentId.Equals(currentId)).First();
                }
                else
                {
                    view = View("Views/InvalidRequest.cshtml");
                }
            }
            else { view = View("Views/InvalidRequest.cshtml"); }

            return view;
        }

        public IActionResult ProfileUpdate(string fullname, string major, int? currentTerm)
        {
            if (fullname == null)
            {
                ViewBag.fullnameInvalid = true;
            }

            if (ViewBag.fullnameInvalid != true)
            {
                string currentId = HttpContext.Session.GetString("currentId");
                FAP_PRN_ProjectContext context = new FAP_PRN_ProjectContext();
                StudentInfo info = context.StudentInfos.Where(x => x.StudentId.Equals(currentId)).First();
                info.StudentName = fullname;
                info.Major = major;
                info.CurrentTerm = currentTerm;
                context.SaveChanges();
                ViewBag.UpdateSuccess = true;
                //HttpContext.Session.SetString("UpdateSuccess", true.ToString());
            }
                return (ViewResult)Profile();
        }
    }
}
