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

    public class DocumentVM
    {

        public int DocumentId { get; set; }
        [DataType(DataType.Date)]//affichi calendrier

        public DateTime DateDoc { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ImageUrl { get; set; }
        public TypeVm TypeVm { get; set; }
        public int? ProjectId { get; set; }
        public IEnumerable<SelectListItem> Projects { get; set; }

    }

}