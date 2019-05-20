using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class VisitStockRequest 
    {
        [Required]
        public Guid ItemID { get; set; }

        [Required]
        public int Qty { get; set; }
    }
}