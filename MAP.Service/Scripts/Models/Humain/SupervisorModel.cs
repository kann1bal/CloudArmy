using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
   public class SupervisorModel : UserModel
    {
        public int SupervisorId { get; set; }
        public string Grade { get; set; }
        public virtual ICollection<ApplicationRequestModel> Requests { get; set; }
    }
}
