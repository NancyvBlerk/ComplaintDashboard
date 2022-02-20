using ComplaintDashboard.Models;
using ComplaintDashboard.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ComplaintDashboard.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var vm = new SubmissionVM();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(SubmissionVM vm) 
        {
            try
            {
                using (var db = new Complaint_DashboardEntities1()) 
                {
                    Complaint comp = new Complaint();

                    comp.Name = vm.Name;
                    comp.Subject = vm.Subject;
                    comp.Body = vm.Body;
                    comp.DateTime = vm.DateTime;

                    db.Entry(comp).State = EntityState.Added;
                    db.SaveChanges();
                }
            }
            catch (Exception ex) 
            {
            }

            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}