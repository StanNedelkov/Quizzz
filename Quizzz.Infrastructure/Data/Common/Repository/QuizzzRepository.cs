using Quizzz.Infrastructure.Data.Common.Contracts;

namespace Quizzz.Infrastructure.Data.Common.Repository
{
    public class QuizzzRepository : Repository, IQuizzzRepository
    {
        public QuizzzRepository(ApplicationDbContext context)
        {
            this.Context = context;
        }
    }
}
