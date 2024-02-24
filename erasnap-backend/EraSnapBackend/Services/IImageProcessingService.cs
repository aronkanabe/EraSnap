using EraSnapBackend.Services.Models;

namespace EraSnapBackend.Services;

public interface IImageProcessingService
{
    protected const string DefaultGenerationModel = "dall-e-3";
    protected const int DefaultNumberOfImagesPerRequest = 1;
    protected const string DefaultImageResolution = "1024x1024";
    protected const string DefaultImageQuality = "standard";
    
    Task<IEnumerable<GeneratedImage>> GenerateImage(string prompt, string model = DefaultGenerationModel,
        int n = DefaultNumberOfImagesPerRequest, string resolution = DefaultImageResolution, string quality = DefaultImageQuality);

    Task<GeneratedImage> SwapFace(string targetImageUrl, string swapImageUrl);
}