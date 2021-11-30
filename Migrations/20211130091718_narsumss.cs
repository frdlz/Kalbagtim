using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class narsumss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_P2kp_P2kp_P2kpID1",
                table: "P2kp");

            migrationBuilder.DropIndex(
                name: "IX_P2kp_P2kpID1",
                table: "P2kp");

            migrationBuilder.DropColumn(
                name: "P2kpID1",
                table: "P2kp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "P2kpID1",
                table: "P2kp",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_P2kp_P2kpID1",
                table: "P2kp",
                column: "P2kpID1");

            migrationBuilder.AddForeignKey(
                name: "FK_P2kp_P2kp_P2kpID1",
                table: "P2kp",
                column: "P2kpID1",
                principalTable: "P2kp",
                principalColumn: "P2kpID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
