using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TGB.WebAPI.Data.Migrations
{
    public partial class ChangeTablePlacesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkTimeStart",
                table: "Places",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "WorkTimeFinish",
                table: "Places",
                nullable: false,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkTimeStart",
                table: "Places",
                nullable: false,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<DateTime>(
                name: "WorkTimeFinish",
                table: "Places",
                nullable: false,
                oldClrType: typeof(TimeSpan));
        }
    }
}
