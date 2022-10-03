using Quizzz.Core.Contracts;
using Quizzz.Core.Models;
using Quizzz.Infrastructure.Data.Common.Contracts;
using Quizzz.Infrastructure.Data.Common.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Services
{
    public class TestService : ITestService
    {
        private IQuizzzRepository repo;

        public TestService(IQuizzzRepository _repo)
        {
            this.repo = _repo;
        }

        public Task<IEnumerable<AnswerViewModel>> GetAnswersForQuestionAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuestionViewModel>> GetQuestionsForQuizAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<QuizViewModel>> GetQuizesAsync()
        {
            throw new NotImplementedException();
        }

        public string Result(IEnumerable<QuestionViewModel> test)
        {
            throw new NotImplementedException();
        }
    }
}
