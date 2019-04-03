using MAP.Domain.Entities.ProjectManagement;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Configurations
{
    public class RequirementConfiguration : EntityTypeConfiguration<Requirement>
    {

        public RequirementConfiguration() 
        {
            HasOptional(pro => pro.Project)
            .WithMany(cl => cl.Requirements)
            .HasForeignKey(pro => pro.ProjectId)
            .WillCascadeOnDelete(true);

        }
    }
}
