using BlogPlatform.Shared.Logging;
using Comments.API;
using Comments.DataAccess.Database.Contracts;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

builder.Host.UseSerilog(SerilogHelpers.Configure);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

var databaseGenerator = app.Services.GetRequiredService<IGenerator>();
await databaseGenerator.GenerateAsync("CommentsDB.sql");

startup.Configure(app, app.Environment);


app.Run();
