namespace Quizzz.Core.Models
{
    public class TestQuestionsViewModel
    {
        public TestQuestionsViewModel()
        {
            this.Questions = new List<QuestionViewModel>();
        }
        public IList<QuestionViewModel> Questions { get; set; }
    }
}
