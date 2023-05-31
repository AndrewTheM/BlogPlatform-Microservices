using Microsoft.AspNetCore.Components.Forms;

namespace BlogPlatform.UI.Services.Contracts;

public interface IFileService
{
    string GetImageUrl(string fileName);

    Task<string> GetImageBase64StringAsync(IBrowserFile image);

    Task<string> PublishFileAsync(IBrowserFile file);
    Task<string> PublishFileAsync(string fileName, byte[] fileBytes);
}
