using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Controllers;

public class StudentVisaRefusalCountriesController (AppDbContext context) : ControllerBase
{
    [HttpGet(Name = "GetStudentVisaRefusalCountries")]
    public IActionResult GetStudentVisaRefusalCountries()
    {
        var studentVisaRefusalCountries = context.StudentVisaRefusalCountry.ToList();
        return Ok(studentVisaRefusalCountries);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentVisaRefusalCountry(int id)
    {
        var studentVisaRefusalCountry = context.StudentVisaRefusalCountry.Find(id);
        if (studentVisaRefusalCountry == null)
        {
            return NotFound();
        }

        return Ok(studentVisaRefusalCountry);
    }
    
    
    
        
    [HttpGet("{studentId}", Name = "GetStudentVisaRefusalCountryByStudentId")]
    public IActionResult GetStudentVisaRefusalCountryByStudentId(int studentId)
    {
        var studentVisaRefusalCountry = context.StudentVisaRefusalCountry
            .Where(x => x.StudentId == studentId)
            .ToList();

        if (!studentVisaRefusalCountry.Any())
            return NotFound($"No records found for studentId {studentId}");

        return Ok(studentVisaRefusalCountry);
    }
    
    
    
    [HttpPost]
    public IActionResult CreateStudentVisaRefusalCountry([FromBody] StudentVisaRefusalCountry studentVisaRefusalCountry)
    {
        context.StudentVisaRefusalCountry.Add(studentVisaRefusalCountry);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentVisaRefusalCountry), new { id = studentVisaRefusalCountry.Id }, studentVisaRefusalCountry);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudentVisaRefusalCountry(int id, [FromBody] StudentVisaRefusalCountry updatedStudentVisaRefusalCountry)
    {
        var studentVisaRefusalCountry = context.StudentVisaRefusalCountry.Find(id);
        if (studentVisaRefusalCountry == null)
        {
            return NotFound();
        }
        
        
        studentVisaRefusalCountry.StudentId = updatedStudentVisaRefusalCountry.StudentId;
        studentVisaRefusalCountry.CountryId = updatedStudentVisaRefusalCountry.CountryId;
        
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentVisaRefusalCountry(int id)
    {
        var studentVisaRefusalCountry = context.StudentVisaRefusalCountry.Find(id);
        if (studentVisaRefusalCountry == null)
        {
            return NotFound();
        }
    
        context.StudentVisaRefusalCountry.Remove(studentVisaRefusalCountry);
        context.SaveChanges();
        return NoContent();
    } 
}