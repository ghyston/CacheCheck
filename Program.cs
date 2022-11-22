var builder = WebApplication.CreateBuilder(args);
builder.Services.AddResponseCaching();
builder.Services.AddControllers();

var app = builder.Build();
app.UseResponseCaching();
app.MapGet("/", () => "open \"https://localhost:5001/create/{count_of_stars}\" to generate picture");
app.MapControllers();
app.Run();