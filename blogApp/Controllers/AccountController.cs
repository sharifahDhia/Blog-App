using blogApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace blogApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) 
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

		[HttpGet]
		public IActionResult RegisterLogin()
		{
			var model = new RegisterLoginView
			{
				Register = new Register(),
				Login = new Login()
			};
			return View(model);
		}

		[HttpGet]
        public IActionResult Register()
        {
            return View("RegisterLogin", new RegisterLoginView());
		}

        [HttpPost]
        public async Task<IActionResult> Register (RegisterLoginView model)
        {
			ModelState.ClearValidationState(nameof(model.Login));
			ModelState.MarkFieldValid(nameof(model.Login));

			if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Register.Email, Email = model.Register.Email };
                var result = await _userManager.CreateAsync(user, model.Register.Password);
                Console.WriteLine(result);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "BlogPost");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    Console.WriteLine("Error:", error);
                }
            }
			foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
			{
				Console.WriteLine(error.ErrorMessage);
			}
			return View("RegisterLogin", model);
		}

        [HttpGet]
        public IActionResult Login()
        {
            return View("RegisterLogin", new RegisterLoginView());
		}

        [HttpPost]
        public async Task<IActionResult> Login(RegisterLoginView model)
        {
			ModelState.ClearValidationState(nameof(model.Register));
			ModelState.MarkFieldValid(nameof(model.Register));

			if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login.Email, model.Login.Password, false, lockoutOnFailure: true);
                Console.WriteLine(result);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "BlogPost");
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            Console.WriteLine(model);
            Console.WriteLine(ModelState.IsValid);
            return View("RegisterLogin", model);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("RegisterLogin", new RegisterLoginView());
		}

    }
}
