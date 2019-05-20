using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class GetVisitRequest : BaseRequest
    {

        public DateTime? Day { get; set; }

        public string DoctorName { get; set; }

        [Required]
        public Guid UserID { get; set; }
    }
}