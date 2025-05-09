using login_api_iw_js.LoginApi_Mappers;
using login_api_iw_js.LoginApi_Middleware;
using login_api_iw_js.LoginApi_Repositories;
using login_api_iw_js.LoginApi_Services;
using login_api_iw_js.Services.Implementations;
using login_api_iw_js.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ======================
// Configuración de servicios
// ======================

// Autenticación con JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// CORS → debe ir antes de builder.Build()
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200","https://crud-iw-js-front.onrender.com") // Cambia si tu frontend está en otro puerto
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger y OpenAPI
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "LoginApi",
        Version = "v1",
        Description = "Api para la gestión de usuarios y autenticación"
    });
});

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Controladores
builder.Services.AddControllers();

// Repositorio y servicios
//builder.Services.AddScoped<IDbConnection>(sp =>
//    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDbConnection>(sp =>
{
    var connectionString = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTION");

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new InvalidOperationException("La cadena de conexión no está definida en la variable de entorno AZURE_SQL_CONNECTION");
    }

    return new SqlConnection(connectionString);
});



builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IObjetivoService, ObjetivoService>();
builder.Services.AddScoped<IHitoService, HitoService>();


// Autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});

// ======================
// Construir la app
// ======================
var app = builder.Build();

// ======================
// Middleware
// ======================

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())

{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login API v1");
        c.RoutePrefix = "swagger";

    });
}

// CORS
app.UseCors("AllowAngularApp");

app.UseHttpsRedirection();

app.UseMiddleware<JwtMiddleware>(); // Middleware personalizado
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGet("/", context =>
{
    context.Response.Redirect("/swagger");
    return Task.CompletedTask;
});

app.Run();
