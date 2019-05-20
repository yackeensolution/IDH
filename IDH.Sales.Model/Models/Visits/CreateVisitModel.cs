using IDH.Sales.Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class CreateVisitRequest : BaseRequest
    {
        [Required]
        public Guid UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public Guid DoctorID { get; set; }

        [Required]
        public string DoctorName { get; set; }

        [Required]
        public Guid OrgID { get; set; }

        [Required]
        public string OrgName { get; set; }

        [Required]
        public DateTime VisitDate { get; set; }

       [Required]
        public VisitLevel VisitLevel { get; set; }

        [Required]
        public VisitType Type { get; set; }
    }
}