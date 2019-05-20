using IDH.Sales.BLL.StockWebReference;
using IDH.Sales.Model.Models.Stocks;
using IDH.Sales.Model.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.BLL.Services.StockServices
{
    public class StockService
    {
        private readonly StockWebService stockWebService;

        public StockService()
        {
            this.stockWebService = new StockWebService();
        }

        public YKN_P_GetStock_Result[] GetStock(GetStockRequest model)
        {
            return stockWebService.GetStock(model.SalesRepID.ToString(), model.CategoryID.ToString(), 
                model.CompanyID,model.Month,model.UserID.ToString(), model.ItemID.ToString());
        }

        public YKN_P_GetStockCategory_Result[] GetStockCategory(UserSimpleModel model)
        {
            return stockWebService.GetStockCategory(model.UserID.ToString(), model.CompanyID);
        }

        public YKN_P_GetStockitemByCategory_Result[] GetStockItemByCategory(GetStockItemByCategoryModel model)
        {
            return stockWebService.GetStockItemByCategory(model.CategoryID, model.UserID.ToString(),model.CompanyID);
        }

        public YKN_P_GetStockReportDetails_Result[] GetStockReportDetails(GetStockReportModel model)
        {
            return stockWebService.GetStockReportDetails(model.CompanyID, model.Month, model.UserID.ToString(), model.RoleID.ToString());
        }

        public void AssignStockItem(AssignStockModel model)
        {
            stockWebService.AssignStockItem(model.FromUserID, model.ItemID, model.ToUserID, model.Qty, model.CompanyID, Guid.NewGuid());
        }
    
    }
}
