using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Models
{
    [Table("Occupation")]
    public class Occupation
    {
        [Key]
        public int OccupationId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual Member Member { get; set;  }
    }
}
