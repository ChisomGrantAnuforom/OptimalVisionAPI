using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentCountryOfPreferencesController(AppDbContext context) : ControllerBase
{
    [HttpGet(Name = "GetStudentCountryOfPreferences")]
    public IActionResult GetStudentCountryOfPreferences()
    {
        var studentCountryOfPreferences = context.StudentCountryOfPreference.ToList();
        return Ok(studentCountryOfPreferences);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentCountryOfPreferences(int id)
    {
        var studentCountryOfPreferences= context.StudentCountryOfPreference.Find(id);
        if (studentCountryOfPreferences == null)
        {
            return NotFound();
        }

        return Ok(studentCountryOfPreferences);
    }
    
    
    [HttpGet("by-studentid/{studentId}", Name = "GetStudentCountryOfPreferencesByStudentId")]
    public IActionResult GetStudentCountryOfPreferencesByStudentId(int studentId)
    {
        var studentCountryOfPreferences = context.StudentCountryOfPreference
            .Where(x => x.StudentId == studentId)
            .ToList();

        if (!studentCountryOfPreferences.Any())
            return NotFound($"No records found for studentId {studentId}");

        return Ok(studentCountryOfPreferences);
    }
    
    
    [HttpPost]
    public IActionResult CreateStudentCountryOfPreference([FromBody] StudentCountryOfPreference studentCountryOfPreference)
    {
        context.StudentCountryOfPreference.Add(studentCountryOfPreference);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentCountryOfPreferences), new { id = studentCountryOfPreference.Id }, studentCountryOfPreference);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudentCountryOfPreference(int id, [FromBody] StudentCountryOfPreference updatedStudentCountryOfPreference)
    {
        var studentCountryOfPreference = context.StudentCountryOfPreference.Find(id);
        if (studentCountryOfPreference == null)
        {
            return NotFound();
        }

        
        studentCountryOfPreference.StudentId = updatedStudentCountryOfPreference.StudentId;
        studentCountryOfPreference.CountryId = updatedStudentCountryOfPreference.CountryId;
        // studentCountryOfPreference.Student = updatedStudentCountryOfPreference.Student;
        // studentCountryOfPreference.Country = updatedStudentCountryOfPreference.Country;
        
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentCountryOfPreference(int id)
    {
        var studentCountryOfPreference = context.StudentCountryOfPreference.Find(id);
        if (studentCountryOfPreference == null)
        {
            return NotFound();
        }
    
        context.StudentCountryOfPreference.Remove(studentCountryOfPreference);
        context.SaveChanges();
        return NoContent();
    }
}