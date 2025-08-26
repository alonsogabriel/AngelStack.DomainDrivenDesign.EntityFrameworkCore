using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConsoleTests.Migrations
{
    /// <inheritdoc />
    public partial class Changephonecodecolumnnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneNumber_CountryCode_Value",
                table: "User",
                newName: "PhoneCountryCode");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber_AreaCode_Value",
                table: "User",
                newName: "PhoneAreaCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PhoneCountryCode",
                table: "User",
                newName: "PhoneNumber_CountryCode_Value");

            migrationBuilder.RenameColumn(
                name: "PhoneAreaCode",
                table: "User",
                newName: "PhoneNumber_AreaCode_Value");
        }
    }
}
