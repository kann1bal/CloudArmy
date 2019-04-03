using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class SkillModel
    {
        public int SkillId { get; set; }

        public virtual ICollection<ResourceModel> Resources { get; set; }
        
    }
}
