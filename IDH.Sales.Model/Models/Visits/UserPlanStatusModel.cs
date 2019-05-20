using IDH.Sales.Model.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class UserPlanStatusModel
    { 
        public UserPlanStatusModel(Guid _UserID,int _PlanStatus)
        {
            this.UserID = _UserID;
            this.PlanStatus = (VisitPlanStatus)_PlanStatus;
        }
        public Guid UserID { get; set; }

        public VisitPlanStatus PlanStatus { get; set; }
    }
}
