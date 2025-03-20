using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Add_column_DateTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateTime",
                table: "reviews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateTime",
                table: "reviews");
        }
    }
}
