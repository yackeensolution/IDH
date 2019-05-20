using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class VisitTestRequest 
    {
        [Required]
        public Guid TestID { get; set; }

        [Required]
        public int FeedbackID { get; set; }

        public string Comment { get; set; }
    }
}