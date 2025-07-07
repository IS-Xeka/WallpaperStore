using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Configurations;

internal class WallpapersConfigurtion : IEntityTypeConfiguration<WallpaperEntity>
{
    public void Configure(EntityTypeBuilder<WallpaperEntity> builder)
    {
        builder.HasKey(w => w.Id);
        builder.Property(w => w.Url)
            .IsRequired();
        builder.Property(w => w.Title)
            .IsRequired();
        builder.Property(w => w.Description)
            .IsRequired();
    }
}
