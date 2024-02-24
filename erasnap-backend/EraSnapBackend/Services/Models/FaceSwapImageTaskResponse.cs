using System.Text.Json.Serialization;

namespace EraSnapBackend.Services.Models;

public class FaceSwapImageTaskResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public FaceSwapImageTaskResponseData Data { get; set; }
}

public class FaceSwapImageTaskResponseData
{
    [JsonPropertyName("task_id")]
    public string TaskId { get; set; }
} 