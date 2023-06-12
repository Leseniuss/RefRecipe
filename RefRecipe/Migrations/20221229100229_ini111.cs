using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RefRecipe.Migrations
{
    /// <inheritdoc />
    public partial class ini111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IngKoodi = table.Column<int>(type: "INTEGER", nullable: false),
                    Nimi = table.Column<string>(type: "TEXT", nullable: true),
                    KgSatsi = table.Column<double>(type: "REAL", nullable: false),
                    Tuotekoodi = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Koodi = table.Column<int>(type: "INTEGER", nullable: false),
                    Satsikoko = table.Column<int>(type: "INTEGER", nullable: false),
                    Nimi = table.Column<string>(type: "TEXT", nullable: true),
                    BrixMin = table.Column<double>(type: "REAL", nullable: false),
                    BrixMax = table.Column<double>(type: "REAL", nullable: false),
                    pHMin = table.Column<double>(type: "REAL", nullable: false),
                    pHMax = table.Column<double>(type: "REAL", nullable: false),
                    KokonaishapotMin = table.Column<double>(type: "REAL", nullable: false),
                    KokonaishapotMax = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");
        }
    }
}
