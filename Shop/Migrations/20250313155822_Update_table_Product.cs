using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Update_table_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "products",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "products");
        }
    }
}
