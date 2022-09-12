using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzz.Infrastructure.Data.Models
{
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(400)]
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }

        public int QuestionId { get; set; }
        [ForeignKey(nameof(QuestionId))]
        public virtual Question Question { get; set; } = null!;

    }
}
