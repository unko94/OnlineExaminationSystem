using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Models.Model;
using Models.ViewModel;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;
using Utility;



namespace OnlineExaminationSystem.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," +SD.Role_Teacher)]
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuestionController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
         

            return View(); 
        }
         
        public IActionResult Create()
        {
            var question = new Question
            {
                Options = new List<string>()
            };
            CreateQuestionVM createQuestionVM = new()
            {
                Question = question,
                Categries = _unitOfWork.category.GetAll().Select(q => new SelectListItem
                {
                      Text = q.CategoryName,
                      Value =q.Id.ToString()
                })
            };
            return View(createQuestionVM) ;
        }
        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> PostCreate(Question question,IFormFile? file)
        {
            if (file != null)
            {
                string path = _webHostEnvironment.WebRootPath;
                Guid filename = Guid.NewGuid();
                string QuestionPath = filename.ToString() + ".jpg";
                string finalpath = Path.Combine(path, "Images/Question/");
                Console.WriteLine(finalpath);
                Console.WriteLine(finalpath + QuestionPath);

                if (!Directory.Exists(finalpath))
                {
                    Directory.CreateDirectory(finalpath);
                }

                using (var filestream = new FileStream(finalpath + QuestionPath, FileMode.Create))
                {
                    await file.CopyToAsync(filestream);
                }

                question.ImageUrl = finalpath + QuestionPath;
            }
           

            if (!ModelState.IsValid)
            {
                return View( new Question());
            }
            _unitOfWork.question.Create(question);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int? id)
        {
           
            if (id == null) 
            {
                return RedirectToAction(nameof(Index));
            }
            var question = _unitOfWork.question.Get(u => u.Id == id);
            return View(question);
        }
        [HttpPost]
        public IActionResult Edit(Question question)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.question.Update(question);
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
          
            return View(question);
        }
        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Console.WriteLine("Delete User Hitted");
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var question = _unitOfWork.question.Get(q=>q.Id == id);
            _unitOfWork.question.Delete(question);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> AssginToExam(List<int> ides,string examname,int category,int duration)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userid);
            var username = user.UserName;
            if (category == null)
            {
                
                return Json(new { success = false, Message = "Category  not found" });
            }
            Exam exam = new Exam()
            {
                ExamName = examname,
                CategoryId = category,
                Duration = duration,
                CreatedBy = username
            };

            if (!ModelState.IsValid) 
            {
             
                return Json(new { success = false ,message= "Faild to create exam" });
            }
             _unitOfWork.exam.Create(exam);
            _unitOfWork.Save();
            foreach (var id in ides)
            {
                ExamQuestion examquestion = new()
                {
                    ExamId = exam.Id,
                    QuestionId = id
                };
                _unitOfWork.examQuestion.Create(examquestion);
                _unitOfWork.Save();
            }

            return Json(new { success = true });
        }
        public async Task<IActionResult> EditAssignedQ(int examid,List<int> ides, string examname, int category, int duration)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userid);
            var username = user.UserName;
            Exam exam = new Exam()
            {
                Id = examid,
                ExamName = examname,
                CategoryId = category,
                Duration = duration,
                CreatedBy = username
            };
            _unitOfWork.exam.Update(exam);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> EditAssignedQPost(int examid, List<int> ides, string examname, int category, int duration)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userid);
            var examfromdb = _unitOfWork.exam.Get(e=>e.Id == examid);
            var examquestionsfromdb = _unitOfWork.examQuestion.GetAll(eq=>eq.ExamId == examid);

            if (examfromdb == null || examfromdb == null)
            {
                return NotFound();
            }
            examfromdb.ExamName = examname;
            examfromdb.CategoryId = category;
            examfromdb.Duration = duration;
            foreach (var oldQ in examquestionsfromdb)
            {
                _unitOfWork.examQuestion.Delete(oldQ);
            }

            // add new questions
            foreach (var qid in ides)
            {
                ExamQuestion newExamQ = new ExamQuestion
                {
                    ExamId = examid,
                    QuestionId = qid
                };
                _unitOfWork.examQuestion.Create(newExamQ);
            }

            return Json(new { success = true, message = "Exam updated successfully" });
        }

        #region
        public IActionResult GetAll(List<int>? SelectedQ)
        {
            var obj = _unitOfWork.question.GetAll(include:"Category");
            // var accessList = Request.TempData["SelectedQ"];
            var accessParameter = Request.Query["SelectedQ"].ToString();
            var IdsList = JsonConvert.DeserializeObject<List<int>>(accessParameter);
            QuestionsVM model = new()
            {
                Questions = obj,
                Selected = IdsList
                //  you here try to complete  edit exam by the approach  that chatgpt told you about
                            //  ,there is a text file in the desctop demonstrate it the file name is EditExam
            };
            return Json(model);
        }
        #endregion

    }
}
