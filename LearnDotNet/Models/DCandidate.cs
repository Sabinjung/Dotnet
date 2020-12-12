using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LearnDotNet.Models
{
    public class DCandidate
    {
        [Key]
        public int id { get; set; }
        public string FullName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        public string BloodGroup { get; set; }
        public string Address { get; set; }

    }
}
