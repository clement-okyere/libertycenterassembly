using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    public partial class AddedCreateDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Event",
                nullable: false,
                defaultValue: new DateTime(2019, 11, 10, 7, 24, 49, 177, DateTimeKind.Local));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Event");
        }
    }
}
