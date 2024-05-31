var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// -- register 2 middleware required for Static Website content.
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();
