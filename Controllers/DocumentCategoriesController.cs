namespace OptimalVisionAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class DocumentCategoriesController(AppDbContext context) : ControllerBase
{
    [HttpGet(Name = "GetDocumentCategories")]
    public IActionResult GetDocumentCategories()
    {
        var documentCategories = context.DocumentCategory.ToList();
        return Ok(documentCategories);
    }

    [HttpGet("{id}")]
    public IActionResult GetDocumentCategory(int id)
    {
        var documentCategory = context.DocumentCategory.Find(id);
        if (documentCategory == null)
        {
            return NotFound();
        }

        return Ok(documentCategory);
    }
    
    
    [HttpGet("{documentCategory}")]
    public IActionResult GetDocumentCategoryByCategoryName(string categoryName)
    {
        var documentCategory = context.DocumentCategory.FirstOrDefault(s => s.CategoryName == categoryName);
    
        if (documentCategory == null)
        {
            return NotFound("Invalid credentials");
        }
    
        return Ok(documentCategory);
    }
    
    [HttpPost]
    public IActionResult CreateDocumentCategory([FromBody] DocumentCategory documentCategory)
    {
        context.DocumentCategory.Add(documentCategory);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetDocumentCategory), new { id = documentCategory.Id }, documentCategory);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateDocumentCategory(int id, [FromBody] DocumentCategory updatedDocumentCategory)
    {
        var documentCategory = context.DocumentCategory.Find(id);
        if (documentCategory == null)
        {
            return NotFound();
        }

    
        documentCategory.CategoryName = updatedDocumentCategory.CategoryName;
    
    
    
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteDocumentCategory(int id)
    {
        var documentCategory = context.DocumentCategory.Find(id);
        if (documentCategory == null)
        {
            return NotFound();
        }
    
        context.DocumentCategory.Remove(documentCategory);
        context.SaveChanges();
        return NoContent();
    }
    
}