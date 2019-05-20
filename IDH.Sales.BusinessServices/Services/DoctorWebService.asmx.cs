using IDH.Sales.DAL.CRM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace IDH.Sales.BusinessServices.Services
{
    /// <summary>
    /// Summary description for DoctorWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DoctorWebService : System.Web.Services.WebService
    {
        Entities db = new Entities();

        [WebMethod]
        public List<YKN_P_GetProfessionalList_Result> GetDoctorList(string _RoleID,int _CompanyID,string _UserID,int _OwnList)
        {
            return db.YKN_P_GetProfessionalList(_RoleID, _CompanyID, _UserID,_OwnList).ToList();
        }

       
        [WebMethod]
        public List<YKN_P_GetOrgList_Result> GetOrgList( int _CompanyID)
        {
            return db.YKN_P_GetOrgList( _CompanyID).ToList();
        }

        [WebMethod]
        public List<YKN_P_GetSpecialityList_Result> GetSpecialities(int _CompanyID)
        {
            return db.YKN_P_GetSpecialityList().ToList();
        }

        [WebMethod]
        public void CreateDoctor(Guid _Guid, Guid _UserID,int _Status,string _DoctorName,int _DoctorCategory,string _Mobile, Guid _SpecialityID, int _CompanyID)
        {
            db.YKN_Push_AddProfessional(_Guid,_UserID,_Status,_DoctorName,_DoctorCategory,_Mobile,_SpecialityID);
        }

        [WebMethod]
        public List<YKN_P_GetProfissionalOrgList_Result> GetDoctorOrgList(Guid _DoctorID,int _CompanyID)
        {
            return db.YKN_P_GetProfissionalOrgList(_CompanyID, _DoctorID).ToList() ;
        }

        [WebMethod]
        public void CreateOrg(Guid _Guid, Guid _UserID, int _Status, string _OrgName,string _OrgAddress,string _OrgPhone, int _CompanyID)
        {
            db.YKN_Push_AddOrganization(_Guid, _UserID, _Status, _OrgName, _OrgAddress,_OrgPhone);
        }

        [WebMethod]
        public void AddDoctorOrg(Guid _Guid,Guid _DoctorID,Guid _OrgID, int _CompanyID)
        {
            db.YKN_Push_AddProfessionalOrg(_Guid, _DoctorID, _OrgID, null);
        }

        [WebMethod]
        public List<YKN_P_GetUnAssignedProfessionals_Result> GetUnAssignedDoctors(int _CompanyID)
        {
            return db.YKN_P_GetUnAssignedProfessionals(_CompanyID).ToList();
        }

        [WebMethod]
        public void AssignDoctor(Guid _Guid,Guid _DoctorID,Guid _SalesRepID,int _CompanyID)
        {
            db.YKN_Push_AssignProfessional(_Guid, _DoctorID, _SalesRepID, _CompanyID);
        }

        [WebMethod]
        public void ReassignDoctor(Guid _Guid, Guid _UserID, Guid _ProfessionalID, Guid _SalesRepID, int _CompanyID)
        {
            db.YKN_Push_ReAssignProfessional(_Guid, _UserID, _ProfessionalID, _SalesRepID);
        }

        [WebMethod]
        public void UpdateSalesRepDoctor(Guid _Guid, Guid _DoctorID, Guid _SalesRepID,int _CompanyID)
        {
            db.YKN_Push_UpdateSalesRepProfessional(_Guid, _DoctorID, _SalesRepID);
        }

        [WebMethod]
        public List<YKN_PH_GetProfessionalTier_Result> GetDoctorCategory()
        {
            return db.YKN_PH_GetProfessionalTier().ToList();
        }
    }
}
