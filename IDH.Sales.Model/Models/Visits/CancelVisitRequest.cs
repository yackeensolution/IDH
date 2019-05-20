using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class CancelVisitRequest:BaseRequest
    {
        [Required]
        public Guid VisitID { get; set; }

        [Required]
        public string CancelReasonID { get; set; }
    }
}