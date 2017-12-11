using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
{
   public class Tasks
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
        public System.DateTime Date { get; set; }
        public long priority { get; set; }
        public string TaskTitle { get; set; }

        public virtual User User { get; set; }
        public virtual State State { get; set; }
    }
}
