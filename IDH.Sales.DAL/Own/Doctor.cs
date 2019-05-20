namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Doctor")]
    public partial class Doctor
    {
        public Guid ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public Guid SpecialityID { get; set; }

        [StringLength(100)]
        public string SpecialityName { get; set; }

        public Guid SalesRepID { get; set; }

        [StringLength(100)]
        public string SalesRepName { get; set; }

        public int CategoryID { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }
    }
}
