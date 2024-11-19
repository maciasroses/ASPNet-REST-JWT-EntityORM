using DotNetEnv;
using System.Text;
using api_rest_cs.Data;
using api_rest_cs.Helpers;
using api_rest_cs.Services;
using api_rest_cs.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuración de servicios
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Configuración del middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Cargar variables de entorno
    Env.Load();

    // Swagger
    services.AddOpenApi();

    // Controladores
    services.AddControllers();

    // AutoMapper
    services.AddAutoMapper(typeof(AutoMapperProfiles));

    // Repositorios
    services.AddScoped<IBookRepository, BookRepository>();
    services.AddScoped<IUserRepository, UserRepository>();

    // Servicios
    services.AddScoped<BookService>();
    services.AddScoped<UserService>();
    services.AddSingleton<JwtService>();

    // DbContext
    var connectionString = $"Host={Environment.GetEnvironmentVariable("DB_HOST")};" +
                            $"Port={Environment.GetEnvironmentVariable("DB_PORT")};" +
                            $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
                            $"Username={Environment.GetEnvironmentVariable("DB_USER")};" +
                            $"Password={Environment.GetEnvironmentVariable("DB_PASS")}";
    services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connectionString));

    // Configuración de JWT
    var jwtKey = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new ArgumentNullException("JWT_KEY");
    var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER");
    var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE");
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi(); // Swagger solo en desarrollo
    }

    app.UseHttpsRedirection();

    // Autenticación y autorización
    app.UseAuthentication();
    app.UseAuthorization();

    // Mapear controladores
    app.MapControllers();
}
