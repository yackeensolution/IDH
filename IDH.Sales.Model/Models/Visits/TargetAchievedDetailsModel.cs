using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Visits
{
    public class TargetAchievedDetailsModel : BaseRequest
    {
        public int Month { get; set; }

        public int Year { get; set; }

        public Guid UserID { get; set; }

        public Guid RoleID { get; set; }
    }
}
