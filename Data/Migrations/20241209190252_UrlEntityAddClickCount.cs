using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class UrlEntityAddClickCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "click_count",
                table: "url_entity",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "click_count",
                table: "url_entity");
        }
    }
}
