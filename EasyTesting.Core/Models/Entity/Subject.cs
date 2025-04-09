using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.Entity
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public string TeacherId { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();
    }
}
