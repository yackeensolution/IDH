using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Stocks
{
    public class AssignStockModel:BaseRequest
    {
      

        public Guid FromUserID { get; set; }    //should be Sales Manager

        [Required]
        public Guid ItemID { get; set; }

        [Required]
        public Guid ToUserID { get; set; }

        [Required]
        public int Qty { get; set; }

    }
}
