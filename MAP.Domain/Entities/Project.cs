using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Domain.Entities
{
    public enum Branche  {
        Net,
        Jee,
        Web
    }


    public class Project
    {
        [Key]
        public int ProjectId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime OutDate { get; set; }
        public string ImageUrl { get; set; }
        public Branche Branche { get; set; }
        public int? Id { get; set; }
        //prop de navigation
        [ForeignKey("Id")]
        public virtual User User { get; set; }
        public virtual ICollection<Documentt> Documents { get; set; }
        public virtual ICollection<Meeting> meetings { get; set; }
        public virtual ICollection<Tasks> tasks { get; set; }
        public virtual ICollection<Request> requests { get; set; }


    }
}
