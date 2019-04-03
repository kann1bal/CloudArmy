using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public enum ProjectType
    {
        NewProject, ProjectInProgress, EndedProject
    }
    public class ProjectModel
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "this field is required")]
        public string NameProject { get; set; }

        public int nbRessourceRequired { get; set; }
        public double Profitability { get; set; }


        [Display(Name = "Image Project")]
        [DataType(DataType.ImageUrl)]
        public int? ClientId { get; set; }
        public string ImageProject { get; set; }
        [Required(ErrorMessage = "this field is required")]
        public ProjectType Type { get; set; }
        [ForeignKey("ClientId")]
        public virtual ClientModel Client { get; set; }

        public virtual ICollection<ResourceModel> Resources { get; set; }
    }
}
