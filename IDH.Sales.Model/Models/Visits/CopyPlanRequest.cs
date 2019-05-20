using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class CopyPlanRequest:BaseRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int SourceMonth { get; set; }

        [Required]
        public int SourceYear { get; set; }

        [Required]
        public int TargetMonth { get; set; }

        [Required]
        public int TargetYear { get; set; }

        public Guid UserID { get; set; }
    }
}
