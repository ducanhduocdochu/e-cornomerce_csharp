using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Backend.Controllers.Middleware;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var Settings = builder.Configuration;

builder.Services.AddDbContext<DBAppContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 21))));

// Đăng kí docs api
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký controllers
builder.Services.AddControllers();

// Rate limiter
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddInMemoryRateLimiting();

//repositories, services
builder.Services.AddScoped<IKeyTokenService, KeyTokenService>();
builder.Services.AddScoped<IKeyTokenRepository, KeyTokenRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Đăng ký dịch vụ phân quyền

var app = builder.Build();

// Đăng ký các middleware

// app.UseMiddleware<Authentication>();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    // Đăng ký docs api
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseRouting();

// app.UseMiddleware<Authentication>();

app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

// app.MapControllers();

// app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/user/logout"), builder =>
// {
//     builder.UseMiddleware<Authentication>();
// });

app.UseAuthorization();

app.MapControllers(); // Điều này là cần thiết để routing hoạt động

app.Run();


