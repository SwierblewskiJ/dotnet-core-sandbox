using EntityModels;

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
app.UseHttpsRedirection();
#endregion

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapRazorPages();
app.MapGet("/hi", () => $"Actual environment is {app.Environment.EnvironmentName}");

app.Run();
WriteLine("Instruction executed after server is down");
