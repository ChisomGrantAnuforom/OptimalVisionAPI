using System.Collections.Generic;

namespace OptimalVisionAPI.Models
{
    public class GroqChatResponse
    {
        public List<GroqChoice> Choices { get; set; }
    }

    public class GroqChoice
    {
        // For chat-style models
        public GroqMessage Message { get; set; }

        // For OSS-style models (e.g., openai/gpt-oss-120b)
        public string Text { get; set; }
    }

    public class GroqMessage
    {
        public string Content { get; set; }
    }
}
