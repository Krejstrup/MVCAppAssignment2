using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MVCAppAssignment2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<IdentityRole> roleManager) // Constructor Injector
        {
            _userManager = userManager; // IdentityUser - Ska den bytas ut mot något annat? Lägg till extra Properties!!
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        //===============Early Questions:=================================================
        // What Roles should we use? - SuperAdmin, Admin & StdUser.
        // These should be created only once and then used!
        // StdUser should be able to show list of people/Add Person/Edit a person.
        // Admin should also be abel to Show/Create/Edit/Delete People/Cities/Countries.
        // (StdUser noAccess to City/Country controller.
        //          Should be able to add person to city, etc(?))
        // Try to remove the menu items too - anoying to watch a unuseful page with errors
        //================================================================================



        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(AccountRegister newUser)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    PhoneNumber = newUser.Phone,
                    FirstName = newUser.FirstName,
                    LastName = newUser.LastName,
                    BirthDate = newUser.BirthDate
                };


                //---- All new registered users should be in role User-----------------------------

                IdentityResult userResult = await _userManager.CreateAsync(user, newUser.Password);
                IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");


                //----- Track errors that the View cannot handle; email, username, password--------
                if (userResult.Succeeded && roleResult.Succeeded)
                {
                    // Are the person logged in now - not by creating the user. So Login:
                    await _signInManager.PasswordSignInAsync(user.UserName, newUser.Password, false, false);
                    return RedirectToAction("Index", "Home");   // Logged in and safe
                }

                foreach (var item in userResult.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description); // Look into the codes here!!
                }
            }
            return View(newUser);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login theUser) // LoginViewModel
        {
            if (ModelState.IsValid)
            {

                SignInResult result = await _signInManager.PasswordSignInAsync(theUser.UserName, theUser.Password, false, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("Locked Out", "To many tries");
                }

            }
            return View(theUser);
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }



        public async Task<IActionResult> AccountInfoAsync()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)   // session time out can get this to null
            {
                return RedirectToAction("Index", "Home");
            }

            IList<string> userRoles = await _userManager.GetRolesAsync(user);
            List<IdentityRole> rolesAvailable = _roleManager.Roles.ToList();
            UserDetailsInfo theUser = new UserDetailsInfo(user, userRoles, rolesAvailable);

            return View(theUser);
        }


        [Authorize(Roles = "SuperAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int userId) // Must be logged in as SuperAdmin to delete an Admin?
        {
            //--- Not implemented yet!


            return View();
        }
    }
}
