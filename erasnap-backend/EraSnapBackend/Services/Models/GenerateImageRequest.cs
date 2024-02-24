namespace EraSnapBackend.Services.Models;

public record GenerateImageRequest(string Model, string Prompt, int N, string Size, string Quality);