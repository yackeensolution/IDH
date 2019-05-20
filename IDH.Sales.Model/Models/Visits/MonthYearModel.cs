using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class MonthYearModel:BaseRequest
    {
        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }
    }
}
