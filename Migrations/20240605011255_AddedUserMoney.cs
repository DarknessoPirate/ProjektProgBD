using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjektProgBD.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserMoney : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Money",
                table: "Users",
                type: "decimal(9,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Money",
                table: "Users");
        }
    }
}
