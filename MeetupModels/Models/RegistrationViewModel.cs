using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Meetup.Models
{
    public class RegistrationViewModel
    {
        [StringLength(50)]
        public string Name { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Age { get; set; }
        public DateTime DOB { get; set; }
        [StringLength(20)]
        public string Profession { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int NumberOfGuest { get; set; }
        [StringLength(50)]
        public string Address { get; set; }
    }
}
