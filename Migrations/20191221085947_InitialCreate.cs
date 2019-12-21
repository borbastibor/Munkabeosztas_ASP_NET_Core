using Microsoft.EntityFrameworkCore.Migrations;

namespace Munkabeosztas_ASP_NET_Core.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adminusers",
                columns: table => new
                {
                    AdminuserId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adminusers", x => x.AdminuserId);
                });

            migrationBuilder.CreateTable(
                name: "Dolgozok",
                columns: table => new
                {
                    DolgozoId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Csaladnev = table.Column<string>(nullable: false),
                    Keresztnev = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dolgozok", x => x.DolgozoId);
                });

            migrationBuilder.CreateTable(
                name: "Gepjarmuvek",
                columns: table => new
                {
                    GepjarmuId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Tipus = table.Column<string>(nullable: false),
                    Rendszam = table.Column<string>(maxLength: 7, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gepjarmuvek", x => x.GepjarmuId);
                });

            migrationBuilder.CreateTable(
                name: "Munkak",
                columns: table => new
                {
                    MunkaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Helyszin = table.Column<string>(nullable: false),
                    Datum = table.Column<string>(nullable: false),
                    Leiras = table.Column<string>(nullable: false),
                    GepjarmuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Munkak", x => x.MunkaId);
                    table.ForeignKey(
                        name: "FK_Munkak_Gepjarmuvek_GepjarmuId",
                        column: x => x.GepjarmuId,
                        principalTable: "Gepjarmuvek",
                        principalColumn: "GepjarmuId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DolgozoMunkak",
                columns: table => new
                {
                    DolgozoId = table.Column<int>(nullable: false),
                    MunkaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DolgozoMunkak", x => new { x.DolgozoId, x.MunkaId });
                    table.ForeignKey(
                        name: "FK_DolgozoMunkak_Dolgozok_DolgozoId",
                        column: x => x.DolgozoId,
                        principalTable: "Dolgozok",
                        principalColumn: "DolgozoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DolgozoMunkak_Munkak_MunkaId",
                        column: x => x.MunkaId,
                        principalTable: "Munkak",
                        principalColumn: "MunkaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DolgozoMunkak_MunkaId",
                table: "DolgozoMunkak",
                column: "MunkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Munkak_GepjarmuId",
                table: "Munkak",
                column: "GepjarmuId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adminusers");

            migrationBuilder.DropTable(
                name: "DolgozoMunkak");

            migrationBuilder.DropTable(
                name: "Dolgozok");

            migrationBuilder.DropTable(
                name: "Munkak");

            migrationBuilder.DropTable(
                name: "Gepjarmuvek");
        }
    }
}
