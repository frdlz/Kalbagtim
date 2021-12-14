﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectAlpha.Models;
using System.Threading.Tasks;

namespace ProjectAlpha.Controllers
{
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;
        private IPasswordValidator<AppUser> passwordValidator;
        private IUserValidator<AppUser> userValidator;

        public AdminController(UserManager<AppUser> usrMgr, IPasswordHasher<AppUser> passwordHash, IPasswordValidator<AppUser> passwordVal, IUserValidator<AppUser> userValid)
        {
            userManager = usrMgr;
            passwordHasher = passwordHash;
            passwordValidator = passwordVal;
            userValidator = userValid;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }
       
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser
                {
                    Id = user.NIP,
                    UserName = user.Name,
                    Email = user.Email,
                    Jabatan = user.Jabatan,
                    Penempatan = user.Penempatan
                };

                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded)
                    return RedirectToAction("Login", "Account");
                else
                {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError("", error.Description);
                }
            }
            return View(user);
        }
       
        public async Task<IActionResult> Update(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
       
        public async Task<IActionResult> Update(string id, string email, string password, string jabatan, string Penempatan)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult validEmail = null;
                if (!string.IsNullOrEmpty(email))
                {
                    validEmail = await userValidator.ValidateAsync(userManager, user);
                    if (validEmail.Succeeded)
                        user.Email = email;
                    else
                        Errors(validEmail);
                }
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await passwordValidator.ValidateAsync(userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = passwordHasher.HashPassword(user, password);
                    else
                        Errors(validPass);
                }

                else
                    ModelState.AddModelError("", "Password tidak boleh kosong");
                if (!string.IsNullOrEmpty(jabatan))
                    user.Jabatan = jabatan;
                else
                    ModelState.AddModelError("", "Jabatan tidak boleh kosong");
                if (!string.IsNullOrEmpty(Penempatan))
                    user.Penempatan = Penempatan;
                else
                    ModelState.AddModelError("", "Penempatan tidak boleh kosong");
                if (validEmail != null && validPass != null && validEmail.Succeeded && validPass.Succeeded && !string.IsNullOrEmpty(jabatan))
                {
                    IdentityResult result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        Errors(result);

                }


            }
            else ModelState.AddModelError("", "User Not Found");
            return View(user);

        }
        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);

        }
        [HttpPost]
       
        public async Task<IActionResult> Delete(string id)
        {
            AppUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "user not found");
            return View("Index", userManager.Users);
        }
    }
}
