using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Configurations
{
    public class MeetingConfiguration : EntityTypeConfiguration<Meeting>
    {
        public MeetingConfiguration()
        {
           


            //One to many configuration (0..1 to 0..*) 
            HasOptional(p => p.User).WithMany(c => c.meetings)
               .HasForeignKey(p => p.Id)
               .WillCascadeOnDelete(false); 

            //One to many configuration (0..1 to 0..*) 
            HasOptional(f => f.Project).WithMany(p => p.meetings)
               .HasForeignKey(f => f.ProjectId)
               .WillCascadeOnDelete(false);




        }

    }
}
