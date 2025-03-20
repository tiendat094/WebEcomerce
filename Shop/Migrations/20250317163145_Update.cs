using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersDetail_checkouts_CheckoutId",
                table: "ordersDetail");

            migrationBuilder.DropIndex(
                name: "IX_ordersDetail_CheckoutId",
                table: "ordersDetail");

            migrationBuilder.DropColumn(
                name: "CheckoutId",
                table: "ordersDetail");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "checkouts");

            migrationBuilder.AddColumn<string>(
                name: "OrderDetailIds",
                table: "checkouts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderDetailIds",
                table: "checkouts");

            migrationBuilder.AddColumn<long>(
                name: "CheckoutId",
                table: "ordersDetail",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Total",
                table: "checkouts",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_ordersDetail_CheckoutId",
                table: "ordersDetail",
                column: "CheckoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_ordersDetail_checkouts_CheckoutId",
                table: "ordersDetail",
                column: "CheckoutId",
                principalTable: "checkouts",
                principalColumn: "Id");
        }
    }
}
