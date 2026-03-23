using System.Text.Json.Serialization;

namespace StudentTaskApi.Models
{
    public class StudentTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int StudentId { get; set; }
        [JsonIgnore]
        public Student? Student { get; set; }

        public int? CourseId { get; set; }
        [JsonIgnore]
        public Course? Course { get; set; }

        public int? CategoryId { get; set; }
        [JsonIgnore]
        public Category? Category { get; set; }
    }
}
