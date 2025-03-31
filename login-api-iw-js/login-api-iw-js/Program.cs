using login_api_iw_js.LoginApi_Mappers;
using login_api_iw_js.LoginApi_Middleware;
using login_api_iw_js.LoginApi_Repositories;
using login_api_iw_js.LoginApi_Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; // Esquema de autenticación predeterminado
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // Esquema de desafío
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // Si estás en desarrollo, puede que quieras deshabilitar HTTPS
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Clave secreta para la validación
    };
});
// Primero, configurar los servicios
builder.Services.AddControllers();
builder.Services.AddOpenApi();  // Este método ya agrega el servicio para Swagger
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<IDbConnection>(sp =>
    new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUsuarioService, UsuarioService>();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


// Configurar la autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
});
builder.Services.AddSwaggerGen(
    options =>
    {
        options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "LoginApi",
            Version = "v1",
            Description = "Api para la gestion de usuarios y autenticación"
        });
    }
    );
// Agregar Swagger para la documentación de la API
 
// Luego, construir la aplicación
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger y Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login API v1");
        c.RoutePrefix = string.Empty;  // Swagger UI estará disponible en la raíz
    });
}


// Configuración del pipeline de solicitud

app.UseHttpsRedirection();

// Configuración de middleware
app.UseMiddleware<JwtMiddleware>(); // Middleware JWT para validar el token
app.UseAuthentication();           // Middleware de autenticación basado en el token
app.UseAuthorization();            // Middleware para autorizar basado en roles

app.MapControllers();

app.Run();
