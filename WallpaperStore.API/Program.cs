using Microsoft.EntityFrameworkCore;
using WallpaperStore.Application.Services;
using WallpaperStore.DataAccess;
using WallpaperStore.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddScoped<IWallpapersRepository, WallpapersRepository>();
builder.Services.AddScoped<IWallpapersService, WallpapersService>();

builder.Services.AddDbContext<WallpaperStoreDbCOntext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(WallpaperStoreDbCOntext)));
    });

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
