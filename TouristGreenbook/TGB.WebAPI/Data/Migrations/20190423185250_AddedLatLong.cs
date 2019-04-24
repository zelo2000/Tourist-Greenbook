using Microsoft.EntityFrameworkCore.Migrations;

namespace TGB.WebAPI.Data.Migrations
{
    public partial class AddedLatLong : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Places",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Longtitude",
                table: "Places",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Places");

            migrationBuilder.DropColumn(
                name: "Longtitude",
                table: "Places");
        }
    }
}
