using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// app.MapGet("/", () => "Hello World!");

app.Run(async context =>
{
    // await context.Response.WriteAsync("Hello world");

    StringBuilder sb = new StringBuilder();
    sb.Append("<html><body>");
    sb.Append("<h1>Hello world</h1>");
    sb.Append("</body></html>");

    await context.Response.WriteAsync(sb.ToString());
});

app.Run();
