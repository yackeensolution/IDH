using IDH.Sales.Model.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.SalesReps
{
    public class DeactivateManagerRequest:BaseRequest
    {
        [Required]
        public Guid ManagerID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        public Guid AlternativeManagerID { get; set; }

        [Required]
        public SalesRepStatus StatusCode { get; set; }
    }
}
