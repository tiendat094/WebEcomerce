using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class update_table_review : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "reviews",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "reviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "reviews");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "reviews");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "reviews",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_reviews_users_UserId",
                table: "reviews",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
