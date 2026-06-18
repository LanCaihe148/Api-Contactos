using Contactos.Application;
using Contactos.Infrastructure;
using Contactos.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(op =>
{
    op.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
