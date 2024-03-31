using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YuniYol.Migrations
{
    /// <inheritdoc />
    public partial class Seventh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_Users_UserId",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.RenameTable(
                name: "Storage",
                newName: "Storages");

            migrationBuilder.RenameIndex(
                name: "IX_Storage_UserId",
                table: "Storages",
                newName: "IX_Storages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Storage_ProductId",
                table: "Storages",
                newName: "IX_Storages_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Products_ProductId",
                table: "Storages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_Users_UserId",
                table: "Storages",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Products_ProductId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_Users_UserId",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Storage");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_UserId",
                table: "Storage",
                newName: "IX_Storage_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_ProductId",
                table: "Storage",
                newName: "IX_Storage_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Products_ProductId",
                table: "Storage",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_Users_UserId",
                table: "Storage",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
