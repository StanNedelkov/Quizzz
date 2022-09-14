using Microsoft.AspNetCore.Mvc;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Core.Services;
using Quizzz.Infrastructure.Data;

namespace Quizzz.Controllers
{
    public class QuizesController : Controller
    {
       // private readonly ApplicationDbContext context;
        public IQuizService service;

        public QuizesController(IQuizService _service)
        {
           // this.context = _context;
            this.service = _service;
        }

        // GET: Quizes
        public async Task<IActionResult> Index()
        {
            return View(await service.GetQuizesAsync());

              /*return context.Quizzes != null ? 
                          View(await context.Quizzes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");*/
        }

        // GET: Quizes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await service.GetDetailsAsync(id ?? 0);
                
                /*context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);*/
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
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
            if (ModelState.IsValid)
            {
                await service.CreateQuizAsync(quiz);
                return RedirectToAction(nameof(Index));
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

            if (ModelState.IsValid)
            {
                try
                {
                    await service.EditQuizAsync(quiz);
                }
                catch(ArgumentException ae)
                {
                    return NotFound();
                }
               /* catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }*/
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
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
            catch (ArgumentException ae)
            {

                return NotFound();
            }
            
            return RedirectToAction(nameof(Index));
        }

      
    }
}
