using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EraSnap.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewPrompts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"),
                column: "Name",
                value: "Török birodalom");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"),
                column: "Name",
                value: "Második világháború");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"),
                column: "Name",
                value: "Középkor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("0b2942ec-93ac-4c23-be54-1389ed3a0c6e"),
                column: "Name",
                value: "Prompt 3");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("bf16162b-279a-40a5-b3ca-bed7748e68e5"),
                column: "Name",
                value: "Prompt 2");

            migrationBuilder.UpdateData(
                table: "Prompt",
                keyColumn: "Id",
                keyValue: new Guid("ecfe5afa-ed76-4472-b38d-5ba2e0fe5591"),
                column: "Name",
                value: "Prompt 1");
        }
    }
}
