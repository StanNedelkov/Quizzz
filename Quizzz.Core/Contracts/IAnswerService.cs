using Quizzz.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Contracts
{
    public interface IAnswerService
    {
        Task<IEnumerable<AnswerViewModel>> GetAnswersAsync();

        Task CreateAnswerAsync(AnswerViewModel model);

        IEnumerable<QuestionViewModel> GetAllQuestions();

        Task<AnswerViewModel> GetDetailsAsync(int id);

        Task DeleteAnswerAsync(int id);

        Task EditAnswerAsync(AnswerViewModel model);
    }
}
