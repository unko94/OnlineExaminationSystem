using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models.ViewModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;

namespace OnlineExaminationSystem.Areas.Teacher.Controllers
{
    [Area("Teacher")]
    [Authorize(Roles =SD.Role_Teacher)]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public async Task<IActionResult> Index()
        {
            
                var claimidentity = (ClaimsIdentity)User.Identity;
                var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;

                var user = await _userManager.FindByIdAsync(userid);
                var notifictions = _unitOfWork.notification.GetAll(n => n.ApplicationUserId == userid);

                var myexams = _unitOfWork.exam.GetAll(e => e.CreatedBy == user.UserName, include: "Category");
            
                int count = 0;

                var questionsCount = 0;
                 
                 
                double totalavg = 0;
                double scoreforallExams = 0;

                List<AvgsVM> Avgs = new List<AvgsVM>();
                foreach (var exam in myexams)
                {
                    //here should calculate a list of avges for each exam   to show them in the index view for teacher
                    var studentExam = _unitOfWork.studentExam.GetAll(se => se.ExamId == exam.Id);
                    var totalscore = _unitOfWork.examQuestion.GetAll(e => e.ExamId == exam.Id, include: "Question")
                        .Select(s => s.Question.Score).Sum();
                    scoreforallExams += totalscore;
                questionsCount = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == myexams[0].Id).Count;
                if (count != 0)
                    {
                        questionsCount += _unitOfWork.examQuestion.GetAll(e => e.ExamId == exam.Id).Count();

                    }
                    if (studentExam != null && studentExam.Any())
                    {
                        var avgScore = studentExam.Average(se => se.Score);
                        totalavg += avgScore;
                        avgScore = (avgScore / totalscore) * 100;
                        Avgs.Add(
                           new AvgsVM
                           {
                               ExamName = exam.ExamName,
                               category = exam.Category?.CategoryName ?? "N/A",
                               Avg = Math.Round(avgScore, 2)
                           });
                    }
                    count++;
                }
                var model = new TeacherHomeVM()
                {
                    Notifications = notifictions,
                    Avgs = Avgs,
                    ExamsNumber = myexams.Count(),
                    QuestionsNumber = questionsCount,
                    AvgForAllExams = Math.Round((totalavg / scoreforallExams) * 100, 2)
                };
                return View(model);
        }
        public IActionResult Delete(int examid)
        {
            var exam = _unitOfWork.exam.Get(e=>e.Id == examid);
            if (exam == null)
            {
                TempData["Error"] = "Exam not found";
                return RedirectToAction("Index");
            }
            _unitOfWork.exam.Delete(exam);
            return RedirectToAction();
        }
        #region
        public IActionResult getmyexams()
        {

            var claimidentity = (ClaimsIdentity)User.Identity;
            if (claimidentity == null)
            {
                return BadRequest("User not authenticated");
            }
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            

            var myexams = _unitOfWork.exam.GetAll(e=>e.CreatedBy == userid);
            return Json(myexams);
        }
        #endregion
    }
}
