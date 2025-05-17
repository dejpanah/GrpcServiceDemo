using GrpcServices;
using GrpcServices.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapGrpcService<GrpcServices.Services.PersonService>();
 

///app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
