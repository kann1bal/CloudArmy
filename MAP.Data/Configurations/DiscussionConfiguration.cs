using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.Configurations
{
   public  class DiscussionConfiguration : EntityTypeConfiguration<Discussion>
    {
        public DiscussionConfiguration()
        {
            HasOptional(disc => disc.Client)
          .WithMany(cl => cl.Disscussions)
          .HasForeignKey(pro => pro.ClientId)
          .WillCascadeOnDelete(true);
        }
    }
}
