using Microsoft.EntityFrameworkCore;
using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
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
        private readonly QuizzzRepository repo;
        public QuizService(QuizzzRepository _repo)
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

        public async Task<IEnumerable<QuizViewModel>> GetQuizesAsync()
        {
            return await repo.AllReadonly<Quiz>()
                .Select(x => new QuizViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                })
                .ToArrayAsync();
        }
    }
}
