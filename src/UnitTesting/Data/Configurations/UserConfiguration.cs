using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitTesting.Domain;

namespace UnitTesting.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder
                .HasMany(user => user.Questions)
                .WithOne()
                .HasForeignKey(question => question.UserId);

            builder
                .HasMany(user => user.Answers)
                .WithOne()
                .HasForeignKey(answer => answer.UserId);
        }
    }
}
