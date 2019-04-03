
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
   public class RatingModel
    {

        public int RatingId { get; set; }

        public int Note { get; set; }
        public int? ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public virtual ResourceModel Resource { get; set; }

    }
}
