using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class ComplaintModel
    {

        [Key]
        public int ComplaintId { get; set; }

        [Display(Name = "Comment")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        public string Comment { get; set; }
        public DateTime DateReclamation { get; set; }
        public int? ClientId { get; set; }//nullable
        public string ResourceName { get; set; }//nullable

        

      //  public virtual Client Client { get; set; }


        //[ForeignKey("ResourceId")]
        //public virtual Resource Resource { get; set; }

    }
}
