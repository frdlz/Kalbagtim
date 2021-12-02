using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class image : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_ImageP2kp_P2kpID",
                table: "ImageP2kp",
                column: "P2kpID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageP2kp");
        }
    }
}
