namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserNotification")]
    public partial class UserNotification
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        [Required]
        [StringLength(200)]
        public string UserName { get; set; }

        [StringLength(500)]
        public string Title { get; set; }

        public DateTime? CreationDate { get; set; }
    }
}
