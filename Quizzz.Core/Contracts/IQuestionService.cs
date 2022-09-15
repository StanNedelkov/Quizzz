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
        Task CreateQuestionAsync(QuestionViewModel model);
    }
}
