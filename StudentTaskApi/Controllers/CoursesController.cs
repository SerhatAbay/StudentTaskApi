using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskApi.Data;
using StudentTaskApi.Models;

namespace StudentTaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : ControllerBase
{
    private readonly AppDbContext _context;

    public CoursesController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
    {
        return await _context.Courses.Include(c => c.Instructor).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Course>> GetCourse(int id)
    {
        var course = await _context.Courses
            .Include(c => c.Instructor)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (course == null)
            return NotFound();
        return course;
    }

    [HttpGet("{id}/tasks")]
    public async Task<ActionResult<IEnumerable<StudentTask>>> GetCourseTasks(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound();
        var tasks = await _context.StudentTasks
            .Include(st => st.Student)
            .Where(st => st.CourseId == id)
            .ToListAsync();
        return tasks;
    }

    [HttpPost]
    public async Task<ActionResult<Course>> CreateCourse(Course course)
    {
        _context.Courses.Add(course);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetCourse), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourse(int id, Course course)
    {
        if (id != course.Id)
            return BadRequest();
        _context.Entry(course).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Courses.AnyAsync(e => e.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourse(int id)
    {
        var course = await _context.Courses.FindAsync(id);
        if (course == null)
            return NotFound();
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
