using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Roulette.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    BetType = table.Column<int>(type: "INTEGER", nullable: false),
                    BetNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    BetAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    SpinNumber = table.Column<int>(type: "INTEGER", nullable: true),
                    RouletteTableID = table.Column<int>(type: "INTEGER", nullable: false),
                    PayoutAmount = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "RouletteTables",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SpinIteration = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouletteTables", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "RouletteTables",
                columns: new[] { "ID", "SpinIteration" },
                values: new object[] { 1, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropTable(
                name: "RouletteTables");
        }
    }
}
