using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Types
{
    public enum VisitPlanStatus
    {
        Created=1,  //the used is currently filling his monthly visit plan
        Submitted=2,    // the user has submitted his plan and waiting for approval
        Approved=3,
        Rejected=4
    }
}
