using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Supplier
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string ContactPerson { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}
