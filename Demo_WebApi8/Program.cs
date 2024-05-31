var builder = WebApplication.CreateBuilder(args);

//----------- Add services to the container.

// Register Cross-Origin Requests (CORS) Policy Service
const string myCORSPolicyNAME = "myCorsPolicyName";
string[] allowedOrigins =  {
    "https://localhost:7250",       // self-URL if needed
    "https://localhost:7224",       // to show JS Client to the WeatherApi
    "https://*.demo.com",
    "https://localhost:7267/"       // to show WebApiClient from Demo_Banking project to the WeatherApi
};
builder.Services
    .AddCors(options =>
    {
        options.AddPolicy(name: myCORSPolicyNAME,
                          policy =>
                          {
                              policy.WithOrigins(allowedOrigins)
                                    .AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowCredentials();
                              // OR .AllowAnyOrigin()
                              // OR .WithHeaders(HeaderNames.ContentType, "x-custom-header", ...)
                              // OR .WithMethods("GET", "POST", "PUT", ...)
                          });
    });


builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//----------- Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Setup the Cross-Origin Requests (CORS) Policy Middleware
app.UseCors(policyName: myCORSPolicyNAME);

app.UseAuthorization();

app.MapControllers();

app.Run();
