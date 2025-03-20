using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class Update_column_IsDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "products",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "ordersDetail",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "products");

            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "ordersDetail");

            migrationBuilder.AlterColumn<string>(
                name: "Tag",
                table: "products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
