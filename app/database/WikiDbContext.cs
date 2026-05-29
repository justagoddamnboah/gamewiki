using gamewiki.net.model;
using Microsoft.EntityFrameworkCore;

namespace gamewiki.net.database;

public class WikiDbContext(DbContextOptions<WikiDbContext> options) : DbContext(options) {
    public DbSet<Dev> Devs { get; set; }
    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        modelBuilder.Entity<Dev>(entity => {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });

        modelBuilder.Entity<Game>(entity => {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
        });
    }
}