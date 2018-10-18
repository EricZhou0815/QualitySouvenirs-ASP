using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace QualitySouvenirs.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public bool Enabled { get; set; }
        public string Address { get; set; }
        public string UName { get; set; }

        public string MobileNumber { get; set; }
    }
}
