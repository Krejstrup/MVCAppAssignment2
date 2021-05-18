using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCAppAssignment2.Models.ViewModel;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace MVCAppAssignment2.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) // Constructor Injector
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
                IdentityUser user = new IdentityUser()
                {
                    UserName = newUser.UserName,
                    Email = newUser.Email,
                    PhoneNumber = newUser.Phone
                };

                IdentityResult result = await _userManager.CreateAsync(user, newUser.Password);

                // Track errors that the View cannot handle; email, username, password
                if (result.Succeeded)   // Propertiy can not be switch'ed!
                {
                    // Are the person logged in now??

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
    }
}
