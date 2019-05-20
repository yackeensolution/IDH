using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Doctors
{
    public class DoctorSimpleModel:BaseRequest
    {
        [Required]
        public Guid DoctorID { get; set; }
    }
}
