using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class changeid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp");

            migrationBuilder.DropColumn(
                name: "Narasumber",
                table: "P2kp");

            migrationBuilder.AlterColumn<int>(
                name: "NarsumID",
                table: "P2kp",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp",
                column: "NarsumID",
                principalTable: "Narsum",
                principalColumn: "NarsumID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp");

            migrationBuilder.AlterColumn<int>(
                name: "NarsumID",
                table: "P2kp",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Narasumber",
                table: "P2kp",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp",
                column: "NarsumID",
                principalTable: "Narsum",
                principalColumn: "NarsumID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
