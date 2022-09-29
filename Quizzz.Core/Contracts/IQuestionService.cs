using Quizzz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Contracts
{
    public interface IQuestionService
    {
        Task CreateQuestionAsync(QuestionViewModel model, QuizViewModel quizModel);

        Task<IEnumerable<QuestionViewModel>> GetQuestionsAsync();

        Task<IEnumerable<QuizViewModel>> GetAllQuizes();

        Task<QuestionViewModel> GetDetailsAsync(int id);
        Task<QuizViewModel> GetQuizForAnswerAsync(int id);

        Task EditQuizAsync(QuestionViewModel model);

        Task DeleteQuestionAsync(int id);

        Task<QuizViewModel> GetLastQuizAsync();

        Task<IEnumerable<AnswerViewModel>> GetAnswersForQuestionAsync(int id);
    }
}
