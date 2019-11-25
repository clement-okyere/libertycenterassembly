using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibertyFamilySystem.ViewModels
{
    public class AttendanceViewModel
    {
        public int? EventId { get; set; }
        public int MemberId { get; set; }
        public string FullName { get; set; }
    }
}
