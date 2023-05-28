namespace BlogPlatform.UI.Services.Contracts;

public interface ITagService
{
    Task<List<string>> GetTagNames();
}
