using EraSnap.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EraSnap.Data;

public sealed class EraSnapDbContext(DbContextOptions<EraSnapDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Image>()
            .HasOne<Prompt>(x => x.Prompt)
            .WithMany(x => x.Images)
            .HasForeignKey(x => x.PromptId);

        modelBuilder.Entity<Image>()
            .Property(x => x.Path)
            .HasMaxLength(254);

        modelBuilder.Entity<Prompt>()
            .Property(x => x.Text)
            .HasMaxLength(5000);
    }
}