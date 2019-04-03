using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public enum TypeClient
    {
        ActualClient, NewClient, CompletedContract
    }
    public enum CategoryClient
    {
        Public, Private
    }
    public class ClientModel:UserModel
    {
        public int ClientId { get; set; }
        [Display(Name = "Client Name")]
    //    [Required(ErrorMessage = "this field is required")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Display(Name = "Logo")]
        [DataType(DataType.Text)]
        public string Logo { get; set; }

        [Display(Name = "Number of Project")]
        [DataType(DataType.Currency)]
        public int NumberProject { get; set; }

        [Display(Name = "Client Type")]
        [DataType(DataType.Text)]
        public string Type { get; set; }

        [Display(Name = "Client Mail")]
        [DataType(DataType.Text)]
        public string Mail { get; set; }

        [Display(Name = "Mobile Number")]
        [DataType(DataType.Text)]
        public string NumTel { get; set; }


        public string Category { get; set; }

        public virtual ICollection<DiscussionModel> Disscussions { get; set; }
        public virtual ICollection<ComplaintModel> Complaints { get; set; }
        public virtual ICollection<AddressModel> Addresses { get; set; }

        public virtual ICollection<ProjectModel> Projects { get; set; }
    }
}
