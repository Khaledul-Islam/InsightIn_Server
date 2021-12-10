using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Model.Note
{
    public class Notes
    {
        [Key]
        public int NoteID { get; set; }
        [Required]
        public int NoteCategoryID { get; set; }

        [ForeignKey("NoteCategoryID")]
        public virtual NoteCat NoteCategory { get; set; }
        [Required]
        public string NoteTitle { get; set; }
        [MaxLength(100)]
        [Required]
        public string NoteDescription { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Reminder { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreateDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
