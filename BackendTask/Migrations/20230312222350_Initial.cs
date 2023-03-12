using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackendTask.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exceptions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExceptionType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exceptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentNodeId = table.Column<long>(type: "bigint", nullable: true),
                    RootName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_nodes_nodes_ParentNodeId",
                        column: x => x.ParentNodeId,
                        principalTable: "nodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "exceptions_data",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExceptionId = table.Column<long>(type: "bigint", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    TraceId = table.Column<string>(type: "text", nullable: false),
                    QueryParameters = table.Column<string[]>(type: "text[]", nullable: false),
                    Headers = table.Column<string[]>(type: "text[]", nullable: false),
                    Body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exceptions_data", x => x.Id);
                    table.ForeignKey(
                        name: "FK_exceptions_data_exceptions_ExceptionId",
                        column: x => x.ExceptionId,
                        principalTable: "exceptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_exceptions_data_ExceptionId",
                table: "exceptions_data",
                column: "ExceptionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nodes_Name_ParentNodeId",
                table: "nodes",
                columns: new[] { "Name", "ParentNodeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_nodes_ParentNodeId",
                table: "nodes",
                column: "ParentNodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "exceptions_data");

            migrationBuilder.DropTable(
                name: "nodes");

            migrationBuilder.DropTable(
                name: "exceptions");
        }
    }
}
