using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class StudentDocumentController(AppDbContext context) : ControllerBase
{ 
    [HttpGet(Name = "GetStudentDocuments")]
    public IActionResult GetStudentDocuments()
    {
        var studentDocuments = context.StudentDocument.ToList();
        return Ok(studentDocuments);
    }

    [HttpGet("{id}")]
    public IActionResult GetStudentDocument(int id)
    {
        var studentDocument = context.StudentDocument.Find(id);
        if (studentDocument == null)
        {
            return NotFound();
        }

        return Ok(studentDocument);
    }
    
    
    
        
    [HttpGet("{studentId}", Name = "GetStudentDocumentsByStudentId")]
    public IActionResult GetStudentDocumentsByStudentId(int studentId)
    {
        var studentDocuments = context.StudentDocument
            .Where(x => x.StudentId == studentId)
            .ToList();

        if (!studentDocuments.Any())
            return NotFound($"No records found for studentId {studentId}");

        return Ok(studentDocuments);
    }
    
    
    
    [HttpPost]
    public IActionResult CreateStudentDocument([FromBody] StudentDocument studentDocument)
    {
        context.StudentDocument.Add(studentDocument);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetStudentDocument), new { id = studentDocument.Id }, studentDocument);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateStudentDocument(int id, [FromBody] StudentDocument updatedStudentDocument)
    {
        var studentDocument = context.StudentDocument.Find(id);
        if (studentDocument == null)
        {
            return NotFound();
        }
        
        
        studentDocument.Title = updatedStudentDocument.Title;
        studentDocument.URL = updatedStudentDocument.URL;
        studentDocument.DocumentType = updatedStudentDocument.DocumentType;
        studentDocument.Size = updatedStudentDocument.Size;
        studentDocument.DocumentCategoryId = updatedStudentDocument.DocumentCategoryId;
        studentDocument.StudentId = updatedStudentDocument.StudentId;
        studentDocument.DateUploaded = updatedStudentDocument.DateUploaded;
    
    
    
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteStudentDocument(int id)
    {
        var studentDocument = context.StudentDocument.Find(id);
        if (studentDocument == null)
        {
            return NotFound();
        }
    
        context.StudentDocument.Remove(studentDocument);
        context.SaveChanges();
        return NoContent();
    }
}