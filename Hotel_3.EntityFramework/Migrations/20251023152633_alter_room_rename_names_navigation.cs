using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_3.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class alter_room_rename_names_navigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomCategories_CategoryId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomStatuses_StatusId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Rooms",
                newName: "RoomStatusId");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Rooms",
                newName: "RoomCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_StatusId",
                table: "Rooms",
                newName: "IX_Rooms_RoomStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_CategoryId",
                table: "Rooms",
                newName: "IX_Rooms_RoomCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomCategories_RoomCategoryId",
                table: "Rooms",
                column: "RoomCategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms",
                column: "RoomStatusId",
                principalTable: "RoomStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomCategories_RoomCategoryId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomStatuses_RoomStatusId",
                table: "Rooms");

            migrationBuilder.RenameColumn(
                name: "RoomStatusId",
                table: "Rooms",
                newName: "StatusId");

            migrationBuilder.RenameColumn(
                name: "RoomCategoryId",
                table: "Rooms",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomStatusId",
                table: "Rooms",
                newName: "IX_Rooms_StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_RoomCategoryId",
                table: "Rooms",
                newName: "IX_Rooms_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomCategories_CategoryId",
                table: "Rooms",
                column: "CategoryId",
                principalTable: "RoomCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomStatuses_StatusId",
                table: "Rooms",
                column: "StatusId",
                principalTable: "RoomStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
