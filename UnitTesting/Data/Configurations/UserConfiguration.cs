using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitTesting.Domain;

namespace UnitTesting.Data.Configurations
{
    public class UserConfiguration
    {
        public void Configure(EntityTypeBuilder<User> modelBuilder)
        {
            modelBuilder
                .HasMany(user => user.Questions)
                .WithOne()
                .HasForeignKey(question => question.UserId);

            modelBuilder
                .HasMany(user => user.Answers)
                .WithOne()
                .HasForeignKey(answer => answer.UserId);
        }
    }
}
