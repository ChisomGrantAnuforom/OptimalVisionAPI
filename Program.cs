using Hangfire;
using Microsoft.EntityFrameworkCore;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Services;

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

builder.Services.AddTransient<ArticleJob>();


builder.Services.AddHttpClient<ArticleGeneratorService>(client =>
{
    client.BaseAddress = new Uri("https://api.groq.com/openai/v1/");
    client.DefaultRequestHeaders.Add("Authorization", "Bearer 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");
});



//using hangfire for daily cron job for auto edu article generator
builder.Services.AddHangfire(config =>
{
    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddHangfireServer();





var app = builder.Build();

app.UseHangfireDashboard("/jobs");//ai article generation job
// app.UseHangfireDashboard("/hangfire");

// RecurringJob.AddOrUpdate<ArticleJob>(
//     "morning-article-job",
//     job => job.GenerateDailyArticle(),
//     Cron.Daily(8)
// );

RecurringJob.AddOrUpdate<ArticleJob>(
    "onetime-article-job",
    job => job.GenerateDailyArticle(),
    "41 19 * * *"
);


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