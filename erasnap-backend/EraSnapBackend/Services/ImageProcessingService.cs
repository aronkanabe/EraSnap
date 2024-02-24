using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using EraSnapBackend.Services.Models;

namespace EraSnapBackend.Services;

public class ImageProcessingService : IImageProcessingService
{
    private readonly string _goApiKey;
    private readonly string _dallE3ImageGenerationUrl;
    private readonly string _faceSwapAsyncUrl;
    private readonly string _faceSwapFetchUrl;
    
    public ImageProcessingService(IConfiguration configuration)
    {
        var goApiSettings = configuration.GetSection("GoApiSettings");
        _dallE3ImageGenerationUrl = goApiSettings.GetValue<string>("DallE3ImageGenerationUrl") ??
                                    throw new Exception("DallE3ImageGenerationUrl is missing.");
        _faceSwapAsyncUrl = goApiSettings.GetValue<string>("FaceSwapAsyncUrl") ??
                            throw new Exception("FaceSwapAsyncUrl is missing.");
        _faceSwapFetchUrl = goApiSettings.GetValue<string>("FaceSwapFetchUrl") ??
                            throw new Exception("FaceSwapFetchUrl is missing.");

        _goApiKey = Environment.GetEnvironmentVariable("GO_API_KEY") ??
                        throw new Exception("GO_API_KEY environment variable is missing.");
    }
    
    public async Task<IEnumerable<GeneratedImage>> GenerateImage(string prompt,
        string model = IImageProcessingService.DefaultGenerationModel,
        int n = IImageProcessingService.DefaultNumberOfImagesPerRequest,
        string resolution = IImageProcessingService.DefaultImageResolution,
        string quality = IImageProcessingService.DefaultImageQuality)
    {
        var request = new GenerateImageRequest(model, prompt, n, resolution, quality);
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _goApiKey);
        var response = await httpClient.PostAsJsonAsync(_dallE3ImageGenerationUrl, request);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadFromJsonAsync<GenerateImageResponse>();
            if (responseBody != null) return await DownloadAndConvertImageToBase64(responseBody.Data);
        }
        else
        {
            throw new Exception("Something bad happened with generating the image.");
        }
        return new List<GeneratedImage>();
    }

    public async Task<GeneratedImage> SwapFace(string targetImageUrl, string swapImageUrl)
    {
        var request = new FaceSwapImageTaskRequest
        {
            TargetImageUrl = targetImageUrl,
            SwapImageUrl = swapImageUrl
        };

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-Key", _goApiKey);
        
        var response = await httpClient.PostAsJsonAsync(_faceSwapAsyncUrl, request);

        if (!response.IsSuccessStatusCode) throw new Exception("Something bad happened with the image face swap.");
        var responseBody = await response.Content.ReadFromJsonAsync<FaceSwapImageTaskResponse>();
        if (responseBody is null || responseBody.Code != 200)
        {
            throw new Exception($"Wrong response code received from face swapping tool. Message: {responseBody?.Message}");   
        }

        foreach (var res in FetchFaceSwapStatus(responseBody.Data.TaskId, 5, 60))
        {
            if (res is null) break;
                
            switch (res.Data.Status)
            {
                case "success":
                    return new GeneratedImage
                    {
                        ImageInBase64 = res.Data.Image
                    };
                case "failed":
                case "retry":
                    throw new Exception($"Face swap failed. Message: {res.Message}");
            }
        }
        throw new Exception("Something bad happened with the image face swap.");   
    }

    private IEnumerable<FaceSwapImageFetchResponse?> FetchFaceSwapStatus(string taskId, int intervalSeconds, int durationSeconds)
    {
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("X-API-Key", _goApiKey);

        var request = new FaceSwapImageFetchRequest
        {
            TaskId = taskId
        };
        
        var clock = new Stopwatch();
        clock.Start();
        while (clock.Elapsed.Seconds < durationSeconds)
        {
            var response = httpClient.PostAsJsonAsync(_faceSwapFetchUrl, request).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                yield return response.Content.ReadFromJsonAsync<FaceSwapImageFetchResponse>().GetAwaiter().GetResult();
                
            }

            if (response.StatusCode is HttpStatusCode.InternalServerError or HttpStatusCode.NotFound)
            {
                yield return null;
            }
            Task.Delay(intervalSeconds * 1000).Wait();
        }
    }
    
    private static async Task<IEnumerable<GeneratedImage>> DownloadAndConvertImageToBase64(
        IEnumerable<GenerateImageResponseEntry> entries)
    {
        var downloadTasks = entries.Select(DownloadAndConvertImageToBase64).ToList();
        return await Task.WhenAll(downloadTasks);
    }

    private static async Task<GeneratedImage> DownloadAndConvertImageToBase64(GenerateImageResponseEntry entry)
    {
        using var httpClient = new HttpClient();
        var byteArray = await httpClient.GetByteArrayAsync(entry.Url);
        return new GeneratedImage
        {
            RevisedPrompt = entry.RevisedPrompt,
            ImageInBase64 = Convert.ToBase64String(byteArray)
        };
    }
}