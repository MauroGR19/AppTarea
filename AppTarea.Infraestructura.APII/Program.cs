using AppTarea;
using AppTarea.Aplicacion.Interfaces;
using AppTarea.Aplicacion.Servicios;
using AppTarea.Dominio.Interfaces;
using AppTarea.Dominio.Interfaces.Repositorio;
using AppTarea.Dominio.Modelos;
using AppTarea.Infraestructura.APII.Utilities;
using AppTarea.Infraestructura.Datos.Contexto;
using AppTarea.Infraestructura.Datos.Repositorios;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<CustomHeadersFilter>();
builder.Services.AddSingleton<JWToken>();
builder.Services.AddSingleton<HttpContextService>();
builder.Services.AddSingleton<ICustomHeadersValidator, CustomHeadersValidator>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<AplicattionContexto>();
builder.Services.AddTransient<IRepositorioBase<Tarea, Guid>, TareaRepositorio>();
builder.Services.AddTransient<IServicioBase<Tarea, Guid>, TareaServicio>();
builder.Services.AddTransient<AplicattionContexto>();
builder.Services.AddTransient<IRrepositorioLogin<Login>, LoginRepositorio>();
builder.Services.AddTransient<IServicioLogin<Login>, LoginServicio>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen(c =>
{
    var securityScheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
        },
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    };

    c.AddSecurityDefinition("Bearer", securityScheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement() {
        { securityScheme, Array.Empty<string>() }
    });
});
//builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
