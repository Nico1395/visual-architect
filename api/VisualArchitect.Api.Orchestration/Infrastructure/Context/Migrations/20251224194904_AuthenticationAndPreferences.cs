using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class AuthenticationAndPreferences : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "authentication");

            migrationBuilder.EnsureSchema(
                name: "preferences");

            migrationBuilder.CreateTable(
                name: "identity",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: false),
                    display_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    avatar_url = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oauth_provider",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oauth_provider", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "preference",
                schema: "preferences",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    default_value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_preference", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "oauth_identity",
                schema: "authentication",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    external_id = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    provider_id = table.Column<int>(type: "integer", nullable: false),
                    identity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oauth_identity", x => x.id);
                    table.ForeignKey(
                        name: "FK_oauth_identity_identity_identity_id",
                        column: x => x.identity_id,
                        principalSchema: "authentication",
                        principalTable: "identity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_oauth_identity_oauth_provider_provider_id",
                        column: x => x.provider_id,
                        principalSchema: "authentication",
                        principalTable: "oauth_provider",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_preference",
                schema: "preferences",
                columns: table => new
                {
                    identity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    preference_id = table.Column<int>(type: "integer", nullable: false),
                    value = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_identity_preference", x => new { x.identity_id, x.preference_id });
                    table.ForeignKey(
                        name: "FK_identity_preference_preference_preference_id",
                        column: x => x.preference_id,
                        principalSchema: "preferences",
                        principalTable: "preference",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.InsertData(
                schema: "preferences",
                table: "preference",
                columns: new[] { "id", "default_value", "key" },
                values: new object[,]
                {
                    { 1, "light", "theme" },
                    { 2, "en", "language" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_identity_preference_preference_id",
                schema: "preferences",
                table: "identity_preference",
                column: "preference_id");

            migrationBuilder.CreateIndex(
                name: "IX_oauth_identity_identity_id",
                schema: "authentication",
                table: "oauth_identity",
                column: "identity_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_oauth_identity_provider_id",
                schema: "authentication",
                table: "oauth_identity",
                column: "provider_id");

            migrationBuilder.CreateIndex(
                name: "IX_oauth_provider_key",
                schema: "authentication",
                table: "oauth_provider",
                column: "key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "identity_preference",
                schema: "preferences");

            migrationBuilder.DropTable(
                name: "oauth_identity",
                schema: "authentication");

            migrationBuilder.DropTable(
                name: "preference",
                schema: "preferences");

            migrationBuilder.DropTable(
                name: "identity",
                schema: "authentication");

            migrationBuilder.DropTable(
                name: "oauth_provider",
                schema: "authentication");
        }
    }
}
