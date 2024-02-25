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
                new Prompt(Guid.Parse("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"), "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.", "Középkor",exampleImagePath: "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg"),
                new Prompt(Guid.Parse("bf16162b-279a-40a5-b3ca-bed7748e68e5"), "Create a colored, realistic portrait of a World War II soldier donning a Hungarian uniform. Position the soldier centrally within the composition. The soldier, a caucasian male in his late twenties, with a mustache, displays a look of solemn determination. The blurred backdrop mirrors the chaotic tumult of the battlefield, with the ominous gray smoke rising, earthen trenches dug in haste, and the distant silhouette of war machinery. The texture and details of the uniform, facial features, and battlefield should be rendered in a high degree of realism, reflecting the gravity of the wartime period. Please make sure there is no text or any unrelated artifacts on the image. \n", "Második világháború",exampleImagePath: "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg"),
                new Prompt(Guid.Parse("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"), "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.", "Oszmán birodalom",exampleImagePath: "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg"),
                // new Prompt(Guid.Parse("6d0332ac-3d90-4f96-a917-d6cb6e11a90d"), "Generate a vivid, colored photorealistic image of a male Samurai descent from the era of the Shogunate, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is not text on the image.", "Samurai",exampleImagePath: "6d0332ac-3d90-4f96-a917-d6cb6e11a90d_image.jpg")
            });
    }
}