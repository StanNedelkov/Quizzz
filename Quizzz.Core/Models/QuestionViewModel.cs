using Quizzz.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Models
{
    public class QuestionViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2)]
        public string Content { get; set; } = null!;

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
        public string QuizName { get; set; }
        public string TimeCreated { get; set; }
    }
}
