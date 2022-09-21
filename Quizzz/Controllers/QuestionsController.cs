using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;

namespace Quizzz.Controllers
{
    public class QuestionsController : Controller
    {
        
        private readonly IQuestionService service;

        public QuestionsController(IQuestionService service)
        {
            this.service = service;
        }

        // GET: Questions
        public async Task<IActionResult> Index()
        {
            return View(await service.GetQuestionsAsync());
        }

        // GET: Questions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await service.GetDetailsAsync(id ?? 0);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // GET: Questions/Create
       
        public async Task<IActionResult> Create()
        {
            
           // ViewData["QuizId"] = new SelectList(service.GetAllQuizes(), "Id", "Name");
           var quiz = await service.GetLastQuizAsync();
            ViewData["LastQuiz"] = quiz.Name;
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,QuizId")] QuestionViewModel question)
        {
            if (string.IsNullOrWhiteSpace(question.Content))
            {
                return View();
            }
            try
            {
                var quiz = await service.GetLastQuizAsync();
                await service.CreateQuestionAsync(question, quiz);
                return RedirectToAction(nameof(Create), "Answers");
            }
            catch (ArgumentNullException)
            {

                return NotFound();
            }
                
             
              
            
           // ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name", question.QuizId);
           // return View(question);
        }

        // GET: Questions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await service.GetDetailsAsync(id ?? 0);
            if (question == null)
            {
                return NotFound();
            }
            ViewData["QuizId"] = new SelectList(await service.GetAllQuizes(), "Id", "Name", question.QuizId);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Content,QuizId")] QuestionViewModel question)
        {
            if (id != question.Id)
            {
                return NotFound();
            }
                try
                {
                    await service.EditQuizAsync(question);
                }
                catch (ArgumentException)
                {
                    return NotFound(); 
                }
                return RedirectToAction(nameof(Index));
            
            //ViewData["QuizId"] = new SelectList(_context.Quizzes, "Id", "Name", question.QuizId);
            //return View(question);
        }

        // GET: Questions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var question = await service.GetDetailsAsync(id ?? 0);
            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await service.DeleteQuestionAsync(id);
            }
            catch (ArgumentNullException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

       /* private bool QuestionExists(int id)
        {
          return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
        }*/
    }
}
