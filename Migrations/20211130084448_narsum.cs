using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class narsum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NarsumID",
                table: "P2kp",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "P2kpID1",
                table: "P2kp",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Narsum",
                columns: table => new
                {
                    NarsumID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Narasumber = table.Column<string>(nullable: true),
                    Keterangan = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Narsum", x => x.NarsumID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_P2kp_NarsumID",
                table: "P2kp",
                column: "NarsumID");

            migrationBuilder.CreateIndex(
                name: "IX_P2kp_P2kpID1",
                table: "P2kp",
                column: "P2kpID1");

            migrationBuilder.AddForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp",
                column: "NarsumID",
                principalTable: "Narsum",
                principalColumn: "NarsumID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_P2kp_P2kp_P2kpID1",
                table: "P2kp",
                column: "P2kpID1",
                principalTable: "P2kp",
                principalColumn: "P2kpID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_P2kp_Narsum_NarsumID",
                table: "P2kp");

            migrationBuilder.DropForeignKey(
                name: "FK_P2kp_P2kp_P2kpID1",
                table: "P2kp");

            migrationBuilder.DropTable(
                name: "Narsum");

            migrationBuilder.DropIndex(
                name: "IX_P2kp_NarsumID",
                table: "P2kp");

            migrationBuilder.DropIndex(
                name: "IX_P2kp_P2kpID1",
                table: "P2kp");

            migrationBuilder.DropColumn(
                name: "NarsumID",
                table: "P2kp");

            migrationBuilder.DropColumn(
                name: "P2kpID1",
                table: "P2kp");
        }
    }
}
