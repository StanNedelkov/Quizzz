using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Quizzz.Infrastructure.Data.Models
{
    public class Question
    {
        public Question()
        {
            this.Answers = new HashSet<Answer>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(280)]

        public string Content { get; set; } = null!;
        [Required]
        public int QuizId { get; set; }
        [ForeignKey(nameof(QuizId))]
        public Quiz Quiz { get; set; } = null!;
        public virtual ICollection<Answer> Answers { get; set; }
    }
}
