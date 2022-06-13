using Aggregator.Mapping;
using Aggregator.Services;
using Aggregator.Services.Contracts;
using CommentGrpcClient = BlogPlatform.Shared.GRPC.Protos.CommentGrpc.CommentGrpcClient;
using PostGrpcClient = BlogPlatform.Shared.GRPC.Protos.PostGrpc.PostGrpcClient;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpcClient<PostGrpcClient>(opts =>
{
    string apiUrl = builder.Configuration["GrpcSettings:PostsUrl"];
    opts.Address = new Uri(apiUrl);
});

builder.Services.AddGrpcClient<CommentGrpcClient>(opts =>
{
    string apiUrl = builder.Configuration["GrpcSettings:CommentsUrl"];
    opts.Address = new Uri(apiUrl);
});

builder.Services.AddTransient<IPostService, PostService>();
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
