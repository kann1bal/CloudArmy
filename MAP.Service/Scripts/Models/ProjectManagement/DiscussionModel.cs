using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class DiscussionModel
    {
        public int DiscussionId { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientModel Client { get; set; }

        public virtual ICollection<MessageModel> Messages { get; set; }
    }
}
