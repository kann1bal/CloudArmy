using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class MeetingModel
    {
        public int MeetingId { get; set; }

        [Display(Name = "Meeting Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "this field is required")]
        public DateTime MeetingDate { get; set; }


    }
}
