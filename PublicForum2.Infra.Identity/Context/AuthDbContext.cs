using Microsoft.AspNet.Identity.EntityFramework;
using PublicForum2.Infra.Identity.EntityConfig;
using PublicForum2.Infra.Identity.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Identity.Context
{
    public class AuthDbContext : IdentityDbContext<ApplicationUser>, IDisposable
    {
        public AuthDbContext()
            : base("DefaultConnection", false)
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApplicationUserConfig());

            base.OnModelCreating(modelBuilder);
        }


        public static AuthDbContext Create()
        {

            return new AuthDbContext();
        }
    }
}
