using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Models
{
    public class TestsViewModel
    {
        public TestsViewModel()
        {
            this.MultiQuestions = new List<TestQuestionsViewModel>();
        }
        public IList<TestQuestionsViewModel> MultiQuestions { get; set; }
    }
}
