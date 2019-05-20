using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Stocks
{
    public class GetStockListRequest : BaseRequest
    {
        [Required]
        public int SalesRepID { get; set; }
    }
}