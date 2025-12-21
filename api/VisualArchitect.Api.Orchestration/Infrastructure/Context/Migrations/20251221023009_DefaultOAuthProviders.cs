using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class DefaultOAuthProviders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "authentication",
                table: "oauth_provider",
                columns: new[] { "id", "key" },
                values: new object[,]
                {
                    { 1, "github" },
                    { 2, "google" },
                    { 3, "microsoft" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "authentication",
                table: "oauth_provider",
                keyColumn: "id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "authentication",
                table: "oauth_provider",
                keyColumn: "id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                schema: "authentication",
                table: "oauth_provider",
                keyColumn: "id",
                keyValue: 3);
        }
    }
}
