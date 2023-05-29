using BlogPlatform.UI.Helpers.Contracts;
using BlogPlatform.UI.Models;
using BlogPlatform.UI.Services.Contracts;

namespace BlogPlatform.UI.Services;

public class ContentAnalysisService : IContentAnalysisService
{
    private readonly IApiClient _apiClient;

    public ContentAnalysisService(IApiClient apiClient, HttpClient httpClient)
    {
        _apiClient = apiClient;
        _apiClient.HttpClient = httpClient;
    }

    public Task<ContentAnalysisResult> AnalyzePostAsync(PostAnalysisRequest request)
    {
        return _apiClient.SendPostApiRequestWithResultAsync<PostAnalysisRequest, ContentAnalysisResult>(
            "analyze/post", request);
    }

    public Task<ContentAnalysisResult> AnalyzeCommentAsync(CommentAnalysisRequest request)
    {
        return _apiClient.SendPostApiRequestWithResultAsync<CommentAnalysisRequest, ContentAnalysisResult>(
            "analyze/comment", request);
    }
}
