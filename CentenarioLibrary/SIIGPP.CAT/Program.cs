using System;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using SIIGPP.CAT.Hubs;
using SIIGPP.Datos;
using Prometheus;

var dataProtectionPath = Path.Combine(Directory.GetCurrentDirectory(), "DataProtection-Keys");
var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];


builder.Services.AddDataProtection().PersistKeysToFileSystem(new DirectoryInfo(dataProtectionPath)).SetApplicationName("SIIGPP.CAT");
builder.Services.AddControllers();
builder.Services.AddSignalR();
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



var app = builder.Build();
app.Urls.Add(builder.Configuration["HOST_URL"]);
//app.Urls.Add(builder.Configuration["http://0.0.0.0:44394"]);
app.UseHsts();
app.UseCors("Todos");
app.UseHttpsRedirection();
app.UseHttpMetrics();
app.MapMetrics();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<TurnoHub>("/turnoHub");


var carpetasPath = Path.Combine(app.Environment.ContentRootPath, "Carpetas");
if (!Directory.Exists(carpetasPath))
{
    Directory.CreateDirectory(carpetasPath);
}

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Carpetas")),
    RequestPath = "/Carpetas"
});

app.Run();