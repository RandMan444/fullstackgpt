using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatGPTAPI
{
    public class CompletionResponse
    {
        [JsonPropertyName("choices")]
        public List<ChatGPTChoice>? Choices
        {
            get;
            set;
        }
        [JsonPropertyName("usage")]
        public ChatGPTUsage? Usage
        {
            get;
            set;
        }
    }
    public class ChatGPTUsage
    {
        [JsonPropertyName("prompt_tokens")]
        public int PromptTokens
        {
            get;
            set;
        }
        [JsonPropertyName("completion_token")]
        public int CompletionTokens
        {
            get;
            set;
        }
        [JsonPropertyName("total_tokens")]
        public int TotalTokens
        {
            get;
            set;
        }
    }

    public class ChatGPTChoice
    {
        [JsonPropertyName("message")]
        public ChatGPTMessage Message
        {
            get;
            set;
        }
    }

    public class ChatGPTMessage
    {
        [JsonPropertyName("role")]
        public string? Role
        {
            get;
            set;
        }
        [JsonPropertyName("content")]
        public string? Content
        {
            get;
            set;
        }
    }
}
