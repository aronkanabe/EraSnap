using System.Text.Json;
using System.Text;
using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using EraSnap.Data;
using EraSnap.Data.Entities;
using EraSnapBackend.Dtos;
using EraSnapBackend.Models.Requests;
using EraSnapBackend.Models.Responses;
using EraSnapBackend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EraSnapDbContext>(optionsBuilder =>
    // optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING") ??
    //                          "User ID=postgres;Password=node-project;Host=localhost;Port=5433;Database=erasnap;Pooling=true;"));
    optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("AZURE_POSTGRESQL_CONNECTIONSTRING") ??
                             $"User ID={Environment.GetEnvironmentVariable("DATABASE_USER")};Password={Environment.GetEnvironmentVariable("DATABASE_PASSWORD")};Host={Environment.GetEnvironmentVariable("DATABASE_HOST")};Port={Environment.GetEnvironmentVariable("DATABASE_PORT")};Database={Environment.GetEnvironmentVariable("DATABASE_NAME")};Pooling=true;"));

var blobUrl = Environment.GetEnvironmentVariable("BLOB_URL");
var blobContainer = Environment.GetEnvironmentVariable("BLOB_CONTAINER");

builder.Services.AddSingleton<IImageProcessingService, ImageProcessingService>();
builder.Services.AddAzureClients(clientBuilder =>
{
    // Register clients for each service
    clientBuilder.AddBlobServiceClient(new Uri(blobUrl!));
    clientBuilder.UseCredential(new DefaultAzureCredential());
});

var app = builder.Build();

app.Services.GetService<EraSnapDbContext>()?.Database.Migrate();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

async Task<byte[]> GenerateImage(BlobServiceClient blobServiceClient, IImageProcessingService imageProcessingService, string prompt, string requestImage)
{
    var requestImageUploadTask =
        UploadToBlob(blobServiceClient, Guid.NewGuid(), Convert.FromBase64String(requestImage));

    var generatedImageUploadTask = imageProcessingService.GenerateImage(prompt).ContinueWith(x =>
    {
        var generatedImage = x.Result.FirstOrDefault();
        return UploadToBlob(blobServiceClient, Guid.NewGuid(), Convert.FromBase64String(generatedImage!.ImageInBase64));
    }).Unwrap();

    await Task.WhenAll(requestImageUploadTask, generatedImageUploadTask);
    
    var image = await imageProcessingService.SwapFace(
        $"{blobUrl}/{blobContainer}/{generatedImageUploadTask.Result}",
        $"{blobUrl}/{blobContainer}/{requestImageUploadTask.Result}");
    
    return Convert.FromBase64String(image.ImageInBase64);
}

async Task<BlobContainerClient> GetBlobContainerClient(BlobServiceClient blobServiceClient1)
{
    var containers = blobServiceClient1.GetBlobContainersAsync();
    if (!await containers.AnyAsync(x => x.Name == blobContainer))
    {
        await blobServiceClient1.CreateBlobContainerAsync(blobContainer);
    }

    var blobContainerClient = blobServiceClient1.GetBlobContainerClient(blobContainer);
    return blobContainerClient;
}

async Task<string> UploadToBlob(BlobServiceClient blobServiceClient ,Guid guid, byte[] data)
{
    // upload to blob
    var container = await GetBlobContainerClient(blobServiceClient);

    var blobClient = container.GetBlobClient($"{guid.ToString()}_image.jpg");
    await blobClient.UploadAsync(new BinaryData(data));
    return blobClient.Name;
}

async Task<byte[]> GetImageFromBlob(BlobServiceClient blobServiceClient ,string imagePath)
{
    var container = await GetBlobContainerClient(blobServiceClient);

    var blobClient = container.GetBlobClient(imagePath);

    await using var readStream = await blobClient.OpenReadAsync();

    var buffer = new byte[readStream.Length];
    
    var readBytes = await readStream.ReadAsync(buffer);

    return buffer;
}

app.MapPost("/image", async ([FromBody] ImageRequest request, EraSnapDbContext dbContext, BlobServiceClient blobServiceClient, IImageProcessingService imageProcessingService) =>
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
            prompt = new Prompt(promptId, request.CustomPrompt, "User Prompt", null,userPrompt: true);
        }

        dbContext.Set<Prompt>().Add(prompt!);
        
        // upload to blob
        var imageId = Guid.NewGuid();
        var imageData = await GenerateImage(blobServiceClient, imageProcessingService, prompt!.Text, request.Image);
        var path = await UploadToBlob(blobServiceClient, imageId, imageData);

        var image = new Image(imageId, prompt.Id, path);
        dbContext.Add(image);

        await dbContext.SaveChangesAsync();
        return Results.Ok(new ImageResponse(image.Id, imageData));
    }).Produces<ImageResponse>()
    .WithOpenApi();

app.MapGet("/prompts", async (EraSnapDbContext dbContext, BlobServiceClient blobServiceClient) =>
    {
        return await dbContext.Set<Prompt>().Where(x => !x.UserPrompt).AsAsyncEnumerable().SelectAwait(async x =>
        {
            var image = await GetImageFromBlob(blobServiceClient, x.ExampleImagePath!);
            return new PromptResponse(x.Id, x.Name, image);
        }).ToListAsync();
}).Produces<List<PromptResponse>>();

app.MapGet("/images/{id:guid}", async ([FromRoute] Guid id, EraSnapDbContext dbContext, BlobServiceClient blobServiceClient) =>
    {
        var image = await dbContext.Set<Image>().FindAsync(id);
        if (image is null)
        {
            return Results.BadRequest("Image not found");
        }
        var imageData = await GetImageFromBlob(blobServiceClient, image.Path);
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