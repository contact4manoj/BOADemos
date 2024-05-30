using BankingProject.WebMvc.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            options.UseSqlServer(connString);
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

// Register Swagger/OpenAPI Documentation Generator
// For more information: https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "areas",
    pattern: "{area}/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();


app.Run();
