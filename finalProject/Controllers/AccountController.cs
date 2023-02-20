using AutoMapper;
using Core.Entities.Concrete;
using Core.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace finaProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }
        //public async Task<IActionResult> Index()
        //{
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "admin" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "user" });
        //    await _roleManager.CreateAsync(new IdentityRole { Name = "doctor" });
        //    return Json("ok");
        //}

        public IActionResult Register ()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDto register)
        {
            AppUser user = await _userManager.FindByNameAsync(register.UserName);
            if (user != null)
            {
                ModelState.AddModelError("", "UserName already exsist");
                return View(register);
            }

            AppUser newuser =_mapper.Map<AppUser>(register);
            var result = await _userManager.CreateAsync(newuser, register.Password);
            if(!result.Succeeded) {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View(register);
            }
            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newuser);
            string link = Url.Action(nameof(Verify), "Account", new {email=newuser.Email,token=token},Request.Scheme,Request.Host.ToString());
            Mail.SendMessage(newuser.Email, "Verify email", link);
            var result2 = await _userManager.AddToRoleAsync(newuser, "user");
            if (!result2.Succeeded)
            {
                foreach (var item in result2.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                    
                }
                return View(register);
            }

            return RedirectToAction("Index","Home");
        }
       
        public async Task<IActionResult> Verify(string email,string token)
        {
            AppUser user= await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.ConfirmEmailAsync(user, token);
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("index", "Home");
        }
        
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ResetPasswordDto dto)
        {
            AppUser user = await _userManager.FindByEmailAsync(dto.Email);
            if(user==null) return NotFound();
            string token=await _userManager.GeneratePasswordResetTokenAsync(user);
            string link = Url.Action(nameof(ResetPassword), "Account", new { email = user.Email, token }, Request.Scheme, Request.Host.ToString());
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("medicalhospital2020@gmail.com");
            mail.To.Add(new MailAddress(user.Email));
            mail.Subject = "Verify email";
            mail.Body = link;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("medicalhospital2020@gmail.com", "tkdsqqyuoxrnlhot");
            smtp.Send(mail);

            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> ResetPassword(string email,string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            if (user == null) return NotFound();
            ResetPasswordDto dto = new ResetPasswordDto()
            {
                Email = email,
                Token = token
            };
            return View(dto);

        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            AppUser user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "User not found");
                return View(dto);
            }
            IdentityResult result =await _userManager.ResetPasswordAsync(user,dto.Token,dto.Password);
           
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("",item.Description);
                }
                return View(dto);
            }
            return RedirectToAction("Index", "Home");
        }
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
            var result= await _signInManager.PasswordSignInAsync(user, login.Password,true,true);
            if (!result.Succeeded)
            {
                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError("", "Please verify email");
                    return View(login);
                }
                ModelState.AddModelError("", "UserName or Password incorrect");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
