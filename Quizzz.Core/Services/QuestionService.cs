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
        public IQuizzzRepository repo;
        public QuestionService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }
        public async Task CreateQuestionAsync(QuestionViewModel model)
        {
            await repo.AddAsync(new Question() 
            { 
                Content = model.Content, QuizId = model.QuizId
            });
            await repo.SaveChangesAsync();
        }

    }
}
