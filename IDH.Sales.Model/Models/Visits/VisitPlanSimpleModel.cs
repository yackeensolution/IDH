using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class VisitPlanSimpleModel:BaseRequest
    {
        [Required]
        public int PlanID { get; set; }

      
    }
}
