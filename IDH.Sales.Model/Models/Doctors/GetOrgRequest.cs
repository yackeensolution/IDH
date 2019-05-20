using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IDH.Sales.Model.Models.Doctors
{
    public class GetOrgRequest:BaseRequest
    {
        public Guid? DoctorID { get; set; }
    }
}