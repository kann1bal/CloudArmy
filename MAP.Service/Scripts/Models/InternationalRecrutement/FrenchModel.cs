using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class FrenchModel :TestModel
    {
        [Display(Name = "Question")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "this field is required")]
        public string Question { get; set; }

        [Display(Name = "Answer")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "this field is required")]
        public string Answer { get; set; }

       
    }
}
