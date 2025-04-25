using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTesting.Core.Data.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> entity)
        {
            #region Question Configuration

            entity.HasKey(q => q.Id);

            entity.Property(q => q.Text)
                  .IsRequired();

            entity.HasOne(q => q.Subject)
                    .WithMany(s => s.Questions)
                    .HasForeignKey(q => q.SubjectId)
                    .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(q => q.Test)
                    .WithMany(t => t.Questions)
                    .HasForeignKey(q => q.TestId)
                    .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(q => q.CreatedBy)
                    .WithMany(u => u.Questions)
                    .HasForeignKey(q => q.CreatedById)
                    .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(q => q.AnswerOptions)
                    .WithOne(a => a.Question)
                    .HasForeignKey(a => a.QuestionId)
                    .OnDelete(DeleteBehavior.Cascade);

            #endregion
        }
    }
}
