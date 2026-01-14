using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualArchitect.Api.Orchestration.Infrastructure.Context.Migrations
{
    /// <inheritdoc />
    public partial class AddedApplicationDesign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "application_design");

            migrationBuilder.CreateTable(
                name: "design_project",
                schema: "application_design",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    identity_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description_payload = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_design_project", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "design_task",
                schema: "application_design",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    project_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<long>(type: "bigint", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description_payload = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_design_task", x => x.id);
                    table.ForeignKey(
                        name: "FK_design_task_design_project_project_id",
                        column: x => x.project_id,
                        principalSchema: "application_design",
                        principalTable: "design_project",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "design",
                schema: "application_design",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    task_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description_payload = table.Column<string>(type: "character varying(4096)", maxLength: 4096, nullable: true),
                    type = table.Column<int>(type: "integer", nullable: false),
                    payload = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_design", x => x.id);
                    table.ForeignKey(
                        name: "FK_design_design_task_task_id",
                        column: x => x.task_id,
                        principalSchema: "application_design",
                        principalTable: "design_task",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_design_task_id",
                schema: "application_design",
                table: "design",
                column: "task_id");

            migrationBuilder.CreateIndex(
                name: "IX_design_task_project_id_number",
                schema: "application_design",
                table: "design_task",
                columns: new[] { "project_id", "number" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "design",
                schema: "application_design");

            migrationBuilder.DropTable(
                name: "design_task",
                schema: "application_design");

            migrationBuilder.DropTable(
                name: "design_project",
                schema: "application_design");
        }
    }
}
