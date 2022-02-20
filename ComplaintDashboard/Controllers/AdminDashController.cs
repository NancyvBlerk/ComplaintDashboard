using ComplaintDashboard.Models;
using ComplaintDashboard.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ComplaintDashboard.Controllers
{
    public class AdminDashController : Controller
    {
        private Complaint_DashboardEntities1 db = new Complaint_DashboardEntities1();

        public AdminDashController()
        {
        }
        // GET: AdminDash
        public ActionResult Index()
        {
            var vm = new SubmissionVM();
            using (var db = new Complaint_DashboardEntities1()) 
            {
                vm.ComplaintsList = db.Complaints.ToList();
            }

            return View(vm);
        }

        public ActionResult _Details(int id) 
        {
            var vm = new SubmissionVM();
            vm.CompDetails = db.Sel_IndivudualComplaint(id).ToList();
            
            foreach (var entry in vm.CompDetails) 
            {
                vm.Name = entry.Name;
                vm.Subject = entry.Subject;
                vm.DateTime = (DateTime)entry.DateTime;
                vm.Body = entry.Body;
            }

            return PartialView(vm);
        }
    }
}