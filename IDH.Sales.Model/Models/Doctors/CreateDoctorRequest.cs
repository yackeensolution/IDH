using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Doctors
{
    public class CreateDoctorRequest : BaseRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public Guid SpecialityID { get; set; }

        [Required]
        public string SpecialityName { get; set; }

        public Guid? UserID { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Mobile { get; set; }

        [Required]
        public Guid[] Orgs { get; set; }

    }
}