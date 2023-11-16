using Notifications.Infrastructure.Api.Configurations;

var builder = WebApplication.CreateBuilder(args);
await builder.ConfigureAsync();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
await app.ConfigureAsync();
await app.RunAsync();
