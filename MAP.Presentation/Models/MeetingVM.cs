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
    public class MeetingVM
    {
        public int MeetingId { get; set; }

        public string Title { get; set; }

        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int? Id { get; set; }

        [Display(Name = "Project")]
        public int? ProjectId { get; set; }
        [DataType(DataType.MultilineText)]
        public string Details { get; set; }

        public String ProjectName { get; set; }

    }
}