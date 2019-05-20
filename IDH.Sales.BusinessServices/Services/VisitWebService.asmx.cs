using IDH.Sales.DAL.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IDH.Sales.BusinessServices.Services
{
    /// <summary>
    /// Summary description for VisitWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VisitWebService : System.Web.Services.WebService
    {
        Entities db = new Entities();     

        [WebMethod]
        public void AddtestFeedBack(Guid _GUID,Guid _VisitGuid,Guid _TestGuid,int _FeedbackID,string _Comment, int _CompanyID)
        {
            db.YKN_Push_AddtestFeedBack(_GUID, _VisitGuid, _TestGuid, _FeedbackID, _Comment, _CompanyID);
        }

        [WebMethod]
        public void AddvisitStock(Guid _GUID, Guid _VisitGuid, Guid _StockItemGUID, int _Qnty,Guid _UserID, int _CompanyID)
        {
            db.YKN_Push_AddvisitStock(_GUID, _VisitGuid, _StockItemGUID, _Qnty, _UserID,_CompanyID);
        }

        [WebMethod]
        public List<YKN_P_GetMissedVisitList_Result> GetMissedVisitList(int _CompanyID, int _Month,int _Year, string _UserID)
        {
            return db.YKN_P_GetMissedVisitList(_CompanyID, _Month, _UserID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetDashInfo_Result> GetDashInfo(int _CompanyID, int _Month, string _UserID,string _RoleID)
        {
            return db.YKN_P_GetDashInfo(_CompanyID,_Month, _UserID, _RoleID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetAVTDetails_Result> GetAVTDetails(int _CompanyID,int _Month,string _UserID,string _RoleID)
        {
            return db.YKN_P_GetAVTDetails(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetTargetAchievedDetails_Result> GetTargetAchievedDetails(int _CompanyID, int _Month, string _UserID, string _RoleID)
        {
            return db.YKN_P_GetTargetAchievedDetails(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetMapLocation_Result> GetMapLocation(DateTime _Day,string _SalesRepGUID, int _CompanyID)
        {
            return db.YKN_P_GetMapLocation(_Day,_SalesRepGUID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetVisitByMonth_Result> GetPlanVisits(Guid _SalesRepID,int _Month, int _Year, int _CompanyID)
        {
            return db.YKN_P_GetVisitByMonth(_CompanyID,_Month,_Year,_SalesRepID.ToString()).ToList();
        }

        [WebMethod]
        public void AddVisitHeader(Guid _GUID,int? _VisitCode,Guid _ProfessionalGUID, Guid _OrgGuid, Guid _SalesRepGuid, DateTime _VisitDate,int _VisitLevel, int _CompanyId, Guid _UserGUID)
        {
            db.YKN_Push_AddVisitHeader(_GUID, _VisitCode, _ProfessionalGUID, _OrgGuid, _SalesRepGuid, _VisitDate, _VisitLevel, _CompanyId, _UserGUID);
        }

        [WebMethod]
        public void UpdateVisitDetails(Guid _VisitGUID, DateTime _StartDate, DateTime _EndDate, decimal _StartLat, decimal _EndLat, decimal _Startlong,
           decimal _Endlong, int _Visitstatus, string _Comment, int _FeedBackId, int _CompanyId, string _StartAdress, string _EndAdress, Guid _UserGUID)
        {
            db.YKN_Push_UpdateVisitDetails(_VisitGUID, _StartDate, _EndDate, _StartLat, _EndLat, _Startlong, _Endlong,
                _Visitstatus, _Comment, _FeedBackId, _CompanyId, _StartAdress, _EndAdress, _UserGUID);
        }

        [WebMethod]
        public List<YKN_P_GetMonthPlan_Result> GetMonthPlan(int _CompanyID,int _Month, string _UserID)
        {
            return db.YKN_P_GetMonthPlan(_CompanyID,_Month,_UserID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetVisitByDay_Result> GetVisitsByDay(int _CompanyID, DateTime _Day, string _UserID)
        {
            return db.YKN_P_GetVisitByDay(_CompanyID, _Day, _UserID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetVisitTests_Result> GetVisitTests(int _CompanyID, Guid _VisitGUID, Guid _UserGUID)
        {
            return db.YKN_P_GetVisitTests(_CompanyID, _VisitGUID, _UserGUID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetVisitStock_Result> GetVisitsByStock(int _CompanyID, Guid _VisitGUID, Guid _UserGUID)
        {
            return db.YKN_P_GetVisitStock(_CompanyID, _VisitGUID, _UserGUID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetTestList_Result> GetTestList(int _CompanyID)
        {
            return db.YKN_P_GetTestList(_CompanyID).ToList();
        }

        [WebMethod]
        public void AddVisitStock(Guid _GUID, Guid _VisitGUID, Guid _StockItemGUID, int _Qnty, Guid _UserGUID,int _CompanyID)
        {
            db.YKN_Push_AddvisitStock(_GUID, _VisitGUID, _StockItemGUID, _Qnty, _UserGUID,_CompanyID);
        }

        [WebMethod]
        public List<YKN_P_GetCustomerReport_Result> GetCustomerReport(DateTime _EndDate,DateTime _StartDate,string _UserID,string _RuleID, int _CompanyID)
        {
            return db.YKN_P_GetCustomerReport(_EndDate,_StartDate,_UserID,_RuleID).ToList();
        }

        [WebMethod]
        public void ResceduleVisit(Guid _VisitGuid,DateTime _VisitDate,int _CompanyID)
        {
            db.YKN_Push_ReschoduleVisit(_VisitGuid, _VisitDate, _CompanyID);
        }

        [WebMethod]
        public void ChangeVisitStatus( Guid _VisitGuid,int _VisitStatus,int _CompanyID)
        {
            db.YKN_Push_ChangeVisitStatus(_VisitGuid, _VisitStatus, _CompanyID);
        }

        [WebMethod]
        public List<YKN_PH_GetCancelReasons_Result> GetCancelReason()
        {
            return db.YKN_PH_GetCancelReasons().ToList();
        }

        [WebMethod]
        public List<YKN_PH_GetFeedbacklist_Result> GetFeedbackList()
        {
            return db.YKN_PH_GetFeedbacklist().ToList();
        }

        [WebMethod]
        public List<YKN_PH_GetMaxMinPlan_Result> GetMinMaxPlan()
        {
            return db.YKN_PH_GetMaxMinPlan().ToList();
        }

        [WebMethod]
        public List<YKN_PH_GetVisitStatusList_Result> GetVisitStatus()
        {
            return db.YKN_PH_GetVisitStatusList().ToList();
        }

        [WebMethod]
        public List<YKN_P_GetDetailedRevReport_Result> GetDetailedAmount(string _SalesRepGuid,int _Month,int _Year,int _CompanyID,string _RoleID)
        {
            return db.YKN_P_GetDetailedRevReport(_SalesRepGuid,_Month,_Year,_CompanyID,_RoleID).ToList();
        }
    }
}
