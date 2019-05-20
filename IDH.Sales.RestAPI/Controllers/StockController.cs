using IDH.Sales.BLL.Services.StockServices;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.Stocks;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.RestAPI.Filters;
using IDH.Sales.RestAPI.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IDH.Sales.RestAPI.Controllers
{
    [EnableCors(origins: "http://yakensolution.cloudapp.net:80", headers: "*", methods: "*")]
    [RolesAdminManagerRequired]
    public class StockController : BaseAPIController
    {
        private readonly StockService stockService;
        private readonly BLL.UserServiceReference.UserWebService userWebService;

        public StockController()
        {
            this.stockService = new StockService();
            this.userWebService = new BLL.UserServiceReference.UserWebService();
        }

        [HttpPost, AuthorizationRequired, CustomExceptionFilter]
        public IHttpActionResult GetStockReport(GetStockReportModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                return Ok(new BaseResponse(stockService.GetStockReportDetails(model).Select(s => new
                {
                    CategoryName = s.CategoryName,
                    ItemName = s.itemName,
                    StartBalance = 0,
                    CurrentBalance = s.availableStock,
                    SalesRepName = s.salesRepName,
                    DirectManager = s.DirectManagerName
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired, CustomExceptionFilter]
        public IHttpActionResult GetStockItems(GetStockItemByCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                return Ok(new BaseResponse(stockService.GetStockItemByCategory(model).Select(s => new
                {
                    ID = s.StockItemGUID,
                    Name = s.StockitemName,
                    Balance = s.Balance
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired, CustomExceptionFilter]
        public IHttpActionResult AssignStockItem(AssignStockModel model)
        {

            if (ModelState.IsValid)
            {
                model.FromUserID = this.UserID;
                stockService.AssignStockItem(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired, CustomExceptionFilter]
        public IHttpActionResult GetStock(GetStockRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                return Ok(new BaseResponse(stockService.GetStock(model).Select(s => new
                {
                    CategoryID = s.categoryID,
                    ItemName = s.itemName,
                    CurrentBalance = s.new_Balance,
                    s.StartBalance
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired, CustomExceptionFilter]
        public IHttpActionResult GetStockCategory(UserSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                return Ok(new BaseResponse(stockService.GetStockCategory(model).Select(s =>
                new
                {
                    s.CategoryID,
                    s.categoryName,
                })));
            }
            return BadRequest(ModelState);
        }
    }
}
