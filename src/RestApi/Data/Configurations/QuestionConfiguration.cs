using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestApi.Domain;

namespace RestApi.Data.Configurations
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
