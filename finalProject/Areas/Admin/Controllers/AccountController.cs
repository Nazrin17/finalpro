
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[Area("admin")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    //public async Task<IActionResult> Index()
    //{
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
    //    await _roleManager.CreateAsync(new IdentityRole { Name = "user" });
    //    return Json("ok");
    //}
    //public async Task<IActionResult> Index()
    //{
    //    AppUser user = new AppUser
    //    {
    //        UserName = "Admin",
    //        Email = "esgerova94@gmail.com"
    //    };
    //    var result = await _userManager.CreateAsync(user, "Admin123@");
    //    if (!result.Succeeded)
    //    {
    //        foreach (var item in result.Errors)
    //        {
    //            return Json(item.Description);
    //        }

    //    }
    //    var result2 = await _userManager.AddToRoleAsync(user, "admin");
    //    if (!result2.Succeeded)
    //    {
    //        return Json("errror");

    //    }
    //    return Json("ok");
    //}
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(UserLoginDto login)
    {
        AppUser user = await _userManager.FindByNameAsync(login.UserName);
        if (user == null)
        {
            ModelState.AddModelError("", "UserName or Password incorrect");
            return View(login);
        }
        var result = await _signInManager.PasswordSignInAsync(user, login.Password, true, true);
        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "UserName or Password incorrect");
            return View(login);
        }
        return RedirectToAction("Index", "Doctor");
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Login", "Account");
    }

}

