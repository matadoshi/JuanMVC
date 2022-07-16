using DomainModels.Models.Common;
using JuanMVC.Tools.EmailHandler;
using JuanMVC.Tools.EmailHandler.Abstraction;
using JuanMVC.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JuanMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;

        public AccountController(RoleManager<IdentityRole> roleManager,
                                UserManager<AppUser> userManager, 
                                SignInManager<AppUser> signInManager,
                                IEmailService emailService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser = new AppUser
            {
                Name = registerVM.Name,
                Surname = registerVM.Surname,
                Email = registerVM.Email,
                UserName = registerVM.UserName
            };

            IdentityResult identityResult = await _userManager.CreateAsync(appUser, registerVM.Password);

            if (!identityResult.Succeeded)
            {
                foreach (var item in identityResult.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

                return View();
            }

            await _userManager.AddToRoleAsync(appUser, "Member");

            //await _emailService.SendEmailAsync(new MailRequest {
            //    ToEmail=appUser.Email,Subject="Congrats",Body="Welcome our team"                
            //});
            return RedirectToAction("Index","Home");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View();

            AppUser appUser = await _userManager.FindByEmailAsync(loginVM.Email);

            if (appUser == null)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }

            if (!await _userManager.CheckPasswordAsync(appUser, loginVM.Password))
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }

            Microsoft.AspNetCore.Identity.SignInResult signInResult = 
                await _signInManager.PasswordSignInAsync(appUser, loginVM.Password, loginVM.RemindMe, true);

            if (signInResult.IsLockedOut)
            {
                ModelState.AddModelError("", "Your Account is blocked");
                return View();
            }

            if (!signInResult.Succeeded)
            {
                ModelState.AddModelError("", "Email Or Password Is InCorrect");
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EmailVerification()
        {
            return View();
        }
        public async Task<IActionResult> VerifyEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();
            AppUser user = await _userManager.FindByIdAsync(userId);
            if (user is null) return BadRequest();
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> CreateRole()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "Member" });

        //    return Content("Roles Successfuly Created");
        //}
        //public async Task<IActionResult> CreateSuperAdmin()
        //{
        //    AppUser appUser = new AppUser
        //    {
        //        Email = "superadmin@juan.com",
        //        UserName = "SuperAdmin",
        //        Name = "Onur",
        //        Surname = "Ismailov",
        //    };

        //    await _userManager.CreateAsync(appUser, "SuperAdmin@123");
        //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

        //    return Content("Super Admin Was Successfully Created");
        //}
    }
}
