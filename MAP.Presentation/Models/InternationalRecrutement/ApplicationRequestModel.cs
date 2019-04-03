using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class ApplicationRequestModel
        
    {


        public int ApplicationRequestId { get; set; }

        public string ReuiredProfile { get; set; }

        public DateTime YearExp { get; set; }

        public DateTime DateDeposit { get; set; }

        public string CV { get; set; }
        //public int TestId { get; set; }
        //[ForeignKey("TestId")]
        //public virtual Test Test { get; set; }

        //public virtual Supervisor Supervisor { get; set; }

        public int JobApplicantId { get; set; }
        [ForeignKey("JobApplicantId")]
        public virtual JobApplicantModel JobApplicant { get; set; }
    }
}
