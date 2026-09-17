using EntityModels;
using Microsoft.AspNetCore.Mvc.Formatters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddNorthwindContext();

builder.Services.AddControllers(options =>
{
    WriteLine("By default input formatter:");
    foreach(IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if(mediaFormatter is null)
        {
            WriteLine($" {formatter.GetType().Name}");
        }
        else
        {
            WriteLine(" {0}, media types: {1}", mediaFormatter.GetType().Name, string.Join(", ", mediaFormatter.SupportedMediaTypes));
        }
    }
}).AddXmlDataContractSerializerFormatters().AddXmlSerializerFormatters();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
