using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTesting.Core.Models.Entity
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }

        [MaxLength(255)]
        public required string Email { get; set; }

        [MaxLength(50)]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        public string? LastName { get; set; }

        public UserRole Role = UserRole.Student;

        public int? TeacherId { get; set; }
        public User? Teacher { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public enum UserRole
        {
            Student,
            Teacher
        }
    }
}
