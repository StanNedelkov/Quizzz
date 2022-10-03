namespace Quizzz.Core.Models
{
    public class MultiAnswersViewModel
    {
        public MultiAnswersViewModel()
        {
            this.Answers = new List<AnswerViewModel>();
        }
        public IList<AnswerViewModel> Answers { get; set; } = null!;
    }
}
