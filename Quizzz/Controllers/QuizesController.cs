using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Infrastructure.Data;
using Quizzz.Infrastructure.Data.Models;

namespace Quizzz.Controllers
{
    public class QuizesController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IQuizService service;

        public QuizesController(ApplicationDbContext _context, IQuizService _service)
        {
            this.context = _context;
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
            if (id == null || context.Quizzes == null)
            {
                return NotFound();
            }

            var quiz = await context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (id == null || context.Quizzes == null)
            {
                return NotFound();
            }

            var quiz = await context.Quizzes.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Quiz quiz)
        {
            if (id != quiz.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(quiz);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quizes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || context.Quizzes == null)
            {
                return NotFound();
            }

            var quiz = await context.Quizzes
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (context.Quizzes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Quizzes'  is null.");
            }
            var quiz = await context.Quizzes.FindAsync(id);
            if (quiz != null)
            {
                context.Quizzes.Remove(quiz);
            }
            
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuizExists(int id)
        {
          return (context.Quizzes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
