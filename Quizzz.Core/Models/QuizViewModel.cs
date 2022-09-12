using System.ComponentModel.DataAnnotations;

namespace Quizzz.Core.Models
{
    public class QuizViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; } = null!;
    }
}
