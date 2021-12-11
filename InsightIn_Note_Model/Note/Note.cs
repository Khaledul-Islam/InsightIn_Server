using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Model.Note
{
    public class Note
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
        public DateTime? Reminder { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; }
        [NotMapped]
        public string UserName { get; set; }
        [NotMapped]
        public string Email { get; set; }
        [NotMapped]
        public string Category_Name { get; set; }
    }
}
