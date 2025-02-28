using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Token.JwtTokenGenerator;

namespace Portfolio.Controllers;

public class AccountController : Controller
{
    private readonly JwtTokenGenerator _jwtTokenGenerator;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager)
    {
        _userManager = userManager;
        _signInManager = signinManager;
    }
    
    public async Task<IActionResult> Register(Register model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Index", "Home");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }


        return View(model);
    }

    public async Task<IActionResult> Login(Login obj)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = null;
            var result = await _signInManager.PasswordSignInAsync(obj.UserNameOrEmail, obj.Password, obj.RememberMe, lockoutOnFailure: false);
            if (obj.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(obj.UserNameOrEmail);

            }
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }
        return View(obj);
    }
}
