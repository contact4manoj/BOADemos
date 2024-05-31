/*******************
 * 
 *  This project demonstrates implementation of JWT based Token Authentication on Web API
 *  
 *  NOTE: 
 *      To generate a developer user JWT token to test out our API,
 *      make sure that the application is not running (coz., because appsettings file will be update).
 *      
 *      Then, open the Developer Command Prompt or the Terminal Window
 *      and navigate into the Project folder.
 *      
 *      Run the following command:
 *          >dotnet user-jwts create
 *      
 *      Apart from the JWT token churned out, then user-jwts tool will add a new section for
 *      the Authentication schema in the appsettings.development.json file!
 *      With the details of the Schema, the Issuer & the Audience!
 *      
 *      To list the data about the token(s)
 *          >dotnet user-jwts list
 *          >dotnet user-jwts print <tokenId>
 *      
 *      For the complete list of options:
 *          >dotnet user-jwts --help
 *      
 *      To remove a specific JWT
 *          >dotnet user-jwts remove <id>
 *      
 *      You can check out the contents of the JWT Token at https://jwt.io/
 *      
 *      To test the API open PostMan, and in the Headers add:
 *      KEY: "Authorization"
 *      VALUE: "Bearer xxxxxxxx"
 *      
 *********/

// Add the following Nuget Packages:
//      for providing support for JWT Bearer Tokens
//          Microsoft.AspNetCore.Authentication.JwtBearer
//      for providing support to apply Authentication Prompt for JWT Bearer token in SwaggerUI
//          Swashbuckle.AspNetCore.Filters

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register the Authentication Services (with the JwtBearerToken schema with the default configuration)
builder.Services.AddAuthentication()
                .AddJwtBearer();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "My Secured API using JWT Authentication",
        Description = "The Demo API",
        TermsOfService = new Uri("https://xyz.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://xyz.com/contact"),
            Email = "support@xyz.com"
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://xyz.com/license")
        }
    });

    // Setup the Authentication and Authorization middleware for SwaggerUI
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//  app.UseAuthentication();  (implicitly done.  No need explicitly define in .NET 7.0 and above)
app.UseAuthorization();

app.MapControllers();

app.Run();
