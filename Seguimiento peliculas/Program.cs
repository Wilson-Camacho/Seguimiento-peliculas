using Microsoft.Extensions.Options;
using Seguimiento_peliculas.Controllers;
using Seguimiento_peliculas.Interface;
using System.Data.SqlTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var sqlConnection = new SQLConfiguration(builder.Configuration.GetConnectionString("MYSQLConnection"));
builder.Services.AddSingleton(sqlConnection);

builder.Services.AddScoped<IClient, ClientRepositorio>();
builder.Services.AddScoped<ISeriePelicula, SeriePeliculaRepositorio>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
