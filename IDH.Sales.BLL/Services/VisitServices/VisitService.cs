using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDH.Sales.DAL.CRM;
using IDH.Sales.Model.Models.Visits;
using IDH.Sales.BLL.VisitServiceReference;
using IDH.Sales.Model.Types;
using IDH.Sales.BLL.Repository;
using IDH.Sales.DAL.Own;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.BLL.UserServiceReference;
using IDH.Sales.BLL.SalesRepWebReference;

namespace IDH.Sales.BLL.Services.VisitServices
{
    public class VisitService
    {
        private readonly VisitWebService visitWebService;
        private readonly UserWebService userService;
        
        private readonly UnitOfWork uow;

        public VisitService()
        {
            this.visitWebService = new VisitWebService();
            this.userService = new UserWebService();
            uow = new UnitOfWork();
        }

        public void CancelVisit(Guid _VisitGUID, int _CompanyID)
        {
            ChangeVisitStatus(_VisitGUID, (Int32)VisitStatus.Cancelled, _CompanyID);
        }

        public void CompleteVisit(CompleteVisitRequest _Model, Guid _UserID)
        {
            _Model.Stocks.ForEach(f => visitWebService.AddvisitStock(Guid.NewGuid(), _Model.VisitID, f.ItemID, f.Qty, _UserID, _Model.CompanyID));
            _Model.Tests.ForEach(f => visitWebService.AddtestFeedBack(Guid.NewGuid(), _Model.VisitID, f.TestID, f.FeedbackID, f.Comment, _Model.CompanyID));
        }

        public bool AddVisitPlan(MonthYearModel model)
        {
            var exists = uow.Repo<VisitPlan>().GetByExression(w => w.Month == model.Month && w.Year == model.Year && model.CompanyID == model.CompanyID).Any();
            if (exists) return false;
           
            uow.Repo<VisitPlan>().Insert(new VisitPlan()
            {
                Name = $"Visit plan for {model.Month}-{model.Year}",
                Month = model.Month,
                Year = model.Year,
                Status = (Int32)VisitPlanStatus.Created,
                CompanyID=model.CompanyID,
                CreatedOn=DateTime.Now
            });
            uow.Save();
            return true;
        }

        public void ChangeVisitPlanStatus(int _PlanID, VisitPlanStatus _Status)
        {
            var plan=uow.Repo<VisitPlan>().GetByExression(w => w.ID == _PlanID).FirstOrDefault();
            if (plan!=null)
            {
                plan.Status = (Int32)_Status;
                uow.Save();
                if (_Status==VisitPlanStatus.Submitted)
                {
                    
                    var userData = userService.GetUserDataByGuid(plan.UserID.ToString());
                    //inserting notification record for manager
                    InsertNotification((Guid)userData.ManagerGUID, userData.Managername, $"Sales Rep: {plan.UserName} has submitted his plan for month: {plan.Month} - {plan.Year}");
                    //insertint notification record for sales admin
                    var managerData = userService.GetUserDataByGuid(userData.ManagerGUID.ToString());
                    InsertNotification((Guid)managerData.ManagerGUID, managerData.Managername, $"Sales Rep: {plan.UserName} has submitted his plan for month: {plan.Month} - {plan.Year}");
                }
                else if(_Status==VisitPlanStatus.Rejected)
                {
                    InsertNotification(plan.UserID, plan.UserName, $"Plan for month: {plan.Month} - {plan.Year} has been rejected.");
                }

                //submitted
                //approved
            }
            else
            {
                throw new Exception("Invalid selected plan, try reload requests");
            }
        }

