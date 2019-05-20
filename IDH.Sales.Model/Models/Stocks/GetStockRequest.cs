using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Stocks
{
    public class GetStockRequest:BaseRequest
    {
        [Required]
        public Guid SalesRepID { get; set; }

        [Required]
        public int CategoryID { get; set; }
        public int Month { get; set; }
        public Guid UserID { get; set; }
        public Guid ItemID { get; set; }
    }
}
