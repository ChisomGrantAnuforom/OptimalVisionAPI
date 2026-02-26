namespace OptimalVisionAPI.Data;

using OptimalVisionAPI.Models;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Admin> Admin { get; set; }
    public DbSet<Country>  Country { get; set; }
    public DbSet<DocumentCategory>  DocumentCategory { get; set; }
    public DbSet<Student> Student { get; set; }
    public DbSet<StudentCountryOfPreference> StudentCountryOfPreference { get; set; }
    public DbSet<StudentDocument> StudentDocument { get; set; }
    public DbSet<StudentQualifiedCourse> StudentQualifiedCourse { get; set; }
    public DbSet<StudentVisaBanCountry> StudentVisaBanCountries { get; set; }
    public DbSet<StudentVisaRefusalCountry>  StudentVisaRefusalCountries { get; set; }
    
}