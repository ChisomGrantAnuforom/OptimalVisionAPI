namespace OptimalVisionAPI.Models;

public class Admin
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string Email { get; set;  }

    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public DateTime DateCreated { get; set; }
}