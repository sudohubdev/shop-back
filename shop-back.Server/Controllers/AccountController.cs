using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using shop_back.Server.Models;

namespace shop_back.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.Phone
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { Success = true });
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        return BadRequest(errors);
    }

#if DEBUG
    [HttpGet]
    [Route("loginDev")]
    public async Task<IActionResult> LoginDev()
    {
        var user = await _userManager.FindByEmailAsync("dev@dev.com");
        if (user == null)
        {
            user = new IdentityUser { UserName = "admin", Email = "dev@dev.com" };
            await _userManager.CreateAsync(user, "P@ssw0rd");
        }
        await _signInManager.SignInAsync(user, isPersistent: false);
        return Ok(new { Success = true });
    }
#endif

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok(new { Success = true });
            }
            else
            {
                return BadRequest("Invalid login.");
            }
        }
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        return BadRequest(errors);
    }

    [HttpPost]
    [Route("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok(new { Success = true });
    }

    [HttpGet]
    [Route("checkUser")]
    public IActionResult CheckUser() =>
        Ok(new { Online = User.Identity?.IsAuthenticated ?? false });

}
