using Microsoft.AspNetCore.Identity;
using MVCAppAssignment2.Models.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class UserDetailsInfo
    {

        [Required]
        [Display(Name = "User Name")]
        [StringLength(20, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(20, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(20, MinimumLength = 2)]
        public string LastName { get; set; }


        [EmailAddress]
        [StringLength(25, MinimumLength = 6)]
        public string Email { get; set; }

        [Phone]
        [StringLength(25, MinimumLength = 6)]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth date")]
        public DateTime BirthDate { get; set; }

        public string UserId { get; set; }

        public IList<string> UserRoles { get; set; }

        public List<IdentityRole> IdentityRoles { get; set; }

        public UserDetailsInfo(ApplicationUser user, IList<string> userRoles, List<IdentityRole> ientityRoles)
        {
            UserName = user.UserName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Phone = user.PhoneNumber;
            Email = user.Email;
            BirthDate = user.BirthDate;

            UserId = user.Id;
            UserRoles = userRoles;
            IdentityRoles = ientityRoles;

            FilterRoles();
        }


        void FilterRoles()  // A genious filter move for the listing *Katchaa*
        {
            foreach (string item in UserRoles)
            {
                IdentityRoles.Remove(IdentityRoles.Single(role => role.Name.Equals(item)));
            }
        }

    }
}
