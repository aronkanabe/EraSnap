namespace EraSnapBackend.Services.Models;

public class FaceSwapImageFetchResponse
{
    public int Code { get; set; }
    public string Message { get; set; }
    public FaceSwapImageFetchData Data { get; set; }
}

public class FaceSwapImageFetchData
{
    public string Status { get; set; }
    public string Image { get; set; }
}