using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class JobApplicantModel:UserModel
    {
        public int JobApplicantId { get; set; }
        public virtual ICollection<ApplicationRequestModel>  ApplicationRequests { get; set; }

    }
}
