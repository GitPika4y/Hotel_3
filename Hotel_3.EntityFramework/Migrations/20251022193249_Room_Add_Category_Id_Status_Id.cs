using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hotel_3.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class Room_Add_Category_Id_Status_Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Rooms");
        }
    }
}
