namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserDevice")]
    public partial class UserDevice
    {
        public int ID { get; set; }

        public Guid UserID { get; set; }

        public Guid SecurityToken { get; set; }

        public string DeviceToken { get; set; }

        public int? Badge { get; set; }

        public int CompanyID { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
