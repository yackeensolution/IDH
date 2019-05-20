using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Stocks
{
    public class GetStockReportModel:BaseRequest
    {

        [Required]
        public int Month { get; set; }

        public Guid UserID { get; set; }

        [Required]
        public Guid RoleID { get; set; }
    }
}
