namespace StudentTaskApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";

        public ICollection<StudentTask> StudentTasks { get; set; } = new List<StudentTask>();
        // Using ICollection to represent the one-to-many relationship between Student and StudentTask 
        // = new List<StudentTask>() initializes the collection to an empty list, ensuring that it is not null when accessed.
    }
}
