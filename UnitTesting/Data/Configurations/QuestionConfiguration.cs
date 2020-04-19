using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UnitTesting.Domain;

namespace UnitTesting.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(question => question.Id);

            builder
                .HasMany(question => question.Answers)
                .WithOne()
                .HasForeignKey(answer => answer.QuestionId);
        }
    }
}
