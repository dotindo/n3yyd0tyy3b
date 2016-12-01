using System.Data.Entity;

namespace DotWeb
{
    public class DotWebDb : DbContext
    {
        public DotWebDb() : base("DotWebDb") { }

        public DbSet<App> Apps { get; set; }

        public DbSet<ModuleGroup> ModuleGroups { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<TableMeta> Tables { get; set; }

        public DbSet<ColumnMeta> Columns { get; set; }

        public DbSet<TableMetaRelation> TableRelations { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<UserGroupMembers> UserGroupMembers { get; set; }

        public DbSet<AccessRight> AccessRights { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TableMeta>()
                .HasMany(x => x.Children).WithRequired(y => y.Parent).HasForeignKey(f => f.ParentId).WillCascadeOnDelete(false);

            modelBuilder.Entity<TableMeta>()
                .HasMany(x => x.Parents).WithRequired(y => y.Child).HasForeignKey(f => f.ChildId).WillCascadeOnDelete(false);

            modelBuilder.Entity<ColumnMeta>()
                .HasRequired(x => x.Table).WithMany(y => y.Columns).HasForeignKey(f => f.TableId);

            modelBuilder.Entity<ColumnMeta>()
                .HasOptional(x => x.ReferenceTable).WithMany().HasForeignKey(f => f.ReferenceTableId);

            modelBuilder.Entity<Module>()
                .HasRequired(x => x.Group).WithMany(g => g.Modules).HasForeignKey(f => f.GroupId);

            modelBuilder.Entity<Role>().HasRequired(p => p.App).WithMany().HasForeignKey(p => p.AppId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Permission>().HasRequired(p => p.Role).WithMany(r => r.Permissions).HasForeignKey(p => p.RoleId);

            modelBuilder.Entity<UserGroupMembers>().HasRequired(m => m.Group).WithMany().HasForeignKey(m => m.GroupId).WillCascadeOnDelete(false);

            modelBuilder.Entity<AccessRight>().HasRequired(r => r.Role).WithMany().HasForeignKey(r => r.RoleId).WillCascadeOnDelete(false);

            modelBuilder.Entity<User>().HasOptional(u => u.Organization).WithMany().HasForeignKey(u => u.OrganizationCode).WillCascadeOnDelete(false);
        }
    }
}
