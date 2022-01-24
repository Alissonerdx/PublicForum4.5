using PublicForum2.Domain.Entities;
using PublicForum2.Infra.Data.EntityConfig;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PublicForum2.Infra.Data.Context
{
    public class EFContext : DbContext
    {
        public EFContext()
             : base("DefaultConnection")
        {
        }

        public DbSet<Topic> Topics { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TopicConfig());
            modelBuilder.Configurations.Add(new UserConfig());


            base.OnModelCreating(modelBuilder);
        }
    }
}
