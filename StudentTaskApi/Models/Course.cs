using System.Text.Json.Serialization;

namespace StudentTaskApi.Models;

public class Course
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Code { get; set; } = "";

    public int? InstructorId { get; set; }

    [JsonIgnore]
    public Instructor? Instructor { get; set; }

    [JsonIgnore]
    public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
}
