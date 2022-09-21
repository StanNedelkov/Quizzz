using Microsoft.AspNetCore.Mvc;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Core.Services;
using Quizzz.Infrastructure.Data;
using Quizzz.Infrastructure.Data.Models;

namespace Quizzz.Controllers
{
    public class QuizesController : Controller
    {
       // private readonly ApplicationDbContext context;
        public IQuizService service;
        public IQuestionService questionService;

        public QuizesController(IQuizService _service, IQuestionService _questionService)
        {
           // this.context = _context;
            this.service = _service;
            this.questionService = _questionService;
        }

        // GET: Quizes
        public async Task<IActionResult> Index()
        {
            return View(await service.GetQuizesAsync());

            
        }

        // GET: Quizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await service.GetDetailsAsync(id ?? 0);
               
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        public async Task<IActionResult> QuestionsForQuiz(int? id)
        {
            return View(await service.GetQuestionsForQuizAsync(id ?? 0));

          
        }

        // GET: Quizes/Create
        public IActionResult Create()
        {
            
            return View();
        }

        // POST: Quizes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] QuizViewModel quiz)
        {
            if (!string.IsNullOrWhiteSpace(quiz.Name))
            {
                await service.CreateQuizAsync(quiz);
                return RedirectToAction(nameof(Create), "Questions");
            }
            
           
            return View(quiz);
        }

        // GET: Quizes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await service.GetDetailsAsync(id ?? 0);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quizes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] QuizViewModel quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            try
            {
                await service.EditQuizAsync(quiz);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
           
            return RedirectToAction(nameof(Index));

        }

        // GET: Quizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await service.GetDetailsAsync(id ?? 0);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await service.DeleteQuizAsync(id);

            }
            catch (ArgumentException )
            {

                return NotFound();
            }
            
            return RedirectToAction(nameof(Index));
        }

      
    }
}
