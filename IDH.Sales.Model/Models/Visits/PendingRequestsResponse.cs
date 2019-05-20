using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class PendingRequestsResponse
    {

        public string RequestID { get; set; }

        public string RequestName { get; set; }

        public string PlanMonth { get; set; }

        public int VisitsCount { get; set; }

        public string RequestType { get; set; }

        public string SalesRepID { get; set; }

        public string SalesRepName { get; set; }

        public string CategoryName { get; set; }

        public string SpecialityName { get; set; }

        public string ManagerName { get; set; }
    }
}
