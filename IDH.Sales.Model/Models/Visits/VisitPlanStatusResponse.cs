using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class VisitPlanStatusResponse
    {
        public VisitPlanStatusResponse(Guid _UserID,int _Status)
        {
            this.UserID = _UserID;
            this.Status = _Status;
        }
        public Guid UserID { get; set; }

        public int Status { get; set; }
    }
}
