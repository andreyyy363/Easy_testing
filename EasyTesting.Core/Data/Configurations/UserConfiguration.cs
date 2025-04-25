using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyTesting.Core.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.HasKey(u => u.Id);

            entity.Property(u => u.Username).IsRequired().HasMaxLength(255);
            entity.Property(u => u.PasswordHash).IsRequired();
            entity.Property(u => u.Email).IsRequired().HasMaxLength(255);
            entity.Property(u => u.FirstName).HasMaxLength(50);
            entity.Property(u => u.LastName).HasMaxLength(50);

            entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            entity.Property(u => u.UpdatedAt).HasDefaultValueSql("GETUTCDATE()");

            entity.Property(u => u.Role)
                      .HasConversion<string>();

            entity.HasOne(u => u.Teacher)
                  .WithMany()
                  .HasForeignKey(u => u.TeacherId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(u => u.Subjects)
                  .WithOne(s => s.Teacher)
                  .HasForeignKey(s => s.TeacherId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.Tests)
                  .WithOne(t => t.Teacher)
                  .HasForeignKey(t => t.TeacherId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(u => u.Questions)
                  .WithOne(q => q.CreatedBy)
                  .HasForeignKey(q => q.CreatedById)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
