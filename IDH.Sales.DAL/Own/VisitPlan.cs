namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("VisitPlan")]
    public partial class VisitPlan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VisitPlan()
        {
            Visits = new HashSet<Visit>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }

        public Guid UserID { get; set; }

        [StringLength(50)]
        public string UserName { get; set; }

        public int Status { get; set; }

        public int CompanyID { get; set; }

        public DateTime CreatedOn { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Visit> Visits { get; set; }
    }
}
