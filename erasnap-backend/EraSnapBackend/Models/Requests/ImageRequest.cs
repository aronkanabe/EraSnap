namespace EraSnapBackend.Models.Requests;

public record ImageRequest(string Image, string Gender, string? CustomPrompt, Guid? PromptId);