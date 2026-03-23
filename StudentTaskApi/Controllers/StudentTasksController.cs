using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskApi.Data;
using StudentTaskApi.Models;

namespace StudentTaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentTasksController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentTasksController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentTask>>> GetStudentTasks([FromQuery] bool? completed, [FromQuery] int? categoryId, [FromQuery] int? courseId)
    {
        var query = _context.StudentTasks
            .Include(st => st.Student)
            .Include(st => st.Course)
            .Include(st => st.Category)
            .AsQueryable();
        if (completed.HasValue)
            query = query.Where(st => st.IsCompleted == completed.Value);
        if (categoryId.HasValue)
            query = query.Where(st => st.CategoryId == categoryId.Value);
        if (courseId.HasValue)
            query = query.Where(st => st.CourseId == courseId.Value);
        return await query.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<StudentTask>> GetStudentTask(int id)
    {
        var task = await _context.StudentTasks
            .Include(st => st.Student)
            .Include(st => st.Course)
            .Include(st => st.Category)
            .FirstOrDefaultAsync(st => st.Id == id);
        if (task == null)
            return NotFound();
        return task;
    }

    [HttpGet("by-student/{studentId}")]
    public async Task<ActionResult<IEnumerable<StudentTask>>> GetTasksByStudent(int studentId)
    {
        return await _context.StudentTasks
            .Include(st => st.Course)
            .Include(st => st.Category)
            .Where(st => st.StudentId == studentId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<StudentTask>> CreateStudentTask(StudentTask studentTask)
    {
        _context.StudentTasks.Add(studentTask);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetStudentTask), new { id = studentTask.Id }, studentTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudentTask(int id, StudentTask studentTask)
    {
        if (id != studentTask.Id)
            return BadRequest();
        _context.Entry(studentTask).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.StudentTasks.AnyAsync(e => e.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentTask(int id)
    {
        var task = await _context.StudentTasks.FindAsync(id);
        if (task == null)
            return NotFound();
        _context.StudentTasks.Remove(task);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
