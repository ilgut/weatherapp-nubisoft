using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace weatherapp_nubisoft.Data;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<FavouriteCity> FavouriteCities { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasMany<FavouriteCity>(e => e.favouriteCities)
            .WithOne(e => e.AppUser)
            .HasForeignKey(e => e.AppUserId);
    }
}