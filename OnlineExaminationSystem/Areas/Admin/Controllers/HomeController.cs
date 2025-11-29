using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Models.Model;
using Models.ViewModel;
using OnlineExaminationSystem.Hubs;
using System.Security.Claims;
using Utility;

namespace OnlineExaminationSystem.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<notificationHub> _hubContext;
        public HomeController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,
            RoleManager<IdentityRole> roleManager,
              UserManager<ApplicationUser> userManager,
              IHubContext<notificationHub> hubContext)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _roleManager = roleManager;
            _userManager = userManager;
            _hubContext = hubContext;
        }
        public async Task<IActionResult> Index()
        {
            var exams = _unitOfWork.exam.GetAll();
            var examsInProgress = _unitOfWork.studentExam.GetAll(e=>e.Status == SD.Exam_InProgress);

            var Students =await _userManager.GetUsersInRoleAsync(SD.Role_Student);
            var Teachers =await _userManager.GetUsersInRoleAsync(SD.Role_Teacher);
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (userid == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userid.Value);
            if (user == null)
            {
                return BadRequest();
            }
            var model = new AdminDashboardVM()
            {
                TotalExams = (exams == null) ? 0 : exams.Count(),
                ExamsInProgress = (examsInProgress == null) ? 0 : examsInProgress.Count(),
                TotalStudents = (Students == null) ? 0 : Students.Count(),
                TotalTeachers = (Teachers == null) ? 0 : Teachers.Count(),
                UserName = user.FullName
            };
            return View(model);
        }
    }
}
