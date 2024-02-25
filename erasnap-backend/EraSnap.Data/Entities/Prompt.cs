namespace EraSnap.Data.Entities;

public class Prompt
{
    public Prompt(Guid id, string manText, string femaleText, string name, string? exampleImagePath = null, bool userPrompt = false)
    {
        Id = id;
        ManText = manText;
        FemaleText = femaleText;
        Name = name;
        ExampleImagePath = exampleImagePath;
        UserPrompt = userPrompt;
    }

    public Guid Id { get; set; }
    public string ManText { get; set; }
    public string FemaleText { get; set; }
    public string Name { get; set; }
    public string? ExampleImagePath { get; set; }
    public virtual IEnumerable<Image>? Images { get; set; }
    public bool UserPrompt { get; set; }
}