        public void SubmitVisitPlan(UserSimpleModel model)
        {
            var minMaxPlan = GetMinMaxPlan();
            var submitedPlan = GetVisitPlans(model.UserID,VisitPlanStatus.Created)
                .Where(w=>w.Year>=DateTime.Now.Year && w.Month>=DateTime.Now.Month).OrderBy(o=>o.Year).ThenBy(tb=>tb.Month).FirstOrDefault();
            var visits = GetVisits(submitedPlan.ID);
            
            if (visits.Count() >= minMaxPlan.MinVisits && visits.Count()<=minMaxPlan.MaxVisits )
            {
                ChangeVisitPlanStatus(submitedPlan.ID, VisitPlanStatus.Submitted);
            }
            else
            {
                throw new Exception($"VisitPlan Cannot be submitted, Number of visits must be between {minMaxPlan.MinVisits.ToString()} and {minMaxPlan.MaxVisits.ToString()} ");
            }
        }

        public IQueryable<Visit> GetVisits(Guid _UserID)
        {
            return uow.Repo<Visit>().GetByExression(w => w.UserID == _UserID);
        }

        public IQueryable<Visit> GetVisits(int _Month,int _Year)
        {
            return uow.Repo<Visit>().GetByExression(w => w.VisitPlan.Month == _Month && w.VisitPlan.Year==_Year);
        }

        public IQueryable<Visit> GetVisits( int _PlanID)
        {
            return uow.Repo<Visit>().GetByExression(w =>w.PlanID == _PlanID);
        }

        public IQueryable<VisitPlan> GetVisitPlans(Guid _UserID)
        {
            return uow.Repo<VisitPlan>().GetByExression(w => w.UserID == _UserID).OrderByDescending(m=>m.Year).ThenByDescending(m=>m.Month);
        }

        public IQueryable<VisitPlan> GetVisitPlans(Guid _UserID,VisitPlanStatus status)
        {
            int planStatus = (Int32)status;
            return GetVisitPlans(_UserID).Where(w => w.Status == planStatus);
        }

        public IQueryable<VisitPlan> GetUnApprovedVisitPlans(List<Guid> users)
        {
            int planStatus = (Int32)VisitPlanStatus.Submitted;
            return uow.Repo<VisitPlan>().GetByExression(w => users.Any(a=>a==w.UserID ) && w.Status==planStatus).OrderByDescending(m => m.Year).ThenByDescending(m => m.Month);
        }


        public IQueryable<Visit> GetUnplannedVisit (List<Guid> users)
        {
            return uow.Repo<Visit>().GetByExression(w =>users.Contains(w.UserID) );
        }
        public VisitPlan GetVisitPlan(int _PlanID)
        {
            return uow.Repo<VisitPlan>().GetById(_PlanID);
        }

        public List<VisitServiceReference.YKN_P_GetVisitByMonth_Result> GetApprovedVisits(Guid _SalesRepID,int _Month,int _Year,int _CompanyID)
        {
            return visitWebService.GetPlanVisits(_SalesRepID, _Month, _Year, _CompanyID).ToList();
        }
        public VisitPlan GetVisitPlan(Guid _UserID,int _PlanID)
        {
            return GetVisitPlans(_UserID).Where(w => w.ID == _PlanID).FirstOrDefault();
        }

        public VisitPlan GetVisitPlan(Guid _UserID, int _Year,int _Month)
        {
            var userplans = GetVisitPlans(_UserID);
            return userplans.Where(w => w.Month == _Month && w.Year==_Year).FirstOrDefault();
        }

        public void ApproveVisitPlan(VisitPlanSimpleModel model)
        {
            var planVisits = uow.Repo<Visit>().GetByExression(w => w.PlanID == model.PlanID).ToList();
            foreach (var visit in planVisits)
            {
                visitWebService.AddVisitHeader(Guid.NewGuid(), visit.VisitCode, visit.DoctorID, visit.OrgID,
                    visit.UserID, visit.VisitDate, visit.VisitLevelID, model.CompanyID, visit.UserID);
            }
            //inserting notification
            var plan = GetVisitPlan( model.PlanID);
            plan.Status = (Int32)VisitPlanStatus.Approved;
            InsertNotification(plan.UserID, plan.UserName, $"Plan for month {plan.Month}-{plan.Year} has been approved");
            //Deleting visits and plan after moving it to CRM
            uow.Repo<Visit>().Delete(w => w.PlanID == model.PlanID);
            //uow.Repo<VisitPlan>().Delete(d => d.ID == model.PlanID);
            uow.Save();
        }

