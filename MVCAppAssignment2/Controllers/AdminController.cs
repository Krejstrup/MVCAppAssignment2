using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCAppAssignment2.Controllers
{

    [Authorize(Roles = "SuperAdmin, Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }


        //===============================================================================================
        // Vad ska vi kunna göra med Rollerna? SuperAdmin, Admin och StdUser?
        // StdUser ska kunna visa lista på personer/Lägga till person/Editera befintlig person.
        // Admin ska kunna visa/skapa/editera/radera people/cities/countries.
        // SuperAdmin är den enda som får hantera Admins.
        // (StdUser noAccess till City/Country controller. Ska kunna lägga till person till city, etc(?))
        //===============================================================================================


        public IActionResult Index()
        {
            return View();
        }


        //------------ Show list of users: UserList---------------------------------------

        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }




        //----------- Show details of user: Details---------------------------------------------------
        public async Task<IActionResult> Details(string id) // This is User specific view, not logged in view. 
        {
            ApplicationUser theUser = await _userManager.FindByIdAsync(id);

            if (theUser == null)
            {
                return RedirectToAction(nameof(UserList));
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(theUser);
            List<IdentityRole> rolesAvailable = _roleManager.Roles.ToList();
            UserDetailsInfo userDetails = new UserDetailsInfo(theUser, userRoles, rolesAvailable);
            return View(userDetails);
        }



        //----------------- Set Roles for user: SetRoles----------------------------------

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> SetRoles(string id)
        {
            ApplicationUser theUser = await _userManager.FindByIdAsync(id);

            if (theUser == null)
            {
                return RedirectToAction(nameof(UserList));
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(theUser);
            List<IdentityRole> rolesAvailable = _roleManager.Roles.ToList();

            RolesManagement dataUser = new RolesManagement(theUser.Id, userRoles, rolesAvailable);

            return View(dataUser);
        }




        // ----------- Create more roles - only SuperAdmins--------------------------------

        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (String.IsNullOrWhiteSpace(roleName))
            {
                ViewBag.ErrorMsg = "Role name must not contain spaces";
                return View("CreateRole", roleName);
            }

            IdentityRole role = new IdentityRole(roleName);

            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(UserList));  //RoleList
            }

            ViewBag.ErrorMsg = "Role was not created";

            return View("CreateRole", roleName);
        }





        //------- Add Role(s) to User - only SuperAdmin ----------------------------------------
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> AddRoleToUser(string userId, string roleName)  // Is this "SuperAdmin"?
        {
            // Check for SuperAdmin as a roleName OR check for User role is SuperAdmin if roleName is Admin

            ApplicationUser theUser = await _userManager.FindByIdAsync(userId); // fetch the user from database


            if (theUser != null)
            {


                if (string.Equals(roleName, "SuperAdmin", StringComparison.OrdinalIgnoreCase))
                {

                    IdentityResult result = await _userManager.AddToRoleAsync(theUser, roleName);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Details", new { id = userId });
                    }

                }

                IList<string> userRoles = await _userManager.GetRolesAsync(theUser);
                List<IdentityRole> identityRoles = _roleManager.Roles.ToList();

                RolesManagement returnVM = new RolesManagement(userId, userRoles, identityRoles);

                ViewBag.ErrorMsg = "Failed to change role for user!";
                return View("Details", returnVM);        //"SetRoles", returnVM);  // Jump back to the RoleManagement page
            }

            return RedirectToAction("UserList");

        }






        //-----------------Removes a role from User - Only SuperAdmin ---------------------------------
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> RemoveRoleFromUser(string userId, string roleName)
        {
            //---Check for SuperAdmin as a roleName - that's not alowed
            //if (! string.Equals(roleName, "SuperAdmin", StringComparison.OrdinalIgnoreCase))
            //{}


            ApplicationUser theUser = await _userManager.FindByIdAsync(userId); // fetch the user from database


            //---------if the User don't exist OR if the roleName is SuperUser then exit---------------
            if (theUser == null)
            {
                return RedirectToAction(nameof(UserList));  // User not found, go back to the list again
            }



            //---------All ok Remove the Role from user------------------------------------------------
            IdentityResult result = await _userManager.RemoveFromRoleAsync(theUser, roleName);

            if (result.Succeeded)
            {
                return RedirectToAction("Details", new { id = userId });        //"SetRoles", new { id = userId });
            }


            //---------If that didn't work arrange and show the page again-----------------------------
            IList<string> userRoles = await _userManager.GetRolesAsync(theUser);
            List<IdentityRole> identityRoles = _roleManager.Roles.ToList();
            RolesManagement returnVM = new RolesManagement(userId, userRoles, identityRoles);

            ViewBag.ErrorMsg = "Failed to delete role for user!";

            return View("Details", returnVM);
        }
    }
}
