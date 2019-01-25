using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageV2.Migrations
{
    public partial class Intial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ParkedVehicle",
                nullable: true,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ParkedVehicle",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
