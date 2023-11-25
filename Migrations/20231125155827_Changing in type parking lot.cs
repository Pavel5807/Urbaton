using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbaton.Migrations
{
    /// <inheritdoc />
    public partial class Changingintypeparkinglot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Charging",
                table: "ParkingLots");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Charging",
                table: "ParkingLots",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
