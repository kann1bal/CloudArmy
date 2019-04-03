using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public enum LeaveType
    {
        Vacation, Maternity, Paternity, Illness
    }
    public class LeaveRequestModel
    {
        public int leaveRequestId { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "this field is required")]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "this field is required")]
        public DateTime EndDate { get; set; }

        public string LeaveType { get; set; }
        public int? ResourceId { get; set; }
        [ForeignKey("ResourceId")]
        public virtual ResourceModel Resource { get; set; }
    }
}
