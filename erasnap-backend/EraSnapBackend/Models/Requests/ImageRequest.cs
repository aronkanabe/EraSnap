namespace EraSnapBackend.Models.Requests;

public record ImageRequest(string Image, string? CustomPrompt, Guid? PromptId);