using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
   public class AddressModel
    {
        public int AddressId { get; set; }
        [Display(Name = "Longitude")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        
        public double Longitude { get; set; }

        [Display(Name = " Latitude ")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        public double Latitude { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public string Region { get; set; }
        public int ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientModel  Client { get; set; }

    }
}
