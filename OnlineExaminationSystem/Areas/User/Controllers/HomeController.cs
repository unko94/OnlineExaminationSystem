using DataAccess.IService;
using DataAccess.Repositery.IRepositery;
using DataAccess.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Models.Model;
using Models.ViewModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;


namespace OnlineExaminationSystem.Controllers
{
    [Area("User")]
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSenderContactUs _emailSender;
        private readonly RoleManager<IdentityRole> _roleManager;

        public HomeController(IUnitOfWork unitOfWork,
            UserManager<ApplicationUser> userManager,
            IEmailSenderContactUs emailSender,
            RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _emailSender = emailSender;
            _roleManager = roleManager;

        }

        public async Task<IActionResult> Index()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;

            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userid !=null)
            {
                var user = await _userManager.FindByIdAsync(userid.Value);
                var role = await _userManager.GetRolesAsync(user);
                if (role.Contains(SD.Role_Teacher))
                {
                    return RedirectToAction("Index", "Home", new { area = "Teacher" });
                }
                if (role.Contains(SD.Role_Student))
                {
                    return RedirectToAction("Index", "Home", new { area = "Student" });
                }
                if (role.Contains(SD.Role_Admin))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }
            }

            

            return View( );
        }

     
        

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> ContactUs()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier);
            if(userid == null) { return BadRequest(); }
            var user = await _userManager.FindByIdAsync(userid.Value);
            if (user == null) { return BadRequest(); }



            ContactUsVM model = new ContactUsVM
            {
                FullName= user.FullName,
                Email = user.Email
            };

            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = SD.Role_Student + "," + SD.Role_Admin + "," + SD.Role_Teacher)]
        public async Task<IActionResult> ContactUs(ContactUsVM model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "some thing wrong";
                return View(model);
            }
            await _emailSender.SendEmailContactUsAsync(model.Email,"problem  subject not added yet",$"${model.Message}");
            return RedirectToAction("Index","Home");
        }
        public IActionResult Features()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
