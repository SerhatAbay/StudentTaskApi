using System.Text.Json.Serialization;

namespace StudentTaskApi.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; } = "";

    [JsonIgnore] // Prevent JSON going back and forth forever, and no use of DTOs in this project
    public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
}
