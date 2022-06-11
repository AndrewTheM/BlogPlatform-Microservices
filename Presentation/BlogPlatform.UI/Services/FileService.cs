using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;

namespace BlogPlatform.UI.Services;

public class FileService : IFileService
{
    protected const long MaxFileSize = 10000000;

    private readonly IApiClient _apiClient;

    public FileService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public string GetImageUrl(string fileName)
    {
        if (Uri.IsWellFormedUriString(fileName, UriKind.Absolute))
        {
            return fileName;
        }

        string fileUrl = _apiClient.HttpClient.BaseAddress.AbsoluteUri;
        return $"{fileUrl}/{fileName}";
    }

    public async Task<string> PublishFile(IBrowserFile file)
    {
        using MultipartFormDataContent formContent = new();
        using StreamContent streamContent = new(file.OpenReadStream(MaxFileSize));
        formContent.Add(streamContent, "\"files\"", file.Name);

        await _apiClient.EnsureAuthorizationHeader();
        var response = await _apiClient.HttpClient.PostAsync("", formContent);
        using var contentStream = await response.Content.ReadAsStreamAsync();
        var result = await JsonSerializer.DeserializeAsync<FileUploadResult>(contentStream, options: new()
        {
            PropertyNameCaseInsensitive = true
        });

        return result.LocalPath;
    }
}
