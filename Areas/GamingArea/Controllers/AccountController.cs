using Gaming.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gaming.Areas.GamingArea.Controllers
{
    [Area("GamingArea")]

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Fullname = model.Fullname
                };

                var result = await _userManager.CreateAsync(user, model.PasswordHash);

                if (result.Succeeded)
                {

                    return RedirectToAction("SignIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(User model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.PasswordHash, false, false);

                if (result.Succeeded)
                {

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Geçersiz giriş denemesi.");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();


            return RedirectToAction("SignIn");
        }
    }
}