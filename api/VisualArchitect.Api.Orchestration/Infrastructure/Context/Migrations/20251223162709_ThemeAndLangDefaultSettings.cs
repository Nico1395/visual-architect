using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class ThemeAndLangDefaultSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "preferences",
                table: "setting",
                columns: new[] { "id", "default_value", "key" },
                values: new object[,]
                {
                    { 1, "light", "theme" },
                    { 2, "en", "language" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "preferences",
                table: "setting",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "preferences",
                table: "setting",
                keyColumn: "id",
                keyValue: 2);
        }
    }
}
