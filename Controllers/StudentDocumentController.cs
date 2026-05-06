using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentDocumentController : ControllerBase
{
    private readonly AppDbContext _context;

    public StudentDocumentController(AppDbContext context)
    {
        _context = context;
    }

    // GET: api/studentdocument
    [HttpGet]
    public IActionResult GetAllDocuments()
    {
        var docs = _context.StudentDocument.ToList();
        return Ok(docs);
    }

    // GET: api/studentdocument/document/5
    [HttpGet("document/{id}")]
    public IActionResult GetDocumentById(int id)
    {
        var doc = _context.StudentDocument.Find(id);
        if (doc == null)
            return NotFound(new { message = "Document not found" });

        return Ok(doc);
    }

    // GET: api/studentdocument/student/5
    [HttpGet("student/{studentId}")]
    public IActionResult GetDocumentsByStudentId(int studentId)
    {
        var docs = _context.StudentDocument
            .Where(x => x.StudentId == studentId)
            .ToList();

        return Ok(docs);
    }
    
    
    
    // POST: api/studentdocument/upload/5
    [HttpPost("upload/{studentId}")]
    public async Task<IActionResult> UploadDocument(int studentId, IFormFile file)
    {
        try
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { message = "No file uploaded" });

            var student = _context.Student.Find(studentId);
            if (student == null)
                return NotFound(new { message = "Student not found" });

            // Ensure wwwroot/Uploads exists
            var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads");
            Directory.CreateDirectory(root);

            // Create student folder inside Uploads
            var studentFolder = Path.Combine(root, studentId.ToString());
            Directory.CreateDirectory(studentFolder);

            // Clean filename
            var safeName = Path.GetFileName(file.FileName);

            // Full physical path
            var filePath = Path.Combine(studentFolder, safeName);

            // Save file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Build public URL (this is what MAUI will use)
            var publicUrl = $"{Request.Scheme}://{Request.Host}/Uploads/{studentId}/{safeName}";

            // Save metadata to DB
            var doc = new StudentDocument
            {
                StudentId = studentId,
                Title = safeName,
                URL = publicUrl,
                Size = file.Length.ToString(),
                DocumentType = Path.GetExtension(safeName),
                DateUploaded = DateTime.UtcNow
            };

            _context.StudentDocument.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Upload successful", document = doc });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message, stack = ex.StackTrace });
        }
    }

    
    
    
    
    
    

    // // POST: api/studentdocument/upload/5
    // [HttpPost("upload/{studentId}")]
    // public async Task<IActionResult> UploadDocument(int studentId, IFormFile file)
    // {
    //     if (file == null || file.Length == 0)
    //         return BadRequest(new { message = "No file uploaded" });
    //
    //     var student = _context.Student.Find(studentId);
    //     if (student == null)
    //         return NotFound(new { message = "Student not found" });
    //
    //     var folder = Path.Combine("Uploads", studentId.ToString());
    //     Directory.CreateDirectory(folder);
    //
    //     var filePath = Path.Combine(folder, file.FileName);
    //
    //     using (var stream = new FileStream(filePath, FileMode.Create))
    //     {
    //         await file.CopyToAsync(stream);
    //     }
    //
    //     var doc = new StudentDocument
    //     {
    //         StudentId = studentId,
    //         Title = file.FileName,
    //         URL = filePath.Replace("\\", "/"),
    //         Size = file.Length.ToString(),
    //         DocumentType = Path.GetExtension(file.FileName),
    //         DateUploaded = DateTime.UtcNow
    //     };
    //
    //     _context.StudentDocument.Add(doc);
    //     _context.SaveChanges();
    //
    //     return Ok(new { message = "Upload successful", document = doc });
    // }

    // PUT: api/studentdocument/5
    [HttpPut("{id}")]
    public IActionResult UpdateDocument(int id, [FromBody] StudentDocument updated)
    {
        var doc = _context.StudentDocument.Find(id);
        if (doc == null)
            return NotFound(new { message = "Document not found" });

        doc.Title = updated.Title;
        doc.URL = updated.URL;
        doc.DocumentType = updated.DocumentType;
        doc.Size = updated.Size;
        doc.DocumentCategoryId = updated.DocumentCategoryId;
        doc.StudentId = updated.StudentId;
        doc.DateUploaded = updated.DateUploaded;

        _context.SaveChanges();

        return Ok(new { message = "Document updated", document = doc });
    }

    // DELETE: api/studentdocument/5
    [HttpDelete("{id}")]
    public IActionResult DeleteDocument(int id)
    {
        var doc = _context.StudentDocument.Find(id);
        if (doc == null)
            return NotFound(new { message = "Document not found" });

        _context.StudentDocument.Remove(doc);
        _context.SaveChanges();

        return Ok(new { message = "Document deleted" });
    }
}
