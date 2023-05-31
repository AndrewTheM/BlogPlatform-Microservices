using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace BlogPlatform.UI.Services;

public class FileService : IFileService
{
    private const long MaxFileSize = 10_000_000;
    private const string FileUrl = "http://localhost:8010/files";

    private readonly IApiClient _apiClient;

    public FileService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public string GetImageUrl(string fileName)
    {
        if (Uri.IsWellFormedUriString(fileName, UriKind.Absolute))
            return fileName;

        return $"{FileUrl}/{fileName}";
    }

    public async Task<string> GetImageBase64StringAsync(IBrowserFile image)
    {
        if (image is null)
            return string.Empty;

        var imageStream = image.OpenReadStream(MaxFileSize);
        using var ms = new MemoryStream();
        await imageStream.CopyToAsync(ms);

        var base64Image = Convert.ToBase64String(ms.ToArray());
        return base64Image;
    }

    public Task<string> PublishFileAsync(IBrowserFile file)
    {
        var formContent = new MultipartFormDataContent();
        var streamContent = new StreamContent(file.OpenReadStream(MaxFileSize));
        formContent.Add(streamContent, "\"files\"", file.Name);
        return SendFileContentAsync(formContent);
    }
    
    public Task<string> PublishFileAsync(string fileName, byte[] fileBytes)
    {
        var formContent = new MultipartFormDataContent();
        var memoryStream = new MemoryStream(fileBytes);
        var streamContent = new StreamContent(memoryStream);
        formContent.Add(streamContent, "\"files\"", fileName);
        return SendFileContentAsync(formContent);
    }

    private async Task<string> SendFileContentAsync(MultipartFormDataContent formContent)
    {
        await _apiClient.EnsureAuthorizationHeader();
        var response = await _apiClient.HttpClient.PostAsync("", formContent);
        using var contentStream = await response.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<FileUploadResult>(contentStream,
            options: new() { PropertyNameCaseInsensitive = true });

        return result.LocalPath;
    }
}
