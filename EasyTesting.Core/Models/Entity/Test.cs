using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.Entity
{
    public class Test
    {
        [Key]
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public required string Name { get; set; }
        public int TeacherId { get; set; }
        public User? Teacher { get; set; }

        public string TestXml { get; set; } = string.Empty;
        public ICollection<Question> Questions { get; set; } = new List<Question>();

    }
}
