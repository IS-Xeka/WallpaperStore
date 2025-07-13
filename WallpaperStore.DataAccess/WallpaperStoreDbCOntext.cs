using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess;

public class WallpaperStoreDbContext : DbContext
{
    public WallpaperStoreDbContext(DbContextOptions<WallpaperStoreDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<WallpaperEntity> Wallpapers {get; set;}
    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
