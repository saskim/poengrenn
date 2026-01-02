using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Poengrenn.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KonkurranseType",
                columns: table => new
                {
                    TypeID = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Navn = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    StandardAntallKonkurranser = table.Column<int>(type: "integer", nullable: false),
                    Aktiv = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonkurranseType", x => x.TypeID);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    PersonID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fornavn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Etternavn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Fodselsar = table.Column<int>(type: "integer", nullable: true),
                    Kjonn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Epost = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Telefon = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.PersonID);
                });

            migrationBuilder.CreateTable(
                name: "Konkurranse",
                columns: table => new
                {
                    KonkurranseID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Serie = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Navn = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Dato = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TypeID = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StartInterval = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Konkurranse", x => x.KonkurranseID);
                    table.ForeignKey(
                        name: "FK_Konkurranse_KonkurranseType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "KonkurranseType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KonkurranseKlasse",
                columns: table => new
                {
                    KlasseID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TypeID = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Navn = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    MinAlder = table.Column<int>(type: "integer", nullable: false),
                    MaxAlder = table.Column<int>(type: "integer", nullable: false),
                    Kjonn = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ForsteStartnummer = table.Column<int>(type: "integer", nullable: false),
                    SisteStartnummer = table.Column<int>(type: "integer", nullable: false),
                    MedTidtaking = table.Column<bool>(type: "boolean", nullable: false),
                    DistanseKm = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonkurranseKlasse", x => new { x.KlasseID, x.TypeID });
                    table.ForeignKey(
                        name: "FK_KonkurranseKlasse_KonkurranseType_TypeID",
                        column: x => x.TypeID,
                        principalTable: "KonkurranseType",
                        principalColumn: "TypeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KonkurranseDeltaker",
                columns: table => new
                {
                    KonkurranseID = table.Column<int>(type: "integer", nullable: false),
                    KlasseID = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PersonID = table.Column<int>(type: "integer", nullable: false),
                    TypeID = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LagNummer = table.Column<int>(type: "integer", nullable: true),
                    StartNummer = table.Column<int>(type: "integer", nullable: true),
                    StartTid = table.Column<TimeSpan>(type: "interval", nullable: true),
                    SluttTid = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Tidsforbruk = table.Column<TimeSpan>(type: "interval", nullable: true),
                    Betalt = table.Column<bool>(type: "boolean", nullable: true),
                    Tilstede = table.Column<bool>(type: "boolean", nullable: true),
                    BetalingsNotat = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KonkurranseDeltaker", x => new { x.KonkurranseID, x.KlasseID, x.PersonID });
                    table.ForeignKey(
                        name: "FK_KonkurranseDeltaker_KonkurranseKlasse_KlasseID_TypeID",
                        columns: x => new { x.KlasseID, x.TypeID },
                        principalTable: "KonkurranseKlasse",
                        principalColumns: new[] { "KlasseID", "TypeID" });
                    table.ForeignKey(
                        name: "FK_KonkurranseDeltaker_Konkurranse_KonkurranseID",
                        column: x => x.KonkurranseID,
                        principalTable: "Konkurranse",
                        principalColumn: "KonkurranseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KonkurranseDeltaker_Person_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Person",
                        principalColumn: "PersonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Konkurranse_TypeID",
                table: "Konkurranse",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_KonkurranseDeltaker_KlasseID_TypeID",
                table: "KonkurranseDeltaker",
                columns: new[] { "KlasseID", "TypeID" });

            migrationBuilder.CreateIndex(
                name: "IX_KonkurranseDeltaker_PersonID",
                table: "KonkurranseDeltaker",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_KonkurranseKlasse_TypeID",
                table: "KonkurranseKlasse",
                column: "TypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KonkurranseDeltaker");

            migrationBuilder.DropTable(
                name: "KonkurranseKlasse");

            migrationBuilder.DropTable(
                name: "Konkurranse");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "KonkurranseType");
        }
    }
}
