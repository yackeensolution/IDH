using IDH.Sales.DAL.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IDH.Sales.BusinessServices.Services
{
    /// <summary>
    /// Summary description for SalesRepWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SalesRepWebService : System.Web.Services.WebService
    {
        Entities db = new Entities();

        [WebMethod]
        public List<YKN_P_GetSalesRepList_Result> GetSalesRepList(string _RoleID, string _UserID, int _CompanyID)
        {
            return db.YKN_P_GetSalesRepList(_RoleID, _UserID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetManagerList_Result> GetManagerList(int _CompanyID)
        {
            return db.YKN_P_GetManagerList(_CompanyID).ToList();
        }

        [WebMethod]
        public void ActivateDeactivate(Guid _Guid, Guid _ManagerID, Guid _UserGuid, Guid _SalesRepID,int _StatusCode,int _CompanyID)
        {
            db.YKN_Push_ActivateDeactivateSalesRep(_Guid, _ManagerID, _UserGuid, _SalesRepID, _StatusCode, _CompanyID);
        }

        [WebMethod]
        public void DeactivateManager(Guid _Guid, Guid _ManagerID, Guid _UserGuid, Guid _AlternateManagerID, int _StatusCode, int _CompanyID)
        {
            db.YKN_Push_DeactivateManager(_Guid, _UserGuid, _ManagerID,  _AlternateManagerID, _StatusCode, _CompanyID);
        }

        [WebMethod]
        public List<YKN_P_GetTeam_Result> GetTeam(string _ManagerID, int _CompanyID)
        {
            return db.YKN_P_GetTeam(_ManagerID, _CompanyID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetTeamManagers_Result> GetTeamManagers( int _CompanyID)
        {
            return db.YKN_P_GetTeamManagers( _CompanyID).ToList();
        }



    }
}
