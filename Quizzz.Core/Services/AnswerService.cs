using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Infrastructure.Data.Common.Contracts;
using Quizzz.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Services
{
    public class AnswerService : IAnswerService
    {
        private readonly IQuizzzRepository repo;
        private const string invalidIdMessage = "Invalid Id. Answer not found.";
        public AnswerService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task CreateAnswerAsync(AnswerViewModel model, QuestionViewModel questionModel)
        {
            await repo.AddAsync(new Answer()
            {
                Content = model.Content,
                QuestionId = questionModel.Id,
                IsCorrect = model.IsCorrect,
                TimeCreated = DateTime.Now.ToString("F")
            }) ;
            await repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<QuestionViewModel>> GetAllQuestions()
        {
            return await repo.AllReadonly<Question>()
                .Select(x => new 
                QuestionViewModel() { Id = x.Id, Content = x.Content })
                .ToArrayAsync();
        }

        public async Task<IEnumerable<AnswerViewModel>> GetAnswersAsync()
        {
            return await repo.AllReadonly<Answer>()
                .Where(x => x.IsActive)
                .Select(x => new 
                AnswerViewModel() {Id = x.Id, Content = x.Content, Question = x.Question, QuestionId = x.QuestionId })
                .ToArrayAsync();
           
        }

        public async Task<AnswerViewModel> GetDetailsAsync(int id)
        {
            var answer = await repo.GetByIdAsync<Answer>(id);
            var questionForAnswer = await repo.GetByIdAsync<Question>(answer.QuestionId);
            CheckIfNullOrInactive(answer);

            return new AnswerViewModel()
            {
                Id = answer.Id,
                Content = answer.Content,
                Question = questionForAnswer,
                QuestionId = answer.QuestionId,
                IsCorrect = answer.IsCorrect,
                TimeCreated = answer.TimeCreated
            };
        }


        private static void CheckIfNullOrInactive(Answer answer)
        {
            if (answer == null || !answer.IsActive)
            {
                throw new ArgumentNullException(invalidIdMessage);
            }
        }



        public async Task DeleteAnswerAsync(int id)
        {
            var answerToDelete = await repo.GetByIdAsync<Answer>(id);

            CheckIfNullOrInactive(answerToDelete);

            answerToDelete.IsActive = false;
            await repo.SaveChangesAsync();
        }

        public async Task EditAnswerAsync(AnswerViewModel model)
        {
            var answerToEdit = await repo.GetByIdAsync<Answer>(model.Id);
            CheckIfNullOrInactive(answerToEdit);

            answerToEdit.Content = model.Content;
            answerToEdit.Question = model.Question;
            answerToEdit.QuestionId = model.QuestionId;
            answerToEdit.IsCorrect = model.IsCorrect;

            await repo.SaveChangesAsync();
        }

        public async Task<QuestionViewModel> GetLastQuestion()
        {
           var lastQuestion = await repo.AllReadonly<Question>()
                .Select(x => new QuestionViewModel() { Id = x.Id, Content = x.Content, QuizId = x.QuizId, TimeCreated = x.TimeCreated})
                .OrderBy(x => x.Id)
                .LastOrDefaultAsync();

            if (lastQuestion == null)
            {
                throw new ArgumentNullException();
            }
            return lastQuestion;
                
        }
    }
}
