using IDH.Sales.BLL.Services.DoctorServices;
using IDH.Sales.BLL.Services.VisitServices;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.Doctors;
using IDH.Sales.RestAPI.Filters;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;

namespace IDH.Sales.RestAPI.Controllers
{
    [EnableCors(origins: "http://yakensolution.cloudapp.net:80", headers: "*", methods: "*")]
    [AuthorizationRequired]
    public class DoctorController : BaseAPIController
    {
        private readonly DoctorService doctorService;
        private readonly VisitService visitService;
        private readonly BLL.UserServiceReference.UserWebService userWebService;
        public DoctorController()
        {
            this.doctorService = new DoctorService();
            this.visitService = new VisitService();
            this.userWebService = new BLL.UserServiceReference.UserWebService();

        }

        [HttpPost]
        public IHttpActionResult GetDoctors(GetDoctorModel model)
        {
            if (ModelState.IsValid)
            {
                //ownList is a flag used by Mahmoud(IDH) to determine whether the desired resultset is for me or one of my my team
                //value (0: false - 1: true)
                model.OwnList = this.UserID == model.UserID ? 1 : 0;
                //model.UserID = this.UserID;
                return Ok(new BaseResponse(doctorService.GetDoctorList(model).ToList().Select(s => new
                {
                    ID = s.professionalGuid,
                    Name = s.ProfessionalName,
                    Speciality = s.speciality,
                    Category = GetCategoryNameByID(s.Class),
                    Mobile = s.new_MobileNumber,
                    SalesRepName = s.salesRepName
                })));
            }
            return BadRequest(ModelState);
        }

        public string GetCategoryNameByID(int? _CategoryID)
        {
            string name = "";
            switch (_CategoryID)
            {
                case 1: name = "A"; break;
                case 2: name = "A+"; break;
                case 3: name = "B"; break;
                case 4: name = "B+"; break;
                case 5: name = "C"; break;
                case 6: name = "C+"; break;
            }
            return name;
        }

        [HttpPost]
        public IHttpActionResult CreateDoctor(CreateDoctorRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.UserID == null)
                    {
                        model.UserID = this.UserID;
                    }
                    doctorService.CreateDoctor(model);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

        }

        [HttpPost,RolesAdminManagerRequired]
        public IHttpActionResult ApproveDoctor(DoctorSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    doctorService.ApproveDoctor(model.DoctorID, model.CompanyID);
                    var doctor = doctorService.GetUnApprovedDoctor(model.DoctorID);
                    visitService.InsertNotification(doctor.SalesRepID, doctor.SalesRepName, $"Doctor: {doctor.Name} has been approved");
                    doctorService.DeleteDoctorRequest(model);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

        }

        [HttpPost]
        public IHttpActionResult DeleteDoctorRequest(DoctorSimpleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    doctorService.DeleteDoctorRequest(model);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }

        }

        [HttpPost]
        public IHttpActionResult CreateOrg(CreateOrgRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.UserID = this.UserID;
                    doctorService.CreateOrg(model);
                    return Ok(new BaseResponse());
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return Ok(new BaseResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public IHttpActionResult GetOrgList(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(doctorService.GetOrgList(model).Select(s => new
                {
                    ID = s.OrgGUID,
                    Name = s.name
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetDoctorOrgs(DoctorSimpleModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(doctorService.GetDoctorOrgList(model).Select(s => new
                {
                    ID = s.OrgGUID,
                    Name = s.name
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost]
        public IHttpActionResult GetSpecialities(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(doctorService.GetSpecialities(model).Select(s => new
                {
                    s.SpecialityID,
                    s.SpecialityName
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetUnAssignedDoctors(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(doctorService.GetUnassignedDoctors(model).Select(s => new
                {
                    DoctorID = s.professionalGuid,
                    DoctorName = s.ProfessionalName
                })));
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult AssignDoctor(ReassignDoctorRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                doctorService.ReassignDoctor(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult UpdateSalesRepDoctor(UpdateSalesRepDoctorRequest model)
        {
            if (ModelState.IsValid)
            {
                doctorService.UpdateSalesRepDoctor(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult ReassignDoctor(ReassignDoctorRequest model)
        {
            if (ModelState.IsValid)
            {
                model.UserID = this.UserID;
                doctorService.ReassignDoctor(model);
                return Ok(new BaseResponse());
            }
            return BadRequest(ModelState);
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetDoctorClasses()
        {
            return Ok();
        }

        [HttpPost, AuthorizationRequired]
        public IHttpActionResult GetDoctorCategory(BaseRequest model)
        {
            if (ModelState.IsValid)
            {
                return Ok(new BaseResponse(doctorService.GetDoctorCategory(model).Select(s => new
                {
                    ID = s.ID,
                    Name = s.TierValue
                })));
            }
            return BadRequest(ModelState);
        }



    }
}
