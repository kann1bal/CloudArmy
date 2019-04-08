using MAP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MAP.Presentation.Models
{
     
        public enum KindVM { defect, feature, patch, claim }
        public enum StatusVM { NotTreatedYet, open, closed }
        public enum CategoryVM { Issues, Administration, Feedback, Security, UI, Code, Tasks, Projects, Documents, Plugins, Notifications, Others }
        public enum PriorityVM { Low, Normal, High, Urgent }

        public class RequestVM
        {
            public int RequestId { get; set; }

            [Required]
            public string Name { get; set; }

            [Required(ErrorMessage = "You must specify the kind of the request")]
            public KindVM Kind { get; set; }
            [Required(ErrorMessage = "You must specify the status of the request")]
            public StatusVM Status { get; set; }
            [Required(ErrorMessage = "You must specify the subject of the request")]
            public string Subject { get; set; }

            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            [DataType(DataType.Date)]
            public DateTime UpdateDate { get; set; }
            [Required(ErrorMessage = "You must specify the category of the request")]
            public CategoryVM Category { get; set; }
            [Required(ErrorMessage = "You must specify the author of the request")]
            
            public string UserCreate { get; set; }
            [Required(ErrorMessage = "You must specify the priority of the request")]
            public PriorityVM Priority { get; set; }
            public string UpdatedBy { get; set; }



        [Display(Name = "Dedicated To")]
        public int? Id { get; set; }
        IEnumerable<SelectListItem> Users { get; set; }
        public virtual UserModel UserVM{ get; set; }




            [Display(Name = "Project Name")]
            public int? ProjectId { get; set; }
            IEnumerable<SelectListItem> Projects { get; set; }








    }
}
