using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Domain.Entities
    {
    public enum Kind { defect, feature, patch, claim }
    public enum Status { NotTreatedYet, open, closed }
    public enum Category { Issues, Administration, Feedback, Security, UI, Code, Tasks, Projects, Documents, Plugins, Notifications, Others }
    public enum Priority { Low, Normal, High, Urgent }


    public class Request
    {


        [Key]
        public int RequestId { get; set; }
        public string Name { get; set; }
        public Kind Kind { get; set; }
        public Status Status { get; set; }
        public string Subject { get; set; }
        public DateTime UpdateDate { get; set; }
        public Category Category { get; set; }
        public string UserCreate { get; set; }
        public string UpdatedBy { get; set; }
        public Priority Priority { get; set; }

       

        public int? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }


        public int? Id { get; set; }
        [ForeignKey("Id")]
        public virtual User User { get; set; }








    }
}
