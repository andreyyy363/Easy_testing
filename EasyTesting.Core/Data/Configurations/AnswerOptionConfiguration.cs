using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyTesting.Core.Data.Configurations
{
    public class AnswerOptionConfiguration : IEntityTypeConfiguration<AnswerOption>
    {
        public void Configure(EntityTypeBuilder<AnswerOption> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Text).IsRequired();

            entity.HasOne(a => a.Question)
                  .WithMany(q => q.AnswerOptions)
                  .HasForeignKey(a => a.QuestionId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
