using System.Threading.Tasks;
using GarageV2.Models;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GarageV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<Member> _signInManager;
        private readonly UserManager<Member> _userManager;

        public AccountController(SignInManager<Member> signInManager, UserManager<Member> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        //Get
        public IActionResult Login()
        {
            return View();
        }

        //Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.Email); 

            if(user is null)
            {
                ModelState.AddModelError("", "User name not found");
                return View(loginViewModel);
            }

            var loginResult = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, isPersistent: false, lockoutOnFailure: false);

            if (!loginResult.Succeeded)
            {
                ModelState.AddModelError("", "Wrong password");
                return View(loginViewModel);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logoff()
        {
            await _signInManager.SignOutAsync();
            return View("LoggedOut");
        }
    }
}