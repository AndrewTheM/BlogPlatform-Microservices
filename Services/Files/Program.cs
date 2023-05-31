using BlogPlatform.Shared.Logging;
using Files.API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net.Http.Headers;


var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(SerilogHelpers.Configure);

const string scheme = JwtBearerDefaults.AuthenticationScheme;
builder.Services.AddAuthentication(scheme)
    .AddJwtBearer(scheme, options =>
    {
        options.Authority = builder.Configuration["IdentityUrl"];
        options.Audience = "filesApi";
        options.RequireHttpsMetadata = false;
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();


app.MapGet("/files/{fileName}",
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    async ([FromRoute] string fileName) =>
    {
        var mimeType = MimeTypes.GetMimeType(fileName);
        var directory = mimeType.StartsWith("image")
            ? "Images"
            : "Misc";

        var filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "StaticFiles",
            directory,
            fileName
        );

        if (!File.Exists(filePath))
            return Results.BadRequest();

        var imageBytes = await File.ReadAllBytesAsync(filePath);
        return Results.File(imageBytes, mimeType);
    }
);

app.MapPost("/files",
    [Authorize(Roles = "Admin, Author")]
    [DisableRequestSizeLimit]
    [ProducesResponseType(StatusCodes.Status200OK)]
    async (HttpRequest request) =>
    {
        var file = request.Form.Files[0];
        var dispositionHeader = ContentDispositionHeaderValue.Parse(file.ContentDisposition);
        var fileName = dispositionHeader.FileName.Trim('\"');
        var constructedFileName = DateTime.UtcNow.ToString("yyyyMMddhhmmssfff") + Path.GetExtension(fileName);
        
        var mimeType = MimeTypes.GetMimeType(fileName);
        var directory = mimeType.StartsWith("image")
            ? "Images"
            : "Misc";

        var fileFolderPath = Path.Combine("StaticFiles", directory);
        Directory.CreateDirectory(fileFolderPath);

        var filePath = Path.Combine(Directory.GetCurrentDirectory(), fileFolderPath, constructedFileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return new { LocalPath = constructedFileName };
    }
);


app.Run();
