using OptimalVisionAPI.DTOs;

namespace OptimalVisionAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;


[Route("api/[controller]")]
[ApiController]
public class StudentsController(AppDbContext context) : ControllerBase
{

    [HttpGet(Name = "GetStudents")]
    public IActionResult GetStudents()
    {
        var students = context.Student.ToList();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudent(int id)
    {
        var student = context.Student.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }


    [HttpGet("{email}/{password}")]
    public IActionResult GetStudentByEmailAndPassword(string email, string password)
    {
        var student = context.Student.FirstOrDefault(s => s.Email == email && s.Password == password);
    
        if (student == null)
        {
            return NotFound("Invalid credentials");
        }
    
        return Ok(student);
    }
    
    
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto) 
    {
        var student = context.Student
            .FirstOrDefault(s => s.Email == dto.Email && s.Password == dto.Password);

        if (student == null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        return Ok(student);
    }

    
    
    [HttpPost]
    public IActionResult CreateStudent([FromBody] Student student)
    {
        context.Student.Add(student);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudent(int id, [FromBody] Student updatedStudent)
    {
        var student = context.Student.Find(id);
        if (student == null)
        {
            return NotFound();
        }

        
        
        student.FirstName = updatedStudent.FirstName;
        student.Surname = updatedStudent.Surname;
        student.Email = updatedStudent.Email;
        student.PhoneNumber = updatedStudent.PhoneNumber;
        student.Password = updatedStudent.Password;
        student.Address = updatedStudent.Address;
        student.DateOfBirth = updatedStudent.DateOfBirth;
    
    
    
        context.SaveChanges();
        return NoContent();
    }
    
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudent(int id)
    {
        var student = context.Student.Find(id);
        if (student == null)
        {
            return NotFound();
        }
    
        context.Student.Remove(student);
        context.SaveChanges();
        return NoContent();
    }

}