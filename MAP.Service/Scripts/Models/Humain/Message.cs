using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MAP.Presentation.Models.Humain
{
    public class Message
    {

        [Key]
        public int IdM { get; set; }

        public string Content { get; set; }
        public string Sender { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public DateTime DateMessage { get; set; }
        public int? Vu { get; set; }
    }
}