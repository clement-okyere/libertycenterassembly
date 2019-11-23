using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Models
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        [Required]
        public int Name { get; set; }
        public virtual Member Member { get; set; }
    }
}
