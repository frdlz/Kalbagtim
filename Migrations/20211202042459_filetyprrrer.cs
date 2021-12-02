using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class filetyprrrer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriP2kp_JenisFile_JenisFileID",
                table: "MateriP2kp");

            migrationBuilder.DropTable(
                name: "JenisFile");

            migrationBuilder.DropIndex(
                name: "IX_MateriP2kp_JenisFileID",
                table: "MateriP2kp");

            migrationBuilder.DropColumn(
                name: "JenisFileID",
                table: "MateriP2kp");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JenisFileID",
                table: "MateriP2kp",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JenisFile",
                columns: table => new
                {
                    JenisFileID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NamaFIle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisFile", x => x.JenisFileID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriP2kp_JenisFileID",
                table: "MateriP2kp",
                column: "JenisFileID");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriP2kp_JenisFile_JenisFileID",
                table: "MateriP2kp",
                column: "JenisFileID",
                principalTable: "JenisFile",
                principalColumn: "JenisFileID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
