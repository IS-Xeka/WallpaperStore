using Microsoft.EntityFrameworkCore;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess;

public class WallpaperStoreDbCOntext : DbContext
{
    public WallpaperStoreDbCOntext(DbContextOptions<WallpaperStoreDbCOntext> options)
        : base(options)
    {

    }
    
    public DbSet<WallpaperEntity> Wallpapers {get; set;}
}
