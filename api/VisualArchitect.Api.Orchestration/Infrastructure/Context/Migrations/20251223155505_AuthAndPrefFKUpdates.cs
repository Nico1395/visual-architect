using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class AuthAndPrefFKUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_identity_setting_setting_id",
                schema: "preferences",
                table: "identity_setting",
                column: "setting_id");

            migrationBuilder.AddForeignKey(
                name: "FK_identity_setting_setting_setting_id",
                schema: "preferences",
                table: "identity_setting",
                column: "setting_id",
                principalSchema: "preferences",
                principalTable: "setting",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_identity_setting_setting_setting_id",
                schema: "preferences",
                table: "identity_setting");

            migrationBuilder.DropIndex(
                name: "IX_identity_setting_setting_id",
                schema: "preferences",
                table: "identity_setting");
        }
    }
}
