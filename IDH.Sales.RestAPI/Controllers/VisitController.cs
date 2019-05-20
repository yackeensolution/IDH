using IDH.Sales.BLL.Services.DoctorServices;
using IDH.Sales.BLL.Services.SalesRepServices;
using IDH.Sales.BLL.Services.SettingServices;
using IDH.Sales.BLL.Services.UserServices;
using IDH.Sales.BLL.Services.VisitServices;
using IDH.Sales.DAL.Own;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.Users;
using IDH.Sales.Model.Models.Visits;
using IDH.Sales.Model.Types;
using IDH.Sales.RestAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Services.Protocols;

namespace IDH.Sales.RestAPI.Controllers
{
    [EnableCors(origins: "http://yakensolution.cloudapp.net:80", headers: "*", methods: "*")]
    [AuthorizationRequired]
    public class VisitController : BaseAPIController
    {
        private readonly VisitService visitService;
        private readonly DoctorService doctorService;
        private readonly UserService userService;
        private readonly SalesRepService salesRepService;
        private readonly SettingService settingService;
        private readonly BLL.UserServiceReference.UserWebService userWebService;

        public VisitController()
        {
            this.visitService = new VisitService();
            this.doctorService = new DoctorService();
            this.userService = new UserService();
            this.salesRepService = new SalesRepService();
            this.settingService = new SettingService();
            this.userWebService = new BLL.UserServiceReference.UserWebService();

        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetVisits(GetVisitRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(new BaseResponse(visitService.GetVisits(model.Day.Value, model.DoctorName, model.CompanyID, model.UserID).ToList().Select(s => new
                    {
                        VisitID = s.VisitGUID,
                        VisitDate = s.VisitDate,
                        DoctorName = s.ProfessionalName,
                        Speciality = s.specility,
                        Org = s.OrgName,
                        Phone = s.new_PhoneNumber,
                        Address = s.Adress,
                        Status = Enum.GetName(typeof(VisitStatus), s.statuscode),
                        SalesRepName = s.salesRepName,
                        VisitFeedback = s.new_VisitFeedback,
                        VisitComment = s.visitComment//,
                        //TestID =s.TestId,
                        //TestName=s.testName,
                        //TestComment=s.TestComment,
                        //TestFeedback=s.Testfeedack,
                        //StockItemID=s.stockitemid,
                        //StockItemName=s.stockitemname,
                        //StockAmount=s.stockitemaount
                    })));
                }
                return BadRequest(ModelState);
            }
            catch(SoapException ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, "Slow Connection"));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetVisitsByDay(GetVisitRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(new BaseResponse(visitService.GetVisits(model.Day.Value, model.DoctorName, model.CompanyID, model.UserID).ToList().Select(s => new
                    {
                        VisitID = s.VisitGUID,
                        VisitDate = s.VisitDate,
                        DoctorName = s.ProfessionalName,
                        Speciality = s.specility,
                        Org = s.OrgName,
                        Phone = s.new_PhoneNumber,
                        Address = s.Adress,
                        Status = Enum.GetName(typeof(VisitStatus), s.statuscode),
                        SalesRepName = s.salesRepName,
                        VisitFeedback = s.new_VisitFeedback,
                        VisitComment = s.visitComment,
                        Tests = visitService.GetVisitTests(model.UserID, s.VisitGUID, model.CompanyID).Select(ts =>
                            new
                            {
                                TestID = ts.TestID,
                                TestName = ts.TestName,
                                TestComment = ts.TestComment,
                                TestFeedback = ts.VisitFeedback
                            }),
                        Stock = visitService.GetVisitStock(model.UserID, s.VisitGUID, model.CompanyID).Select(ts =>
                           new
                           {
                               StockItemID = ts.ItemID,
                               StockItemName = ts.ItemName,
                               StockAmount = ts.amount
                           })
                    })));
                }
                return BadRequest(ModelState);
            }
            catch (SoapException ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, "Slow Connection"));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetVisitDetails(VisitGUIDSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(new BaseResponse( new
                    {
                        Tests = visitService.GetVisitTests(this.UserID, model.VisitID, model.CompanyID).Select(ts =>
                            new
                            {
                                TestID = ts.TestID,
                                TestName = ts.TestName,
                                TestComment = ts.TestComment,
                                TestFeedback = ts.VisitFeedback
                            }),
                        Stock = visitService.GetVisitStock(this.UserID, model.VisitID, model.CompanyID).Select(ts =>
                           new
                           {
                               StockItemID = ts.ItemID,
                               StockItemName = ts.ItemName,
                               StockAmount = ts.amount
                           })
                    }));
                }
                return BadRequest(ModelState);
            }
            catch (SoapException ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, "Slow Connection"));
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetPlanVisits(UserMonthYearModel model)
        {
            if (ModelState.IsValid)
            {
                var plan = visitService.GetVisitPlan(model.SalesRepID, model.Year, model.Month);
                if (plan!=null)
                {
                    if (plan.Status!=(Int32)VisitPlanStatus.Approved)
                    {
                        var data = visitService.GetVisits(plan.ID).ToList();
                        return Ok(new BaseResponse(new
                        {

                            Status = plan != null ? Enum.GetName(typeof(VisitPlanStatus), plan.Status) : "",
                            Visits = data.Select(s => new
                            {
                                VisitID = s.ID,
                                VisitDate = s.VisitDate,
                                DoctorID=s.DoctorID,
                                DoctorName = s.DoctorName,
                                OrgID=s.OrgID,
                                Org = s.OrgName,
                                LevelID=s.VisitLevelID,
                                Level = s.VisitLevelName,
                                SalesRepID=s.VisitPlan.UserID,
                                SalesRepName = s.VisitPlan.UserName
                            }).OrderBy(o => o.VisitDate)
                        }));
                    }
                    else
                    {
                        var data = visitService.GetApprovedVisits(model.SalesRepID, model.Month, model.Year, model.CompanyID);
                        return Ok(new BaseResponse(new
                        {

                            Status = VisitPlanStatus.Approved.ToString(),
                            Visits = data.Select(s => new
                            {
                                VisitID = s.VisitGUID,
                                VisitDate = s.VisitDate,
                                DoctorID=s.professionalid,
                                DoctorName = s.ProfessionalName,
                                OrgID=s.organizationid,
                                Org = s.OrgName,
                                LevelID=s.new_VisitLevel,
                                Level = s.VisitLevel,
                                SalesRepID=s.salesRepID,
                                SalesRepName = s.salesRepName
                            }).OrderBy(o => o.VisitDate)
                        }));
                    }
                   
                }
                else
                {
                    return Ok(new BaseResponse(new
                    {

                        Status = "No provided plan for this month",
                        Visits = new { }
                    }));
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetMissedVisits(GetMissedVisitRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(visitService.GetMissedVisitList(model.CompanyID,this.UserID.ToString(),model.Month,model.Year).Select(s=>new
                {
                    VisitID=s.VisitId,
                    VisitDate=s.new_VisitDate,
                    DoctorName=s.professional,
                    OrgName=s.organization
                })));
            }
            return BadRequest(ModelState);
        }      

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CreateVisit(CreateVisitRequest model)
        {
            try
            {
                //will insert to my database first and after approved it will be migrated to the CRM
                if (ModelState.IsValid)
                {
                    visitService.CreateVisit(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost,AuthorizationRequired]
        public IHttpActionResult CopyMonthVisitPlan(CopyPlanRequest model)
        {
            try
            {
                //will insert to my database first and after approved it will be migrated to the CRM
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    visitService.CopyMonthVisitPlan(model.CompanyID,model.SourceMonth,model.SourceYear,model.TargetMonth,model.TargetYear,model.UserID,model.UserName);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CreateVisitPlan(CreatePlanRequest model)
        {
            try
            {
                //will insert to my database first and after approved it will be migrated to the CRM
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    visitService.CreateVisitPlan(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult EndVisit(EndVisitRequest model)
        {
            try
            {
                //will used update Visit Details
                if (ModelState.IsValid)
                {
                    visitService.EndVisit(model, this.UserID);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult SubmitVisitPlan(UserSimpleModel model)
        {
            try
            {
                //will used update visit details
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    visitService.SubmitVisitPlan(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult ApproveVisitPlan(VisitPlanSimpleModel model)
        {
            try
            {
                //will used update Visit Details
                if (ModelState.IsValid)
                {
                    //var plan = visitService.GetVisitPlan(this.UserID, model.PlanID);
                    visitService.ApproveVisitPlan(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetTests(BaseRequest model)
        {
            return Ok(new BaseResponse(visitService.GetTestList(model.CompanyID).Select(s => new { TestID = s.TestGUID, TestName = s.TestName })));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetFeedbackList(BaseRequest model)
        {
            return Ok(new BaseResponse(visitService.GetFeedbackList(model).Select(s=>new
            {
                ID=s.ID,
                Name=s.Feedback
            })));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CompleteVisit(CompleteVisitRequest model)
        {
            try
            {
                //will used these services (YKN_Push_AddtestFeedBack - YKN_Push_AddvisitStock - UpdateVisitDetails)
                if (ModelState.IsValid)
                {
                    visitService.CompleteVisit(model, this.UserID);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult RescheduleVisit(RescheduleVisitRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitService.RescheduleVisit(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult ChangeVisitDate(ChangeVisitDateRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitService.ChangeVisitDate(model);
                    return Ok(new BaseResponse());
                }
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetDashBoard(GetDashboardRequrest model)
        {           
            var dashInfo = visitService.GetDashboard(model.CompanyID, model.Month, this.UserID.ToString(), model.RoleID.ToString()).FirstOrDefault();
            return Ok(new BaseResponse(new
            {
                AverageVisitTime = dashInfo==null ?  0 : dashInfo.AVT,
                TargetAverageVisitTime = dashInfo ==null ?  0 : dashInfo.TargetAVT,
                TargetVisits = dashInfo ==null ?  0 : dashInfo.TargetVisits,
                Achieved = dashInfo ==null ?  0 : dashInfo.ActualVisits,
                MissedVisits = dashInfo ==null ?  0 : dashInfo.Missedvisits,
                ActualAmount= dashInfo == null ? 0 : dashInfo.ActualAmount,
                TargetAmount =dashInfo==null?0:dashInfo.TargetAmount,
                TotalStock=dashInfo == null ? 0 : dashInfo.TotalStock,
                TotalBalance=dashInfo == null ? 0 : dashInfo.TotalBalance
            }));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult CancelVisit(CancelVisitRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitService.CancelVisit(model.VisitID, model.CompanyID);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetPendingRequests(UserSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    List<VisitPlan> visitPlans = null;
                    List<Visit> unplannedVisits = null;
                    List<Doctor> doctors = null;
                    //checking if the user of sales rep or sales manager
                    //if the user of type sales rep we will return the his pending requests
                    //if the user of type sales manager we will return all sub-sales reps pending requests
                    var user = userService.GetUserDetails(this.UserID);
                    if (user.RuleName == (Int32)SalesUserType.SalesRep)
                    {
                        visitPlans = visitService.GetUnApprovedVisitPlans(new List<Guid>() { this.UserID }).ToList();
                        //unplannedVisits=visitService.GetUnplannedVisit(new List<Guid>() { this.UserID }).ToList();
                        doctors = doctorService.GetUnApprovedDoctors(new List<Guid>() { this.UserID }).ToList();
                    }
                    else
                    {
                        //we will get all sub-salesreps pending requests
                        List<Guid> subSalesReps = salesRepService.GetTeam(model).Select(s => s.SalesRepID).ToList();
                        subSalesReps.Add(model.UserID);//adding manager guid in order to list his plans in the pending requests
                        visitPlans = visitService.GetUnApprovedVisitPlans(subSalesReps).ToList();
                        //unplannedVisits = visitService.GetUnplannedVisit(subSalesReps).ToList().Where(w=>w.PlanID==null).ToList();
                        doctors = doctorService.GetUnApprovedDoctors(subSalesReps).ToList();
                    }
                    if (visitPlans==null && doctors ==null && unplannedVisits==null)
                    {
                        return Ok(new BaseResponse());
                    }
                    else
                    {
                        List<PendingRequestsResponse> result = new List<PendingRequestsResponse>();
                        if (visitPlans!=null)
                        {
                            result.AddRange(visitPlans.Select(s => new PendingRequestsResponse
                            {
                                RequestID = s.ID.ToString(),
                                RequestName = s.UserName,
                                PlanMonth = $"{s.Month} - {s.Year}",
                                VisitsCount = s.Visits.Count(),
                                RequestType = "VisitPlan",
                                SalesRepID = s.UserID.ToString(),
                                SalesRepName = s.UserName,
                                CategoryName = "",
                                SpecialityName = "",
                                ManagerName = userService.GetUserDetails(s.UserID).Managername
                            }));
                        }
                        //if (unplannedVisits!=null)
                        //{
                        //    result.AddRange(unplannedVisits.Select(s => new PendingRequestsResponse
                        //    {
                        //        RequestID=s.ID.ToString(),
                        //        RequestName=s.DoctorName,
                        //        PlanMonth=s.VisitDate.ToShortDateString(),
                        //        VisitsCount=0,
                        //        RequestType="UnplannedVisit",
                        //        SalesRepID=s.UserID.ToString(),
                        //        SalesRepName=s.UserName,
                        //        CategoryName="",
                        //        SpecialityName="",
                        //        ManagerName= userService.GetUserDetails(s.UserID).Managername
                        //    }));
                        //}
                        if (doctors!=null)
                        {
                            result.AddRange(doctors.Select(s => new PendingRequestsResponse
                            {
                                RequestID = s.ID.ToString(),
                                RequestName = s.Name,
                                PlanMonth = "",
                                VisitsCount = 0,
                                RequestType = "Doctor",
                                SalesRepID = "",
                                SalesRepName = string.IsNullOrEmpty(s.SalesRepName) ? "" : s.SalesRepName,
                                CategoryName = string.IsNullOrEmpty(s.CategoryName) ? "" : s.CategoryName,
                                SpecialityName = string.IsNullOrEmpty(s.SpecialityName) ? "" : s.SpecialityName,
                                ManagerName = userService.GetUserDetails(s.SalesRepID)?.Managername
                            }));
                        }
                        return Ok(new BaseResponse(result));
                    }

                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
           
        }


        // unComment it 
        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult ApproveUnplannedVisit(OwnVisitSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                visitService.ApproveUnplannedVisit(model.VisitID, model.CompanyID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult RejectUnplannedVisit(OwnVisitSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                visitService.DeleteUnplannedVisitRequest(model.VisitID, model.CompanyID);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult GetAVTDetails(AVTDetailsModel model)
        {
            return Ok(new BaseResponse(visitService.GetAVTDetails(model.CompanyID,model.Month,model.UserID.ToString(),model.RoleID.ToString())
                .Select(s=> new
                {
                    DirectManagerName=s.DirectManagerName,
                    SalesRepName=s.SalesRepName,
                    AVT=s.AVT,
                    TotalTime=s.totalTime,
                    VisitsNo=s.VisitsNo                    
                })));
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult GetTargetAchievedDetails(TargetAchievedDetailsModel model)
        {
            model.UserID = this.UserID;
            return Ok(new BaseResponse(visitService.GetTargetAchievedDetails(model.CompanyID, model.Month, model.UserID.ToString(), model.RoleID.ToString()).ToList()
                .Select(s=>new
                {
                    SalesRepName=s.salesRepName,
                    ManagerName=s.DirectManagerName,
                    TargetVisits=s.new_TargetVisits == null ? 0 :s.new_TargetVisits,
                    ActualVisits=s.new_ActualVisits == null ? 0 :s.new_ActualVisits,
                    TargetAmount=s.new_TargetAmount == null ? 0 : s.new_TargetAmount,
                    ActualAmount=s.new_ActualAmount==null ? 0 : s.new_ActualAmount
                })));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult MapLocaion(MapLocationRequest model)
        {
            if (ModelState.IsValid)
            {
                var locations=visitService.GetMapLocation(model).ToList();
                return Ok(new BaseResponse(locations.Select(s=> new
                {

                    longitude = s.new_GPSXcheckIn,
                    latitude = s.new_GPSYcheckin
                    
                })));
            }
            return BadRequest(ModelState);
        }
       
        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetCustomerReport(CustomerReportRequest model)
        {
            model.UserID = this.UserID;
            var x = visitService.GetCustomerReport(model).ToList();
            return Ok(new BaseResponse(visitService.GetCustomerReport(model).ToList().Select(s=>new
            {
                s.VisitDate,
                SalesRepName=s.salesrepname,
                VisitComment=s.new_CommentOntheVisit!=null?s.new_CommentOntheVisit:"",
                VisitFeedback=s.new_VisitFeedback!=null? Enum.GetName(typeof(FeedbackList),s.new_VisitFeedback):"",
                DoctorName=s.ProfessionalName!=null ? s.ProfessionalName : "",
                TestName=s.testname!=null?s.testname:"",
                TestFeedback=s.testFeedback!=null? s.testFeedback:"",
                TestComment=s.TestComment !=null?s.testname:""
            })));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetVisitStatus(BaseRequest model)
        {
            return Ok(new BaseResponse(visitService.GetVisitStatus(model).ToList().Select(s => new
            {
               ID=s.ID,
               Name=s.VisitStatus
            })));
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetCancelReason(BaseRequest model)
        {
            return Ok(new BaseResponse(visitService.GetCancelReason(model).ToList().Select(s => new
            {
               ID=s.ID,
               Name=s.ReasonValue
            })));
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult RejectVisitPlan(VisitPlanSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    visitService.ChangeVisitPlanStatus(model.PlanID, VisitPlanStatus.Created);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetPlanDeadline()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var deadLineDay=settingService.GetSetting("VisitPlan.DeadLineDay").Value;
                    return Ok(new BaseResponse(new { DeadlineDay=deadLineDay}));
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost, AuthorizationRequired,RolesAdminManagerRequired]
        public IHttpActionResult ChangePlanDeadline(DaySimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    settingService.UpdateSettingValue("VisitPlan.DeadLineDay", model.Day.ToString());
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost,AuthorizationRequired]
        public IHttpActionResult GetNotifications(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                var notifications = visitService.GetNotifications(this.UserID);
                return Ok(new BaseResponse(notifications.Select(s=> new
                {
                    Title=s.Title,
                    Date=s.CreationDate
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost,AuthorizationRequired]
        public IHttpActionResult GetDetailedAmount(DetailedAmountModel model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID.ToString();
                return Ok(new BaseResponse(visitService.GetDetailedAmount(model).Select(s => new
                {
                    SalesRepID = s.SalesRepID,
                    SalesRepName=s.SalesRepName,
                    DoctorID=s.professionalGuid,
                    DoctorName=s.ProfessionalName,
                    Speciality=s.speciality,
                    Mobile=s.new_MobileNumber,
                    Class=s.Class,
                    MonthlyRevenue=s.MonthlyRevenue
                })));
            }
            return BadRequest(ModelState);
        }

    }
}

