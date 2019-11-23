using Microsoft.EntityFrameworkCore.Migrations;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    public partial class ChangedEventNameType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Event",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Name",
                table: "Event",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
