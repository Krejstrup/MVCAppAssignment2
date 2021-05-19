using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.Data;
using MVCAppAssignment2.Models.ViewModel;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MVCAppAssignment2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager) // Constructor Injector
        {
            _userManager = userManager; // IdentityUser - Ska den bytas ut mot något annat? Lägg till extra Properties!!
            _signInManager = signInManager;
        }


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

                //SqlException: Invalid column name 'BirthDate'.
                //Invalid column name 'FirstName'.
                //Invalid column name 'LastName'.


                IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);

                // Track errors that the View cannot handle; email, username, password
                if (result.Succeeded)   // Propertiy can not be switch'ed!
                {
                    // Are the person logged in now - not by creating the user. So Login:
                    await _signInManager.PasswordSignInAsync(user.UserName, newUser.Password, false, false);

                    return RedirectToAction("Index", "Home");   // Logged in and safe
                }

                foreach (var item in result.Errors)
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
            AccountRegister theUser = new AccountRegister()
            {
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Email = user.Email
            };
            return View(theUser);
        }
    }
}
