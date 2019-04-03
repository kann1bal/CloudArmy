using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Data.CustomConventions
{
   public class DateTime2Convention : Convention
    {

        public DateTime2Convention()
        {
            Properties<DateTime>().Configure(d => d.HasColumnType("datetime2"));
            Properties<string>().Where(c => c.Name.StartsWith("code")).Configure(c => c.IsKey());
        }
    }
}
