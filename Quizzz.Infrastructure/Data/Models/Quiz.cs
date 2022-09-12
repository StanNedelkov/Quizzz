using System.ComponentModel.DataAnnotations;

namespace Quizzz.Infrastructure.Data.Models
{
    public class Quiz
    {
        public Quiz()
        {
            this.Questions = new HashSet<Question>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Question> Questions { get; set; }

    }
}
