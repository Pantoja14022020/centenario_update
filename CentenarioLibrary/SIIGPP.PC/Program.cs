using System.IO;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SIIGPP.Datos;
using Prometheus;
using CentenarioLibrary;

var builder = WebApplication.CreateBuilder(args);
var dataProtectionPath = Path.Combine(Directory.GetCurrentDirectory(), "DataProtection-Keys");
var jwtKey = builder.Configuration["Jwt:Key"];

builder.Services.AddMemoryCache();
builder.Services.AddControllers();
builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(dataProtectionPath)).SetApplicationName("SIIGPP.PC");
builder.Services.AddDbContext<DbContextSIIGPP>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion")));
builder.Services.AddCors(options => { options.AddPolicy("Todos", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()); });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            RoleClaimType = builder.Configuration["Jwt:RoleClaimType"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

// Configuración de Redis Cache
/*builder.Configuration.AddEnvironmentVariables();

var redisConfig = builder.Configuration.GetSection("Redis")["Configuration"];
var redisInstance = builder.Configuration.GetSection("Redis")["InstanceName"];

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConfig; // "redis:6379"
    options.InstanceName = redisInstance; // "MyApp:"
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins("http://localhost:8080", "http://192.168.100.23:8080")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddSingleton<CentenarioLibrary.ResponseCacheMiddleware>();*/

var app = builder.Build();

//app.Urls.Add(builder.Configuration["HOST_URL"]);
//app.UseHttpsRedirection();
app.UseHsts();
app.UseCors("Todos");
app.UseHttpMetrics();
app.UseAuthentication();
app.UseAuthorization();
//app.UseMiddleware<ResponseCacheMiddleware>();
app.UseCors("AllowFrontend");
app.MapControllers();
app.MapMetrics();

app.Run();