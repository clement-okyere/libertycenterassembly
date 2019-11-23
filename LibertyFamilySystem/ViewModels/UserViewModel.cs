using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
