using Bulky.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Token;
using Token.JwtTokenGenerator;

namespace Portfolio.Controllers;

public class AccountController : Controller
{
    private readonly TokenService _tokenService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _config;
    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signinManager, TokenService tokenService, IConfiguration config)
    {
        _userManager = userManager;
        _signInManager = signinManager;
        _tokenService = tokenService;
        _config = config;
    }
    
    public async Task<IActionResult> Register(Register model)
    {
        if (ModelState.IsValid)
        {
            IdentityUser user = new IdentityUser { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                var roleResult = await _userManager.AddToRoleAsync(currentUser, "Admin");
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

            // Determine if input is email or username
            if (obj.UserNameOrEmail.Contains("@"))
            {
                user = await _userManager.FindByEmailAsync(obj.UserNameOrEmail);
            }
            else
            {
                user = await _userManager.FindByNameAsync(obj.UserNameOrEmail);
            }
            if (user == null)
            {
                return Unauthorized("Invalid Credentials from user == nulll");
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, obj.Password, obj.RememberMe, lockoutOnFailure: false);

            //if (!result.Succeeded)
            //{
            //    return Unauthorized("Invalid Credentials from result succeeded");
            //}

            var token = await _tokenService.GenerateJwtToken(user);
            SetTokenCookie(token);

            return View();
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            return View();
        }
    }
    private void SetTokenCookie(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.UtcNow.AddMinutes(double.Parse(_config["JWT:ExpirationInMinutes"]))
        };
        Response.Cookies.Append("AuthToken", token, cookieOptions);
    }
}
