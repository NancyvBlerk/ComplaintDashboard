using ComplaintDashboard.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplaintDashboard.Models
{
    public class SubmissionVM
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateTime { get; set; } = DateTime.Now;
        public List<Complaint> ComplaintsList { get; set; }
        public List<Sel_IndivudualComplaint_Result> CompDetails { get; set; }
    }

    public class ComplaintDetails 
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
    }
}