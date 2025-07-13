using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WallpaperStore.Core.Models;
using WallpaperStore.DataAccess.Entities;

namespace WallpaperStore.DataAccess.Configurations;

public class UsersConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Name)
            .IsRequired();
        builder.Property(u => u.Email)
            .IsRequired();
        builder.Property(u => u.PasswordHash)
            .IsRequired();
        builder.Property(u => u.RegisterDate)
            .IsRequired();
        builder.Property(u => u.IsPublicProfile)
            .IsRequired()
            .HasDefaultValue(false);
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                dbValue => Email.Create(dbValue)
            )
            .HasMaxLength(100)
            .HasColumnName("Email");



        builder.HasMany(u => u.AddedWallpapers)
            .WithOne(w => w.Owner)
            .HasForeignKey(w => w.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(u => u.SavedWallpapers)
            .WithOne(sw => sw.UserEntity)
            .HasForeignKey(sw => sw.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
