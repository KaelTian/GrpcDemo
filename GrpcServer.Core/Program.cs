using GrpcService.Core.Services;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
// ÅäÖÃ Kestrel ·þÎñÆ÷
builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Loopback, 5000);
    options.Listen(IPAddress.Loopback, 5001, listenOptions =>
    {
        listenOptions.UseHttps("test.pfx", "1qaz2wsxE");
    });
});
var app = builder.Build();


// Configure the HTTP request pipeline.
//app.MapGrpcService<GreeterService>();
app.MapGrpcService<PersonService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();
