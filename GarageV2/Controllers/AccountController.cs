using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GarageV2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GarageV2.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
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
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(loginViewModel);
            }
        }

        //Get
        public IActionResult Register()
        {
            return View();
        }


    }
}