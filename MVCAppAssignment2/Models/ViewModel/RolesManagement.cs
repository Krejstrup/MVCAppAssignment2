using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace MVCAppAssignment2.Models.ViewModel
{
    public class RolesManagement
    {

        public string UserId { get; set; }

        public IList<string> UserRoles { get; set; }

        public List<IdentityRole> IdentityRoles { get; set; }

        public RolesManagement(string userId, IList<string> userRoles, List<IdentityRole> ientityRoles)
        {
            UserId = userId;
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
