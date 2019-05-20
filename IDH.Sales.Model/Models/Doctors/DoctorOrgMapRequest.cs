using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Doctors
{
    public class DoctorOrgMapRequest : BaseRequest
    {
        [Required]
        public int DoctorID { get; set; }

        [Required]
        public int OrgID { get; set; }
    }
}