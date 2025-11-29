using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Models.Model;
using Models.ViewModel;
using Newtonsoft.Json;
using OnlineExaminationSystem.Hubs;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;



namespace OnlineExaminationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Teacher)]
    public class ExamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHubContext<notificationHub> _hubContext;
        public ExamController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,
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
         
            return View();
        }
        public IActionResult Create()
        {
            IEnumerable<SelectListItem> categoeryList = _unitOfWork.category.GetAll().Select(c => new SelectListItem
            {
                Text = c.CategoryName,
                Value = c.Id.ToString()
            });
            CreateExamVM createExamVM = new()
            {
                Categories = categoeryList
            };

            return View(createExamVM);
        }
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> CreatePost(CreateExamVM createExamVM)
        {
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            Console.WriteLine("Create Method Hitted");
            if (ModelState.IsValid) 
            {
                _unitOfWork.exam.Create(createExamVM.Exam);
                _unitOfWork.Save();
                assignQuestionsToExam(createExamVM.QuetionsNumbers, createExamVM.Exam.Id,createExamVM.Difficulty
                    ,createExamVM.Exam.CategoryId);
                return RedirectToAction(nameof(Index));
            }
            createExamVM = new()
            {
                
                Categories = _unitOfWork.category.GetAll().Select(c => new SelectListItem
                {
                    Text=c.CategoryName,
                    Value = c.Id.ToString()
                })
            };
            var notification = new Notification()
            {
                CreatedAt = DateTime.UtcNow,
                Message = $"Exam {createExamVM.Exam.ExamName} created successfully",
                ApplicationUserId = userid,
                IsRead = false
            };
            _unitOfWork.notification.Create(notification);
             await _hubContext.Clients.User(userid).SendAsync("ReceiveNotification", $"Exam '{createExamVM.Exam.ExamName}' created successfully.");
             
            return View(createExamVM);
        }
        public IActionResult Edit(int? id)
        {
             
                var exam = _unitOfWork.exam.Get(e => e.Id == id);


                 if (exam == null)
                 {
                return BadRequest();
                 }
                var examQuestionsids = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id).Select(eq => eq.QuestionId).ToList();
                var Questions = _unitOfWork.question.GetAll(include: "Category");
                Dictionary<Question, bool> QSelected = new Dictionary<Question, bool>();
                foreach (var Q in Questions)
                {
                    if (!QSelected.ContainsKey(Q))
                    {
                        QSelected[Q] = false;
                    }
                    QSelected[Q] = examQuestionsids.Contains(Q.Id);


                }
                EditExamVM model = new EditExamVM()
                {
                    ExamId = exam.Id,
                    ExamName = exam.ExamName,
                    Duration = exam.Duration,
                    SelectedQ = examQuestionsids,
                    Questions = QSelected

                };
                return View(model);
          
      
            
        }
        [HttpPost]
        public IActionResult Edit(EditExamVM model)
        {
            if (ModelState.IsValid)
            {
                var exam = _unitOfWork.exam.Get(e => e.Id == model.ExamId);

                exam.ExamName = model.ExamName;
                exam.Duration = model.Duration;
            
                var examquestions = _unitOfWork.examQuestion.GetAll(e => e.ExamId == model.ExamId).Select(e=>e.QuestionId).ToList();
                foreach (var id in model.SelectedQ)
                {
                    if (!examquestions.Contains(id))
                    {
                        examquestions.Add(id);
                        var examQ = new ExamQuestion()
                        {
                            ExamId = model.ExamId,
                            QuestionId = id
                        };
                        _unitOfWork.examQuestion.Create(examQ);
                        _unitOfWork.Save();
                    }
                }
                
                return RedirectToAction(nameof(Index));

            }
       

            return BadRequest();

        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            var exam = _unitOfWork.exam.Get(e=>e.Id == id);
            _unitOfWork.exam.Delete(exam);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        private void assignQuestionsToExam(int questionsNumbers, int examId,string Diffeculty,int CategoryId)
        {

            //Random rand = new Random();
            //int x = rand.Next(1, questionsNumbers);
            int x = questionsNumbers;
            List<Question> question = _unitOfWork.question.GetAll(q=>q.Difficulty == Diffeculty && q.CategoryId == CategoryId);
           
            if (questionsNumbers >question.Count)
            {
                // x = rand.Next(1, question.Count);
                x = question.Count;
            }
            for (int i =0;i<x;i++)
            {
                ExamQuestion examQuestion = new()
                {
                    ExamId = examId,
                    QuestionId = question[i].Id
                };
                _unitOfWork.examQuestion.Create(examQuestion);
                _unitOfWork.Save();
            }

             
        }
        
        #region
        public async Task<IActionResult> GetAllAsync()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userid);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains(SD.Role_Teacher))
            {
                var exams = _unitOfWork.exam.GetAll(e=>e.CreatedBy == user.UserName,include: "ExamQuestions,Category,ExamQuestions.Question").Select(e => new ExamVM
                {
                    ExamId = e.Id,
                    ExamName = e.ExamName,
                    ExamScore = e.ExamQuestions.Sum(eq => eq.Question.Score),
                    TotalQuestions = e.ExamQuestions.Count(),
                    CategoryId = e.CategoryId,
                    Category = e.Category.CategoryName,
                    CreatedBy = e.CreatedBy,
                    CreatedAt = e.CreatedAt

                });


                return Json(exams);
            }
            else
            {
                var exams = _unitOfWork.exam.GetAll(include: "ExamQuestions,Category,ExamQuestions.Question").Select(e => new ExamVM
                {
                    ExamId = e.Id,
                    ExamName = e.ExamName,
                    ExamScore = e.ExamQuestions.Sum(eq => eq.Question.Score),
                    TotalQuestions = e.ExamQuestions.Count(),
                    CategoryId = e.CategoryId,
                    Category = e.Category.CategoryName,
                    CreatedBy = e.CreatedBy,
                    CreatedAt = e.CreatedAt
             
                });
                return Json(exams);
            }
              
        }
        #endregion

    }
}
