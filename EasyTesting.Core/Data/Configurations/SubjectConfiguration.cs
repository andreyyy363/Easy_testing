using EasyTesting.Core.Models.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTesting.Core.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name).IsRequired();

            entity.HasOne(s => s.Teacher)
                  .WithMany(u => u.Subjects)
                  .HasForeignKey(s => s.TeacherId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(s => s.Questions)
                  .WithOne(q => q.Subject)
                  .HasForeignKey(q => q.SubjectId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(s => s.Tests)
                  .WithOne(t => t.Subject)
                  .HasForeignKey(t => t.SubjectId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
