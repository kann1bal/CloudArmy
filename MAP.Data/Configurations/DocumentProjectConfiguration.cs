using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Configurations
{
    class DocumentProjectConfiguration : EntityTypeConfiguration<Documentt>
    {
        public DocumentProjectConfiguration()
        {
            HasRequired(t => t.Project).WithMany(p => p.Documents)
                .HasForeignKey(f => f.ProjectId)
                .WillCascadeOnDelete(true);


        }
    }
}
