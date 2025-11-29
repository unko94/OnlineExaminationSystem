using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Models.Model;
using Models.ViewModel;
using System.Text;
using Utility;


namespace OnlineExaminationSystem.Areas.Account.Controllers
{
    [Area("RigAndLog")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
             IEmailSender emailSender,
             IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            ApplicationUser? email = await _userManager.FindByEmailAsync(registerVM.email);
            ApplicationUser? username = await _userManager.FindByNameAsync(registerVM.username);
            if (email !=null || username !=null)
            {
                TempData["RegisterError"] = "username  or email already exists";
                return View("Register", registerVM);
            }
            var user = new ApplicationUser()
            {
                 FullName = registerVM.firstname + "" + registerVM.lastname,
                 Email = registerVM.email,
                 UserName = registerVM.username

            };
            
            var result = await _userManager.CreateAsync(user, registerVM.password);

           
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var encodetoken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var confirmationLink = Url.ActionLink("ConfirmEmail", "Account", new { area= "RigAndLog", userId = user.Id, token = encodetoken }, Request.Scheme);
                await _emailSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>");
               
                return RedirectToAction(nameof(LogIn));
            }
            return View();
        }
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {    // var user = await _userManager.FindByNameAsync(userId);
            var user = await _userManager.FindByIdAsync(userId);
            if (user  == null) return NotFound();
            var decodetoken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user,decodetoken);
            if (result.Succeeded)
            {
                return View("EmailConfirmed");
            }
            return View("Error");

        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model)
        {
            var email = model.Email?.Trim();
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");

                //user.AccessFailedCount =  user.AccessFailedCount + 1;
                TempData["Error"] = "Invalid login attempt.";
                return View(model);
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                TempData["Error"] = "Invalid login attempt.";
                return View(model);
            }
            if (roles.Contains(SD.Role_Admin))
            {
                await _userManager.SetLockoutEnabledAsync(user,false);
                await _userManager.ResetAccessFailedCountAsync(user);
            }
            if (user.LockoutEnabled == true && user.LockoutEnd > DateTime.UtcNow)
            {
                
                TempData["Error"] = " your account is locked please try later   ";
                return View(model);
            }

            if (!await _userManager.IsEmailConfirmedAsync(user))
            {
                //user.AccessFailedCount = user.AccessFailedCount + 1;
                TempData["Error"] = "You must confirm your email to log in.";
                await _userManager.AccessFailedAsync(user);
                return View(model);

            }

            //lockoutOnFailure : true increase AccessFailedCount
            var result = await _signInManager.PasswordSignInAsync(
                user.UserName, model.Password, model.RememberMe, lockoutOnFailure: true
            );

            if (result.Succeeded)
            {
                await _userManager.ResetAccessFailedCountAsync(user);
                return RedirectToAction("Index", "Home", new { area = "User" });
            }

            var count =await _userManager.GetAccessFailedCountAsync(user);

            if (count >= 3)
            {
                
                await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.UtcNow.AddMinutes(15));
            }

            //  ModelState.AddModelError("", "Invalid login attempt.");
            TempData["Error"] = "You must confirm your email to log in.";
            user.AccessFailedCount = user.AccessFailedCount + 1;
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "User" });

        }
     //   [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public IActionResult EmailConfirmed()
        {
            return View();
        }
     //   [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public IActionResult ForgetPasswordEmail() 
        {

            return View();
        }
        // [HttpPost(Name = "ForgetPasswordEmail")]
        [HttpPost]
      //  [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> ForgetPasswordEmail(ForgetPasswordEmailVM model)
        {
            Random random = new Random();
            int verfiycode = random.Next(100000,999999);
            var user = _userManager.FindByEmailAsync(model.Email);
            if (user == null) 
            {
                // return View("ForgetPasswordEmail",forgetPasswordEmailVM)
                TempData["Error"] = "User not found";
                return View(model);
            }
            await _emailSender.SendEmailAsync(model.Email,"Verfication code",$"this is your verfication code ${verfiycode}");
            HttpContext.Session.SetInt32("verfiycode", verfiycode);
            HttpContext.Session.SetString("Email",model.Email);

            return RedirectToAction("ForgetPasswordVerfiyCode");
        }
       // [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public IActionResult ForgetPasswordVerfiyCode()
        {
           
            return View();
        }
        [HttpPost]
       // [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public IActionResult ForgetPasswordVerfiyCode(ForgetPasswordVerfiyCodeVM model)
        {
            var verfiycode = HttpContext.Session.GetInt32("verfiycode");
            if (verfiycode == null)
            {
                TempData["VerfiyCode"] = "Verfication Code is not correct";
                return View(model);
            }
            if(verfiycode != model.VerficationCode)
            {
                TempData["VerfiyCode"] = "Verfication Code is not correct";
                return View(model);
            }

            return RedirectToAction("ChangePassword");
        }
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> ChangePassword(ForgetPasswordNewPasswordVM model)
        {
            var Email = HttpContext.Session.GetString("Email");
            if (string.IsNullOrEmpty(Email))
            {
                TempData["Error"] = "Session expired. Please restart the password reset process";
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(Email);
            if (user  == null)
            {
                TempData["Error"] = " user not found";
                return View(model);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user,token,model.newpassword);
            if (!result.Succeeded)
            {
                TempData["Error"] = "Reset password faild";
                return View(model);
            }
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Remove("verfiycode");
            return RedirectToAction("LogIn");

        }

    }
}
