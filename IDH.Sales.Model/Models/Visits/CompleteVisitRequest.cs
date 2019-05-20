using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IDH.Sales.Model.Models.Visits
{
    public class CompleteVisitRequest : BaseRequest
    {
        [Required]
        public Guid VisitID { get; set; }

        [Required]
        public List<VisitStockRequest> Stocks { get; set; }

        [Required]
        public List<VisitTestRequest> Tests { get; set; }
    }
}