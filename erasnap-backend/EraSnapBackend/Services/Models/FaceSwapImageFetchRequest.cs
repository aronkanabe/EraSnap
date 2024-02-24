using System.Text.Json.Serialization;

namespace EraSnapBackend.Services.Models;

public class FaceSwapImageFetchRequest
{
    [JsonPropertyName("task_id")]
    public required string TaskId { get; set; }
}
