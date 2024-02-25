using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EraSnap.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealPromptsAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"),
                columns: new[] { "Name", "Text" },
                values: new object[] { "Oszmán birodalom", "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image." });

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"),
                column: "Text",
                value: "Create a colored, realistic portrait of a World War II soldier donning a Hungarian uniform. Position the soldier centrally within the composition. The soldier, a caucasian male in his late twenties, with a mustache, displays a look of solemn determination. The blurred backdrop mirrors the chaotic tumult of the battlefield, with the ominous gray smoke rising, earthen trenches dug in haste, and the distant silhouette of war machinery. The texture and details of the uniform, facial features, and battlefield should be rendered in a high degree of realism, reflecting the gravity of the wartime period. Please make sure there is no text or any unrelated artifacts on the image. \n");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"),
                column: "Text",
                value: "Generate a vivid, colored photorealistic image of a male Janissary of Ottoman descent from the era of the Ottoman Empire, standing stoically in the center of the frame. He is shot as though through a Canon EOS 5D Mark IV DSLR, with characteristics including f/5.6 aperture, 1/125 second shutter speed, and ISO 100. The background, though blurry due to the camera settings, resembles a war-torn battlefield indicative of that era. Please ensure the details of his attire, weaponry, and surroundings are historically accurate to the time of the Ottoman Empire. Please make sure there is no text or any unrelated artifacts on the image.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"),
                columns: new[] { "Name", "Text" },
                values: new object[] { "Török birodalom", "test prompt" });

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"),
                column: "Text",
                value: "test prompt");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"),
                column: "Text",
                value: "test prompt");
        }
    }
}
