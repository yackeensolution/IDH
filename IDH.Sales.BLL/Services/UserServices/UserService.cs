using IDH.Sales.BLL.Repository;
using IDH.Sales.DAL.Own;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.Model.Types;
using System;
using System.Linq;

namespace IDH.Sales.BLL.Services.UserServices
{
    public class UserService
    {
        private readonly UserServiceReference.UserWebService userWebService;
        private readonly UnitOfWork uow;

        public UserService()
        {
            this.userWebService = new UserServiceReference.UserWebService();
            this.uow = new UnitOfWork();
        }

        public LoginResultModel Login(string _UserName, string _Password, string _DeviceToken, int _CompanyID, int Louck, string employeeID)
        {
            var userData = userWebService.GetUserData(_UserName, _Password, _CompanyID);
            if (userData != null)
            {
                var secToken = Guid.NewGuid();
                var user = userWebService.GetUserDataByGuid(userData.UserGUID.ToString());
                Louck |= user.Lock ?? 0;
                if (Louck == 1)
                {
                    userWebService.UpdateLock(1, userData.UserGUID.ToString(), employeeID);
                }
                else
                {
                    var device = uow.Repo<UserDevice>().Insert(new UserDevice()
                    {
                        UserID = userData.UserGUID,
                        SecurityToken = secToken,
                        DeviceToken = _DeviceToken,
                        Badge = 0,
                        CompanyID = _CompanyID,
                        CreatedOn = DateTime.Now
                    });
                    uow.Save();
                }
                return new LoginResultModel(userData.UserGUID, secToken, userData.FullName, userData.ImageURL, userData.RuleGUID, Enum.GetName(typeof(SalesUserType), (Int32)userData.RuleName), Louck);
            }
            return null;
        }

        public UserServiceReference.YKN_P_GetUserDataGUID_Result GetUserDetails(Guid _UserID)
        {
            return userWebService.GetUserDataByGuid(_UserID.ToString());
        }

        public void Logout(Guid _SecurityToken)
        {
            uow.Repo<UserDevice>().Delete(w => w.SecurityToken == _SecurityToken);
            uow.Save();
        }

        public bool UpdateDeviceToken(Guid _SecurityToken, string _DeviceToken)
        {
            var device = uow.Repo<UserDevice>().GetFirstOrDefault(w => w.SecurityToken == _SecurityToken);
            if (device != null)
            {
                device.DeviceToken = _DeviceToken;
                uow.Save();
                return true;
            }
            return false;
        }

        public bool ResetPassword(string UserID, string NewPassword, DateTime LastUpdate)
        {
            userWebService.UpdatePassword(NewPassword, UserID, LastUpdate);
            return true;
        }

        public bool UpdatePassword(string UserName, string OldPassword, string NewPassword, DateTime LastUpdate, int CompanyID)
        {
            if (OldPassword == "1234")
            {
                var userData = userWebService.GetUserData(UserName, OldPassword, CompanyID);
                if (userData == null)
                    return false;
                userWebService.UpdatePassword(NewPassword, userData.UserGUID.ToString(), LastUpdate);
                userWebService.UpdateLock(0, userData.UserGUID.ToString(), userData.UserGUID.ToString());
                return true;
            }
            return false;
        }
        public bool UpdatePriod(int Peroid, string UserID)
        {
            userWebService.UpdatePeriod(Peroid, UserID);
            return true;
        }

        public bool ValidateSecurityToken(Guid _SecurityToken, Guid _UserID)
        {
            return uow.Repo<UserDevice>().GetByExression(w => w.SecurityToken == _SecurityToken && w.UserID == _UserID).Any();
        }

    }
}
