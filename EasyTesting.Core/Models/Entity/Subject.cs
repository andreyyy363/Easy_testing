﻿using System.ComponentModel.DataAnnotations;

namespace EasyTesting.Core.Models.Entity
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TeacherId { get; set; }
        public User? Teacher { get; set; }

        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Test> Tests { get; set; } = new List<Test>();
    }
}
