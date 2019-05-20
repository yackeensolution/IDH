using IDH.Sales.BLL.DoctorWebReference;
using IDH.Sales.BLL.Repository;
using IDH.Sales.DAL.Own;
using IDH.Sales.Model.Models;
using IDH.Sales.Model.Models.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDH.Sales.BLL.Services.DoctorServices
{
    public class DoctorService
    {
        private readonly DoctorWebService doctorService;
        private readonly UnitOfWork uow;

        public DoctorService()
        {
            this.doctorService = new DoctorWebService();
            this.uow = new UnitOfWork();
        }

        public YKN_P_GetProfessionalList_Result[] GetDoctorList(GetDoctorModel _Model)
        {
            return doctorService.GetDoctorList(_Model.RoleID.ToString(), _Model.CompanyID, _Model.UserID.ToString(),_Model.OwnList);
        }

        public YKN_P_GetOrgList_Result[] GetOrgList(BaseRequest model)
        {
            return doctorService.GetOrgList(model.CompanyID);
        }

        public YKN_P_GetProfissionalOrgList_Result[] GetDoctorOrgList(DoctorSimpleModel model)
        {
            return doctorService.GetDoctorOrgList(model.DoctorID, model.CompanyID);
        }

        public void ApproveDoctor(Guid _DoctorID,int _CompanyID)
        {
            var doctor = uow.Repo<Doctor>().GetFirstOrDefault(w => w.ID == _DoctorID);
            var doctorOrgs = uow.Repo<DoctorOrgMapping>().GetByExression(w => w.DoctorID == _DoctorID);
            doctorService.CreateDoctor(doctor.ID, doctor.SalesRepID, 0, doctor.Name,doctor.CategoryID, doctor.Mobile, doctor.SpecialityID,_CompanyID);
            doctorService.AssignDoctor(Guid.NewGuid(), doctor.ID, doctor.SalesRepID, _CompanyID);
            doctorOrgs.ToList().ForEach(f => 
            {
                doctorService.AddDoctorOrg(Guid.NewGuid(), _DoctorID,f.OrgID, _CompanyID);
                uow.Repo<DoctorOrgMapping>().Delete(d => d.ID == f.ID);
            });
            uow.Save();
        }

        public void CreateDoctor(CreateDoctorRequest model)
        {
            var insertedDoctor=uow.Repo<Doctor>().Insert(new Doctor()
            {
                ID=Guid.NewGuid(),
                Name=model.Name,
                SalesRepID=(Guid) model.UserID,
                SalesRepName=model.UserName,
                SpecialityID=model.SpecialityID,
                SpecialityName=model.SpecialityName,
                CategoryID=model.CategoryID,
                CategoryName=model.CategoryName,
                Mobile=model.Mobile
            });

            foreach (var orgID in model.Orgs)
            {
                uow.Repo<DoctorOrgMapping>().Insert(new DoctorOrgMapping() { DoctorID=insertedDoctor.ID,OrgID=orgID});
            }
            uow.Save();
        }

        public void CreateOrg(CreateOrgRequest model)
        {
            doctorService.CreateOrg(Guid.NewGuid(), model.UserID,0, model.Name, model.Address,model.Phone,model.CompanyID);
        }

        public void ReassignDoctor(ReassignDoctorRequest model)
        {
            doctorService.ReassignDoctor(Guid.NewGuid(), model.UserID, model.DoctorID, model.SalesRepID,model.CompanyID);
        }
        
        public IQueryable<Doctor> GetUnApprovedDoctors(List<Guid> _Users)
        {
            return uow.Repo<Doctor>().GetByExression(w => _Users.Any(a => a == w.SalesRepID));
        }


        public Doctor GetUnApprovedDoctor(Guid DoctorID)
        {
            return uow.Repo<Doctor>().GetById(DoctorID);
        }

        public void UpdateSalesRepDoctor(UpdateSalesRepDoctorRequest model)
        {
            doctorService.UpdateSalesRepDoctor(Guid.NewGuid(), model.DoctorID, model.SalesRepID, model.CompanyID);
        }

        public YKN_P_GetSpecialityList_Result[] GetSpecialities(BaseRequest model)
        {
            return doctorService.GetSpecialities(model.CompanyID);
        }

        public YKN_P_GetUnAssignedProfessionals_Result[] GetUnassignedDoctors(BaseRequest model)
        {
            return doctorService.GetUnAssignedDoctors(model.CompanyID);
        }

        public YKN_PH_GetProfessionalTier_Result[] GetDoctorCategory(BaseRequest model)
        {
            return doctorService.GetDoctorCategory();
        }

        public void DeleteDoctorRequest(DoctorSimpleModel model)
        {
            uow.Repo<Doctor>().Delete(w => w.ID == model.DoctorID);
            uow.Save();
        }
    }
}
