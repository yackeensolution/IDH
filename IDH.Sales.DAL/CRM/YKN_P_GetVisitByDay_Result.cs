//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IDH.Sales.DAL.CRM
{
    using System;
    
    public partial class YKN_P_GetVisitByDay_Result
    {
        public System.Guid new_mdrepmasterId { get; set; }
        public string salesRepName { get; set; }
        public System.Guid new_mdsalesrepvisitId { get; set; }
        public string ProfessionalName { get; set; }
        public string specility { get; set; }
        public string OrgName { get; set; }
        public string Adress { get; set; }
        public Nullable<decimal> new_GPSPosX { get; set; }
        public Nullable<decimal> new_GPSPosY { get; set; }
        public string new_PhoneNumber { get; set; }
        public Nullable<System.DateTime> VisitDate { get; set; }
        public Nullable<int> statuscode { get; set; }
        public System.Guid VisitGUID { get; set; }
        public string visitComment { get; set; }
        public Nullable<int> new_VisitFeedback { get; set; }
    }
}
