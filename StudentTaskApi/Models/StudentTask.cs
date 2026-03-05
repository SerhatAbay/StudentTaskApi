namespace StudentTaskApi.Models
{
    public class StudentTask
    {
        public int Id { get; set; }
        public string Title { get; set; } = ""; //The "" means empty string to make sure there is a Title when new StudentTask is created
        public string? Description { get; set; } // Description is optional, so it can be null hence the "?" after string
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }

        public int StudentId { get; set; } // This is FK to the Student entity, linking each task to a specific student
        public Student Student { get; set; } = null!; // Navigation property to the student who owns the task
    }
}
