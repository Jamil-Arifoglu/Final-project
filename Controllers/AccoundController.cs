using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Gaming.ViewModels;
using Gaming.Entities;

namespace Gaming.Controllers
{
    public class AccoundController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccoundController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM account)
        {
            if (!ModelState.IsValid)
                return View();

            if (!account.Terms)
                return View();

            if (string.IsNullOrEmpty(account.Password))
            {
                ModelState.AddModelError("", "Password cannot be null or empty.");
                return View();
            }

            User user = new User
            {
                UserName = account.Username,
                Fullname = string.Concat(account.Firstname, " ", account.Lastname),
                Email = account.Email
            };

            IdentityResult result = await _userManager.CreateAsync(user, account.Password);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View();
            }

            return RedirectToAction("Index", "Gaming");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = await _userManager.FindByNameAsync(login.Username);
            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, login.Password, login.RememberMe, true);
            if (!result.Succeeded)
            {
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError("", "Due to overtrying, your account has been blocked for 5 minutes");
                    return View();
                }

                ModelState.AddModelError("", "Username or password is incorrect");
                return View();
            }

            return RedirectToAction("Index", "Gaming");
        }
        public IActionResult ShowAuthenticated()
        {
            return Json(User.Identity.IsAuthenticated);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Gaming");
        }
    }
}