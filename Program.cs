using System.Reflection;
using GeoGuardian.Data;
using GeoGuardian.Interfaces;
using GeoGuardian.Middlewares;
using GeoGuardian.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

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
});

builder.Services.AddDbContext<GeoGuardianContext>(opt =>
    opt.UseOracle(builder.Configuration.GetConnectionString("Oracle")));

builder.Services.AddScoped<IRiskAreaService, RiskAreaService>();
builder.Services.AddScoped<ISensorService, SensorService>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlertService, AlertService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseGlobalException();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "GeoGuardian v1");

    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();