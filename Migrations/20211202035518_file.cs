using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class file : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JenisFile",
                columns: table => new
                {
                    JenisFileID = table.Column<string>(nullable: false),
                    NamaFIle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JenisFile", x => x.JenisFileID);
                });

            migrationBuilder.CreateTable(
                name: "MateriP2kp",
                columns: table => new
                {
                    MateriP2kpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MateriName = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    File = table.Column<string>(nullable: true),
                    JenisFileID = table.Column<string>(nullable: true),
                    P2kpID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriP2kp", x => x.MateriP2kpID);
                    table.ForeignKey(
                        name: "FK_MateriP2kp_JenisFile_JenisFileID",
                        column: x => x.JenisFileID,
                        principalTable: "JenisFile",
                        principalColumn: "JenisFileID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MateriP2kp_P2kp_P2kpID",
                        column: x => x.P2kpID,
                        principalTable: "P2kp",
                        principalColumn: "P2kpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MateriP2kp_JenisFileID",
                table: "MateriP2kp",
                column: "JenisFileID");

            migrationBuilder.CreateIndex(
                name: "IX_MateriP2kp_P2kpID",
                table: "MateriP2kp",
                column: "P2kpID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MateriP2kp");

            migrationBuilder.DropTable(
                name: "JenisFile");
        }
    }
}
