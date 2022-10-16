using Quizzz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Contracts
{
    public interface IQuizService
    {
        Task<IEnumerable<QuizViewModel>> GetQuizesAsync();
        Task<IEnumerable<QuestionViewModel>> GetQuestionsForQuizAsync(int id);

        Task<TestsViewModel> GetQuestionsForTestAsync(int id);

        Task CreateQuizAsync(QuizViewModel model);

        Task<QuizViewModel> GetDetailsAsync(int id);

        Task EditQuizAsync(QuizViewModel model);

        Task DeleteQuizAsync(int id);
    }
}
