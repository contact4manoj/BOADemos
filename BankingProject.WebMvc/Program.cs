using BankingProject.WebMvc.Data;
using BankingProject.WebMvc.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

var builder = WebApplication.CreateBuilder(args);

//--- Add services to the container.  (ConfigureServices)

// Register EF Core to connect to SQL Server using the DbContext.
// NOTE: It should be the first step.
const string connectionStringNAME = "DefaultConnectionString";
//var connString 
//    = builder.Configuration.GetConnectionString(connectionStringNAME);
//if(connString is null)
//{
//    throw new InvalidOperationException("connection not found!");
//}
var connString
    = builder.Configuration.GetConnectionString(connectionStringNAME) 
      ?? throw new InvalidOperationException("connection not found");
builder.Services
       .AddDbContext<ApplicationDbContext>(options =>
       {
           // options.UseSqlServer(connString);

           options.UseSqlServer(connString, builderOptions =>
           {
               // Configure the Retry On Failure Policy on every single operation on the database.
               builderOptions.EnableRetryOnFailure(
                   maxRetryCount: 5,
                   maxRetryDelay: TimeSpan.FromSeconds(5),
                   errorNumbersToAdd: null);
           });

           // log the SQL Query
           options.LogTo(Console.WriteLine, LogLevel.Information);

           // Enable detailed errors when handling data values exceptions that occur
           // while processing stored query results, mainly occuring due to misconfiguration of entity properties.
           options.EnableDetailedErrors();

       });

// Register the OWIN IdentityService using the custom User model mapped to the DbContext.
builder.Services
       .AddDefaultIdentity<BankingUser>(options =>
       {
           options.Password.RequiredLength = 8;
           options.Password.RequireUppercase = true;
           options.Password.RequireLowercase = true;
           options.Password.RequireNonAlphanumeric = true;

           options.SignIn.RequireConfirmedAccount = true;
       })
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services
       .AddControllersWithViews();

builder.Services
       .AddRazorPages();

// To mitigate Cross-Site Request Forgery (XSRF/CSRF) attacks:
// Register the Service to automatically validate anti-forgery tokens for unsafe non-API HTTP Methods by default
// instead of applying [ValidateAntiForgeryToken] for all non-GET methods.
// NOTE: It is not applied for the HTTP - GET, HEAD, OPTIONS and TRACE methods.
builder.Services
       .AddAntiforgery();


// Register the GDPR Compliant Cookie Consent Service
builder.Services
       .Configure<CookiePolicyOptions>(options =>
        {
            // Determine if user consent for non-essential cookies is needed for a given request.
            options.CheckConsentNeeded = context => true;

            options.MinimumSameSitePolicy = SameSiteMode.None;

            options.ConsentCookieValue = "true";        // customize the cookie consent value.
        });


// Register Swagger/OpenAPI Documentation Generator
// For more information: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register the Logging Provider Service. And map it to the Console Window.
// NOTE: Console is the Default in .NET 8
builder.Services.AddLogging(configureOptions =>
{
    configureOptions.AddConsole(Console.WriteLine);
});

// Register the Custom Email Sender Service.
// -- builder.Services.AddSingleton<IEmailSender, MyEmailSenderService>();

var app = builder.Build();

//--- Configure the HTTP request pipeline.  (Configure)

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // Adding the Swagger Generation Middleware
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Call the (XSRF/CSRF) mitigation middleware
app.UseAntiforgery();

// Call the GDPR Compliance Cookie Consent Policy Middleware
app.UseCookiePolicy();


app.UseRouting();

// Call the OWIN Middleware
// app.UseAuthentication();             // implicitly done in .NET 7 and above
app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

// Custom middleware to seed the database.
app.Use( async (context, next) =>
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        bool dbExists = dbContext.GetService<IDatabaseCreator>().CanConnect();
        if (!dbExists)
        {
            dbContext.Database.EnsureCreated();
        }

        await dbContext.SeedCategoriesAsync();

        // dbContext.Database.Migrate();
    }

    await next.Invoke();
});

app.Run();
