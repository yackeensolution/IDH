using IDH.Sales.BLL.SalesRepWebReference;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.SalesReps;
using IDH.Sales.Model.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.BLL.Services.SalesRepServices
{
    public class SalesRepService
    {
        private readonly SalesRepWebService salesRepService;

        public SalesRepService()
        {
            this.salesRepService = new SalesRepWebService();
        }

        //public YKN_P_GetSalesRepList_Result[] GetSalesRepList(GetSalesRepListRequest model)
        //{
        //    return salesRepService.GetSalesRepList(model.RoleID.ToString(), model.UserID.ToString(), model.CompanyID);
        //}

        public YKN_P_GetManagerList_Result[] GetManagerList(BaseRequest model)
        {
            return salesRepService.GetManagerList(model.CompanyID);
        }

        public void ActivateDeactivate(ActivateDeactivateRequest model)
        {
            salesRepService.ActivateDeactivate(Guid.NewGuid(), model.ManagerID, model.UserID, model.SalesRepID,(Int32) model.StatusCode, model.CompanyID);
        }

        public void DeactivateManager(DeactivateManagerRequest model)
        {
            salesRepService.DeactivateManager(Guid.NewGuid(), model.ManagerID, model.UserID, model.AlternativeManagerID,(Int32) model.StatusCode, model.CompanyID);
        }

        public YKN_P_GetTeam_Result[] GetTeam(UserSimpleModel model)
        {
            return salesRepService.GetTeam(model.UserID.ToString(), model.CompanyID);
        }

        public YKN_P_GetTeamManagers_Result[] GetTeamManagers(BaseRequest model)
        {
            return salesRepService.GetTeamManagers( model.CompanyID);
        }
    }
}
