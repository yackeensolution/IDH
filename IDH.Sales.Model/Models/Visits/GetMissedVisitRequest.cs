using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class GetMissedVisitRequest : BaseRequest
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}