using PublicForum2.Infra.Identity.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Identity.EntityConfig
{
    public class ApplicationUserConfig : EntityTypeConfiguration<ApplicationUser>
    {
        public ApplicationUserConfig()
        {
            HasKey(u => u.Id);

            Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(256);

            Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(256);

            Property(u => u.UserName)
               .IsRequired()
               .HasMaxLength(256);

            Property(u => u.FirstName)
              .IsRequired()
              .HasMaxLength(200);

            Property(u => u.LastName)
              .IsRequired()
              .HasMaxLength(200);

            Property(u => u.Enabled)
              .IsRequired();

            ToTable("AspNetUsers");
        }
    }
}
