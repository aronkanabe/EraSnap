﻿// <auto-generated />
using System;
using EraSnap.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EraSnap.Data.Migrations
{
    [DbContext(typeof(EraSnapDbContext))]
    [Migration("20240225024037_RealPromptsAdded")]
    partial class RealPromptsAdded
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("EraSnap.Data.Entities.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<Guid>("PromptId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("PromptId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("EraSnap.Data.Entities.Prompt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ExampleImagePath")
                        .HasMaxLength(254)
                        .HasColumnType("character varying(254)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<bool>("UserPrompt")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Prompt");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"),
                            ExampleImagePath = "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg",
                            Name = "Középkor",
                            Text = "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.",
                            UserPrompt = false
                        },
                        new
                        {
                            Id = new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"),
                            ExampleImagePath = "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg",
                            Name = "Második világháború",
                            Text = "Create a colored, realistic portrait of a World War II soldier donning a Hungarian uniform. Position the soldier centrally within the composition. The soldier, a caucasian male in his late twenties, with a mustache, displays a look of solemn determination. The blurred backdrop mirrors the chaotic tumult of the battlefield, with the ominous gray smoke rising, earthen trenches dug in haste, and the distant silhouette of war machinery. The texture and details of the uniform, facial features, and battlefield should be rendered in a high degree of realism, reflecting the gravity of the wartime period. Please make sure there is no text or any unrelated artifacts on the image. \n",
                            UserPrompt = false
                        },
                        new
                        {
                            Id = new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"),
                            ExampleImagePath = "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg",
                            Name = "Oszmán birodalom",
                            Text = "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.",
                            UserPrompt = false
                        });
                });

            modelBuilder.Entity("EraSnap.Data.Entities.Image", b =>
                {
                    b.HasOne("EraSnap.Data.Entities.Prompt", "Prompt")
                        .WithMany("Images")
                        .HasForeignKey("PromptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Prompt");
                });

            modelBuilder.Entity("EraSnap.Data.Entities.Prompt", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
