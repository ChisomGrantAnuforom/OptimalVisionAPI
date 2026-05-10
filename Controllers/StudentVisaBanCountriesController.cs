using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;



namespace OptimalVisionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentVisaBanCountriesController (AppDbContext context) : ControllerBase
{
    [HttpGet(Name = "GetStudentVisaBanCountries")]
    public IActionResult GetStudentVisaBanCountries()
    {
        var studentVisaBanCountries = context.StudentVisaBanCountry.ToList();
        return Ok(studentVisaBanCountries);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentVisaBanCountry(int id)
    {
        var studentVisaBanCountry = context.StudentVisaBanCountry.Find(id);
        if (studentVisaBanCountry == null)
        {
            return NotFound();
        }

        return Ok(studentVisaBanCountry);
    }
    
    
    
        
    [HttpGet("{studentId}", Name = "GetStudentVisaBanCountryByStudentId")]
    public IActionResult GetStudentVisaBanCountryByStudentId(int studentId)
    {
        var studentVisaBanCountry = context.StudentVisaBanCountry
            .Where(x => x.StudentId == studentId)
            .ToList();

        if (!studentVisaBanCountry.Any())
            return NotFound($"No records found for studentId {studentId}");

        return Ok(studentVisaBanCountry);
    }
    
    
    
    [HttpPost]
    public IActionResult CreateStudentVisaBanCountry([FromBody] StudentVisaBanCountry studentVisaBanCountry)
    {
        context.StudentVisaBanCountry.Add(studentVisaBanCountry);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentVisaBanCountry), new { id = studentVisaBanCountry.Id }, studentVisaBanCountry);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudentVisaBanCountry(int id, [FromBody] StudentVisaBanCountry updatedStudentVisaBanCountry)
    {
        var studentVisaBanCountry = context.StudentVisaBanCountry.Find(id);
        if (studentVisaBanCountry == null)
        {
            return NotFound();
        }
        
        
        studentVisaBanCountry.StudentId = updatedStudentVisaBanCountry.StudentId;
        studentVisaBanCountry.CountryId = updatedStudentVisaBanCountry.CountryId;
        
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentVisaBanCountry(int id)
    {
        var studentVisaBanCountry = context.StudentVisaBanCountry.Find(id);
        if (studentVisaBanCountry == null)
        {
            return NotFound();
        }
    
        context.StudentVisaBanCountry.Remove(studentVisaBanCountry);
        context.SaveChanges();
        return NoContent();
    } 
}