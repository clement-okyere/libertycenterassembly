using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace LibertyFamilySystem.Migrations.MembersDb
{
    public partial class addedGroupSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Occupation_OcccupationId",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "OcccupationId",
                table: "Member",
                newName: "OccupationId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_OcccupationId",
                table: "Member",
                newName: "IX_Member_OccupationId");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Member",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Group",
                columns: table => new
                {
                    GroupId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.GroupId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_GroupId",
                table: "Member",
                column: "GroupId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Group_GroupId",
                table: "Member",
                column: "GroupId",
                principalTable: "Group",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Occupation_OccupationId",
                table: "Member",
                column: "OccupationId",
                principalTable: "Occupation",
                principalColumn: "OccupationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Member_Group_GroupId",
                table: "Member");

            migrationBuilder.DropForeignKey(
                name: "FK_Member_Occupation_OccupationId",
                table: "Member");

            migrationBuilder.DropTable(
                name: "Group");

            migrationBuilder.DropIndex(
                name: "IX_Member_GroupId",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Member");

            migrationBuilder.RenameColumn(
                name: "OccupationId",
                table: "Member",
                newName: "OcccupationId");

            migrationBuilder.RenameIndex(
                name: "IX_Member_OccupationId",
                table: "Member",
                newName: "IX_Member_OcccupationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Member_Occupation_OcccupationId",
                table: "Member",
                column: "OcccupationId",
                principalTable: "Occupation",
                principalColumn: "OccupationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
