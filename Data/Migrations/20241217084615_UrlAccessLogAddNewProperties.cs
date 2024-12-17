using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace url_shortener.Data.Migrations
{
    /// <inheritdoc />
    public partial class UrlAccessLogAddNewProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "user_agent",
                table: "url_access_log");

            migrationBuilder.AlterColumn<string>(
                name: "shortened_url",
                table: "url_entity",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddColumn<string>(
                name: "browser",
                table: "url_access_log",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "device_type",
                table: "url_access_log",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "operating_system",
                table: "url_access_log",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "browser",
                table: "url_access_log");

            migrationBuilder.DropColumn(
                name: "device_type",
                table: "url_access_log");

            migrationBuilder.DropColumn(
                name: "operating_system",
                table: "url_access_log");

            migrationBuilder.AlterColumn<string>(
                name: "shortened_url",
                table: "url_entity",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "user_agent",
                table: "url_access_log",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "");
        }
    }
}
