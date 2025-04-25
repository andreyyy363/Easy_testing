using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTesting.Core.Data.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> entity)
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Name).IsRequired();

            entity.HasOne(t => t.Subject)
                  .WithMany(s => s.Tests)
                  .HasForeignKey(t => t.SubjectId)
                  .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(t => t.Teacher)
                  .WithMany(u => u.Tests)
                  .HasForeignKey(t => t.TeacherId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(t => t.Questions)
                  .WithOne(q => q.Test)
                  .HasForeignKey(q => q.TestId)
                  .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
