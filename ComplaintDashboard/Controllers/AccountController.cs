using ComplaintDashboard.App_Start;
using ComplaintDashboard.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static ComplaintDashboard.Models.Account;

namespace ComplaintDashboard.Controllers
{
    public class AccountController : Controller
    {
        private Complaint_DashboardEntities1 db = new Complaint_DashboardEntities1();

        public AccountController()
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var user = await UserManager.FindByEmailAsync(model.Email);
            var data = db.AspNetUsers.Where(x => x.Email.Equals(model.Email) && x.Password.Equals(model.Password)).ToList();
            if (data.Count() > 0)
            {
                Session["Username"] = data.FirstOrDefault().Username;
                Session["Email"] = data.FirstOrDefault().Email;

                if (data.FirstOrDefault().Password.Contains("admin"))
                {
                    return RedirectToAction("Index", "AdminDash");
                }
                else 
                {
                    return View();
                }

            }
            else
            {
                return View();
            }
            //using (var db = new Complaint_DashboardEntities1())
            //{
            //    var _user = db.AspNetUsers.Where(x => x.Email == model.Email).FirstOrDefault();
            //    model.Username = _user.Username;
            //}

            //var result = await SignInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, shouldLockout: false);
            //var result = await SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, shouldLockout: false);
            //switch (result)
            //{
            //    case SignInStatus.Success:
            //        return RedirectToAction("Index", "AdminDash");
            //    case SignInStatus.Failure:
            //    default:
            //        ModelState.AddModelError("", "Invalid login attempt.");
            //        return View(model);
            //}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}