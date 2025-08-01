using System.IO;
using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SIIGPP.Datos;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

var dataProtectionPath = Path.Combine(Directory.GetCurrentDirectory(), "DataProtection-Keys");

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(dataProtectionPath))
    .SetApplicationName("SIIGPP.PC");

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContextSIIGPP>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Conexion"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Todos", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
});

var jwtKey = builder.Configuration["Jwt:Key"];

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

var app = builder.Build();
app.Urls.Add(builder.Configuration["HOST_URL"]);

app.UseHsts();
app.UseCors("Todos");
app.UseHttpMetrics();
app.MapMetrics();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();