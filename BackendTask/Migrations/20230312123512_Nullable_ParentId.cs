using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendTask.Migrations
{
    /// <inheritdoc />
    public partial class Nullable_ParentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes");

            migrationBuilder.AlterColumn<long>(
                name: "ParentNodeId",
                table: "nodes",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

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

            migrationBuilder.AlterColumn<long>(
                name: "ParentNodeId",
                table: "nodes",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_nodes_nodes_ParentNodeId",
                table: "nodes",
                column: "ParentNodeId",
                principalTable: "nodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
