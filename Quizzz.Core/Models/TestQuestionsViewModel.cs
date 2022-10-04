using Quizzz.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Quizzz.Core.Models
{
    public class TestQuestionsViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2)]
        public string Content { get; set; } = null!;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; } = null!;
        public string QuizName { get; set; } = null!;
        public string TimeCreated { get; set; } = null!;
        public string CorrectAnswer { get; set; } = null!;
        public AnswerViewModel Answer01 { get; set; } = null!;
        public AnswerViewModel Answer02 { get; set; } = null!;
        public AnswerViewModel Answer03 { get; set; } = null!;
        public AnswerViewModel Answer04 { get; set; } = null!;

    }
}
