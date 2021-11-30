using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class p2kp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "P2kp",
                columns: table => new
                {
                    P2kpID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Judul = table.Column<string>(nullable: true),
                    Tanggal = table.Column<DateTime>(nullable: false),
                    Narasumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_P2kp", x => x.P2kpID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "P2kp");
        }
    }
}
