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
                new Quiz() { Name = model.Name }
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
                Name = quizWithDetails.Name
            };
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
