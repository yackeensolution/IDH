using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class CreatePlanRequest:BaseRequest
    {
        [Required]
        public string PlanName { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public Guid UserID { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
