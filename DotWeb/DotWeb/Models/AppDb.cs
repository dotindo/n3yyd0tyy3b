using System.Data.Entity;

namespace DotWeb.Models
{
    public partial class AppDb : DbContext
    {
        public AppDb()
            : base("name=AppDb")
        {
        }

        public virtual DbSet<AssemblyType> AssemblyTypes { get; set; }
        public virtual DbSet<CheckListGroup> CheckListGroups { get; set; }
        public virtual DbSet<CheckListInstanceInfo> CheckListInstanceInfoes { get; set; }
        public virtual DbSet<CheckListInstanceStep> CheckListInstanceSteps { get; set; }
        public virtual DbSet<CheckListTemplateInfo> CheckListTemplateInfoes { get; set; }
        public virtual DbSet<CheckListTemplateStep> CheckListTemplateSteps { get; set; }
        public virtual DbSet<ChecklistType> ChecklistTypes { get; set; }
        public virtual DbSet<Model> Models { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Variant> Variants { get; set; }
        public virtual DbSet<AssemblySection> AssemblySections { get; set; }
        public virtual DbSet<CGISFilter> CGISFilters { get; set; }
        public virtual DbSet<Stations> Stationses { get; set; }
        public virtual DbSet<FileType> FileTypes { get; set; }
        public virtual DbSet<ProductionLine> Productline { get; set; }
        public virtual DbSet<ProductionSequence> ProductionSequences { get; set; }
        public virtual DbSet<ProductionSequenceDetail> ProductionSequenceDetails { get; set; }
        public virtual DbSet<SMTPConfig> SMTPConfigs { get; set; }
        public virtual DbSet<NotificationEmail> NotificationEmails { get; set; }
        public virtual DbSet<NotificationApp> NotificationApps { get; set; }
        //public virtual DbSet<NotificationAppDetail> NotificationAppDetails { get; set; }
        public virtual DbSet<PendingTasks> PendingTasks { get; set; }
        public virtual DbSet<RecordImplemControl> RecordImplemControls { get; set; }
        public virtual DbSet<RecordImplemControlDetail> RecordImplemControlDetails { get; set; }
        public virtual DbSet<VehicleOrders> VehicleOrders { get; set; }
        public virtual DbSet<VehicleOrderDetails> VehicleOrderDetails { get; set; }
        public virtual DbSet<ToolVerificationResult> ToolVerificationResults { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<ToolSetup> ToolSetups { get; set; }
        public virtual DbSet<Tool> Tools { get; set; }
        public virtual DbSet<ToolAssignment> ToolAssigns { get; set; }
        public virtual DbSet<ToolInventory> ToolInventories { get; set; }
        //public virtual DbSet<ToolList> ToolLists { get; set; }
        public virtual DbSet<ToolVerification> ToolVerifications { get; set; }
        public virtual DbSet<ToolCalibration> ToolCalibrations { get; set; }
        public virtual DbSet<ToolCalibrationResult> ToolCalibrationResults { get; set; }
        public virtual DbSet<CPConsumptionMaterial> CpConsumptionMaterials { get; set; }
        public virtual DbSet<CPDetail> CpDetails { get; set; }
        public virtual DbSet<CPHeader> CpHeaders { get; set; }
        public virtual DbSet<CPToolList> CpToolLists { get; set; }
        public virtual DbSet<ControlPlanImage> ControlPlanImages { get; set; }
        public virtual DbSet<ControlPlanMaterial> ControlPlanMaterials { get; set; }
        public virtual DbSet<ControlPlanProcess> ControlPlanProcesses { get; set; }
        public virtual DbSet<ControlPlan> ControlPlans { get; set; }
        public virtual DbSet<ControlPlanStation> ControlPlanStations { get; set; }
        public virtual DbSet<ControlPlanTool> ControlPlanTools { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<DocType> DocTypes { get; set; }
        public virtual DbSet<CGISSynchronized> CGISSynchronizeds { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AssemblyType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListGroup>()
                .Property(e => e.CheckListGroupCode)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListGroup>()
                .Property(e => e.CheckListGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.PackingMonth)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.Variant)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.RunningNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.InstanceName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.InstanceDocument)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.LastActivity)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.Progress)
                .HasPrecision(5, 2);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceInfo>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceStep>()
                .Property(e => e.StepName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceStep>()
                .Property(e => e.UrlLink)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceStep>()
                .Property(e => e.StoreProcedureName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceStep>()
                .Property(e => e.EmailNotification)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListInstanceStep>()
                .Property(e => e.Predecessor)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateInfo>()
                .Property(e => e.TemplateName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateInfo>()
                .Property(e => e.TemplateDocNumber)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateInfo>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateInfo>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.StepName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.UrlLink)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.StoreProcedureName)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.EmailNotification)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.Predecessor)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<CheckListTemplateStep>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ChecklistType>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Model>()
                .HasMany(e => e.Variants)
                .WithRequired(e => e.Model)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Organization>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.Type1)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.CreatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<Type>()
                .Property(e => e.ModifiedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);
        }
    }
}
