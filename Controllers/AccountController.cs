using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Stupify.Models;
using Stupify.Services;
using Stupify.ViewModels;

namespace Stupify.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Route("[controller]")]
public class AccountController : Controller
{
    private readonly UserService _userService;

    public AccountController(UserService userService)
    {
        _userService = userService;
    }

    private static string EncryptPassword(string password)
    {
        var data = Encoding.UTF8.GetBytes(password);
        using var shaM = SHA512.Create();
        {
            var hashedInputBytes = shaM.ComputeHash(data);
            var hashedInputStringBuilder = new StringBuilder(128);
            foreach (var b in hashedInputBytes)
                hashedInputStringBuilder.Append(b.ToString("X2"));
            return hashedInputStringBuilder.ToString();
        }
    }
    
    private async Task Authenticate(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login)
        };
        var id = new ClaimsIdentity(claims, "ApplicationCookie",
            ClaimsIdentity.DefaultNameClaimType,
            ClaimsIdentity.DefaultRoleClaimType);
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
    }

    [HttpGet("Login")]
    public ActionResult Login()
    {
        return View();
    }
    
    [HttpPost("Login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);
        var user = _userService.GetList().FirstOrDefault(x => x.Login == model.Login && x.Password == EncryptPassword(model.Password));
        if (user != null)
        {
            await Authenticate(user);
            return RedirectToAction("Index", "Music");
        }
        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
        return View(model);
    }
    
    [HttpGet("Logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }
    
    [HttpGet("Register")]
    public ActionResult Register()
    {
        return View();
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid) 
            return View(model);
        if (_userService.GetList().FirstOrDefault(x => x.Login == model.Login) == null)
        {
            var user = new User { Login = model.Login, 
                Password = EncryptPassword(model.Password), 
                Role = "Пользователь", 
                Likes = new List<UserLike>()};
            _userService.Create(user);

            await Authenticate(user);
            return RedirectToAction("Index", "Music");
        }
        ModelState.AddModelError("", "Пользователь с таким логином уже существует");
        return View(model);
    }
}