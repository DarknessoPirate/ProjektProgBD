using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektProgBD.Migrations
{
    /// <inheritdoc />
    public partial class klucz_zlozony : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames");

            migrationBuilder.DropIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserGames");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                type: "decimal(9,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames",
                columns: new[] { "UserId", "GameId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserGames",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Games",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserGames",
                table: "UserGames",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserGames_UserId",
                table: "UserGames",
                column: "UserId");
        }
    }
}
