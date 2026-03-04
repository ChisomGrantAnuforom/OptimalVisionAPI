using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentQualifiedCoursesController (AppDbContext context) : ControllerBase
{
    
    [HttpGet(Name = "GetStudentQualifiedCourses")]
    public IActionResult GetStudentQualifiedCourses()
    {
        var studentQualifiedCourses = context.StudentQualifiedCourse.ToList();
        return Ok(studentQualifiedCourses);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentQualifiedCourse(int id)
    {
        var studentQualifiedCourse = context.StudentQualifiedCourse.Find(id);
        if (studentQualifiedCourse == null)
        {
            return NotFound();
        }

        return Ok(studentQualifiedCourse);
    }
    
    
    
        
    [HttpGet("{studentId}", Name = "GetStudentQualifiedCourseByStudentId")]
    public IActionResult GetStudentQualifiedCourseByStudentId(int studentId)
    {
        var studentQualifiedCourse = context.StudentQualifiedCourse
            .Where(x => x.StudentId == studentId)
            .ToList();

        if (!studentQualifiedCourse.Any())
            return NotFound($"No records found for studentId {studentId}");

        return Ok(studentQualifiedCourse);
    }
    
    
    
    [HttpPost]
    public IActionResult CreateStudentQualifiedCourse([FromBody] StudentQualifiedCourse studentQualifiedCourse)
    {
        context.StudentQualifiedCourse.Add(studentQualifiedCourse);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentQualifiedCourse), new { id = studentQualifiedCourse.Id }, studentQualifiedCourse);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudentQualifiedCourse(int id, [FromBody] StudentQualifiedCourse updatedStudentQualifiedCourse)
    {
        var studentQualifiedCourse = context.StudentQualifiedCourse.Find(id);
        if (studentQualifiedCourse == null)
        {
            return NotFound();
        }
        
        
        studentQualifiedCourse.StudentId = updatedStudentQualifiedCourse.StudentId;
        studentQualifiedCourse.CourseName = updatedStudentQualifiedCourse.CourseName;
        
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentQualifiedCourse(int id)
    {
        var studentQualifiedCourse = context.StudentQualifiedCourse.Find(id);
        if (studentQualifiedCourse == null)
        {
            return NotFound();
        }
    
        context.StudentQualifiedCourse.Remove(studentQualifiedCourse);
        context.SaveChanges();
        return NoContent();
    }
}