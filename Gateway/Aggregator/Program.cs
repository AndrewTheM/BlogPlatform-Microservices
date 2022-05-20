using Aggregator.Services;
using Aggregator.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IPostService, PostService>(config =>
{
    string apiUrl = builder.Configuration["ApiSettings:PostsUrl"];
    config.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<ICommentService, CommentService>(config =>
{
    string apiUrl = builder.Configuration["ApiSettings:CommentsUrl"];
    config.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();


app.Run();
