using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.Models
{
    [Table("Member")]
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string WhatsAppNumber { get; set; }
        public bool IsGroupAdmin { get; set; } 
        [ForeignKey("Occupation")]
        public int OccupationId { get; set; }
        public virtual Occupation Occupation { get; set; }
        [ForeignKey("Group")]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }
        [NotMapped]
        public SelectList GroupSel { get; set; }
        [NotMapped]
        public SelectList OccupationSel { get; set; }
        public virtual Attendance Attendance { get; set; }
    }
}
