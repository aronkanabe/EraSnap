using System.Text.Json.Serialization;

namespace EraSnapBackend.Services.Models;

public record GenerateImageResponseEntry
{
    [JsonPropertyName("revised_prompt")]
    public string RevisedPrompt { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
}
