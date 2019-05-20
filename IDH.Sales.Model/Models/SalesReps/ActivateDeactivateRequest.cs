using IDH.Sales.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.SalesReps
{
    public class ActivateDeactivateRequest:BaseRequest
    {
        public Guid ManagerID { get; set; }

        public Guid UserID { get; set; }

        public Guid SalesRepID { get; set; }

        public SalesRepStatus StatusCode { get; set; }
    }
}
