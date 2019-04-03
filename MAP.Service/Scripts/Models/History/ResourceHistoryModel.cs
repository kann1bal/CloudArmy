using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.Presentation.Models
{
    public class ResourceHistoryModel
    {


        public int ResourceHistoryId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateBegin { get; set; }

        public DateTime DateEnd { get; set; }
    }
}
