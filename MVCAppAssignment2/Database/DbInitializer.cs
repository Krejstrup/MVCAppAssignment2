using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.Repo;
using System;
using System.Linq;

namespace MVCAppAssignment2.Database
{
    internal class DbInitializer        // Step 1
    {

        internal static void Initialize(
                        PeopleDbContext context,
                        RoleManager<IdentityRole> roleManager,
                        UserManager<ApplicationUser> userManager)    //Note that this is Static, use only one!
        {
            // Entity Framwork Migrations has been used dont run this: context.Database.EnsureCreated();
            context.Database.Migrate(); // This will update the database migrations for us.

            // Add three different userroles only once, check the database if exist:
            if (context.Roles.Any())
            {
                return; // Yes roles exists, return 
            }

            //----Now we can seed the Roles for the Users into the database--------------------------

            string[] seedRoles = new string[] { "SuperAdmin", "Admin", "User" };

            for (int i = 0; i < seedRoles.Length; i++)
            {
                IdentityRole role = new IdentityRole(seedRoles[i]);

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;

                if (!roleResult.Succeeded)
                {
                    throw new Exception("Failed to Create Role: " + seedRoles[i]);

                }
            }

            //------------ Create the User for SuperAdmin-------------------------------------

            ApplicationUser user = new ApplicationUser()
            {
                UserName = "AdminPower",        // The Super Not Spy
                Email = "info@information.info",
                PhoneNumber = "045464322",
                FirstName = "Power",
                LastName = "Adminson",
                BirthDate = DateTime.Now
            };

            IdentityResult userResult = userManager.CreateAsync(user, "Qwert!234").Result;

            if (!userResult.Succeeded)
            {
                throw new Exception("Failed to Create User: " + user.UserName);
            }


            //---------Assign the Role SuperAdmin to User---------------------------------------

            IdentityResult addResult = userManager.AddToRoleAsync(user, seedRoles[0]).Result;

            if (!addResult.Succeeded)
            {
                throw new Exception($"Failed to assign user {user.UserName} to role {seedRoles[0]}");
            }
        }
    }
}
