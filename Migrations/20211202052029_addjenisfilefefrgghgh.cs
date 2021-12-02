using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class addjenisfilefefrgghgh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "P2kp",
                columns: table => new
                {
                    P2kpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false),
                    JamMulai = table.Column<DateTime>(nullable: false),
                    JamSelesai = table.Column<DateTime>(nullable: false),
                    Tempat = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    WaktuBuat = table.Column<DateTime>(nullable: false),
                    WaktuSelesai = table.Column<DateTime>(nullable: false),
                    NarsumID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2kp", x => x.P2kpID);
                    table.ForeignKey(
                        name: "FK_P2kp_Narsum_NarsumID",
                        column: x => x.NarsumID,
                        principalTable: "Narsum",
                        principalColumn: "NarsumID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageP2kp",
                columns: table => new
                {
                    ImageP2kpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    P2kpID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageP2kp", x => x.ImageP2kpID);
                    table.ForeignKey(
                        name: "FK_ImageP2kp_P2kp_P2kpID",
                        column: x => x.P2kpID,
                        principalTable: "P2kp",
                        principalColumn: "P2kpID",
                        onDelete: ReferentialAction.Cascade);
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
                    FileType = table.Column<string>(nullable: true),
                    JenisFIleID = table.Column<string>(nullable: true),
                    P2kpID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MateriP2kp", x => x.MateriP2kpID);
                    table.ForeignKey(
                        name: "FK_MateriP2kp_P2kp_P2kpID",
                        column: x => x.P2kpID,
                        principalTable: "P2kp",
                        principalColumn: "P2kpID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageP2kp_P2kpID",
                table: "ImageP2kp",
                column: "P2kpID");

            migrationBuilder.CreateIndex(
                name: "IX_MateriP2kp_P2kpID",
                table: "MateriP2kp",
                column: "P2kpID");

            migrationBuilder.CreateIndex(
                name: "IX_P2kp_NarsumID",
                table: "P2kp",
                column: "NarsumID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageP2kp");

            migrationBuilder.DropTable(
                name: "MateriP2kp");

            migrationBuilder.DropTable(
                name: "P2kp");

            migrationBuilder.DropTable(
                name: "Narsum");
        }
    }
}
