using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskApi.Data;
using StudentTaskApi.Models;

namespace StudentTaskApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstructorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public InstructorsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instructor>>> GetInstructors()
    {
        return await _context.Instructors.Include(i => i.Courses).ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Instructor>> GetInstructor(int id)
    {
        var instructor = await _context.Instructors
            .Include(i => i.Courses)
            .FirstOrDefaultAsync(i => i.Id == id);
        if (instructor == null)
            return NotFound();
        return instructor;
    }

    [HttpGet("{id}/courses")]
    public async Task<ActionResult<IEnumerable<Course>>> GetInstructorCourses(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
            return NotFound();
        return await _context.Courses.Where(c => c.InstructorId == id).ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<Instructor>> CreateInstructor(Instructor instructor)
    {
        _context.Instructors.Add(instructor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetInstructor), new { id = instructor.Id }, instructor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateInstructor(int id, Instructor instructor)
    {
        if (id != instructor.Id)
            return BadRequest();
        _context.Entry(instructor).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await _context.Instructors.AnyAsync(e => e.Id == id))
                return NotFound();
            throw;
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstructor(int id)
    {
        var instructor = await _context.Instructors.FindAsync(id);
        if (instructor == null)
            return NotFound();
        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
