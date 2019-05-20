using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class MapLocationRequest:BaseRequest
    {
        [Required]
        public DateTime Day { get; set; }

        [Required]
        public Guid SalesRepID { get; set; }
    }
}
