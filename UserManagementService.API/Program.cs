using UserManagementService.API.Endpoints;
using UserManagementService.API.Extensions;
using UserManagementService.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(IEndpointDefinition));
builder.Services.AddCoreServices(builder.Configuration);

var app = builder.Build();
app.UseEndpointDefinitions();

app.Run();