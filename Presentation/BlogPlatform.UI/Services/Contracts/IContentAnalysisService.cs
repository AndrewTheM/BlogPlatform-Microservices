using BlogPlatform.UI.Models;

namespace BlogPlatform.UI.Services.Contracts;

public interface IContentAnalysisService
{
    Task<ContentAnalysisResult> AnalyzePostAsync(PostAnalysisRequest request);
    Task<ContentAnalysisResult> AnalyzeCommentAsync(CommentAnalysisRequest request);
}
