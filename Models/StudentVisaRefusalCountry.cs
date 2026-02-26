namespace OptimalVisionAPI.Models;

public class StudentVisaRefusalCountry
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CountryId { get; set; }
    public Student Student { get; set; }
    public Country Country { get; set; }
}