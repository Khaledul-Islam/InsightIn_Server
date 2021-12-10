using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsightIn_Note_Model.Note
{
    public class NoteCat
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(20)]
        public string Category_Name { get; set; }
    }
}
