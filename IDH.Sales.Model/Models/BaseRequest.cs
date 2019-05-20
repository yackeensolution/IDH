using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models
{
    public class BaseRequest
    {
        [Required]
        public int CompanyID { get; set; }
    }
}