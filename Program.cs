using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });


app.MapGet("/ips", GetIps)
    .WithName("GetWeatherForecast")
    .WithOpenApi();

app.Run();
return;

Dictionary<string, string?> GetIps(HttpContext context) => new ()
{
    {"httpForwardedFor", context.Request.Headers["Http-X-Forwarded-For"].ToString()},
    {"forwarededFor" , context.Request.Headers["X-Forwarded-For"].ToString()},
    {"forwardedProto" , context.Request.Headers["X-Forwarded-Proto"].ToString()},
    {"localIp" , context.Connection.LocalIpAddress?.ToString()},
    {"remoteIp" , context.Connection.RemoteIpAddress?.ToString()},
    {"servar_remoteip", context.GetServerVariable("REMOTE_ADDR")}
};

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}