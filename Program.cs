using System.Reflection;
using System.Text;
using System.Security.Cryptography; 
using GeoGuardian.Data;
using GeoGuardian.Interfaces;
using GeoGuardian.Middlewares;
using GeoGuardian.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddDbContext<GeoGuardianContext>(opt =>
{
    var connectionString = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("Oracle");
    opt.UseOracle(connectionString);
});

builder.Services.AddScoped<IRiskAreaService, RiskAreaService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlertService, AlertService>();

builder.Services.AddScoped<JwtService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"];

if (string.IsNullOrEmpty(jwtSecretKey))
{
    throw new InvalidOperationException("JWT Secret Key is not configured. Please set 'JwtSettings:SecretKey' in your appsettings.Development.json (local) or configure as a Secret File (production).");
}

byte[] jwtKeyBytes;
try
{
    jwtKeyBytes = Convert.FromBase64String(jwtSecretKey.Trim());
    Console.WriteLine($"DEBUG: JWT Secret Key from config (length: {jwtSecretKey.Length} chars). Converted to {jwtKeyBytes.Length * 8} bits.");
    if (jwtKeyBytes.Length * 8 < 256)
    {
        Console.WriteLine($"WARNING: Configured JWT Key is less than 256 bits ({jwtKeyBytes.Length * 8} bits). This might cause issues for HS256.");
    }
}
catch (FormatException ex)
{
    throw new InvalidOperationException($"JWT Secret Key in appsettings is not a valid Base64 string: {ex.Message}", ex);
}


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

var port = builder.Configuration["Port"] ?? Environment.GetEnvironmentVariable("PORT") ?? "8081";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

app.UseGlobalException();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint($"/swagger/v1/swagger.json", "GeoGuardian v1");
});

app.UseCors("AllowAll");

app.UseHttpsRedirection(); 
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();