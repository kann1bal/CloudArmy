using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Domain.Entities
{
    public enum Complexity { easy, medium, hard }
    public enum Statuss { affected, notAffected, suggested }
    public enum Progress { level0, level1, level2, level3, level4 }
    public enum IsDone { Done, NotDone}
    public class Tasks
    {
        [Key]
        public int taskId { get; set; }
        public string Description { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime startDate { get; set; }
        public double SpentTime { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime deadline { get; set; }
        public Complexity complexity { get; set; }
        public float rate { get; set; }
        public Statuss status { get; set; }
        public Progress progress { get; set; }
        public IsDone IsDone { get; set; }
        public string estimation { get; set; }




        public int? Id { get; set; }
        public User user { get; set; }
        public int? ProjectId { get; set; }
        public Project project { get; set; }




    }
}
