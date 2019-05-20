using IDH.Sales.Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Visits
{
    public class GetDashboardRequrest : BaseRequest
    {
        [Required]
        public SalesUserType UserType { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public Guid RoleID { get; set; }
    }
}