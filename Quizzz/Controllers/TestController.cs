using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;

namespace Quizzz.Controllers
{
    public class TestController : Controller
    {
        private readonly IQuizService quizService;
        /*private readonly IQuestionService questionService;
        private readonly IAnswerService answerService;*/
        public TestController(IQuizService quizService)
        {
            this.quizService = quizService;
        }
        // GET: TestController
        public async Task<IActionResult> Index()
        {
            return View(await quizService.GetQuizesAsync());
        }

        [HttpGet]
        public async Task<IActionResult> Complete(int? id)
        {
           var questions = await quizService.GetQuestionsForTestAsync(id ?? 0);
            ViewData["questions"] = questions.MultiQuestions;
            return View(questions);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(TestsViewModel questions, string Command)
        {

            int correctAnswers = questions.MultiQuestions.Where(x => 
            (x.Answer01.IsSelected && x.Answer01.IsCorrect) || (x.Answer02.IsSelected && x.Answer02.IsCorrect) ||
            (x.Answer03.IsSelected && x.Answer03.IsCorrect) || (x.Answer04.IsSelected && x.Answer04.IsCorrect))
                .Count();
            int allAnswers = questions.MultiQuestions.Count();
            TempData["correctA"] = correctAnswers;
            TempData["allA"] = allAnswers;
           
            return RedirectToAction(nameof(TestResult));
        }

        public IActionResult TestResult()
        {
            var res = new TestResultViewModel()
            {
                CorrectAnswers = Convert.ToInt32(TempData["correctA"]),
                TotalAnswers = Convert.ToInt32(TempData["allA"]),
            };
            return View(res);
        }

    }
}

