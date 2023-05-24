namespace Posts.BusinessLogic.Services.Contracts;

public interface IContentService
{
    Task<List<string>> CheckTextContentAsync(string content);
}
