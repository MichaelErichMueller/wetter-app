using WeatherBackend.GraphQL;
using WeatherBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services
    .AddGraphQLServer()
    .AddQueryType<WeatherQuery>();

var app = builder.Build();

app.UseCors();
app.MapGraphQL();
app.Run();
