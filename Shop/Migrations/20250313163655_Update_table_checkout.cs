using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_checkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiffAddress",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffCity",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffCompanyName",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffCountry",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffFirstName",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffLastName",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffPhone",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "DiffPostcode",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "OrderNotes",
                table: "checkouts");

            migrationBuilder.DropColumn(
                name: "Postcode",
                table: "checkouts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DiffAddress",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffCity",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffCompanyName",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffCountry",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffFirstName",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffLastName",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffPhone",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiffPostcode",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderNotes",
                table: "checkouts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postcode",
                table: "checkouts",
                type: "text",
                nullable: true);
        }
    }
}
