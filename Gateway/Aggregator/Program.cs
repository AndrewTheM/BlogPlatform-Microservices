using Aggregator.Mapping;
using Aggregator.Services;
using Aggregator.Services.Contracts;
using CommentGrpcClient = BlogPlatform.Shared.GRPC.Protos.CommentGrpc.CommentGrpcClient;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IPostService, PostService>(config =>
{
    string apiUrl = builder.Configuration["ApiSettings:PostsUrl"];
    config.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddGrpcClient<CommentGrpcClient>(opts =>
{
    string apiUrl = builder.Configuration["ApiSettings:CommentsUrl"];
    opts.Address = new Uri(apiUrl);
});

builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddAutoMapper(typeof(GrpcMappingProfile));

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
