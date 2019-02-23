using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GarageV2.Migrations
{
    public partial class VehicleType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkedVehicleType",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                table: "ParkedVehicle",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ParkedVehicle",
                maxLength: 8,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleTypeId",
                table: "ParkedVehicle",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VehicleType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ParkedVehicle_VehicleTypeId",
                table: "ParkedVehicle",
                column: "VehicleTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkedVehicle_VehicleType_VehicleTypeId",
                table: "ParkedVehicle",
                column: "VehicleTypeId",
                principalTable: "VehicleType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkedVehicle_VehicleType_VehicleTypeId",
                table: "ParkedVehicle");

            migrationBuilder.DropTable(
                name: "VehicleType");

            migrationBuilder.DropIndex(
                name: "IX_ParkedVehicle_VehicleTypeId",
                table: "ParkedVehicle");

            migrationBuilder.DropColumn(
                name: "VehicleTypeId",
                table: "ParkedVehicle");

            migrationBuilder.AlterColumn<string>(
                name: "RegNo",
                table: "ParkedVehicle",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "ParkedVehicle",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 8,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParkedVehicleType",
                table: "ParkedVehicle",
                nullable: false,
                defaultValue: 0);
        }
    }
}
