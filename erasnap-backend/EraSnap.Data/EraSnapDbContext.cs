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

        modelBuilder.Entity<Prompt>()
            .Property(x => x.ExampleImagePath)
            .HasMaxLength(254);

        modelBuilder.Entity<Prompt>()
            .Property(x => x.Name)
            .HasMaxLength(200);

        modelBuilder.Entity<Prompt>()
            .HasData(new List<Prompt>()
            {
                new Prompt(Guid.Parse("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"), "test prompt", "Középkor",exampleImagePath: "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg"),
                new Prompt(Guid.Parse("bf16162b-279a-40a5-b3ca-bed7748e68e5"), "test prompt", "Második világháború",exampleImagePath: "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg"),
                new Prompt(Guid.Parse("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"), "test prompt", "Török birodalom",exampleImagePath: "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg")
            });
    }
}