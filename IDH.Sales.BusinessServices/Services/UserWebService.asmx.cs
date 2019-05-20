using IDH.Sales.DAL.CRM;
using System;
using System.Linq;
using System.Web.Services;

namespace IDH.Sales.BusinessServices.Services
{
    /// <summary>
    /// Summary description for UserWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class UserWebService : System.Web.Services.WebService
    {
        Entities db = new Entities();

        [WebMethod]
        public YKN_P_GetUserData_Result GetUserData(string _UserName, string _Password, int _CompanyID)
        {
            return db.YKN_P_GetUserData(_UserName, _Password, _CompanyID).FirstOrDefault();
        }

        [WebMethod]
        public YKN_P_GetUserDataGUID_Result GetUserDataByGuid(string _UserGuid)
        {
            return db.YKN_P_GetUserDataGUID(_UserGuid).FirstOrDefault();
        }
        [WebMethod]
        public void UpdatePeriod(int Priod, string _UserGuid)
        {
            db.YKN_Push_UpdatePeriod(Priod, _UserGuid);
        }
        [WebMethod]
        public void UpdateLock(int Lock, string _UserGuid, string employeeID)
        {
            db.YKN_Push_UpdateLock(Lock, _UserGuid, employeeID);
        }
        [WebMethod]
        public void UpdatePassword(string Password, string _UserGuid, DateTime LastUpdate)
        {
            db.YKN_Push_UpdatePassword(Password, _UserGuid, LastUpdate);
        }
    }
}
