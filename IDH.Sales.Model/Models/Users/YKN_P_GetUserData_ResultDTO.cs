using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.Model.Models.Users
{
    public class YKN_P_GetUserData_ResultDTO
    {
        public string FullName { get; set; }
        public System.Guid UserGUID { get; set; }
        public string Email { get; set; }
        public string ImageURL { get; set; }
        public string Password { get; set; }
        public System.Guid RuleGUID { get; set; }
        public string RuleName { get; set; }
    }
}
