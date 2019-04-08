using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Models
{
    public enum BrancheVM
    {
        web,
        JEE,
        Net
    }
    public class ProjectVM
    {
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DataType(DataType.Date)]
        public DateTime OutDate { get; set; }
        public string ImageUrl { get; set; }
        public BrancheVM Branche { get; set; }
        [Display(Name = "User")]
        public int? Id { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }


    }
}