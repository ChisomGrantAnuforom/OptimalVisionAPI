using Microsoft.AspNetCore.Mvc;
using OptimalVisionAPI.Services;
using OptimalVisionAPI.Data;
using OptimalVisionAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OptimalVisionAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArticleController : ControllerBase
    {
        private readonly ArticleGeneratorService _generator;
        private readonly AppDbContext _db;

        public ArticleController(ArticleGeneratorService generator, AppDbContext db)
        {
            _generator = generator;
            _db = db;
        }

        // GET: api/article
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var articles = await _db.Articles
                .OrderByDescending(a => a.CreatedAt)
                .ToListAsync();

            return Ok(articles);
        }

        // POST: api/article/generate
        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromBody] GenerateRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Topic))
                return BadRequest("Topic is required.");

            var article = await _generator.GenerateArticle(request.Topic);

            _db.Articles.Add(article);
            await _db.SaveChangesAsync();

            return Ok(article);
        }

        // GET: api/article/testgroq
        [HttpGet("testgroq")]
        public async Task<IActionResult> TestGroq()
        {
            var article = await _generator.GenerateArticle("The importance of reading for students");
            return Ok(article);
        }
    }

    public class GenerateRequest
    {
        public string Topic { get; set; }
    }
}



// using Microsoft.AspNetCore.Mvc;
// using OptimalVisionAPI.Data;
// using OptimalVisionAPI.Services;
//
// namespace OptimalVisionAPI.Controllers;
//
// [Route("api/[controller]")]
// [ApiController]
// public class ArticleController(AppDbContext context) : ControllerBase 
// {
//     
//     private readonly ArticleGeneratorService _ai;
//     private readonly AppDbContext _db;
//     
//     [HttpGet(Name = "GetArticles")]
//     public IActionResult GetArticles()
//     {
//         ArticleJob job = new ArticleJob(_ai, _db);
//         job.GenerateDailyArticle();
//         
//         return Ok(context.Articles.OrderByDescending(a => a.CreatedAt).ToList());
//     }
//
//     [HttpGet("{id}")]
//     public IActionResult GetArticle(int id)
//     {
//         var article = context.Articles.Find(id);
//         if (article == null) return NotFound();
//         return Ok(article);
//     }
//
//     
//     [HttpGet("groq-test")]
//     public async Task<IActionResult> TestGroq([FromServices] ArticleGeneratorService ai)
//     {
//         var article = await ai.GenerateArticle("Test topic");
//         return Ok(article);
//     }
//
//     
//     // public async Task GenerateDailyArticle(ArticleGeneratorService ai, AppDbContext db)
//     // {
//     //     _ai = ai;
//     //     _db = db;
//     //     
//     //     var topics = new[]
//     //     {
//     //         "How to Study Effectively",
//     //         "Understanding Scholarships",
//     //         "How to Choose a Course",
//     //         "Time Management for Students",
//     //         "How to Prepare for Exams"
//     //     };
//     //
//     //     var topic = topics[new Random().Next(topics.Length)];
//     //
//     //     var article = await _ai.GenerateArticle(topic);
//     //
//     //     _db.Articles.Add(article);
//     //     await _db.SaveChangesAsync();
//     // }
//     
// }
//
