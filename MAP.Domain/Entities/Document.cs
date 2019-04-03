using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Domain.Entities
{
    public enum Type
    {
        Pdf,
        Doc,
        Images

    }
    public  class Document
    {

        [Key]
        public int DocumentId { get; set; }
        public DateTime DateDoc { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ImageUrl { get; set; }
        public Type Type { get; set; }
        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
    }
}
