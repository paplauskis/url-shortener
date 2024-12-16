using Microsoft.EntityFrameworkCore;
using url_shortener.Data.Context;
using url_shortener.Data.Repositories;
using url_shortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<UrlEntityRepository>();
builder.Services.AddScoped<UrlAccessLogRepository>();
builder.Services.AddScoped<UrlEntityService>();
builder.Services.AddScoped<UrlAccessLogService>();
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();