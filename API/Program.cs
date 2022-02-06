using MediatR;
using UserManagementService.Core.Interfaces;
using UserManagementService.Core.Services;
using UserManagementService.Core.UserAggregate.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());
builder.Services.AddSingleton<IUserService, UserService>();

builder.Services.AddMediatR(typeof(GetAllUsersHandler));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();