using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_rest_cs.Migrations
{
    /// <inheritdoc />
    public partial class upToDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Books");

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Books",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
