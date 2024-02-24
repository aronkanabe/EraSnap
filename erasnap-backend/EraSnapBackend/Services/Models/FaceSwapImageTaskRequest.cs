using System.Text.Json.Serialization;

namespace EraSnapBackend.Services.Models;

public class FaceSwapImageTaskRequest
{
    [JsonPropertyName("target_image")]
    public required string TargetImageUrl { get; set; }
    [JsonPropertyName("swap_image")]
    public required string SwapImageUrl { get; set; }
}