        public bool CopyMonthVisitPlan(int _CompanyID,int _SourceMonth,int _SourceYear,int _TargetMonth,int _TargetYear,Guid _UserID,string _UserName)
        {
            var plan = visitWebService.GetMonthPlan(_CompanyID, _SourceMonth, _UserID.ToString());
            if (plan.Length>0)
            {
                var insertedVisitPlan=uow.Repo<VisitPlan>().Insert(new VisitPlan()
                {
                    Name = $"Visit plan for {_TargetMonth}-{_TargetYear}",
                    Month = _TargetMonth,
                    Year = _TargetYear,
                    Status = (Int32)VisitPlanStatus.Created,
                    UserID = _UserID,
                    UserName =_UserName,
                    CompanyID =_CompanyID,
                    CreatedOn =DateTime.Now
                });
                foreach (var visit in plan)
                {
                    uow.Repo<Visit>().Insert(new Visit()
                    {
                        PlanID =insertedVisitPlan.ID,
                        DoctorID =visit.new_ProfessionalNameId.Value,
                        DoctorName=visit.professionalName,
                        OrgID = visit.new_OrganizationNameId.Value,
                        OrgName = visit.orgName,
                        UserID =_UserID,
                        VisitLevelID=(Int32)visit.visitLevelId,
                        VisitLevelName=Enum.GetName(typeof(VisitLevel),visit.visitLevelId),
                        VisitCode=0,
                        //any visit dates that exceed the month last day will be Migrated to the last day of the month
                        VisitDate =new DateTime(_TargetYear,_TargetMonth, visit.new_VisitDate.Value.Day>DateTime.DaysInMonth(_TargetYear,_TargetMonth)? DateTime.DaysInMonth(_TargetYear, _TargetMonth): visit.new_VisitDate.Value.Day)
                    });
                }
                uow.Save();
                return true;
            }
            return false;
        }

        public void CreateVisit(CreateVisitRequest model)
        {
            if (model.Type == VisitType.Planned)
            {
                var visitMonth = model.VisitDate.Month;
                var visitYear = model.VisitDate.Year;
                var checkVisitPlan = GetVisitPlan(model.UserID, visitYear, visitMonth);
                //these 4 line of code will be uncommented be
                if (model.Type == VisitType.Planned && checkVisitPlan != null && checkVisitPlan.Status != (Int32)VisitPlanStatus.Created)
                {
                    throw new Exception($"Visit plan for this month has been {Enum.GetName(typeof(VisitPlanStatus), checkVisitPlan.Status)}");
                }
                if (checkVisitPlan == null && model.Type == VisitType.Planned)
                {
                    throw new Exception("No available plan found");
                }
                else
                {
                    var latestVisit = uow.Repo<Visit>().GetByExression(w => w.UserID == model.UserID && w.VisitDate.Month == visitMonth && w.VisitDate.Year == visitYear)
                     .OrderByDescending(o => o.VisitCode).FirstOrDefault();

                    uow.Repo<Visit>().Insert(new Visit()
                    {
                        PlanID = checkVisitPlan?.ID,
                        VisitCode = (latestVisit == null ? 1 : latestVisit.VisitCode + 1),
                        VisitDate = model.VisitDate,
                        UserID = model.UserID,
                        UserName = model.UserName,
                        DoctorID = model.DoctorID,
                        DoctorName = model.DoctorName,
                        OrgID = model.OrgID,
                        OrgName = model.OrgName,
                        VisitLevelID = (Int32)model.VisitLevel,
                        VisitLevelName = model.VisitLevel.ToString()
                    });
                    uow.Save();
                }
            }
            else if (model.Type == VisitType.Unplanned)
            {
                //var userData = userService.GetUserDataByGuid(model.UserID.ToString());
                //InsertNotification(userData.ManagerGUID.Value,userData.Managername, $"User {model.UserName} has create an unplanned visit");
                visitWebService.AddVisitHeader(Guid.NewGuid(), null, model.DoctorID, model.OrgID, model.UserID, model.VisitDate, (Int32)model.VisitLevel, model.CompanyID, model.UserID);
            }
        }

