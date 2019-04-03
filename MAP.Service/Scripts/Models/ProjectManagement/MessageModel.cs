using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class MessageModel
    {
        public int MessageId { get; set; }

        public string MessageContent { get; set; }
        public virtual MessageModel message { get; set; }
    }
}
