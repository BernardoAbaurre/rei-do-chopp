using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReiDoChopp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PrintControl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PrintControls",
                schema: "RDC",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<int>(type: "int", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrintControls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrintControls_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "RDC",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrintControls_UserId",
                schema: "RDC",
                table: "PrintControls",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrintControls",
                schema: "RDC");
        }
    }
}
