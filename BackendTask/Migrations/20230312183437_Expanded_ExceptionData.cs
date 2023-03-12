using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTask.Migrations
{
    /// <inheritdoc />
    public partial class Expanded_ExceptionData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Body",
                table: "exceptions_data",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string[]>(
                name: "Headers",
                table: "exceptions_data",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "QueryParameters",
                table: "exceptions_data",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string>(
                name: "TraceId",
                table: "exceptions_data",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Body",
                table: "exceptions_data");

            migrationBuilder.DropColumn(
                name: "Headers",
                table: "exceptions_data");

            migrationBuilder.DropColumn(
                name: "QueryParameters",
                table: "exceptions_data");

            migrationBuilder.DropColumn(
                name: "TraceId",
                table: "exceptions_data");
        }
    }
}
