using IDH.Sales.DAL.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IDH.Sales.BusinessServices.Services
{
    /// <summary>
    /// Summary description for StockWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StockWebService : System.Web.Services.WebService
    {

        Entities db = new Entities();

        [WebMethod]
        public List<YKN_P_GetStock_Result> GetStock(string _SalesRepID, string _CategoryID, int _CompanyID, int _Month, string _UserID, string _ItemID)
        {
            return db.YKN_P_GetStock(_SalesRepID, _CategoryID, _CompanyID, _Month, _UserID, _ItemID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetStockCategory_Result> GetStockCategory(string _UserID, int _CompanyID)
        {
            return db.YKN_P_GetStockCategory(_UserID, _CompanyID).ToList();
        }


        [WebMethod]
        public List<YKN_P_GetStockitemByCategory_Result> GetStockItemByCategory(Guid _CategoryID, string _SalesRepID, int _CompanyID)
        {
            return db.YKN_P_GetStockitemByCategory(_CategoryID, _SalesRepID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetStockReportDetails_Result> GetStockReportDetails(int _CompanyID, int _Month, string _UserID, string _RoleID)
        {
            return db.YKN_P_GetStockReportDetails(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        [WebMethod]
        public void AssignStockItem(Guid _UserID,Guid _ItemID,Guid _SalesRepID,int _Qty,int _CompanyID,Guid _Guid)
        {
            db.YKN_Push_AssignStockItem(_UserID,_ItemID,_SalesRepID,_Qty,_CompanyID,_Guid);
        }
    }
}
