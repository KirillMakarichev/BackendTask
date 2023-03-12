using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTask.Migrations
{
    /// <inheritdoc />
    public partial class Root_name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "nodes");

            migrationBuilder.AddColumn<string>(
                name: "RootName",
                table: "nodes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_nodes_Name_ParentNodeId",
                table: "nodes",
                columns: new[] { "Name", "ParentNodeId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes",
                column: "ParentNodeId",
                principalTable: "nodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes");

            migrationBuilder.DropIndex(
                name: "IX_nodes_Name_ParentNodeId",
                table: "nodes");

            migrationBuilder.DropColumn(
                name: "RootName",
                table: "nodes");

            migrationBuilder.AddColumn<long>(
                name: "RootId",
                table: "nodes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes",
                column: "ParentNodeId",
                principalTable: "nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
