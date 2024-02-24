namespace EraSnapBackend.Models.Responses;

public record PromptResponse(Guid Id, string Name, byte[] Image);