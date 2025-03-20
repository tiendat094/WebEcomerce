using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Remove_Table_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersDetail_carts_CartId",
                table: "ordersDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ordersDetail_orders_OrderId",
                table: "ordersDetail");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropIndex(
                name: "IX_ordersDetail_OrderId",
                table: "ordersDetail");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "ordersDetail");

            migrationBuilder.AlterColumn<long>(
                name: "CartId",
                table: "ordersDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ordersDetail_carts_CartId",
                table: "ordersDetail",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ordersDetail_carts_CartId",
                table: "ordersDetail");

            migrationBuilder.AlterColumn<long>(
                name: "CartId",
                table: "ordersDetail",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "OrderId",
                table: "ordersDetail",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TotalAmount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ordersDetail_OrderId",
                table: "ordersDetail",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_UserId",
                table: "orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ordersDetail_carts_CartId",
                table: "ordersDetail",
                column: "CartId",
                principalTable: "carts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ordersDetail_orders_OrderId",
                table: "ordersDetail",
                column: "OrderId",
                principalTable: "orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
