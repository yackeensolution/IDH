using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Types
{
   public enum VisitStatus
    {
        Pending = 1,
        Done = 2,
        Overdue = 3,
        Cancelled = 4,
        UnplannedDone = 5
    }
}
