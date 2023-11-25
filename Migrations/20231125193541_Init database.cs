using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Urbaton.Migrations
{
    /// <inheritdoc />
    public partial class Initdatabase : Migration
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

            migrationBuilder.CreateTable(
                name: "PlacemarkLookAt",
                columns: table => new
                {
                    Longitude = table.Column<double>(type: "double precision", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: false),
                    Altitude = table.Column<double>(type: "double precision", nullable: false),
                    Range = table.Column<double>(type: "double precision", nullable: false),
                    Tilt = table.Column<double>(type: "double precision", nullable: false),
                    Heading = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacemarkLookAt", x => new { x.Latitude, x.Longitude });
                });

            migrationBuilder.CreateTable(
                name: "Placemarks",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    LookAtLatitude = table.Column<double>(type: "double precision", nullable: false),
                    LookAtLongitude = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Placemarks", x => new { x.Name, x.Description });
                    table.ForeignKey(
                        name: "FK_Placemarks_PlacemarkLookAt_LookAtLatitude_LookAtLongitude",
                        columns: x => new { x.LookAtLatitude, x.LookAtLongitude },
                        principalTable: "PlacemarkLookAt",
                        principalColumns: new[] { "Latitude", "Longitude" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parkings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    SecurityCameras = table.Column<bool>(type: "boolean", nullable: false),
                    PlacemarkName = table.Column<string>(type: "text", nullable: false),
                    PlacemarkDescription = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parkings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parkings_Placemarks_PlacemarkName_PlacemarkDescription",
                        columns: x => new { x.PlacemarkName, x.PlacemarkDescription },
                        principalTable: "Placemarks",
                        principalColumns: new[] { "Name", "Description" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParkingLots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    AccessibleEnviroment = table.Column<bool>(type: "boolean", nullable: false),
                    BasePrice = table.Column<double>(type: "double precision", nullable: false),
                    ParkingId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParkingLots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParkingLots_Parkings_ParkingId",
                        column: x => x.ParkingId,
                        principalTable: "Parkings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkingLots_ParkingId",
                table: "ParkingLots",
                column: "ParkingId");

            migrationBuilder.CreateIndex(
                name: "IX_Parkings_PlacemarkName_PlacemarkDescription",
                table: "Parkings",
                columns: new[] { "PlacemarkName", "PlacemarkDescription" });

            migrationBuilder.CreateIndex(
                name: "IX_Placemarks_LookAtLatitude_LookAtLongitude",
                table: "Placemarks",
                columns: new[] { "LookAtLatitude", "LookAtLongitude" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "ParkingFidback");

            migrationBuilder.DropTable(
                name: "ParkingLots");

            migrationBuilder.DropTable(
                name: "Parkings");

            migrationBuilder.DropTable(
                name: "Placemarks");

            migrationBuilder.DropTable(
                name: "PlacemarkLookAt");
        }
    }
}
