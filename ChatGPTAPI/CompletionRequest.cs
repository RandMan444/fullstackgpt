using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ChatGPTAPI
{
    public class CompletionRequest
    {
        [JsonPropertyName("model")]
        public string? Model
        {
            get;
            set;
        }
        [JsonPropertyName("messages")]
        public List<CompletionMessage> Messages
        {
            get;
            set;
        }
        [JsonPropertyName("max_tokens")]
        public int? MaxTokens
        {
            get;
            set;
        }
    }
}
