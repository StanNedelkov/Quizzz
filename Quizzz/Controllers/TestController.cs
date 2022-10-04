using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quizzz.Core.Contracts;

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
        public async Task<ActionResult> Index()
        {
            return View(await quizService.GetQuizesAsync());
        }


        public async Task<ActionResult> Complete(int? id)
        {
            return View(await quizService.GetQuestionsForTestAsync(id ?? 0));
        }
        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TestController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
