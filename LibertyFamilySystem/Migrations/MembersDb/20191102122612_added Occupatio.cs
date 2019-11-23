using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    public partial class addedOccupatio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Member",
                newName: "MemberId");

            migrationBuilder.AddColumn<int>(
                name: "OcccupationId",
                table: "Member",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Occupation",
                columns: table => new
                {
                    OccupationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupation", x => x.OccupationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_OcccupationId",
                table: "Member",
                column: "OcccupationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Occupation_OcccupationId",
                table: "Member",
                column: "OcccupationId",
                principalTable: "Occupation",
                principalColumn: "OccupationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Occupation_OcccupationId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "Occupation");

            migrationBuilder.DropIndex(
                name: "IX_Member_OcccupationId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "OcccupationId",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "MemberId",
                table: "Member",
                newName: "Id");
        }
    }
}
