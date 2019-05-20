namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DoctorOrgMapping")]
    public partial class DoctorOrgMapping
    {
        public int ID { get; set; }

        public Guid DoctorID { get; set; }

        public Guid OrgID { get; set; }
    }
}
