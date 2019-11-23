using Microsoft.EntityFrameworkCore.Migrations;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    public partial class AddedIGroupAdminField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsGroupAdmin",
                table: "Member",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGroupAdmin",
                table: "Member");
        }
    }
}
