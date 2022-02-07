using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectAlpha.Migrations
{
    public partial class vksin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vaksin",
                columns: table => new
                {
                    VaksinID = table.Column<string>(nullable: false),
                    NamaPegawai = table.Column<string>(nullable: true),
                    NipPegawai = table.Column<string>(nullable: true),
                    NikPegawai = table.Column<string>(nullable: true),
                    UnitKerja = table.Column<string>(nullable: true),
                    StatusASN = table.Column<string>(nullable: true),
                    Vaksin1 = table.Column<DateTime>(nullable: false),
                    Vaksin2 = table.Column<DateTime>(nullable: false),
                    Vaksin3 = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vaksin", x => x.VaksinID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vaksin");
        }
    }
}
