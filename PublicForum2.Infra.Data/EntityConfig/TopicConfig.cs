using PublicForum2.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Data.EntityConfig
{
    public class TopicConfig : EntityTypeConfiguration<Topic>
    {
        public TopicConfig()
        {
            Property(c => c.Description)
                .HasMaxLength(10000)
                .IsRequired();

            Property(c => c.Title)
                    .HasMaxLength(250)
                    .IsRequired();

            Property(c => c.Created)
                    .IsRequired();

            Property(c => c.OwnerId)
                  .HasMaxLength(250)
                  .IsRequired();

            Property(c => c.IsDeleted)
                   .IsRequired();
        }
        
    }
}
