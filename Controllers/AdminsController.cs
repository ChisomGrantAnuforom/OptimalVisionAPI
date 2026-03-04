namespace OptimalVisionAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class AdminsController(AppDbContext context) : ControllerBase
{
    [HttpGet(Name = "GetAdmins")]
    public IActionResult GetAdmins()
    {
        var admins = context.Admin.ToList();
        return Ok(admins);
    }

    [HttpGet("{id}")]
    public IActionResult GetAdmin(int id)
    {
        var admin = context.Admin.Find(id);
        if (admin == null)
        {
            return NotFound();
        }

        return Ok(admin);
    }
    
    
    [HttpGet("{email}/{password}")]
    public IActionResult GetAdminByEmailAndPassword(string email, string password)
    {
        var admin = context.Admin.FirstOrDefault(s => s.Email == email && s.Password == password);
    
        if (admin == null)
        {
            return NotFound("Invalid credentials");
        }
    
        return Ok(admin);
    }
    
    [HttpPost]
    public IActionResult CreateAdmin([FromBody] Admin admin)
    {
        context.Admin.Add(admin);
        context.SaveChanges();
        return CreatedAtAction(nameof(GetAdmin), new { id = admin.Id }, admin);
    }
    
    
    [HttpPut("{id}")]
    public IActionResult UpdateAdmin(int id, [FromBody] Admin updatedAdmin)
    {
        var admin = context.Admin.Find(id);
        if (admin == null)
        {
            return NotFound();
        }

    
        admin.FirstName = updatedAdmin.FirstName;
        admin.Surname = updatedAdmin.Surname;
        admin.Email = updatedAdmin.Email;
        admin.PhoneNumber = updatedAdmin.PhoneNumber;
        admin.Password = updatedAdmin.Password;
        admin.DateCreated = updatedAdmin.DateCreated;
    
    
    
        context.SaveChanges();
        return NoContent();
    }
    
    
    [HttpDelete("{id}")]
    public IActionResult DeleteAdmin(int id)
    {
        var admin = context.Admin.Find(id);
        if (admin == null)
        {
            return NotFound();
        }
    
        context.Admin.Remove(admin);
        context.SaveChanges();
        return NoContent();
    }
    
}