namespace EraSnap.Data.Entities;

public class Image(Guid id, Guid promptId, string path)
{
    public Guid Id { get; init; } = id;
    public Guid PromptId { get; init; } = promptId;
    public virtual Prompt? Prompt { get; set; }
    public string Path { get; init; } = path;
}