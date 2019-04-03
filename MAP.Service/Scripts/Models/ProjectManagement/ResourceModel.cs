using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public enum AvailabilityStatus
    {
        Available, NonAvailable
    }
    //public enum ContractType
    //{
    //    Employee, Freelancer
    //}
    public class ResourceModel :UserModel
    {
       // public int ResourceId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string ContractType { get; set; }


        public string Seniority { get; set; }

        public string Photo { get; set; }

        public string Availability { get; set; }

        public DateTime DateBegin { get; set; }

        public string Entreprise { get; set; }

        public  int? SkillId { get; set; }

        [ForeignKey("SkillId")]
        public virtual SkillModel Skill { get; set; }
        public  string ResourceType { get; set; }

        public virtual ICollection<LeaveRequestModel> Leaves { get; set; }

        public virtual ICollection<ProjectModel> Projects { get; set; }
        public virtual ICollection<RatingModel> Ratings { get; set; }
    }
}
