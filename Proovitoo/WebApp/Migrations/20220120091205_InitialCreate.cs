using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WebApp.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Isikud",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tyyp = table.Column<int>(type: "integer", nullable: false),
                    Nimi1 = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: false),
                    Nimi2 = table.Column<string>(type: "character varying(70)", maxLength: 70, nullable: true),
                    Kood = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Isikud", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yritused",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nimi = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    Info = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Koht = table.Column<string>(type: "character varying(125)", maxLength: 125, nullable: true),
                    Algus = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    KustutamiseKP = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yritused", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Osalemised",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsikId = table.Column<int>(type: "integer", nullable: false),
                    YritusId = table.Column<int>(type: "integer", nullable: false),
                    Makseviis = table.Column<int>(type: "integer", nullable: false),
                    Lisainfo = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    OsalejateArv = table.Column<int>(type: "integer", nullable: false, comment: "Üritusele saabuvate osalejate arv. Füüsilise isiku puhul alati 1. Juriidilise isiku puhul 1 - 1000000."),
                    KustutamiseKP = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Osalemised", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Osalemised_Isikud_IsikId",
                        column: x => x.IsikId,
                        principalTable: "Isikud",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Osalemised_Yritused_YritusId",
                        column: x => x.YritusId,
                        principalTable: "Yritused",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Osalemised_IsikId",
                table: "Osalemised",
                column: "IsikId");

            migrationBuilder.CreateIndex(
                name: "IX_Osalemised_YritusId",
                table: "Osalemised",
                column: "YritusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Osalemised");

            migrationBuilder.DropTable(
                name: "Isikud");

            migrationBuilder.DropTable(
                name: "Yritused");
        }
    }
}
