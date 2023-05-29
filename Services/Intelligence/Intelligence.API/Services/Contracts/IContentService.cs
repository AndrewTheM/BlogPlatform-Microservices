namespace Intelligence.API.Services.Contracts;

public interface IContentService
{
    Task CheckTextContentAsync(string content, string contentSource);
    Task CheckImageContentAsync(string content, string contentSource);
}
