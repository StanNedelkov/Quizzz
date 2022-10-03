using Quizzz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Contracts
{
    public interface ITestService
    {
        Task<IEnumerable<QuizViewModel>> GetQuizesAsync();
        Task<IEnumerable<QuestionViewModel>> GetQuestionsForQuizAsync(int id);
        Task<IEnumerable<AnswerViewModel>> GetAnswersForQuestionAsync(int id);
        string Result(IEnumerable<QuestionViewModel> test);

    }
}
