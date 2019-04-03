using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class MissionHistoryModel
    {
        public int MissionHistoryId { get; set; }


        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "this field is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "this field is required")]
        public DateTime EndDate { get; set; }

        public int Id_Resource { get; set; }

        public int Id_Project { get; set; }
    }
}
