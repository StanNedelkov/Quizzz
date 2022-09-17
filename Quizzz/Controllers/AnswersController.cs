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
        public async Task<ActionResult> Details(int id)
        {
            return View();
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
        public async Task<ActionResult> Edit(int id)
        {
            return View();
        }

        // POST: AnswersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, IFormCollection collection)
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

        // GET: AnswersController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View();
        }

        // POST: AnswersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
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
