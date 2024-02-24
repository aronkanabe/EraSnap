namespace EraSnapBackend.Services.Models;

public class GenerateImageResponse
{
    public int Created { get; set; }
    
    public List<GenerateImageResponseEntry> Data { get; set; } = new();
}