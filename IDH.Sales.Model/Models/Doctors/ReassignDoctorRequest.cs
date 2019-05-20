using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Doctors
{
    public class ReassignDoctorRequest:BaseRequest
    {
        public Guid UserID { get; set; }

        [Required]
        public Guid DoctorID { get; set; }

        [Required]
        public Guid SalesRepID { get; set; }
    }
}
