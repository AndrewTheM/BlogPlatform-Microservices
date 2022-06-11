using Microsoft.AspNetCore.Components.Forms;

namespace BlogPlatform.UI.Services.Contracts;

public interface IFileService
{
    string GetImageUrl(string fileName);

    Task<string> PublishFile(IBrowserFile file);
}
