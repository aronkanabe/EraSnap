namespace EraSnap.Data.Entities;

public class Prompt(Guid id, string text, bool userPrompt = false)
{
    public Guid Id { get; init; } = id;
    public string Text { get; init; } = text;
    public IEnumerable<Image>? Images { get; set; }
    public bool UserPrompt { get; init; } = userPrompt;
}