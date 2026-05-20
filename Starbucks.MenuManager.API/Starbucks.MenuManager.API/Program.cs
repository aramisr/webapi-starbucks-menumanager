using Starbucks.MenuManager.API.Extensions;
using Starbucks.MenuManager.API.Application;
using Starbucks.MenuManager.API.Persistence;

var builder = WebApplication.CreateBuilder(args);
var environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
//builder.Services.AddSwagger();

var app = builder.Build();

//// configure the Http request pipeline
//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
//    app.UseSwagger();
//    app.UseSwaggerUI(c =>
//    {
//        // build a swagger endpoint for each discovered API version

//        foreach (var description in provider.ApiVersionDescriptions)
//        {
//            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
//        }
//    });
//}

await app.ApplyMigration(environment);

// Configure the HTTP request pipeline. 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
