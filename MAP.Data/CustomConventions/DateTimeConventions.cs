using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Data.Custom_Conventions
{
    public class DateTimeConventions : Convention
    {
        public DateTimeConventions()
        {
            Properties<DateTime>().Configure(e => e.HasColumnType("datetime2")); //Accepter les dates a partir de l'annee 0
     
            
            
            
        }
    }
}
