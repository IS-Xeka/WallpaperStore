using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using WallpaperStore.API;
using WallpaperStore.Application.Mapping;
using WallpaperStore.Application.Services;
using WallpaperStore.DataAccess;
using WallpaperStore.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddProgramDependencies(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