        public void ApproveUnplannedVisit(int _VisitID,int _CompanyID)
        {
            var visitRecord = uow.Repo<Visit>().GetByExression(w => w.ID == _VisitID).FirstOrDefault();
            visitWebService.AddVisitHeader(Guid.NewGuid(), null, visitRecord.DoctorID, visitRecord.OrgID, visitRecord.UserID, visitRecord.VisitDate, (Int32)visitRecord.VisitLevelID, _CompanyID, visitRecord.UserID);
            InsertNotification(visitRecord.UserID, visitRecord.UserName, $"Your unplanned visit for doctor {visitRecord.DoctorName} has been approved");
            uow.Repo<Visit>().Delete(w=>w.ID== visitRecord.ID);
            uow.Save();
        }

        public void CreateVisitPlan(CreatePlanRequest model)
        {
            //check if there is a plan for the provided month or not
            var isExists = uow.Repo<VisitPlan>().GetByExression(w => w.UserID == model.UserID && w.Month == model.Month && w.Year == model.Year).Any();
            if (isExists)
            {
                throw new Exception("Provided month is already contain a plan");
            }
            uow.Repo<VisitPlan>().Insert(new VisitPlan()
            {
                Name = model.PlanName,
                Month = model.Month,
                Year = model.Year,
                UserID = model.UserID,
                UserName = model.UserName,
                Status = (Int32)VisitPlanStatus.Created,
                CreatedOn = DateTime.Now,
                CompanyID = model.CompanyID
            });
            uow.Save();
        }

        public VisitServiceReference.YKN_P_GetMapLocation_Result[] GetMapLocation  (MapLocationRequest model)
        {
            return visitWebService.GetMapLocation(model.Day.Date, model.SalesRepID.ToString(), model.CompanyID);
        }

        public void EndVisit(EndVisitRequest model, Guid _UserID)
        {
            visitWebService.UpdateVisitDetails(model.VisitID, model.StartTime, model.EndTime, model.StartLat, model.EndLat, model.StartLong,
                model.EndLong, (Int32)VisitStatus.Done, model.Comment, model.FeedbackID, model.CompanyID, model.StartAddress, model.EndAddress, _UserID);
        }

