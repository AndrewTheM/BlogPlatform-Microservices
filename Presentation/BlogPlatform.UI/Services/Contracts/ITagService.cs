namespace BlogPlatform.UI.Services.Contracts;

public interface ITagService
{
    Task<IEnumerable<string>> GetTagNames();
}
