using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.UserModels
{
    public class LogoutModel : BaseRequest
    {
        [Required]
        public Guid SecurityToken { get; set; }
    }
}