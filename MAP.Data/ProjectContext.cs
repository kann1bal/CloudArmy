using MAP.Data.Configurations;
using MAP.Domain.Entities;
using Solution.Data.Custom_Conventions;
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



        public DbSet<Documentt> Documentt { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Tasks> tasks { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        modelBuilder.Configurations.Add(new DocumentProjectConfiguration());
            modelBuilder.Configurations.Add(new MeetingConfiguration());
            modelBuilder.Configurations.Add(new RequestConfiguration(modelBuilder));


            modelBuilder.Conventions.Add(new DateTimeConventions());

            modelBuilder.Entity<Request>()
               .HasKey(i => i.RequestId);

            modelBuilder.Entity<Request>()
                .Property(b => b.ProjectId)
                .IsOptional();

            modelBuilder.Entity<Request>()
                 .Property(b => b.Id)
                 .IsOptional();


            //    modelBuilder.Conventions.Add(new DateTime2Convention());
        }
        public static ProjectContext Create()
        {
            return new ProjectContext();
        }

    }
}
