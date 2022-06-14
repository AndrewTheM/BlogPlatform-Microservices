using IdentityServer;


var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.InitializeDatabase(app);
startup.Configure(app, app.Environment);


app.Run();
