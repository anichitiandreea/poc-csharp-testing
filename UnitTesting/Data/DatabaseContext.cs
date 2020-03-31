using Microsoft.EntityFrameworkCore;
using UnitTesting.Domain;

namespace UnitTesting.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns();

            modelBuilder.Entity<Question>()
                .HasMany(question => question.Answers)
                .WithOne()
                .HasForeignKey(answer => answer.QuestionId);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Questions)
                .WithOne()
                .HasForeignKey(question => question.UserId);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Answers)
                .WithOne()
                .HasForeignKey(answer => answer.UserId);
        }
    }
}
