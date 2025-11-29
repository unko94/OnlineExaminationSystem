using DataAccess.Repositery.IRepositery;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Build.Framework;
using Models.Model;
using Models.ViewModel;
using OnlineExaminationSystem.Hubs;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utility;
using Microsoft.AspNetCore.Authorization;
using DataAccess.Migrations;


namespace OnlineExaminationSystem.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = SD.Role_Student)]
    public class TakeExamController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<notificationHub> _hubContext;
        private readonly UserManager<ApplicationUser> _userManager;
 
        public TakeExamController(IUnitOfWork unitOfWork, IHubContext<notificationHub> hubContext,
            UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
            _userManager = userManager;
        
        }
        public IActionResult Index(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            HttpContext.Session.SetInt32("CurrentIndex", 0);
            var idenetityclaim = (ClaimsIdentity)User.Identity;
            var userid = idenetityclaim.FindFirst(ClaimTypes.NameIdentifier).Value;
            var studentexam = _unitOfWork.studentExam.Get(se => se.ApplicationUserId == userid && se.ExamId == id);
            var exam = _unitOfWork.exam.Get(e=>e.Id == id);
            if (exam == null)
            {
                return BadRequest();
            }

            if (studentexam == null) 
            {
                //get it from db
                var Rand = new Random();
                int randID = Rand.Next(0, 1000);

                StudentExam studentExam = new StudentExam()
                {
                    ApplicationUserId = userid,
                    ExamId = id,
                    Score = 0,
                    Status = SD.Exam_InProgress,
                    Result = SD.ExamResult_Failed,
                    RandID = randID,
                };
                _unitOfWork.studentExam.Create(studentExam);
                _unitOfWork.Save();

                studentexam = _unitOfWork.studentExam.Get(se => se.ApplicationUserId == userid && se.ExamId == id);

            }
            
                

            var questions = _unitOfWork.examQuestion
                  .GetAll(eq => eq.ExamId == id, include: "Question")
                  .Select(q => q.Question)
                  .ToList();
         
            Console.WriteLine(questions.Count);


            var answers = _unitOfWork.studentAnswer
    .GetAll(sa => sa.StudentExamId == studentexam.Id)
    .ToDictionary(sa => sa.QuestionId, sa => sa.SelectedOption);


            var randid = _unitOfWork.studentExam.Get(se=>se.ExamId == id && se.ApplicationUserId == userid);
            if (randid == null)
            {
                return BadRequest();
            }
            if (randid.RandID == null)
            {
                return BadRequest();
            }
            TakeExamVM takeExamVM = new()
                {
                    StudentExamId = studentexam.Id,
                    ExamId =  id,
                    Questions = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id , include: "Question")
                   .Select(q => q.Question).ToList(),
                    ExamName = _unitOfWork.exam.Get(e => e.Id == id).ExamName,
                    StartTime = 10,
                    EndTime = 20,
                    QuestionsCount = _unitOfWork.exam.GetAll(e => e.Id == id, include: "ExamQuestions").Count(),
                    CurrentIndex = 0,
                    Answers = answers,
                    AttempID = randid.RandID,
                    Duration = exam.Duration
                };

                return View(takeExamVM);
           
        }
        
        [HttpPost]
        public IActionResult NxtBtn(int id, string selectedoption ,double score )
        {
            var identityclaim = (ClaimsIdentity)User.Identity;
            var userid = identityclaim.FindFirst(ClaimTypes.NameIdentifier).Value;
            int studentexamid = _unitOfWork.studentExam.Get(e => e.ExamId == id && e.ApplicationUserId == userid).Id;
            int currentindex = HttpContext.Session.GetInt32("CurrentIndex") ?? 0;
            int questionid = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id, include: "Question")
               .Select(q => q.Question).ToList()[currentindex].Id;
            currentindex++;
            HttpContext.Session.SetInt32("CurrentIndex",currentindex);

            var answers = _unitOfWork.studentAnswer
    .GetAll(sa => sa.StudentExamId == studentexamid)
    .ToDictionary(sa => sa.QuestionId, sa => sa.SelectedOption);
            var count = _unitOfWork.examQuestion.GetAll(eq=>eq.ExamId == id).Count();

            SubmitQuestion(selectedoption,_unitOfWork.studentExam.Get(es=>es.ExamId == id && es.ApplicationUserId == userid).Id, questionid);
            var studentexam = _unitOfWork.studentExam.Get(se=>se.ExamId == id && se.ApplicationUserId == userid);
            var exam = _unitOfWork.exam.Get(e=>e.Id == id);
            if (studentexam == null || exam == null)
            {
                return BadRequest();
            }
            if (studentexam.RandID == null)
            {
                return BadRequest();
            }
            TakeExamVM takeExamVM = new()
            {
                StudentExamId = studentexamid,
                ExamId = id,
                Questions = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id, include: "Question")
               .Select(q => q.Question).ToList(),
                ExamName = _unitOfWork.exam.Get(e => e.Id == id).ExamName,
                StartTime = 10,
                EndTime = 20,
                QuestionsCount = _unitOfWork.exam.GetAll(e => e.Id == id, include: "ExamQuestions").Count(),
                CurrentIndex = currentindex,
                Answers = answers,
                AttempID = studentexam.RandID,
                Duration = exam.Duration
            };
            return View("Index", takeExamVM);
        }
        [HttpPost]
        public IActionResult PrevBtn(int id)
        {
            var identityclaim = (ClaimsIdentity)User.Identity;
            var userid = identityclaim.FindFirst(ClaimTypes.NameIdentifier).Value;

            int currindex = HttpContext.Session.GetInt32("CurrentIndex") ?? 0;
            currindex--;
            int questionid = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id, include: "Question")
            .Select(q => q.Question).ToList()[currindex].Id;
            HttpContext.Session.SetInt32("CurrentIndex" , currindex);
            var studentexamid = _unitOfWork.studentExam.Get(e => e.ExamId == id && e.ApplicationUserId == userid).Id;

            var studentanswer = _unitOfWork.studentAnswer.Get(sa=>sa.StudentExamId == studentexamid);
            var answers = _unitOfWork.studentAnswer
    .GetAll(sa => sa.StudentExamId == studentexamid)
    .ToDictionary(sa => sa.QuestionId, sa => sa.SelectedOption);
            ////
            var studentexam = _unitOfWork.studentExam.Get(se => se.ExamId == id && se.ApplicationUserId == userid);
            var exam = _unitOfWork.exam.Get(e => e.Id == id);
            if (studentexam == null || exam == null)
            {
                return BadRequest();
            }
            if (studentexam.RandID == null)
            {
                return BadRequest();
            }
            ////
            TakeExamVM takeExamvm = new()
            {
                StudentExamId = studentexamid,
                ExamId = id,
                Questions = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == id, include: "Question")
               .Select(q => q.Question).ToList(),
                ExamName = _unitOfWork.exam.Get(e => e.Id == id).ExamName,
                StartTime = 10,
                EndTime = 20,
                QuestionsCount = _unitOfWork.exam.GetAll(e => e.Id == id, include: "ExamQuestions").Count(),
                CurrentIndex = currindex,
                Answers = answers,
                AttempID = studentexam.RandID,
                Duration = exam.Duration
                
            };
            return View("Index", takeExamvm);
        }


        public async Task<IActionResult> SubmitExam(int id)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var userid = claimIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (id == null || userid == null)
            {
                return BadRequest();
            }
            var model = _unitOfWork.studentExam.Get(se => se.Id == id);

            var exam = _unitOfWork.exam.Get(e=>e.Id == model.ExamId);
            var user = await _userManager.FindByIdAsync(userid);
            var answers = _unitOfWork.studentAnswer.GetAll(sa => sa.StudentExamId == id, include: "Question");
            var unansweredQ = answers.Select(a=>a.SelectedOption == null);
            var answeredQ = answers.Select(a => a.SelectedOption == null);
            if (exam == null || model == null || user == null || answers == null)
            {
                return BadRequest();
            }

           
           
            if (model == null || exam == null)
            {
                return BadRequest();
            }
            if (model.RandID == null)
            {
                return BadRequest();
            }


            SubmitExamVM submitExam = new()
            {
                ExamId = exam.Id,
                studentAnswers = answers,
                studentExam = model,
                ExamName = exam.ExamName,
                StudentName = user.UserName,
                TotalQuestions = answers.Count(),
                UnAnsweredQ = unansweredQ.Count(),
                AnsweredQ = answeredQ.Count(),
                AttempID = model.RandID,
                Duration = exam.Duration
                
            };
          

            return View(submitExam);
        }
        [HttpPost]
        public async Task<IActionResult> SubmitExamPost(int? ExamId)
        {
            if (ExamId == null)
            {
                return BadRequest();
            }
            var claimidentity = (ClaimsIdentity)User.Identity;
            var userid = claimidentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userid == null)
            {
                return BadRequest();
            }
            var studentExamFromDb = _unitOfWork.studentExam.Get(se => se.ExamId ==  ExamId 
            && se.ApplicationUserId == userid
            ,include:"Exam");
            if (studentExamFromDb ==null)
            {
                return BadRequest();
            }

            var teacherName = studentExamFromDb.Exam.CreatedBy;
            var teacher = await _userManager.FindByNameAsync(teacherName);

            var questionsscore = _unitOfWork.studentAnswer.GetAll(s => s.StudentExamId == studentExamFromDb.Id
            , include: "Question");
            
            double score = 0;
            double total = 0;
            foreach (var s in questionsscore)
            {
                if (s.IsCorrect)
                {
                    score += s.Question.Score;
                }
                total += s.Question.Score;
            }

            studentExamFromDb.Status = SD.Exam_Completed;
            if (score < total/2)
            {
                studentExamFromDb.Result = SD.ExamResult_Failed;
            }
            else
            {
                studentExamFromDb.Result = SD.ExamResult_Passed;
            }
            studentExamFromDb.ApplicationUserId = userid;
            studentExamFromDb.Score = score;
            studentExamFromDb.RandID = -1;
            _unitOfWork.Save();

            if (teacher != null)
            {
                var msg = $"Student {User.Identity.Name} submitted exam '{studentExamFromDb.Exam.ExamName}'";
                var notifymodel = new Notification()
                {
                    ApplicationUserId = userid,
                    Message = msg,

                    IsRead =false
                };
              
                await _hubContext.Clients.User(teacher.Id).SendAsync("ReceiveNotification",msg );
                _unitOfWork.notification.Create(notifymodel);
                _unitOfWork.Save();
            }
            _unitOfWork.studentAnswer.DeleteAll(questionsscore);
            _unitOfWork.Save();

            // return RedirectToAction("Index", "Home");
            return Json(new { redirectUrl = Url.Action("Index", "Home") });

        }
        [HttpPost]
        public IActionResult NavigateToQ(int studentexamid,int currquestion)
        {
            HttpContext.Session.SetInt32("CurrentIndex", currquestion);
            int examid = _unitOfWork.studentExam.Get(se => se.Id == studentexamid).ExamId;
            int questionid = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == examid, include: "Question")
           .Select(q => q.Question).ToList()[currquestion].Id;

            var answers = _unitOfWork.studentAnswer
               .GetAll(sa => sa.StudentExamId == studentexamid)
               .ToDictionary(sa => sa.QuestionId, sa => sa.SelectedOption);


            ////
            var studentexam = _unitOfWork.studentExam.Get(se =>se.Id == studentexamid);
            var exam = _unitOfWork.exam.Get(e => e.Id == examid);
            if (studentexam == null || exam == null)
            {
                return BadRequest();
            }
            if (studentexam.RandID == null)
            {
                return BadRequest();
            }
            ////
            ///
            TakeExamVM takeExamvm = new()
            {
                StudentExamId = studentexamid,
                ExamId = examid,
                Questions = _unitOfWork.examQuestion.GetAll(eq => eq.ExamId == examid, include: "Question")
              .Select(q => q.Question).ToList(),
                ExamName = _unitOfWork.exam.Get(e => e.Id == examid).ExamName,
                StartTime = 10,
                EndTime = 20,
                QuestionsCount = _unitOfWork.exam.GetAll(e => e.Id == examid, include: "ExamQuestions").Count(),
                CurrentIndex = currquestion,
                 Answers = answers,
                 AttempID = studentexam.RandID,
                 Duration = exam.Duration
            };

            return View("Index", takeExamvm);

        }

        private void SubmitQuestion(string selectedoption,int studentexamid, int questionid)
        {
           
            string CorrAns = _unitOfWork.question.Get(q => q.Id == questionid).CorrectAnswer;
            var studentans = _unitOfWork.studentAnswer.Get(sa=>sa.QuestionId == questionid &&
            sa.StudentExamId == studentexamid);
            if (studentans != null)
            {
               studentans.SelectedOption = selectedoption == null ? "NotSolved" : selectedoption;
               studentans.IsCorrect = selectedoption == CorrAns ? true : false;
                _unitOfWork.studentAnswer.Update(studentans);
                _unitOfWork.Save();

            }
            else
            {
                StudentAnswer studentAnswer = new()
                {

                    StudentExamId = studentexamid,
                    SelectedOption = selectedoption == null ? "NotSolved" : selectedoption,
                    QuestionId = questionid,
                    IsCorrect = selectedoption == CorrAns ? true : false
                };
                _unitOfWork.studentAnswer.Create(studentAnswer);
                _unitOfWork.Save();
            }
        }
    }
}
