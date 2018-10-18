﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualitySouvenirs.Models
{
    public class Category
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public ICollection<Souvenir> Souvenirs { get; set; }
    }
}
