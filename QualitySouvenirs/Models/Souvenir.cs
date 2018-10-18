using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Souvenir
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string PathOfImage { get; set; }
        [Required]
        public string SupplierName { get; set; }
        [Required]
        public string CategoryName { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
