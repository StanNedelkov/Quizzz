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
       
        public IActionResult Create()
        {
            
            ViewData["QuizId"] = new SelectList(service.GetAllQuizes(), "Id", "Name");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,QuizId")] QuestionViewModel question)
        {
           
                await service.CreateQuestionAsync(question);
                return RedirectToAction(nameof(Index));
            
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
            ViewData["QuizId"] = new SelectList(service.GetAllQuizes(), "Id", "Name", question.QuizId);
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
            catch (ArgumentNullException ae)
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
