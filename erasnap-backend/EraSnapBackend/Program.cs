using System.Text.Json;
using EraSnap.Data;
using EraSnap.Data.Entities;
using EraSnapBackend.Dtos;
using EraSnapBackend.Models.Requests;
using EraSnapBackend.Models.Responses;
using EraSnapBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddSingleton<IImageProcessingService, ImageProcessingService>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<EraSnapDbContext>(optionsBuilder =>
    {
        optionsBuilder.UseInMemoryDatabase("DevelopmentDB");
    });
}
else
{
    builder.Services.AddDbContext<EraSnapDbContext>(optionsBuilder =>
        optionsBuilder.UseNpgsql(
            $"User ID={Environment.GetEnvironmentVariable("DATABASE_USER")};" +
            $"Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};" +
            $"Host={Environment.GetEnvironmentVariable("DATABASE_HOST")};" +
            $"Port={Environment.GetEnvironmentVariable("DATABASE_PORT")};" +
            $"Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};Pooling=true;"));    
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.Services.GetService<EraSnapDbContext>()?.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

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

app.MapPost("/image-processing", async ([FromBody] ImageProcessingRequestDto dto,
    IImageProcessingService imageProcessingService) =>
{
    var res = await imageProcessingService.GenerateImage(dto.Prompt);
    return Results.Ok(res);
});

app.MapPost("/image-generation", async ([FromBody] ImageProcessingRequestDto dto,
    IImageProcessingService imageProcessingService) =>
{
    var res = (await imageProcessingService.GenerateImage(dto.Prompt)).FirstOrDefault();
    return res is null ? Results.Ok() : Results.File(Convert.FromBase64String(res.ImageInBase64),
        fileDownloadName: "result.png");
});

app.MapGet("faceswap-test", async (IImageProcessingService imageProcessingService) =>
{
    var image = await imageProcessingService.SwapFace(
        "https://i.ibb.co/7bdcL20/img-Jwf-Xdq-TDk1-GLYb-Hx0-JGPCq-X1.png",
        "https://i.ibb.co/59Xk1N7/jennifer-aniston-murder-mystery-2-premiere-033023-1-ee3f91c303c544069a095b83a2e7a4a1.jpg");
    return Results.Ok(image);
});

app.Run();