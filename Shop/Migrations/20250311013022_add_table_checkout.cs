using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class add_table_checkout : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CheckoutId",
                table: "ordersDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "checkouts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Country = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: false),
                    City = table.Column<string>(type: "text", nullable: false),
                    Postcode = table.Column<string>(type: "text", nullable: true),
                    EmailAddres = table.Column<string>(type: "text", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    DiffCountry = table.Column<string>(type: "text", nullable: true),
                    DiffFirstName = table.Column<string>(type: "text", nullable: true),
                    DiffLastName = table.Column<string>(type: "text", nullable: true),
                    DiffPhone = table.Column<string>(type: "text", nullable: true),
                    DiffCompanyName = table.Column<string>(type: "text", nullable: true),
                    DiffAddress = table.Column<string>(type: "text", nullable: true),
                    DiffCity = table.Column<string>(type: "text", nullable: true),
                    DiffPostcode = table.Column<string>(type: "text", nullable: true),
                    OrderNotes = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_checkouts_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ordersDetail_CheckoutId",
                table: "ordersDetail",
                column: "CheckoutId");

            migrationBuilder.CreateIndex(
                name: "IX_checkouts_UserId",
                table: "checkouts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ordersDetail_checkouts_CheckoutId",
                table: "ordersDetail",
                column: "CheckoutId",
                principalTable: "checkouts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersDetail_checkouts_CheckoutId",
                table: "ordersDetail");

            migrationBuilder.DropTable(
                name: "checkouts");

            migrationBuilder.DropIndex(
                name: "IX_ordersDetail_CheckoutId",
                table: "ordersDetail");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "ordersDetail");
        }
    }
}
