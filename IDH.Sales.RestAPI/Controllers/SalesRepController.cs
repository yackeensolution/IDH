using IDH.Sales.BLL.Services.SalesRepServices;
using IDH.Sales.BLL.Services.VisitServices;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.SalesReps;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.Model.Types;
using IDH.Sales.RestAPI.Filters;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IDH.Sales.RestAPI.Controllers
{
    [EnableCors(origins: "http://yakensolution.cloudapp.net:80", headers: "*", methods: "*")]
    public class SalesRepController : BaseAPIController
    {
        private readonly SalesRepService salesRepService;
        private readonly VisitService visitService;
        private readonly BLL.UserServiceReference.UserWebService userWebService;

        public SalesRepController()
        {
            this.salesRepService = new SalesRepService();
            this.visitService = new VisitService();
            this.userWebService = new BLL.UserServiceReference.UserWebService();
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult GetSalesRepList(UserSimpleModel model)
        {
            var re = Request;
            var headers = re.Headers;
            if (headers.Contains("UserID"))
            {
                string userID = headers.GetValues("UserID").First();
                var user = userWebService.GetUserDataByGuid(Guid.Parse(userID).ToString());
                string RoleName = Enum.GetName(typeof(SalesUserType), (Int32)user.RuleName);
                if (RoleName != "SalesAdmin" && RoleName != "SalesManager")
                {
                    return Ok(new BaseResponse(HttpStatusCode.Unauthorized, "You Should be SalesAdmin Or SalesManager"));
                }
            }
            try
            {
                if (ModelState.IsValid)
                {
                    //This represent managerID to get the subordinates by
                    return Ok(new BaseResponse(salesRepService.GetTeam(model).ToList().Select(s => new
                    {
                        s.SalesRepID,
                        s.SalesRepName,
                        ManagerID = s.ManagerGuid,
                        s.ManagerName,
                        Mobile = s.new_Mobile,
                        s.ActiveDate,
                        Status = Enum.GetName(typeof(SalesRepStatus), s.Status)
                    })));
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost, AuthorizationRequired,RolesAdminRequired]
        public IHttpActionResult GetManagerList(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(salesRepService.GetManagerList(model).Select(s => new
                {
                    ID = s.ManagerGUID,
                    Name = s.ManagerName
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetTeam(UserSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    var resultset = salesRepService.GetTeam(model).ToList();
                    var teamIDs = resultset.Select(s => s.SalesRepID).ToList();
                    var teamPlanStatus = visitService.GetTeamVisitStatus(teamIDs);
                    return Ok(new BaseResponse(resultset.Select(s => new
                    {
                        s.SalesRepID,
                        s.SalesRepName,
                        ManagerID = s.ManagerGuid,
                        s.ManagerName,
                        Mobile = s.new_Mobile,
                        s.ActiveDate,
                        Status = Enum.GetName(typeof(SalesRepStatus), s.Status),
                        PlanStatus=teamPlanStatus.Where(w=>w.UserID==s.SalesRepID).Select(m=>m.PlanStatus).FirstOrDefault()
                    })));
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
           
        }

        [HttpPost, AuthorizationRequired,RolesAdminRequired]
        public IHttpActionResult GetTeamManagers(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(salesRepService.GetTeamManagers(model).ToList().Select(s => new
                {
                    ManagerID = s.ManagerGuid,
                    s.ManagerName,
                    Mobile = s.new_Mobile,
                    s.ActiveDate,
                    Status = Enum.GetName(typeof(SalesRepStatus), s.Status)
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired,RolesAdminRequired]
        public IHttpActionResult ActivateDeactivate(ActivateDeactivateRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                salesRepService.ActivateDeactivate(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired,RolesAdminRequired]
        public IHttpActionResult DeactivateManager(DeactivateManagerRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                salesRepService.DeactivateManager(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }
    }
}
