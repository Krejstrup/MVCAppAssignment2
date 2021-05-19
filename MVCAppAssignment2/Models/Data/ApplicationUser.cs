using Microsoft.AspNetCore.Identity;
using System;

namespace MVCAppAssignment2.Models.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
