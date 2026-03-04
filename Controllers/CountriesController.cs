namespace OptimalVisionAPI.Controllers;


using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class CountriesController (AppDbContext context) : ControllerBase
{
    
    [HttpGet(Name = "GetCountries")]
    public IActionResult GetCountries()
    {
        var countries = context.Student.ToList();
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public IActionResult GetCountry(int id)
    {
        var country = context.Country.Find(id);
        if (country == null)
        {
            return NotFound();
        }

        return Ok(country);
    }

}