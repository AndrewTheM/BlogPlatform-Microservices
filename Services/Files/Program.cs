using Files.API;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapGet("/files/{fileName}",
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    async ([FromRoute] string fileName) =>
    {
        string filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "StaticFiles",
            "Images",
            fileName
        );

        if (!System.IO.File.Exists(filePath))
            return Results.BadRequest();

        byte[] imageBytes = await System.IO.File.ReadAllBytesAsync(filePath);
        string mimeType = MimeTypes.GetMimeType(fileName);
        return Results.File(imageBytes, mimeType);
    }
);

app.MapPost("/files",
    [DisableRequestSizeLimit]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async (HttpRequest request) =>
    {
        var file = request.Form.Files[0];
        var dispositionHeader = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
        string fileName = dispositionHeader.FileName.Trim('\"');
        string constructedFileName = DateTime.UtcNow.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(fileName);

        string fileFolderPath = Path.Combine("StaticFiles", "Images");
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileFolderPath, constructedFileName);

        using FileStream stream = new(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return new { LocalPath = constructedFileName };
    }
);


app.Run();
