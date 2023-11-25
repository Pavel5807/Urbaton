using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbaton.Migrations
{
    /// <inheritdoc />
    public partial class AddedAccountandFeedbacktables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    DeviceId = table.Column<Guid>(type: "uuid", nullable: false),
                    LotType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.DeviceId);
                });

            migrationBuilder.CreateTable(
                name: "ParkingFidback",
                columns: table => new
                {
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Creation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingFidback", x => new { x.ParkingId, x.UserId, x.Creation });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ParkingFidback");
        }
    }
}
