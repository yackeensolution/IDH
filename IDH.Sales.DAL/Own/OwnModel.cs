namespace IDH.Sales.DAL.Own
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OwnModel : DbContext
    {
        public OwnModel()
            : base("name=OwnModelConnection")
        {
        }

        public virtual DbSet<Doctor> Doctors { get; set; }
        public virtual DbSet<DoctorOrgMapping> DoctorOrgMappings { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UserDevice> UserDevices { get; set; }
        public virtual DbSet<UserNotification> UserNotifications { get; set; }
        public virtual DbSet<Visit> Visits { get; set; }
        public virtual DbSet<VisitPlan> VisitPlans { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VisitPlan>()
                .HasMany(e => e.Visits)
                .WithRequired(e => e.VisitPlan)
                .HasForeignKey(e => e.PlanID)
                .WillCascadeOnDelete(false);
        }
    }
}
