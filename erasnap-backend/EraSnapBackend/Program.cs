using EraSnap.Data;
using EraSnap.Data.Entities;
using EraSnapBackend.Models.Requests;
using EraSnapBackend.Models.Responses;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EraSnapDbContext>(optionsBuilder =>
    optionsBuilder.UseNpgsql(
        $"User ID={Environment.GetEnvironmentVariable("DATABASE_USER")};Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};Host={Environment.GetEnvironmentVariable("DATABASE_HOST")};Port={Environment.GetEnvironmentVariable("DATABASE_PORT")};Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};Pooling=true;"));

var app = builder.Build();

app.Services.GetService<EraSnapDbContext>()?.Database.EnsureCreated();
// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

string GenerateImage(string prompt)
{
    // ai calls
    return "data";
}

string UploadToBlob(Guid guid, string data)
{
    // upload to blob
    return "path";
}

string GetImageFromBlob(string imagePath)
{
    // download/ stream from blob
    return "";
}

app.MapPost("/image", async ([FromBody] ImageRequest request, EraSnapDbContext dbContext) =>
    {
        if (request is {PromptId: null, CustomPrompt: null} or { PromptId: not null, CustomPrompt: not null })
            return Results.BadRequest("Either PromptId or CustomPrompt should be defined");
        
        Guid promptId;
        Prompt? prompt = null;
        
        if (request.PromptId.HasValue)
        {
            promptId = request.PromptId.Value;
            prompt = await dbContext.Set<Prompt>().FindAsync(request.PromptId);
        }
        
        if (request.CustomPrompt is not null)
        {
            promptId = Guid.NewGuid();
            prompt = new Prompt(promptId, request.CustomPrompt, true);
        }
        
        
        // upload to blob
        var imageId = Guid.NewGuid();
        var imageData = GenerateImage(prompt!.Text);
        var path = UploadToBlob(imageId, imageData);

        var image = new Image(Guid.NewGuid(), prompt.Id, path);
        dbContext.Add(image);

        await dbContext.SaveChangesAsync();
        return Results.Ok(new ImageResponse(image.Id, imageData));
    }).Produces<ImageResponse>()
    .WithOpenApi();

app.MapGet("/prompts", (EraSnapDbContext dbContext) =>
{
    return dbContext.Set<Prompt>().AsAsyncEnumerable();
}).Produces<IEnumerable<PromptResponse>>()
.CacheOutput(cachePolicyBuilder => cachePolicyBuilder.Cache().Expire(TimeSpan.FromSeconds(60)));

app.MapGet("/images/{id:guid}", async ([FromRoute] Guid id, EraSnapDbContext dbContext) =>
    {
        var image = await dbContext.Set<Image>().FindAsync(id);
        if (image is null)
        {
            return Results.BadRequest("Image not found");
        }
        var imageData = GetImageFromBlob(image.Path);
        return Results.Ok(new ImageResponse(image.Id, imageData));
    }).Produces<ImageResponse>()
    .WithOpenApi();

app.Run();