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
    public class QuestionService : IQuestionService
    {
        private const string invalidIdMessage = "Invalid Question ID!";
        public IQuizzzRepository repo;
        public QuestionService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }
        public async Task CreateQuestionAsync(QuestionViewModel model, QuizViewModel quizModel)
        {
            await repo.AddAsync(new Question() 
            { 
                Content = model.Content, 
                QuizId = quizModel.Id, 
                TimeCreated = DateTime.Now.ToString("f")
            });
            await repo.SaveChangesAsync();
        }

        public async Task DeleteQuestionAsync(int id)
        {
            var question = await repo.GetByIdAsync<Question>(id);

            CheckIfQuestionIsNull(question);

            question.IsActive = false;
            await repo.SaveChangesAsync();
        }

        public async Task EditQuizAsync(QuestionViewModel model)
        {
            var question = await repo.GetByIdAsync<Question>(model.Id);

            CheckIfQuestionIsNull(question);

            question.Content = model.Content;
            question.Quiz = model.Quiz;
            question.QuizId = model.QuizId;

            await repo.SaveChangesAsync();
        }

        public async Task<QuestionViewModel> GetDetailsAsync(int id)
        {
            var question = await repo.GetByIdAsync<Question>(id);
            var quizForQuestion = await repo.GetByIdAsync<Quiz>(question.QuizId);

            CheckIfQuestionIsNull(question);

            return new QuestionViewModel()
            {
                Id = question.Id,
                Content = question.Content,
                Quiz = quizForQuestion,
                QuizId = question.QuizId
            };
           
        }

        public async Task<IEnumerable<QuestionViewModel>> GetQuestionsAsync()
        {
            return await repo.AllReadonly<Question>()
                 .Where(x => x.IsActive)
                 .Select(x => new QuestionViewModel() { Id = x.Id, Content = x.Content, QuizId = x.QuizId, Quiz = x.Quiz })
                 .ToArrayAsync();
        }

        private static void CheckIfQuestionIsNull(Question question)
        {
            if (question == null || !question.IsActive)
            {
                throw new ArgumentNullException(invalidIdMessage);
            }
        }

        public async Task<IEnumerable<QuizViewModel>> GetAllQuizes()
        {
            return await repo.AllReadonly<Quiz>()
                .Select(x => new QuizViewModel() { Id = x.Id, Name = x.Name })
                .ToArrayAsync();
        }

        public async Task<QuizViewModel> GetLastQuizAsync()
        {
            var lastQuiz = await repo.AllReadonly<Quiz>()
                .OrderBy(x => x.Id)
                .Select(x => new QuizViewModel() { Id = x.Id, Name = x.Name, Created = x.TimeCreated})
                .LastOrDefaultAsync();

            if (lastQuiz == null)
            {
                throw new ArgumentNullException();
            }
                return lastQuiz;
        }
    }
}
