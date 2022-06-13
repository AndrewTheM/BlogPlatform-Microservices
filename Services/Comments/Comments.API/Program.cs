using Comments.API;
using Comments.DataAccess.Database.Contracts;


var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);


var app = builder.Build();

var databaseGenerator = app.Services.GetRequiredService<IGenerator>();
await databaseGenerator.GenerateAsync("CommentsDB.sql");

startup.Configure(app, app.Environment);


app.Run();
