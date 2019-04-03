using MAP.Data.Configurations;
using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data
{
    public class ProjectContext : DbContext
    {
        public ProjectContext() : base("ProjectManagement")
        {

        }

        public DbSet<User> users { get; set; }
        public DbSet<CustomRole> roles { get; set; }



        public DbSet<Document> Document { get; set; }

        public DbSet<Project> Project { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        modelBuilder.Configurations.Add(new DocumentProjectConfiguration());

        //    modelBuilder.Conventions.Add(new DateTime2Convention());
        }
        public static ProjectContext Create()
        {
            return new ProjectContext();
        }

    }
}
