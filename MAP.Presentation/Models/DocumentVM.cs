using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Models
{
    public enum TypeVm
    {
        Pdf,
        Doc,
        Images

    }
   
    public enum ChoixVMS { Name, Project };

    public class DocumentVM
    {

        public int DocumentId { get; set; }
        public DateTime DateDoc { get; set; }
        public string Name { get; set; }
        [Display(Name = "Description")]
        public string Size { get; set; }
    
        public string ImageUrl { get; set; }
        [Display(Name = "Type Document")]
        public TypeVm TypeVm { get; set; }
        public string Extension { get; set; }

        public string ProjectNames { get; set; }
        [Display(Name = "Project Name")]
        public int? ProjectId { get; set; }
        IEnumerable<SelectListItem> Projects { get; set; }
    }

}