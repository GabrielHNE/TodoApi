using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Infra.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdUser",
                table: "TodoItem",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_IdUser",
                table: "TodoItem",
                column: "IdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_User_IdUser",
                table: "TodoItem",
                column: "IdUser",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_User_IdUser",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_IdUser",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "IdUser",
                table: "TodoItem");
        }
    }
}
