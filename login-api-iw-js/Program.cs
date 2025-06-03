using login_api_iw_js.Data;
using login_api_iw_js.LoginApi_Mappers;
using login_api_iw_js.LoginApi_Middleware;
using login_api_iw_js.LoginApi_Repositories;
using login_api_iw_js.LoginApi_Services;
using login_api_iw_js.Services.Implementations.Administrador;
using login_api_iw_js.Services.Implementations.Usuario;
using login_api_iw_js.Services.Interfaces.Administrador;
using login_api_iw_js.Services.Interfaces.Usuario;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Security.Claims;
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        RoleClaimType = ClaimTypes.Role
    };
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://crud-iw-js-front.onrender.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Swagger
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

// Obtener la cadena desde variable de entorno
var connectionString = Environment.GetEnvironmentVariable("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
    throw new InvalidOperationException("La variable de entorno 'DefaultConnection' no está configurada.");

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

// Conexión manual a DB para procedimientos (si se usa)
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(connectionString));

// Controladores
builder.Services.AddControllers();

// Repositorios y Servicios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ReporteHitoMayorMenorPuntaje>();
builder.Services.AddScoped<HitoService>();



// Servicios y repositorios de usuario
builder.Services.AddScoped<IProgresoService, ProgresoService>();
builder.Services.AddScoped<IObjetivoUsuarioService, ObjetivoUsuarioService>();
builder.Services.AddScoped<IRecomendacionService, RecomendacionService>();

// Servicios de administración
builder.Services.AddScoped<IAsigancionService, AsignacionService>();
builder.Services.AddScoped<ITemaService, TemaService>();
builder.Services.AddScoped<IObjetivoService, ObjetivoService>();
builder.Services.AddScoped<IHitoService, HitoService>();

// Autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
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
