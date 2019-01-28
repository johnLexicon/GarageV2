using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageV2.Migrations
{
    public partial class ParkedVehicleChangesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "ParkedVehicle");

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ParkedVehicleType",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "ParkedVehicleType",
                table: "ParkedVehicle");

            migrationBuilder.AddColumn<byte[]>(
                name: "TimeStamp",
                table: "ParkedVehicle",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: 0);
        }
    }
}
