using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace BlogPlatform.UI.Services;

public class FileService : IFileService
{
    private const long MaxFileSize = 10000000;
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

    public async Task<string> PublishFile(IBrowserFile file)
    {
        using var formContent = new MultipartFormDataContent();
        using var streamContent = new StreamContent(file.OpenReadStream(MaxFileSize));
        formContent.Add(streamContent, "\"files\"", file.Name);

        await _apiClient.EnsureAuthorizationHeader();
        var response = await _apiClient.HttpClient.PostAsync("", formContent);
        using var contentStream = await response.Content.ReadAsStreamAsync();

        var result = await JsonSerializer.DeserializeAsync<FileUploadResult>(contentStream,
            options: new() { PropertyNameCaseInsensitive = true });

        return result.LocalPath;
    }
}
