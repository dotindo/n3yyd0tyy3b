using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotWeb
{
    public class IdentityDb : IdentityDbContext
    {
        public IdentityDb() : base("DotWebDb") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().ToTable("IdentityRole");
            modelBuilder.Entity<IdentityUser>().ToTable("IdentityUser");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("IdentityUserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("IdentityUserLogin");
            modelBuilder.Entity<IdentityUserRole>().ToTable("IdentityUserRole");
        }
    }
}