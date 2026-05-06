using Microsoft.EntityFrameworkCore;
using OptimalVisionAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Controllers
builder.Services.AddControllers();

// EF Core
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// OpenAPI / Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

// Enable static files (for wwwroot/Uploads)
app.UseStaticFiles();

// CORS
app.UseCors("AllowAll");

// Routing
app.UseRouting();

// Only redirect HTTPS in development (your server uses HTTP)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
    app.MapOpenApi();
}

// Map controllers
app.MapControllers();

app.Run();