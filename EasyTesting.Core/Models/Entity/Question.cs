using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyTesting.Core.Models.Entity
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public required string Text { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public ICollection<AnswerOption> AnswerOptions { get; set; } = new List<AnswerOption>();
    }
}
