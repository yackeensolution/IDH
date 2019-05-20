using IDH.Sales.BLL.Services.UserServices;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Types;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace IDH.Sales.RestAPI.Filters
{
    public class AuthorizationRequiredAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string Token = "Token";
        private const string UserID = "UserID";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            BaseResponse res = new BaseResponse();
            try
            {
                if (filterContext.Request.Headers.Contains(Token) && filterContext.Request.Headers.Contains(UserID))
                {
                    var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                    var UserIDValue = filterContext.Request.Headers.GetValues(UserID).First();

                    UserService userService = new UserService();
                    // Validate Token
                    if (!userService.ValidateSecurityToken(Guid.Parse(tokenValue), Guid.Parse(UserIDValue)))
                    {
                        filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(System.Net.HttpStatusCode.Unauthorized, "You need to login"));
                    }
                }
                else
                {
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.Unauthorized, "You need to login"));
                }
            }
            catch (Exception ex)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.InternalServerError, ex.StackTrace));
            }
            base.OnActionExecuting(filterContext);
        }
    }


    public class RolesAdminManagerRequiredAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string Token = "Token";
        private const string UserID = "UserID";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            BaseResponse res = new BaseResponse();
            try
            {
                if (filterContext.Request.Headers.Contains(Token) && filterContext.Request.Headers.Contains(UserID))
                {
                    var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                    var UserIDValue = filterContext.Request.Headers.GetValues(UserID).First();
                    BLL.UserServiceReference.UserWebService userWebService = new BLL.UserServiceReference.UserWebService();


                    var user = userWebService.GetUserDataByGuid(Guid.Parse(UserIDValue).ToString());
                    string RoleName = Enum.GetName(typeof(SalesUserType), (Int32)user.RuleName);
                    if (RoleName != "SalesAdmin" && RoleName != "SalesManager")
                    {
                        filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.Unauthorized, "You Should be SalesAdmin Or SalesManager"));
                    }
                }
                else
                {
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.Unauthorized, "You Should be SalesAdmin Or SalesManager"));
                }
            }
            catch (Exception ex)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.InternalServerError, ex.StackTrace));
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class RolesAdminRequiredAttribute : System.Web.Http.Filters.ActionFilterAttribute
    {
        private const string Token = "Token";
        private const string UserID = "UserID";
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            BaseResponse res = new BaseResponse();
            try
            {
                if (filterContext.Request.Headers.Contains(Token) && filterContext.Request.Headers.Contains(UserID))
                {
                    var tokenValue = filterContext.Request.Headers.GetValues(Token).First();
                    var UserIDValue = filterContext.Request.Headers.GetValues(UserID).First();
                    BLL.UserServiceReference.UserWebService userWebService = new BLL.UserServiceReference.UserWebService();


                    var user = userWebService.GetUserDataByGuid(Guid.Parse(UserIDValue).ToString());
                    string RoleName = Enum.GetName(typeof(SalesUserType), (Int32)user.RuleName);
                    if (RoleName != "SalesAdmin")
                    {
                        filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.Unauthorized, "You Should be SalesAdmin Or SalesManager"));
                    }
                }
                else
                {
                    filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.Unauthorized, "You Should be SalesAdmin Or SalesManager"));
                }
            }
            catch (Exception ex)
            {
                filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.OK, new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            base.OnActionExecuting(filterContext);
        }
    }



}