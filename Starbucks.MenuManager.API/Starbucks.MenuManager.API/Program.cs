using Starbucks.MenuManager.API.Extensions;
using Starbucks.MenuManager.API.Persistence;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddPersistence(builder.Configuration);

var app = builder.Build();

await app.ApplyMigration(environment);

// Configure the HTTP request pipeline. 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
