using System.Text.Json.Serialization;

namespace StudentTaskApi.Models;

public class Instructor
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";

    [JsonIgnore]
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}
