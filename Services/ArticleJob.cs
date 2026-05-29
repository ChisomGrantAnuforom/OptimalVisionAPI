// using OptimalVisionAPI.Data;
//
// namespace OptimalVisionAPI.Services;
//
//
// public class ArticleJob
// {
//     private readonly ArticleGeneratorService _ai;
//     private readonly AppDbContext _db;
//
//     public ArticleJob(ArticleGeneratorService ai, AppDbContext db)
//     {
//         _ai = ai;
//         _db = db;
//     }
//
//     public async Task GenerateDailyArticle()
//     {
//         var topics = new[]
//         {
//             "How to Study Effectively",
//             "Understanding Scholarships",
//             "How to Choose a Course",
//             "Time Management for Students",
//             "How to Prepare for Exams"
//         };
//
//         var topic = topics[new Random().Next(topics.Length)];
//
//         var article = await _ai.GenerateArticle(topic);
//
//         _db.Articles.Add(article);
//         await _db.SaveChangesAsync();
//     }
// }



using System;
using System.Threading.Tasks;
using OptimalVisionAPI.Services;
using OptimalVisionAPI.Data;   // your DbContext namespace
using OptimalVisionAPI.Models; // your Article model namespace
using Microsoft.Extensions.Logging;

public class ArticleJob
{
    private readonly ArticleGeneratorService _generator;
    private readonly AppDbContext _db;
    private readonly ILogger<ArticleJob> _logger;

    public ArticleJob(
        ArticleGeneratorService generator,
        AppDbContext db,
        ILogger<ArticleJob> logger)
    {
        _generator = generator;
        _db = db;
        _logger = logger;
    }

    public async Task GenerateDailyArticle()
    {
        try
        {
            _logger.LogInformation("Starting daily article generation...");

            // You can randomize topics or pull from DB
            string topic = "Study tips for Nigerian students";

            var article = await _generator.GenerateArticle(topic);

            _db.Articles.Add(article);
            await _db.SaveChangesAsync();

            _logger.LogInformation("Daily article generated and saved successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating daily article");
            throw; // ensures Hangfire marks job as failed
        }
    }
}
