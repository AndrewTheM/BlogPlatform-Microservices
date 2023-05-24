using Microsoft.Extensions.Configuration;
using Posts.BusinessLogic.DTO.Requests;
using Posts.BusinessLogic.DTO.Responses;
using Posts.BusinessLogic.Services.Contracts;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;
using System.Text.Json;

namespace Posts.BusinessLogic.Services;

public class ContentService : IContentService
{
    private readonly string _urlTemplate;
    private readonly HttpClient _httpClient;

    public ContentService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        var section = configuration.GetRequiredSection("ContentManager");
        _urlTemplate = $"{section["Endpoint"]}/{{0}}?api-version={section["Version"]}";
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", section["ApiKey"]);
    }

    public async Task<List<string>> CheckTextContentAsync(string content)
    {
        var url = string.Format(_urlTemplate, "text:analyze");
        var request = new ContentManagerTextRequest { Text = content };
        var json = JsonSerializer.Serialize(request);

        var httpMessage = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new(url),
            Content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json)
        };

        var response = await _httpClient.SendAsync(httpMessage);
        var contentResponse = await response.Content.ReadFromJsonAsync<ContentManagerResponse>();
        var categories = contentResponse.CategoryResults
            .Where(cr => cr.Severity > 0)
            .Select(cr => cr.Category)
            .ToList();

        return categories;
    }
}
