using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class TestModel
    {
        public int TestId { get; set; }


        public DateTime DateTest { get; set; }

        public int Result { get; set; }

        //public int ApplicationRequestId { get; set; }
        //[ForeignKey("ApplicationRequestId")]
        //public virtual ApplicationRequest ApplicationRequest{ get; set; }

    }
}
