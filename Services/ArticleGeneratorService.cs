// using System.Text.Json;
// using OptimalVisionAPI.Models;
//
// namespace OptimalVisionAPI.Services;


// public class ArticleGeneratorService
// {
//     private readonly HttpClient _http;
//
//     public ArticleGeneratorService(HttpClient http)
//     {
//         _http = http;
//     }
//     
//     public async Task<Article> GenerateArticle(string topic)
//     {
//         var prompt = $"Write an educational article for Nigerian students about: {topic}";
//
//         var response = await _http.PostAsJsonAsync("chat/completions", new
//         {
//             model = "openai/gpt-oss-120b",
//             messages = new[]
//             {
//                 new { role = "user", content = prompt }
//             }
//         });
//
//         var json = await response.Content.ReadAsStringAsync();
//         Console.WriteLine("RAW GROQ JSON: " + json);
//
//         var result = JsonSerializer.Deserialize<GroqChatResponse>(json);
//
//         var choice = result?.Choices?.FirstOrDefault();
//
//         var content =
//             
//             
//             choice?.Message?.Content
//             ?? choice.ToString(); 
//
//         if (string.IsNullOrWhiteSpace(content))
//             throw new Exception("Groq returned empty content");
//
//         return new Article
//         {
//             Title = topic,
//             Body = content,
//             Category = "Education",
//             CreatedAt = DateTime.UtcNow
//         };
//     }



    // public async Task<Article> GenerateArticle(string topic)
    // {
    //     var prompt = $"Write a clear, educational article for Nigerian students about: {topic}. Include examples and keep it engaging.";
    //
    //     // var response = await _http.PostAsJsonAsync("v1/chat/completions", new
    //     // {
    //     //     model = "gpt-4o-mini",
    //     //     messages = new[]
    //     //     {
    //     //         new { role = "user", content = prompt }
    //     //     }
    //     // });
    //     
    //     var response = await _http.PostAsJsonAsync("chat/completions", new
    //     {
    //         model = "llama3-70b-8192",
    //         messages = new[]
    //         {
    //             new { role = "user", content = prompt }
    //         }
    //     });
    //
    //
    //     var json = await response.Content.ReadFromJsonAsync<dynamic>();
    //     string content = json.choices[0].message.content;
    //
    //     return new Article
    //     {
    //         Title = topic,
    //         Body = content,
    //         Category = "Education"
    //     };
    // }
// }



using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using OptimalVisionAPI.Models;

namespace OptimalVisionAPI.Services
{
    public class ArticleGeneratorService
    {
        private readonly HttpClient _http;

        // You can switch this to another Groq model if you want
        private const string ModelName = "openai/gpt-oss-120b";

        public ArticleGeneratorService(HttpClient http)
        {
            _http = http;
        }

        public async Task<Article> GenerateArticle(string topic)
        {
            var prompt = $"Write a clear, engaging educational article for Nigerian students about: {topic}. " +
                         "Use simple English, short paragraphs, and practical examples.";

            var requestBody = new
            {
                model = ModelName,
                messages = new[]
                {
                    new { role = "user", content = prompt }
                },
                temperature = 0.7
            };

            var response = await _http.PostAsJsonAsync("chat/completions", requestBody);

            var json = await response.Content.ReadAsStringAsync();
            Console.WriteLine("RAW GROQ JSON: " + json);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Groq error: {response.StatusCode} - {json}");
            }

            var result = JsonSerializer.Deserialize<GroqChatResponse>(json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var choice = result?.Choices?.FirstOrDefault();

            var content =
                choice?.Message?.Content   // chat models
                ?? choice?.Text;           // OSS models

            if (string.IsNullOrWhiteSpace(content))
                throw new Exception("Groq returned empty content");

            return new Article
            {
                Title = topic,
                Body = content.Trim(),
                Category = "Education",
                CreatedAt = DateTime.UtcNow
            };
        }
    }
    //
    // // Example Article entity – adjust to your real model
    // public class Article
    // {
    //     public string Title { get; set; }
    //     public string Body { get; set; }
    //     public string Category { get; set; }
    //     public DateTime CreatedAt { get; set; }
    // }
}


//
// public class GroqChatResponse
// {
//     public List<GroqChoice> Choices { get; set; }
// }
//
// public class GroqChoice
// {
//     public string Text { get; set; }   // OSS models use "text"
//     public GroqMessage Message { get; set; } // Chat models use "message"
// }
//
// public class GroqMessage
// {
//     public string Content { get; set; }
// }

