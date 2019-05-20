using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Doctors
{
    public class CreateOrgRequest : BaseRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Contact { get; set; }

        public string Phone { get; set; }

        public Guid UserID { get; set; }
    }
}