        public List<VisitServiceReference.YKN_P_GetAVTDetails_Result> GetAVTDetails(int _CompanyID, int _Month, string _UserID, string _RoleID)
        {
            return visitWebService.GetAVTDetails(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        public List<VisitServiceReference.YKN_P_GetCustomerReport_Result> GetCustomerReport(CustomerReportRequest model)
        {
            return visitWebService.GetCustomerReport(model.EndDate, model.StartDate, model.UserID.ToString(), model.RoleID.ToString(),model.CompanyID).ToList();
        }

        public VisitServiceReference.YKN_PH_GetFeedbacklist_Result[] GetFeedbackList(BaseRequest model)
        {
            return visitWebService.GetFeedbackList();
        }

        public List<VisitServiceReference.YKN_P_GetDashInfo_Result> GetDashboard(int _CompanyID, int _Month, string _UserID, string _RoleID)
        {
            return visitWebService.GetDashInfo(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        public List<VisitServiceReference.YKN_P_GetMissedVisitList_Result> GetMissedVisitList(int _CompanyID, string _UserID, int _Month,int _Year)
        {
            return visitWebService.GetMissedVisitList(_CompanyID, _Month, _Year,_UserID).ToList();
        }

        public List<VisitServiceReference.YKN_P_GetTargetAchievedDetails_Result> GetTargetAchievedDetails(int _CompanyID, int _Month, string _UserID, string _RoleID)
        {
            return visitWebService.GetTargetAchievedDetails(_CompanyID, _Month, _UserID, _RoleID).ToList();
        }

        public VisitServiceReference.YKN_P_GetTestList_Result[] GetTestList(int _CompanyID)
        {
            return visitWebService.GetTestList(_CompanyID);
        }

        public VisitServiceReference.YKN_P_GetVisitByDay_Result[] GetVisits(DateTime _Day, string _DoctorName, int _CompanyID,Guid _UserID)
        {
            return visitWebService.GetVisitsByDay(_CompanyID, _Day, _UserID.ToString());
        }

        public VisitServiceReference.YKN_P_GetVisitTests_Result[] GetVisitTests(Guid _UserGUID,Guid _VisitGUID, int _CompanyID)
        {
            return visitWebService.GetVisitTests(_CompanyID, _VisitGUID, _UserGUID);
        }

        public VisitServiceReference.YKN_P_GetVisitStock_Result[] GetVisitStock(Guid _UserGUID, Guid _VisitGUID, int _CompanyID)
        {
            return visitWebService.GetVisitsByStock(_CompanyID, _VisitGUID, _UserGUID);
        }

        public void RescheduleVisit(RescheduleVisitRequest model)
        {
            visitWebService.ResceduleVisit(model.VisitID, model.NewDate, model.CompanyID);
        }

        public void ChangeVisitDate(ChangeVisitDateRequest model)
        {
            var visit = uow.Repo<Visit>().GetByExression(w => w.ID == model.VisitID).FirstOrDefault();
            if (visit!=null)
            {
                visit.VisitDate = model.NewDate;
                uow.Save();
            }
        }

        public void ChangeVisitStatus(Guid _VisitID,int _NewStatus,int _CompanyID)
        {
            visitWebService.ChangeVisitStatus(_VisitID,_NewStatus,_CompanyID);
        }

        public VisitServiceReference.YKN_PH_GetCancelReasons_Result[] GetCancelReason(BaseRequest model)
        {
            return visitWebService.GetCancelReason();
        }

        public VisitServiceReference.YKN_PH_GetMaxMinPlan_Result GetMinMaxPlan()
        {
            return visitWebService.GetMinMaxPlan().FirstOrDefault();
        }

        public VisitServiceReference.YKN_PH_GetVisitStatusList_Result[] GetVisitStatus(BaseRequest model)
        {
            return visitWebService.GetVisitStatus();
        }

        public void InsertNotification(Guid _UserID,string _UserName,string _Title)
        {
            uow.Repo<UserNotification>().Insert(new UserNotification()
            {
                UserID=_UserID,
                UserName=_UserName,
                Title=_Title,
                CreationDate=DateTime.Now
            });
            uow.Save();
        }

        public IQueryable<UserNotification> GetNotifications(Guid _UserID)
        {
            return uow.Repo<UserNotification>().GetByExression(w => w.UserID == _UserID);
        }

        public VisitServiceReference.YKN_P_GetDetailedRevReport_Result[] GetDetailedAmount(DetailedAmountModel model)
        {
            return visitWebService.GetDetailedAmount(model.UserID, model.Month, model.Year, model.CompanyID, model.RoleID);
        }

        public List<UserPlanStatusModel> GetTeamVisitStatus(List<Guid> users)
        {
            return uow.Repo<VisitPlan>().GetByExression(w => users.Contains(w.UserID) && w.Month == DateTime.Now.Month && w.Year == DateTime.Now.Year)
                .ToList().Select(s => new UserPlanStatusModel(s.UserID, s.Status)).ToList();
        }

        public void DeleteUnplannedVisitRequest(int _VisitID,int _CompanyID)
        {
            uow.Repo<Visit>().Delete(w => w.ID == _VisitID);
            uow.Save();
        }

    }
}
