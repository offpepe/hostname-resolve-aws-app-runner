using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All });


app.MapGet("/ips", GetIps)
    .WithName("Ips");

app.Run();
return;

Dictionary<string, string?> GetIps(HttpContext context) => new ()
{
    {"httpForwardedFor", context.Request.Headers["Http-X-Forwarded-For"].ToString()},
    {"forwarededFor" , context.Request.Headers["X-Forwarded-For"].ToString()},
    {"forwardedProto" , context.Request.Headers["X-Forwarded-Proto"].ToString()},
    {"localIp" , context.Connection.LocalIpAddress?.ToString()},
    {"remoteIp" , context.Connection.RemoteIpAddress?.ToString()},
    {"servar_remoteip", context.GetServerVariable("REMOTE_ADDR")},
    {"public_ip", context.GetServerVariable("")}
};

