using Quizzz.Infrastructure.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizzz.Core.Models
{
    public class AnswerViewModel
    {
        
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public string TimeCreated { get; set; }
    }
}
