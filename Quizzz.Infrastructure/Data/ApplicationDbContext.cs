using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quizzz.Infrastructure.Data.Models;

namespace Quizzz.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(
                @"Server=MAINDESKTOP\\SQLEXPRESS;Database=QuizzzDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }
        public DbSet<Answer> Answers { get; set; } = null!;
        public DbSet<Question> Questions { get; set; } = null!;
        public DbSet<Quiz> Quizzes { get; set; } = null!;
    }
}