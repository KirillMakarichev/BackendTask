using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTask.Migrations
{
    /// <inheritdoc />
    public partial class Added_Root_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes");

            migrationBuilder.DropColumn(
                name: "RootId",
                table: "nodes");

            migrationBuilder.AddForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes",
                column: "ParentNodeId",
                principalTable: "nodes",
                principalColumn: "Id");
        }
    }
}
