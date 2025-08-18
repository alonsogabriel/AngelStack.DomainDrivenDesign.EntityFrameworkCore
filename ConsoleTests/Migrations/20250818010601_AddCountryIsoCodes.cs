using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleTests.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryIsoCodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsoA2",
                table: "Country",
                type: "nvarchar(2)",
                maxLength: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsoA3",
                table: "Country",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IsoNumber",
                table: "Country",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsoA2",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsoA3",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsoNumber",
                table: "Country");
        }
    }
}
