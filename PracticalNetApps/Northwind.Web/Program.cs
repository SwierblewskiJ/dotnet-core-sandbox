#region www server configure
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddNorthwindContext();

var app = builder.Build();
#endregion

#region http and pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.Use(async (HttpContext context, Func<Task> next) =>
{

    RouteEndpoint? rep = context.GetEndpoint() as RouteEndpoint;

    if(rep is not null)
    {
        WriteLine($"Endpoint name: {rep.DisplayName}");
        WriteLine($"Endpoint's path pattern: {rep.RoutePattern.RawText}");
    }
    if(context.Request.Path == "/bonjour")
    {
        await context.Response.WriteAsync("Bonjour!");
        return;
    }
    await next();
});

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

#endregion

app.MapRazorPages();
app.MapGet("/hi", () => $"Actual environment is {app.Environment.EnvironmentName}");

app.Run();
WriteLine("Instruction executed after server is down");
