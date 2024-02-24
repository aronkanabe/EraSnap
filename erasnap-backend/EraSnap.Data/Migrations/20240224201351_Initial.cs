using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EraSnap.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Prompt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Text = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ExampleImagePath = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    UserPrompt = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prompt", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PromptId = table.Column<Guid>(type: "uuid", nullable: false),
                    Path = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Image_Prompt_PromptId",
                        column: x => x.PromptId,
                        principalTable: "Prompt",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Prompt",
                columns: new[] { "Id", "ExampleImagePath", "Name", "Text", "UserPrompt" },
                values: new object[,]
                {
                    { new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"), "0b2942ec-93ac-4c23-be54-1389ed3a0c6e_image.jpg", "Prompt 3", "test prompt", false },
                    { new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"), "bf16162b-279a-40a5-b3ca-bed7748e68e5_image.jpg", "Prompt 2", "test prompt", false },
                    { new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"), "ecfe5afa-ed76-4472-b38d-5ba2e0fe5591_image.jpg", "Prompt 1", "test prompt", false }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Image_PromptId",
                table: "Image",
                column: "PromptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Prompt");
        }
    }
}
