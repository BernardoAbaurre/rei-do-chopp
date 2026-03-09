using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReiDoChopp.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UserActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                schema: "RDC",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                schema: "RDC",
                table: "AspNetUsers");
        }
    }
}
