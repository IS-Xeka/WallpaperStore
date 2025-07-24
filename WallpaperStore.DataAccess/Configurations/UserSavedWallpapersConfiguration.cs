using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Configurations;

public class UserSavedWallpapersConfiguration : IEntityTypeConfiguration<UserSavedWallpapersEntity>
{
    public void Configure(EntityTypeBuilder<UserSavedWallpapersEntity> builder)
    {
        builder.HasKey(sw => new { sw.UserId, sw.WallpaperId });

        builder.Property(sw => sw.UserId).IsRequired();
        builder.Property(sw => sw.WallpaperId).IsRequired();
        builder.Property(sw => sw.IsFavorite).HasDefaultValue(false);

        builder.HasOne(us => us.UserEntity)
            .WithMany(u => u.SavedWallpapers)
            .HasForeignKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(us => us.WallpaperEntity)
            .WithMany()
            .HasForeignKey(w => w.WallpaperId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}
