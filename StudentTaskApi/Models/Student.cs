using System.Text.Json.Serialization;

namespace StudentTaskApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        [JsonIgnore]
        public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
    }
}
