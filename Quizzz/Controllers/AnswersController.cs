using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;

namespace Quizzz.Controllers
{
    public class AnswersController : Controller
    {
        private readonly IAnswerService service;
        public AnswersController(IAnswerService _service)
        {
            this.service = _service;
        }
        // GET: AnswersController
        public async Task<ActionResult> Index()
        {
            return View(await service.GetAnswersAsync());
        }

        // GET: AnswersController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var answer = await service.GetDetailsAsync(id ?? 0);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }

        // GET: AnswersController/Create
        public IActionResult Create()
        {
            ViewData["QuestionId"] = new SelectList(service.GetAllQuestions(), "Id", "Content");
            
            return View();
        }

        // POST: AnswersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,QuestionId,IsCorrect")] AnswerViewModel answer)
        {
            await service.CreateAnswerAsync(answer);
            return RedirectToAction(nameof(Index));
        }

        // GET: AnswersController/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var answerToEdit = await service.GetDetailsAsync(id ?? 0);
            if (answerToEdit == null)
            {
                return NotFound();
            }

            ViewData["QuestionId"] = new SelectList(service.GetAllQuestions(), "Id", "Content");
            return View(answerToEdit);
        }

        // POST: AnswersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id,
            [Bind("Id, Content, Question, QuestionId, IsCorrect")] AnswerViewModel answer)
        {
            if (id != answer.Id)
            {
                return NotFound();
            }
            try
            {
               await service.EditAnswerAsync(answer);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: AnswersController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound(); 
            }
            var answerToDelete = await service.GetDetailsAsync(id ?? 0);
            if (answerToDelete == null)
            {
                return NotFound();
            }
            return View(answerToDelete);
        }

        // POST: AnswersController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await service.DeleteAnswerAsync(id);
            }
            catch(ArgumentNullException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
