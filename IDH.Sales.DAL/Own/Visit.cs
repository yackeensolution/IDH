namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Visit")]
    public partial class Visit
    {
        public int ID { get; set; }

        public int VisitCode { get; set; }

        public int? PlanID { get; set; }

        public Guid DoctorID { get; set; }

        [Required]
        [StringLength(100)]
        public string DoctorName { get; set; }

        public Guid OrgID { get; set; }

        [Required]
        [StringLength(100)]
        public string OrgName { get; set; }

        [Column(TypeName = "date")]
        public DateTime VisitDate { get; set; }

        public int VisitLevelID { get; set; }

        [Required]
        [StringLength(100)]
        public string VisitLevelName { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        public virtual VisitPlan VisitPlan { get; set; }
    }
}
