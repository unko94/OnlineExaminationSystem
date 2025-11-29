using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Model;
using Models.ViewModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Utility;
namespace OnlineExaminationSystem.Areas.Student.Controllers
{
    [Area("Student")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager)
        {

            _unitOfWork = unitOfWork;
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var avgscore = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid).Select(se => se.Score).Sum() / _unitOfWork.studentExam.GetAll(se=>se.ApplicationUserId == userid).Count();
           
            var completed = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid && se.Status == SD.Exam_Completed).Count();
            var newcategrey = _unitOfWork.category.GetAll()
                .OrderByDescending(c=>c.Id).Select(c=>c.CategoryName).FirstOrDefault();
            var newexam = _unitOfWork.exam.GetAll().OrderByDescending(e => e.Id).Select(e => e.ExamName).FirstOrDefault();
            var availableexams = _unitOfWork.exam.GetAll().Count();

            StudentHomeVM model = new()
            {
                AvgScore = avgscore,
                Completed = completed,
                NewCategory = newcategrey,
                NewExam = newexam,
                Available = availableexams,
                
            };
            return View(model);
        }
        public IActionResult Start(int id)
        {

            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studentexam = _unitOfWork.studentExam.Get(se => se.ExamId == id && se.ApplicationUserId == userid);
            if (studentexam != null)
            {
                return RedirectToAction("Index", "TakeExam", new { id = studentexam.Id });

            }
            else
            {
                studentexam = new()
                {
                    ApplicationUserId = userid,
                    ExamId = _unitOfWork.exam.Get(e => e.Id == id).Id,
                    Score = 0,
                    Status = SD.Exam_InProgress
                };

            }



            _unitOfWork.studentExam.Create(studentexam);
            _unitOfWork.Save();
            var studentexamid = _unitOfWork.studentExam.Get(se => se.ExamId == id && se.ApplicationUserId == userid).Id;

            return RedirectToAction("Index", "TakeExam", new { id = studentexamid });
        }
        [Authorize]
        public IActionResult AddToStExam(int id)
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studentexam = _unitOfWork.studentExam.Get(se => se.ExamId == id && se.ApplicationUserId == userid);
            if (studentexam != null)
            {
                return RedirectToAction("Index", "TakeExam", new { id = studentexam.Id });
            }
            else
            {
                studentexam = new()
                {
                    ApplicationUserId = userid,
                    ExamId = _unitOfWork.exam.Get(e => e.Id == id).Id,
                    Score = 0,
                    //StartTime = DateTime.Now,
                    //EndTime = DateTime.Now.AddMinutes(5),
                    Status = SD.Exam_InProgress
                };

            }
            _unitOfWork.studentExam.Create(studentexam);
            _unitOfWork.Save();
            var studentexamid = _unitOfWork.studentExam.Get(se => se.ExamId == id && se.ApplicationUserId == userid).Id;

            return RedirectToAction("Index", "TakeExam", new { id = studentexamid });
        }
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var model = await _userManager.FindByIdAsync(userid);

            var myexams = _unitOfWork.exam.GetAll(e=>e.CreatedBy == model.UserName);

            var totaluploadedQ = _unitOfWork.question.GetAll(q=>q.Createdby ==model.UserName );
            
            ProfileVM profileVM = new ProfileVM()
            {
                FullName = model.FullName,
                Email = model.Email,
                Gender = model.Gender,
                UserName = model.UserName,
                Id = model.Id,
                AvgScore = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid).Select(se => se.Score).Sum(),
                TotalExamsTaken = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid).Count(),
                ExamsPassed = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid && se.Result == SD.ExamResult_Passed).Count(),
                ExamsFailed = _unitOfWork.studentExam.GetAll(se => se.ApplicationUserId == userid && se.Result == SD.ExamResult_Failed).Count(),
                Phone = model.PhoneNumber,
                TotalCreatedExams = (myexams == null) ? 0 : myexams.Count(),
                TotalQuestionsUploaded = (totaluploadedQ == null ) ? 0 :totaluploadedQ.Count()
            };
            return View(profileVM);
        }
        public async Task<IActionResult> EditProfile()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userid);
            var jsonstring = System.IO.File.ReadAllText("C:\\Users\\HP\\Desktop\\Online Examination System\\OnlineExaminationSystem\\OnlineExaminationSystem\\wwwroot\\PhoneExtentions.json");
            //var json = JsonSerializer.Deserialize<List<Extention>>();
            var exentions = JsonSerializer.Deserialize<List<Extention>>(jsonstring);
            string[] code = {"+962"," 7XXXXXXXX" };
            if (user.PhoneNumber !=null)
            {
                code = user.PhoneNumber.Split(" ");
            } 
           // var code = (user.PhoneNumber == null) ? new string[0] : user.PhoneNumber.Split(" ");
            var Extentions = exentions.Select(e => new SelectListItem
            {
                Text = $"{e.name} ({e.dial_code})",
                Value = e.dial_code
            }).ToList();
            EditProfileVM model = new()
            {
                FullName =  user.FullName,
                UserName = user.UserName,
                Email = user.Email ,
                Gender = (user.Gender == null) ? SD.Gender_Male: user.Gender,
                Phone = code[1],
                Extentions = Extentions,
                Extention = code[0]
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(EditProfileVM model)
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Edit Profile Failed";
                return RedirectToAction("EditProfile");
            }
            var applicationUser = await _userManager.FindByIdAsync(userid);
            applicationUser.FullName = model.FullName;
            applicationUser.UserName = model.UserName;
            applicationUser.PhoneNumber = model.Extention + " "+ model.Phone ;
            applicationUser.Gender = model.Gender;
            applicationUser.Email = model.Email;
            
                 
            
            await _userManager.UpdateAsync(applicationUser);

            return RedirectToAction("Profile");
        }
        #region
        public IActionResult getall()
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studentexam = _unitOfWork.studentExam.GetAll(se=> se.ApplicationUserId == userid,include:"Exam");
            return Json(studentexam);
        }
        public IActionResult getallExams()
        {
            var exams = _unitOfWork.exam.GetAll(include:"Category");
            return Json(exams);
        }
        #endregion
    }
}
