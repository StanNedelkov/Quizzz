using Microsoft.EntityFrameworkCore;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Infrastructure.Data.Common.Contracts;
using Quizzz.Infrastructure.Data.Common.Repository;
using Quizzz.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Services
{
    public class QuizService : IQuizService
    {
        private const string invalidIdMessage = "Invalid Quiz ID!";

        public IQuizzzRepository repo;
        public QuizService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }

        public async Task CreateQuizAsync(QuizViewModel model)
        {
            await repo.AddAsync 
            (
                new Quiz() { Name = model.Name, TimeCreated = DateTime.Now.ToString("F") }
            );
            await repo.SaveChangesAsync();
            
        }

        public async Task DeleteQuizAsync(int id)
        {

            var quizToDelete = await repo.GetByIdAsync<Quiz>(id);
            if (quizToDelete == null)
            {
                throw new ArgumentException(invalidIdMessage);
            }
            if (quizToDelete.IsActive)
            {
                quizToDelete.IsActive = false;
                await repo.SaveChangesAsync();
            }
          
        }

        public async Task EditQuizAsync(QuizViewModel model)
        {
            var quiz = await repo.GetByIdAsync<Quiz>(model.Id);
            if (quiz == null)
            {
                throw new ArgumentException(invalidIdMessage);
            }

            quiz.Name = model.Name;
            await repo.SaveChangesAsync();
        }

        public async Task<QuizViewModel> GetDetailsAsync(int id)
        {
            var quizWithDetails = await repo.GetByIdAsync<Quiz>(id);

            if (quizWithDetails == null || !quizWithDetails.IsActive)
            {
                throw new ArgumentException(invalidIdMessage);
            }

            return new QuizViewModel()
            {
                Id = quizWithDetails.Id,
                Name = quizWithDetails.Name,
                Created = quizWithDetails.TimeCreated
                
            };
        }


        public async Task<IEnumerable<QuestionViewModel>> GetQuestionsForQuizAsync(int id)
        {
            var questions = await repo.AllReadonly<Question>()
                .Where(x => x.QuizId == id && x.IsActive)
                .Select(x => new QuestionViewModel()
                {
                    Id = x.Id, 
                    Content = x.Content, 
                    TimeCreated = x.TimeCreated, 
                    QuizId = x.QuizId,
                    Answers = x.Answers
                })
                .ToArrayAsync();

            if (questions == null)
            {
                throw new ArgumentNullException();
            }

            return questions;
           
        }

        public async Task<TestsViewModel> GetQuestionsForTestAsync(int id)
        {

          var questions = await repo.AllReadonly<Question>()
                .Where(x => x.QuizId == id && x.IsActive)
                .Select(x => new TestQuestionsViewModel()
                {
                    
                    Id = x.Id,
                    Content = x.Content,
                    TimeCreated = x.TimeCreated,
                    QuizId = x.QuizId
                   
                   
                })
                .ToArrayAsync();

            foreach (var question in questions)
            {
                
                var answers = await repo.AllReadonly<Answer>()
                    .Where(x => x.QuestionId == question.Id && x.IsActive)
                    .Select(x => new AnswerViewModel()
                    {
                        Id = x.Id,
                        IsActive = x.IsActive,
                        IsCorrect = x.IsCorrect,
                        QuestionId = x.QuestionId,
                        Question = x.Question,
                        Content = x.Content,
                        TimeCreated = x.TimeCreated
                    }).ToArrayAsync();
                        
                    question.Answer01 = answers[0];
                    question.Answer02 = answers[1];
                    question.Answer03 = answers[2];
                    question.Answer04 = answers[3];

                for (int i = 0; i < 4; i++)
                {
                    if (answers[i].IsCorrect)
                    {
                        question.CorrectAnswer = answers[i].Content;
                    }
                }
            }

            var col = new TestsViewModel();
            col.MultiQuestions = questions;

            return col;
        }

        public async Task<IEnumerable<QuizViewModel>> GetQuizesAsync()
        {
            return await repo.AllReadonly<Quiz>()
                .Where(x => x.IsActive)
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArrayAsync();
        }
    }
}
