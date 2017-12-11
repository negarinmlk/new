using System.ComponentModel.DataAnnotations;
namespace DataLayer
{
    public class TaskMetadata
    {
        public int ID { get; set; }
        public int UserId { get; set; }
        public int StateId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public System.DateTime Date { get; set; }
        public long priority { get; set; }
        public string TaskTitle { get; set; }
        
        public virtual User User { get; set; }
        public virtual State State { get; set; }
    }
    [MetadataType(typeof(TaskMetadata))]
    public partial class Task
    {

    }
}
