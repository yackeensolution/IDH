using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class ChangeVisitDateRequest : BaseRequest
    {
        [Required]
        public int VisitID { get; set; }

        [Required]
        public DateTime NewDate { get; set; }
    }
}