using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class nenenenrt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIleType",
                table: "MateriP2kp");

            migrationBuilder.AlterColumn<string>(
                name: "Tempat",
                table: "P2kp",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Judul",
                table: "P2kp",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JenisFIleID",
                table: "MateriP2kp",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "JenisFile",
                columns: table => new
                {
                    JenisFileID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisFile", x => x.JenisFileID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriP2kp_JenisFIleID",
                table: "MateriP2kp",
                column: "JenisFIleID");

            migrationBuilder.AddForeignKey(
                name: "FK_MateriP2kp_JenisFile_JenisFIleID",
                table: "MateriP2kp",
                column: "JenisFIleID",
                principalTable: "JenisFile",
                principalColumn: "JenisFileID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MateriP2kp_JenisFile_JenisFIleID",
                table: "MateriP2kp");

            migrationBuilder.DropTable(
                name: "JenisFile");

            migrationBuilder.DropIndex(
                name: "IX_MateriP2kp_JenisFIleID",
                table: "MateriP2kp");

            migrationBuilder.DropColumn(
                name: "JenisFIleID",
                table: "MateriP2kp");

            migrationBuilder.AlterColumn<string>(
                name: "Tempat",
                table: "P2kp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Judul",
                table: "P2kp",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "FIleType",
                table: "MateriP2kp",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
