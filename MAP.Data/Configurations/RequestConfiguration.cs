using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Configurations
{
    public class RequestConfiguration:EntityTypeConfiguration<Request>
    {
        public RequestConfiguration(DbModelBuilder modelBuilder)
        {
        
            //One to many configuration (0..1 to 0..*) 
           modelBuilder.Entity<Request>().HasRequired(p => p.User).WithMany(c => c.requests)
                .HasForeignKey(p => p.Id)
                .WillCascadeOnDelete(false);


            //One to many configuration (0..1 to 0..*)
            modelBuilder.Entity<Request>().HasRequired(p => p.Project).WithMany(c => c.requests)
               .HasForeignKey(f => f.ProjectId)
               .WillCascadeOnDelete(false); 



        }

    }
}
