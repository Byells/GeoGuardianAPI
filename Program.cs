using System.Reflection;
using System.Text;
using GeoGuardian.Data;
using GeoGuardian.Interfaces;
using GeoGuardian.Middlewares;
using GeoGuardian.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Carregar configurações específicas para desenvolvimento
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true);
}

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SchemaGeneratorOptions.UseInlineDefinitionsForEnums = true;
    c.UseInlineDefinitionsForEnums();

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title       = "GeoGuardian API",
        Version     = "v1",
        Description = "API REST do GeoGuardian para monitoramento de enchentes, deslizamentos e rompimento de barragens."
    });

    // Configuração de segurança para o Swagger (autenticação com JWT)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Digite: Bearer {seu token aqui}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuração do contexto de banco de dados
builder.Services.AddDbContext<GeoGuardianContext>(opt =>
{
    var connectionString = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Oracle");
    opt.UseOracle(connectionString);
});

// Adicionar serviços
builder.Services.AddScoped<IRiskAreaService, RiskAreaService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlertService, AlertService>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

// Configuração do CORS para permitir qualquer origem (útil para Swagger)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

// Configuração do JWT
var jwtSecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY") ?? builder.Configuration["JwtSettings:SecretKey"];



if (string.IsNullOrEmpty(jwtSecretKey))
{
    throw new InvalidOperationException("JWT Secret Key is not configured. Please set the 'JWT_SECRET_KEY' environment variable or 'JwtSettings:SecretKey' in appsettings.");
}

var jwtKeyBytes = Convert.FromBase64String(jwtSecretKey);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer           = true,
            ValidIssuer              = builder.Configuration["JwtSettings:Issuer"],
            ValidateAudience         = true,
            ValidAudience            = builder.Configuration["JwtSettings:Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey         = new SymmetricSecurityKey(jwtKeyBytes),
            ValidateLifetime         = true
        };
    });

// Configuração da porta - pode ser ajustada por ambiente local ou variável de ambiente
var port = builder.Configuration["Port"] ?? Environment.GetEnvironmentVariable("PORT") ?? "8081"; 
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Redirecionamento automático para o Swagger
app.MapGet("/8081", () => Results.Redirect("/swagger"));

// Aplicar middleware de exceção global
app.UseGlobalException();

// Configuração do Swagger (agora carregado em todos os ambientes)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    // Certifique-se de que o Swagger está usando o caminho correto
    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "GeoGuardian v1");
});

// Segurança
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

// Mapear os controladores
app.MapControllers();

// Iniciar a aplicação
app.Run();
