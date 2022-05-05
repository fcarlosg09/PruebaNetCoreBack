using Aplicacion.Implementaciones.Repositorios;
using Aplicacion.Implementaciones.Servicios;
using Aplicacion.Mediators.Marcas;
using Dominio.Interfaces.Repositorios;
using Dominio.Interfaces.Servicios;
using FluentValidation.AspNetCore;
using Infraestructura.EntityFrameworkCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;
builder.Services.AddDbContext<VehiculosDbContext>(opt =>
{
    opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});


//// Dependencia MediatR
builder.Services.AddMediatR(typeof(MarcaCrear.Manejador).Assembly);

//// Dependencias AutoMapper
builder.Services.AddAutoMapper(typeof(MarcaCrear.Manejador));

builder.Services.AddControllers()
.AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<MarcaCrear>())
.ConfigureApiBehaviorOptions(opt =>
{
    opt.InvalidModelStateResponseFactory = c =>
    {
        var errors = c.ModelState.Values.Where(v => v.Errors.Count > 0)
                            .SelectMany(v => v.Errors).FirstOrDefault()!.ErrorMessage;

        return new BadRequestObjectResult(new
        {
            mensaje = errors
        });
    };
});

#region Repositorios
builder.Services.AddScoped<IHttpConfiguration, HttpConfiguration>();
builder.Services.AddScoped<IMarcasRepository, MarcasRepository>();
builder.Services.AddScoped<IVehiculosRepository, VehiculosRepository>();
builder.Services.AddScoped<IUsuariosRepository, UsuariosRepository>();
builder.Services.AddHttpClient("Clientes", config =>
{
    config.BaseAddress = new Uri("https://reqres.in/api/users");
});
#endregion

builder.Services.AddHttpContextAccessor();

#region Cors
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin();
    });
});
#endregion

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ManejadorErroresMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
