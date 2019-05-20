using IDH.Sales.BLL.Services.UserServices;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.UserModels;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.RestAPI.Filters;
using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IDH.Sales.RestAPI.Controllers
{
    [EnableCors(origins: "http://yakensolution.cloudapp.net:80", headers: "*", methods: "*")]
    public class UserController : BaseAPIController
    {
        private readonly UserService userService;
        public UserController()
        {
            this.userService = new UserService();
        }

        [HttpPost, AllowAnonymous]
        public IHttpActionResult Login(LoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = userService.Login(model.Name, model.Password, model.DeviceToken, model.CompanyID, model.Louck, model.employeeID);
                    if (result == null)
                    {
                        return BadRequest("Invalid user name or password");
                    }
                    if (result.Louck == 1)
                    {
                        return BadRequest("Sorry This account Locked");
                    }
                    if (result != null)
                    {
                        return Ok(new BaseResponse(result));
                    }
                }
                return BadRequest("Invalid user name or password");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult UpdateDeviceToken(UpdateDeviceTokenRequest model)
        {
            if (ModelState.IsValid)
            {
                var success = userService.UpdateDeviceToken(model.SecurityToken, model.DeviceToken);
                if (success)
                {
                    return Ok(new BaseResponse());
                }
            }
            return BadRequest("Some data is missed");
        }


        [HttpPost, AllowAnonymous]
        public IHttpActionResult UpdatePassword(ChangePasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = userService.UpdatePassword(model.Name, model.OldPassword, model.NewPassword, model.CreationDate, model.CompanyID);
                if (result == false)
                    return Ok("Update Faild");
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AllowAnonymous]
        public IHttpActionResult ResetPassword(ResetPasswordRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = userService.ResetPassword(model.UserID, model.NewPassword, model.CreationDate);
                return Ok(new BaseResponse(new { Contact = "info@info.com" }));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, RolesAdminRequired]
        public IHttpActionResult UpdatePeriod(UpdatePeriodModel model)
        {
            if (ModelState.IsValid)
            {
                var result = userService.UpdatePriod(model.Period, model.UserID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult Logout(LogoutModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    userService.Logout(model.SecurityToken);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

            return BadRequest("Some data is missed");
        }

    }
}
