using BlogPlatform.Shared.Logging;
using IdentityServer;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

builder.Host.UseSerilog(SerilogHelpers.Configure);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.InitializeDatabase(app);
startup.Configure(app, app.Environment);


app.Run();
